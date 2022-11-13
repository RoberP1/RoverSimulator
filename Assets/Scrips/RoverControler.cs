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
    public bool back, isBrake;
    Rigidbody rb;
    WheelCollider[] wheelsColliders;
    [SerializeField] GameObject[] VisualsWheels;

    //quitar
    public Slider slider, slider2;
    public Toggle toggle;
    void Start()
    {
       rb= GetComponent<Rigidbody>();
        wheelsColliders= GetComponentsInChildren<WheelCollider>();
    }

    void Update()
    {
        Movement(slider.value);
        back = toggle.isOn;
        Direction(slider2.value);
        Visual();
    }

    void Movement(float input)
    {
        float velocity = input * _acceleration * Time.deltaTime * -1;

        if (back) velocity = -velocity;

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
        if (isBrake)
        {
            foreach (WheelCollider item in wheelsColliders) item.brakeTorque = 0;
            isBrake = false;
        }
        else
        {
            foreach (WheelCollider item in wheelsColliders) item.brakeTorque = _brakeForce;
            isBrake = true;
        }
    }
    void Visual()
    {
        VisualsWheels[0].transform.Rotate(0, actualVelocity, 0);
        VisualsWheels[1].transform.Rotate(0, actualVelocity, 0);
        VisualsWheels[2].transform.Rotate(0, actualVelocity, 0);
        VisualsWheels[3].transform.Rotate(0, actualVelocity, 0);
        VisualsWheels[4].transform.Rotate(0, actualVelocity, 0);
        VisualsWheels[5].transform.Rotate(0, actualVelocity, 0);
    }
}
