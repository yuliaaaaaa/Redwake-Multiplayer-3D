using UnityEngine;

public class CameraMouseLook : MonoBehaviour
{
    public float rotationSpeed = 5f;
    private bool isRotating = false;
    private Vector3 lastMousePosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(2)) 
        {
            isRotating = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            float rotX = -delta.y * rotationSpeed * Time.deltaTime;
            float rotY = delta.x * rotationSpeed * Time.deltaTime;

            transform.eulerAngles += new Vector3(rotX, rotY, 0);
            lastMousePosition = Input.mousePosition;
        }
    }

}
