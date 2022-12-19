using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsRover : MonoBehaviour
{
    public ToggleGroup maxSpeedTG, turnAngleTG, amountToFuelTG, typeWeightTG;
    public string  maxSpeed, turnAngle, amountToFuel, typeWeight;
    public bool SolarEnergy;

    public void SetSpeed(string speed) => maxSpeed = speed;
    public void SetAngle(string angle) => turnAngle = angle;
    public void SetamountToFuel(string fuel) => amountToFuel = fuel;
    public void SetEnergy(bool energy)=> SolarEnergy = energy;
    public void SetWeight(string weight) => typeWeight= weight;
    public float ValueWeight()
    {
        float valueWeight = 0;

        if (amountToFuel == "Maximum") valueWeight += 50;
        else if (amountToFuel == "Minimum") valueWeight -= 50;
        
        
        if(typeWeight == "Maximum") valueWeight += 500;
        else if(typeWeight == "Minimum") valueWeight += 300;
        else if(typeWeight == "Medium") valueWeight += 400;
        return valueWeight;
    }
    public void StartSettings()
    {
        SetSpeed(maxSpeedTG.GetFirstActiveToggle().name);
        SetAngle(turnAngleTG.GetFirstActiveToggle().name);
        SetamountToFuel(amountToFuelTG.GetFirstActiveToggle().name);
        SetWeight(typeWeightTG.GetFirstActiveToggle().name);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) StartSettings();
    }
}
