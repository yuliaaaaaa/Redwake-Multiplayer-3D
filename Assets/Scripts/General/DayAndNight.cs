using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public Material skyboxMaterial;
    public float rotationSpeed = 10f;

    private float currentRotation = 0f;

    void Update()
    {
        if (skyboxMaterial == null) return;

        currentRotation += rotationSpeed * Time.deltaTime;
        if (currentRotation >= 360f)
            currentRotation -= 360f;

        skyboxMaterial.SetFloat("_Rotation", currentRotation);
    }
}

