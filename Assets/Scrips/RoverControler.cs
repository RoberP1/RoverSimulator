using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class RoverControler : MonoBehaviour
{
    [SerializeField] WheelCollider _wheelsFrontLeft, _wheelsFrontRight;
    [SerializeField] float _acceleration, _turn, _velocityMax, _brakeForce;
    public float actualVelocity;
    bool _back, _isBrake;
    Rigidbody rb;
    WheelCollider[] wheelsColliders;
    [SerializeField] GameObject[] _visualsWheels,_rotationWheels;
    [SerializeField] GameObject _throttleLever, _rotationLever,_baseRotationLever, _hologram;
    [SerializeField] float _maxDirectionLever,_maxRotationLever;

    public bool isAgus;

    //quitar
    public Slider slider, slider2;
    void Start()
    {
       rb= GetComponent<Rigidbody>();
        wheelsColliders= GetComponentsInChildren<WheelCollider>();
        
    }

    void Update()
    {
        if (isAgus)
        {
            Movement(slider.value);
            Direction(slider2.value);
            Visual(slider2.value);

        }
        else
        {
            _rotationLever.transform.position= new Vector3(0,0, _baseRotationLever.transform.position.z);
            float inputRotation = _rotationLever.transform.localPosition.z < 0 ? 0 : _rotationLever.transform.localPosition.z;
            Movement(_throttleLever.transform.localPosition.x/ _maxDirectionLever);
            Direction(0.4376513761751f * inputRotation - 0.9919922799418f);
            Visual(0.4376513761751f * inputRotation - 0.9919922799418f);
        }
    }

    void Movement(float input)
    {
        float velocity = input * _acceleration * Time.deltaTime * -1;

        if (_back) velocity = -velocity;

         actualVelocity= math.abs(2 * Mathf.PI * _wheelsFrontLeft.radius * _wheelsFrontLeft.rpm * 60 / 1000);

        if (actualVelocity >= _velocityMax) velocity = 0;

        foreach (WheelCollider item in wheelsColliders) item.motorTorque = velocity;
       
    }
    void Direction(float input)
    {
       // Debug.Log(input);
        float newDirection = input* _turn;
        _wheelsFrontLeft.steerAngle = newDirection;
       _wheelsFrontRight.steerAngle = newDirection;
    }
    public void Stop()
    {
        if (_isBrake)
        {
            foreach (WheelCollider item in wheelsColliders) item.brakeTorque = 0;           
        }
        else
        {
            foreach (WheelCollider item in wheelsColliders) item.brakeTorque = _brakeForce;          
        }
        _isBrake = !_isBrake;
    }
    void Visual(float input)
    {
        _rotationWheels[0].transform.localRotation = Quaternion.Euler(0, 0, _turn*input);
        _rotationWheels[1].transform.localRotation = Quaternion.Euler(0, 0, _turn*input);

        _visualsWheels[0].transform.Rotate(0, actualVelocity, 0);
        _visualsWheels[1].transform.Rotate(0, actualVelocity, 0);
        _visualsWheels[2].transform.Rotate(0, actualVelocity, 0);
        _visualsWheels[3].transform.Rotate(0, actualVelocity, 0);
        _visualsWheels[4].transform.Rotate(0, actualVelocity, 0);
        _visualsWheels[5].transform.Rotate(0, actualVelocity, 0);

        _hologram.transform.localPosition = transform.localPosition;
        _hologram.transform.localRotation = transform.localRotation;
    }

    public void BackButton() => _back = !_back;

    public void ResetRotation() => _rotationLever.transform.rotation= Quaternion.Euler(70,0,0);
}
