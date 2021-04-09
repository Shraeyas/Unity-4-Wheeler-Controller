using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private bool ai_drive = false;
    public float horizontalAxis, verticalAxis;
    public bool headLights = false, rearLights = false, handBrake = false;

    void Update()
    {
        horizontalAxis = Input.GetAxis ("Horizontal");
        verticalAxis = Input.GetAxis ("Vertical");

        if (Input.GetKeyDown ("h"))
        {
            if (headLights == false)
            {
                headLights = true;
                Debug.Log("HeadLights On");
            }

            else
            {
                headLights = false;
                Debug.Log("HeadLights Off");
            }
        }

        if(Input.GetAxisRaw("Vertical") < 0)
        {
            rearLights = true;
        }

        else
        {
            rearLights = false;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Handbrake Applied");
            handBrake = true;
        }

        else
        {
            Debug.Log("Handbrake Released");
            handBrake = false;
        }
    }
}
