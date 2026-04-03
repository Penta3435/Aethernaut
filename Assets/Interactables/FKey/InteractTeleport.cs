using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InteractTeleport : MonoBehaviour, IInteractables
{
    [SerializeField] Vector3 teleportLocation;
    [SerializeField] float playerYRotation;
    [SerializeField] Vector2 cameraRotation;
    [SerializeField] Transform pointRef;
    [SerializeField] GameObject FocusedIcon;


    public bool Focused { get; set; }

    TagHandle playerTag;
    void Awake()
    {
        playerTag = TagHandle.GetExistingTag("Player");
        if (pointRef != null)
        {
            teleportLocation = pointRef.position;
        }
    }
    void IInteractables.Interact(PlayerController playerController)
    {
       if (playerController.gameObject.CompareTag(playerTag))
       {
           playerController.SetPosition(teleportLocation);
           playerController.transform.rotation = Quaternion.Euler(0, playerYRotation, 0);
           Camera.main.GetComponent<CameraController>().xAngle = cameraRotation.x;
           Camera.main.GetComponent<CameraController>().yAngle = cameraRotation.y;

           if (FocusedIcon != null) FocusedIcon.SetActive(false);
       }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && Focused == true)
        {
            if (FocusedIcon != null) FocusedIcon.SetActive(true);
            Focused = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if (FocusedIcon != null) FocusedIcon.SetActive(false);
        }
    }

    private void Update()
    {
        Debug.DrawLine(teleportLocation, transform.position, Color.red);
    }
}
