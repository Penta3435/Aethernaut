using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float totalHp = 100;

    [SerializeField] float runSpeed = 5;
    [SerializeField] float turnSpeed = 10;
    [SerializeField] float jumpSpeed = 10;
    [SerializeField] float fallSpeed = 10;
    [SerializeField] float jumpTime = 0.5f;

    [SerializeField] Animator animator;
    [SerializeField] CharacterController cc;
    [SerializeField] Volume volume;
    Vignette vignette;


    float currentHp;

    int runId;
    int jumpId;

    Vector3 cameraForward;
    Vector3 cameraRight;

    Vector3 runDir;
    bool canRunJump = true;

    Vector3 posToSet;

    bool running = false;
    bool jumping = false;
    bool settingPos = false;
    float jumpTimer;


    LayerMask interactablesLayerMask;
    private void Start()
    {
        currentHp = totalHp;
        interactablesLayerMask = LayerMask.GetMask("Interactables");
        runId = Animator.StringToHash("Running");
        jumpId = Animator.StringToHash("Jump");
        volume.profile.TryGet(out vignette);
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
        if (Input.GetKey(KeyCode.W) && canRunJump)
        {
            runDir += cameraForward;
            running = true;
        }
        else if (Input.GetKey(KeyCode.S) && canRunJump)
        {
            runDir -= cameraForward;
            running = true;
        }
        if (Input.GetKey(KeyCode.D) && canRunJump)
        {
            runDir += cameraRight;
            running = true;
        }
        else if (Input.GetKey(KeyCode.A) && canRunJump)
        {
            runDir -= cameraRight;
            running = true;
        }
        runDir.Normalize();
        if (running && cc.enabled)
        {
            if (runDir != Vector3.zero) 
            { 
                //transform.forward = runDir.normalized;
                transform.forward = Vector3.Lerp(transform.forward+new Vector3(0.01f,0,0), runDir.normalized, Time.deltaTime * turnSpeed);
            }
            cc.Move(transform.forward * runSpeed *  Time.deltaTime);
        }
        animator.SetBool(runId, running);


        //gravity
        if (cc.enabled && !cc.isGrounded) cc.Move(Vector3.down * fallSpeed * Time.deltaTime);

        //jumpLogic
        if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded && cc.enabled && canRunJump) 
        {
            animator.SetTrigger(jumpId);
            jumpTimer = jumpTime;
            canRunJump = false;
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
                closestHitIInteractables.Interact(this);
            } 
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        this.transform.parent = null;
        if (hit.transform.CompareTag("MovingPlatform")) transform.parent = hit.transform;
    }
    public void Jump()
    {
        jumping = true;
        canRunJump = true;
    }

    public void SetPosition(Vector3 pos)
    {
        cc.enabled = false;
        settingPos = true;
        posToSet = pos;
    }

    public void ReproduceAudioWhenGrounded(AudioClip audioClip)
    {
        if (cc.isGrounded)SFXManager.instance.ReproduceAudioClip(audioClip);
    }
    public void Damage(float damage)
    {
        StartCoroutine("DamageVignette");
        currentHp -= damage;
        UiManager.instance.SetHpBar(currentHp / totalHp);
        CheckAlive();
    }
    public void CheckAlive()
    {
        if (currentHp <= 0)
            GameManager.instance.RestartGame();
    }
    IEnumerator DamageVignette()
    {
        for (float i = 0; i < 1; i+=Time.deltaTime*2) 
        {
            vignette.intensity.value = Mathf.Sin(i*Mathf.PI)/2;
            yield return null;
        }
    }
}
