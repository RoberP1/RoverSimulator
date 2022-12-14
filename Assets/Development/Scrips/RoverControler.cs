using System.Collections;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Energy))]
[RequireComponent(typeof(Rigidbody))]
public class RoverControler : MonoBehaviour
{
    [SerializeField] WheelCollider _wheelsFrontLeft, _wheelsFrontRight;
    [SerializeField] float _acceleration, _turn, _changeTurn, _velocityMax, _changeSpeed, _brakeForce;
    public float actualVelocity;
    bool _back, _isBrake;
    Rigidbody rb;
    WheelCollider[] wheelsColliders;
    [SerializeField] GameObject[] _visualsWheels,_rotationWheels;
    [SerializeField] GameObject _throttleLever, _rotationLever,_baseRotationLever, _hologram;
    [SerializeField] float _maxDirectionLever,_maxRotationLever;
    SettingsRover settings;
    Energy _energy;

    void Start()
    {


        rb = GetComponent<Rigidbody>();
        wheelsColliders = GetComponentsInChildren<WheelCollider>();
        _energy = GetComponent<Energy>();
        //StartRover();
    }

    public void StartRover()
    {
        settings = FindObjectOfType<SettingsRover>();

        if (settings.maxSpeed == "Maximum") _velocityMax += _changeSpeed;
        else if (settings.maxSpeed == "Minimum") _velocityMax -= _changeSpeed;

        if (settings.turnAngle == "Maximum") _turn += _changeTurn;
        else if (settings.turnAngle == "Minimum") _turn -= _changeTurn;

        rb.mass = settings.ValueWeight();

        StartCoroutine(downEnergy(_energy.timeLoad, _throttleLever.transform.localPosition.x / _maxDirectionLever));
    }

    void Update()
    {
        if (_energy.actualEnergy >= 0) 
    { 
            _rotationLever.transform.position = new Vector3(0, 0, _baseRotationLever.transform.position.z);
        float inputRotation = _rotationLever.transform.localPosition.z < 0 ? 0 : _rotationLever.transform.localPosition.z;
        Movement(_throttleLever.transform.localPosition.x / _maxDirectionLever);
        Direction(0.4376513761751f * inputRotation - 0.9919922799418f);
        Visual(0.4376513761751f * inputRotation - 0.9919922799418f);
    }
        else
        {
        Movement(0);
        Direction(0);
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
        _hologram.transform.localScale = transform.localScale;
    }

    public void BackButton() => _back = !_back;

    IEnumerator downEnergy(float time, float inputAceleretion)
    {
        yield return new WaitForSeconds(time);
        if (inputAceleretion != 0 && _energy.actualEnergy > 0 && !_isBrake)
        {
         if(_energy.solar)   _energy.actualEnergy -= math.abs(inputAceleretion)*2;
         else _energy.actualEnergy -= math.abs(inputAceleretion);
        }
        StartCoroutine(downEnergy(time, _throttleLever.transform.localPosition.x / _maxDirectionLever));
    }
}
