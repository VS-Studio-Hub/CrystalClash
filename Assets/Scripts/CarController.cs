using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public InputActionAsset inputActions;

    private InputAction moveAction;
    private InputAction brakeAction;
    private InputAction accelAction;
    private InputAction reverseAction;

    public WheelCollider frontRightWheelCollider;
    public WheelCollider backRightWheelCollider;
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider backLeftWheelCollider;

    public Transform frontRightWheelTransform;
    public Transform backRightWheelTransform;
    public Transform frontLeftWheelTransform;
    public Transform backLeftWheelTransform;

    public Transform carCentreOfMassTransform;
    public Rigidbody rigidbody;

    public float motorForce = 100f;
    public float reverseForce = 50f;
    public float steeringAngle = 30f;
    public float brakeForce = 1000f;

    float horizontalInput;
    float verticalInput;
    float brakeInput;
    float accelInput;
    float reverseInput;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        var playerMap = inputActions.FindActionMap("Player");
        moveAction = playerMap.FindAction("Move");
        brakeAction = playerMap.FindAction("Brake");
        accelAction = playerMap.FindAction("Acceleration");
        reverseAction = playerMap.FindAction("Reverse");
    }

    private void Start()
    {
        rigidbody.centerOfMass = carCentreOfMassTransform.localPosition;
    }

    private void FixedUpdate()
    {
        GetInput();
        ApplyMotor();
        ApplyBrakes();
        Steering();
        UpdateWheels();
    }

    void GetInput()
    {
        Vector2 inputVector = moveAction.ReadValue<Vector2>();
        horizontalInput = inputVector.x;
        verticalInput = inputVector.y;

        brakeInput = brakeAction.ReadValue<float>();
        accelInput = accelAction.ReadValue<float>();
        reverseInput = reverseAction.ReadValue<float>();
    }

    void ApplyMotor()
    {
        float torque = 0f;

        if (accelInput > 0.1f )
            torque = motorForce;
        else if (reverseInput > 0.1f)
            torque = -reverseForce;

        backRightWheelCollider.motorTorque = torque;
        backLeftWheelCollider.motorTorque = torque;
    }

    void ApplyBrakes()
    {
        float torque = brakeInput > 0.1f ? brakeForce : 0f;

        frontRightWheelCollider.brakeTorque = torque;
        //backRightWheelCollider.brakeTorque = torque;
        frontLeftWheelCollider.brakeTorque = torque;
        //backLeftWheelCollider.brakeTorque = torque;
    }

    void Steering()
    {
        float steer = steeringAngle * horizontalInput;
        frontRightWheelCollider.steerAngle = steer;
        frontLeftWheelCollider.steerAngle = steer;
    }

    void UpdateWheels()
    {
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(backRightWheelCollider, backRightWheelTransform);
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
    }

    void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
}
