using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLights : MonoBehaviour
{
    [SerializeField] private int index = 0;
    private Material material;
    private CarController carController;

    void Start()
    {
        material = GetComponent<MeshRenderer>().materials[index];
        carController = FindObjectOfType<CarController>();
    }

    void Update()
    {
        if (carController.headLights == true)
        {
            material.SetColor("_EmissiveColor", Color.white);
        }

        else
        {
            material.SetColor("_EmissiveColor", Color.black);
        }
    }
}
