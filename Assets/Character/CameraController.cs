using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] float sens = 2;
    [SerializeField] float distance = 5;
    [SerializeField] float horizontalOffset = 0;
    [SerializeField] float verticalOffset = 0;
    [Range(89.5f, 15)]
    [SerializeField] float topMaxAngle = -89;
    [Range (-89.5f, 0)]
    [SerializeField] float bottomMaxAngle = 89;
    [SerializeField] LayerMask layersDontCoverCamera;

    public float yAngle = 0;
    public float xAngle = 0;
    void Update()
    {
        //get mouse x y value
        var mouseX = -Input.GetAxis("Mouse X") * sens;
        var mouseY = -Input.GetAxis("Mouse Y") * sens;

        //convert mouse x y to angle x y, and angleY stay between 90 -90
        yAngle += mouseX;

        if(xAngle == bottomMaxAngle && mouseY > 0) 
        { 
            xAngle += mouseY;
        }
        else if (xAngle == topMaxAngle && mouseY < 0)
        {
            xAngle += mouseY;
        }
        else xAngle += mouseY;
        if (xAngle < bottomMaxAngle)
        {
            xAngle = bottomMaxAngle;
        }
        else if (xAngle > topMaxAngle)
        {
            xAngle = topMaxAngle;
        }
        

        //convert angles to position
        var y = Mathf.Sin(Mathf.Deg2Rad * xAngle);      //get heigh
        var r = Mathf.Sqrt(1 - Mathf.Pow(y,2));         //get the radius of the circle cuthed from "1radius sphere" in "y height" (r^2 + y^2 = 1^2) "1" is "1 radius sphere´s" radius, y is heigh
        var x = Mathf.Cos(Mathf.Deg2Rad * yAngle) * r;
        var z = Mathf.Sin(Mathf.Deg2Rad * yAngle) * r;


        //where camera look with offset
        var cameraFocusPoint = target.position + new Vector3(Mathf.Cos(Mathf.Deg2Rad * (yAngle+90)), 0, Mathf.Sin(Mathf.Deg2Rad * (yAngle+90))).normalized * horizontalOffset + new Vector3(0, verticalOffset, 0);

        transform.position = target.position + new Vector3(x, y, z) * distance;
        transform.LookAt(cameraFocusPoint);

        RaycastHit hit;
        if(Physics.Linecast(target.position + new Vector3(0,verticalOffset,0),transform.position,out hit, layersDontCoverCamera))
        {
            transform.position = hit.point;
        }
    }
}
