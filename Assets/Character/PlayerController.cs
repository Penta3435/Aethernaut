using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float runSpeed = 5;
    [SerializeField] float jumpSpeed = 10;
    [SerializeField] float fallSpeed = 10;
    [SerializeField] float jumpTime = 0.5f;

    [SerializeField] Animator animator;
    [SerializeField] CharacterController cc;

    int runId;
    int jumpId;

    Vector3 cameraForward;
    Vector3 cameraRight;

    Vector3 runDir;

    Vector3 posToSet;

    bool running = false;
    bool jumping = false;
    bool settingPos = false;
    float jumpTimer;


    LayerMask interactablesLayerMask;
    private void Start()
    {
        interactablesLayerMask = LayerMask.GetMask("Interactables");
        runId = Animator.StringToHash("Running");
        jumpId = Animator.StringToHash("Jump");
    }
    void Update()
    {
        //update variables
        cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        cameraRight = Camera.main.transform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();

        running = false;

        //calc runDir and run logic
        if (Input.GetKey(KeyCode.W))
        {
            runDir += cameraForward;
            running = true;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            runDir -= cameraForward;
            running = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            runDir += cameraRight;
            running = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            runDir -= cameraRight;
            running = true;
        }
        runDir.Normalize();
        if (running && cc.enabled)
        {
            if (runDir != Vector3.zero)
                transform.forward = runDir.normalized;

            cc.Move(transform.forward * runSpeed *  Time.deltaTime);
        }
        animator.SetBool(runId, running);


        //gravity
        if (cc.enabled) cc.Move(Vector3.down * fallSpeed *  Time.deltaTime);


        //jumpLogic
        if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded && cc.enabled) 
        {
            animator.SetTrigger(jumpId);
            jumpTimer = jumpTime;
        }
        if (jumping && cc.enabled)
        {
            cc.Move(Vector3.up * (Mathf.Lerp(0, jumpSpeed, jumpTimer/jumpTime) + fallSpeed) * Time.deltaTime);
            jumpTimer -= Time.deltaTime;

            if (jumpTimer <= 0) jumping = false;
        }

        //Change position
        if (settingPos) 
        { 
            transform.position = posToSet;
            settingPos = false;
            cc.enabled = true;
        }


        //Interact
        Collider[] hit = Physics.OverlapSphere(transform.position + Vector3.up, 2, interactablesLayerMask);
        Collider closestHit = null;
        if (hit.Length > 0)
        {
            foreach (Collider col in hit)
            {
                if(closestHit == null) closestHit = col;
                else
                {
                    float actualClosestHitDistance = (transform.position - closestHit.ClosestPoint(transform.position)).magnitude;
                    float newHitDistance = (transform.position - col.ClosestPoint(transform.position)).magnitude;
                    if (actualClosestHitDistance > newHitDistance) closestHit = col;
                }
            }
            
            IInteractables closestHitIInteractables = closestHit.GetComponent<IInteractables>();
            closestHitIInteractables.Focused = true;
            if (Input.GetKeyDown(KeyCode.F))
            {
                closestHitIInteractables.Interact();
            } 
        }
    }
    public void Jump()
    {
        jumping = true;
    }

    public void SetPosition(Vector3 pos)
    {
        cc.enabled = false;
        settingPos = true;
        posToSet = pos;
    }
}
