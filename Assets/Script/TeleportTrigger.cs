using System.Collections;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour
{
    [SerializeField] Vector3 teleportLocation;
    [SerializeField] float playerYRotation;
    [SerializeField] Vector2 cameraRotation;

    TagHandle playerTag;
    void Awake()
    {
        playerTag = TagHandle.GetExistingTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            other.transform.GetComponent<PlayerController>().SetPosition(teleportLocation);
            other.transform.rotation = Quaternion.Euler(0, playerYRotation, 0);
            Camera.main.GetComponent<CameraController>().xAngle = cameraRotation.x;
            Camera.main.GetComponent<CameraController>().yAngle = cameraRotation.y;
        }
    }
    private void Update()
    {
        Debug.DrawLine(teleportLocation, transform.position, Color.red);
    }
}
