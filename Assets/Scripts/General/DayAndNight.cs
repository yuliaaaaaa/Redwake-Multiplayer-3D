using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    Vector3 rotation = Vector3.zero;
    private int previousRotation;
    public float degPerSec = 6;
    public Material skyBox;
    private float rotationShader;
    private float xposureShader = 1.1f;//0.6550002f;
    public float rotationSkySpeed = 10;
    public float rotateXPosition;
    private int coppyAndChack;

    private void Start()
    {
       skyBox.SetFloat("_Exposure", xposureShader);
       rotation.x = 90;
    }

    void Update()
    {
        if (previousRotation == 270)
        {
            xposureShader = 0.21f;
        }
        rotation.x = degPerSec * Time.deltaTime;
        transform.Rotate(rotation, Space.World);
        if (rotationShader < 360)
        {
            rotationShader += Time.deltaTime * rotationSkySpeed;
        }
        else
        {
            rotationShader = 0;
        }

        /*rotateXPosition = transform.rotation.eulerAngles.x;
        previousRotation = Mathf.RoundToInt(rotateXPosition);
        Debug.Log(rotateXPosition);
        Debug.Log(previousRotation);

        if (previousRotation - coppyAndChack == 1)
        {
            if (rotateXPosition < 90 && rotateXPosition <= 0 || rotateXPosition > 270 && rotateXPosition <= 360)
            {
                xposureShader += 0.00727778f;
            }
            else if (rotateXPosition > 90 && rotateXPosition < 270)
            {
                xposureShader -= 0.00727778f;
            }

            if (previousRotation >= 90)
            {
                xposureShader = 1.1f;
            }
            if (previousRotation >= 270)
            {
                xposureShader = 0.21f;
            }

        }

        coppyAndChack = previousRotation;*/
        skyBox.SetFloat("_Rotation", rotationShader);
        skyBox.SetFloat("_Exposure", xposureShader);
    }
}
