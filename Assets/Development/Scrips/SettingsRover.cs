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

        if (amountToFuel == "Maximum") valueWeight += 30;
        else if (amountToFuel == "Minimum") valueWeight -= 30;
        
        
        if(typeWeight == "Maximum") valueWeight += 550;
        else if(typeWeight == "Minimum") valueWeight += 450;
        else if(typeWeight == "Medium") valueWeight += 500;
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
