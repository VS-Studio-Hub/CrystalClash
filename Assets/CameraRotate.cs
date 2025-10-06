using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float rotationSpeed = 100f;

    void Update()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float vertical = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        // Rotate horizontally around Y axis
        transform.Rotate(0, horizontal, 0, Space.World);
        // Rotate vertically around X axis
        transform.Rotate(vertical, 0, 0, Space.Self);
    }
}
