using UnityEngine;

public class LookCamera : MonoBehaviour
{
    [SerializeField] bool fixZ;

    Quaternion originalRotation;
    private void Awake()
    {
        originalRotation = transform.rotation;
    }
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        if(fixZ)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        }
    }
}
