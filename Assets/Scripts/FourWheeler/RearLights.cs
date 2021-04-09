using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RearLights : MonoBehaviour
{
    [SerializeField] private int index = 0;
    [SerializeField] private Light leftLight, rightLight;
    private Material material;
    private CarController carController;
    float dampLeftIntensityIncrease = 0, dampLeftIntensityDecrease = 0f, dampEmissionIncrease = 0f, dampEmissionDecrease = 0f, smoothTime = 0.1f;

    void Start()
    {
        material = GetComponent<MeshRenderer>().materials[index];
        
        carController = FindObjectOfType<CarController>();
        material.EnableKeyword("_EmissiveIntensity");

        material.SetFloat("_EmissiveIntensity", 1000000f);
        material.SetInt("_UseEmissiveIntensity", 1);
        
    }

    void Update()
    {
        if (carController.rearLights)
        {
            dampLeftIntensityDecrease = 0f;
            float newIntensity = Mathf.SmoothDamp(leftLight.intensity, 210000f, ref dampLeftIntensityIncrease, smoothTime);
            leftLight.intensity = newIntensity;
            rightLight.intensity = newIntensity;

            material.SetColor("_EmissiveColor", Color.red);

            dampEmissionDecrease = 0f;
            float newEmissionIntensity = Mathf.SmoothDamp(material.GetFloat("_EmissiveExposureWeight"), -210f, ref dampEmissionIncrease, smoothTime);
            material.SetFloat("_EmissiveExposureWeight", newEmissionIntensity);
        }

        else
        {
            dampLeftIntensityIncrease = 0f;
            float newIntensity = Mathf.SmoothDamp(leftLight.intensity, 0f, ref dampLeftIntensityDecrease, smoothTime);
            leftLight.intensity = newIntensity;
            rightLight.intensity = newIntensity;

            material.SetColor("_EmissiveColor", Color.black);

            dampEmissionIncrease = 0f;
            float newEmissionIntensity = Mathf.SmoothDamp(material.GetFloat("_EmissiveExposureWeight"), 1f, ref dampEmissionDecrease, smoothTime);
            material.SetFloat("_EmissiveExposureWeight", newEmissionIntensity);
        }
    }
}
