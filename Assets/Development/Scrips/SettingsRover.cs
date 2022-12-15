using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsRover : MonoBehaviour
{
    public string  maxSpeed, turnAngle, amountToFuel, typeEnergy, typeWeight;
    void Start()
    {
    
    }

    void Update()
    {
        
    }

    public void SetSpeed(string speed) => maxSpeed = speed;
    public void SetAngle(string angle) => turnAngle = angle;
    public void SetamountToFuel(string fuel) => amountToFuel = fuel;
    public void SetEnergy(string energy)=> typeEnergy= energy;
    public void SetWeight(string weight) => typeWeight= weight;
    public float ValueWeight()
    {
        float valueWeight = 0;
        if (amountToFuel == "Maximum") valueWeight += 100;
        else if (amountToFuel == "Minimum") valueWeight -= 100;
        if(typeWeight == "Maximum") valueWeight += 700;
        else if(typeWeight == "Minimum") valueWeight += 300;
        else if(typeWeight == "Medium") valueWeight += 300;
        return valueWeight;
    }
}
