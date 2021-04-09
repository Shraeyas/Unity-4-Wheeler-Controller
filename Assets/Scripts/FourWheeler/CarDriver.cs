using System;
using UnityEngine;

public class CarDriver : MonoBehaviour
{
    private CarController carController;
    private float horizontalAxis, verticalAxis;
    private bool handBrake = false;
    private Rigidbody rigidBody;

    [SerializeField] private Transform centerOfMass, steer;
    [SerializeField] private int maxSteerAngle = 30, maxTorque = 1000, brakeTorque = 100000, handBrakeTorque = 200000;
    [SerializeField] private WheelCollider frontLeft, rearLeft, frontRight, rearRight;
    [SerializeField] private Transform frontLeftWheel, rearLeftWheel, frontRightWheel, rearRightWheel;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        try
        {
            rigidBody.centerOfMass = centerOfMass.localPosition;
        }

        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        
        carController = FindObjectOfType<CarController>();
    }
    
    bool isMovingForward()
    {
        if (transform.InverseTransformDirection(rigidBody.velocity).z > 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    void Accelerate ()
    {
        if (isMovingForward() ==  true)
        {
            Debug.Log("Try to Accelerate");
            try
            {
                ReleaseBrake();

                frontLeft.motorTorque = maxTorque * verticalAxis;
                frontRight.motorTorque = maxTorque * verticalAxis;

                Debug.Log("Accelerate");
            }

            catch (Exception e)
            {

            }
        }

        else
        {
            Debug.Log("Trying to Brake while Moving Forward");
            try
            {
                ApplyBrake();
            }

            catch (Exception e)
            {

            }
        }
    }

    void Reverse ()
    {
        if (isMovingForward() == true)
        {
            Debug.Log("Trying to Brake");
            try
            {
                ApplyBrake();
            }

            catch (Exception e)
            {

            }
        }

        else
        {
            Debug.Log("Trying to Brake while Moving Backward");
            try
            {
                ReleaseBrake();

                frontLeft.motorTorque = maxTorque * verticalAxis;
                frontRight.motorTorque = maxTorque * verticalAxis;

                Debug.Log("Accelerate");
            }

            catch (Exception e)
            {

            }
        }
    }

    void Steer ()
    {
        frontLeft.steerAngle = (horizontalAxis * maxSteerAngle);
        frontRight.steerAngle = (horizontalAxis * maxSteerAngle);

        //steer.Rotate(0, horizontalAxis * 30, 0);
        steer.localRotation = Quaternion.Euler(steer.localRotation.eulerAngles.x, horizontalAxis * 30, steer.localRotation.eulerAngles.z);
        Debug.Log("Angle Test" + steer.localRotation.eulerAngles);
    }

    void ApplyBrake ()
    {
        Debug.Log("Trying to Brake");
     
        frontLeft.motorTorque = 0f;
        frontRight.motorTorque = 0f;

        frontLeft.brakeTorque = brakeTorque;
        frontRight.brakeTorque = brakeTorque;
    }

    void ReleaseBrake()
    {
        Debug.Log("Trying to Release Brake");

        frontLeft.brakeTorque = 0f;
        frontRight.brakeTorque = 0f;
    }

    void ApplyHandBrake()
    {
        try
        {
            rearLeft.brakeTorque = handBrakeTorque;
            rearRight.brakeTorque = handBrakeTorque;
        }

        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    void ReleaseHandbrake ()
    {
        try
        {
            rearLeft.brakeTorque = 0f;
            rearRight.brakeTorque = 0f;
        }

        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    void Neutral()
    {
        frontRight.motorTorque = 0f;
        frontLeft.motorTorque = 0f;

        frontRight.brakeTorque = 0f;
        frontLeft.brakeTorque = 0f;
    }

    private void FixedUpdate()
    {
        Debug.Log("Vertical Axis : " + verticalAxis);

        if (verticalAxis > 0f)
        {
            Accelerate();
        }

        if (verticalAxis == 0f)
        {
            Neutral();
        }

        if (verticalAxis < 0f)
        {
            Reverse();
        }

        if (handBrake)
        {
            ApplyHandBrake();
        }

        else
        {
            ReleaseHandbrake();
        }

        Steer();
    }

    private void Update()
    {
        horizontalAxis = carController.horizontalAxis;
        verticalAxis = carController.verticalAxis;
        handBrake = carController.handBrake;

        var pos = Vector3.zero;
        var rot = Quaternion.identity;

        frontLeft.GetWorldPose(out pos, out rot);
        frontLeftWheel.position = pos;
        frontLeftWheel.rotation = rot;// * Quaternion.Euler(0, 0, 0);

        frontRight.GetWorldPose(out pos, out rot);
        frontRightWheel.position = pos;
        frontRightWheel.rotation = rot;// * Quaternion.Euler(0, 0, 0);


        rearLeft.GetWorldPose(out pos, out rot);
        rearLeftWheel.position = pos;
        rearLeftWheel.rotation = rot;// * Quaternion.Euler(0, 0, 0);

        rearRight.GetWorldPose(out pos, out rot);
        rearRightWheel.position = pos;
        rearRightWheel.rotation = rot;// * Quaternion.Euler(0, 0, 0);
    }
}
