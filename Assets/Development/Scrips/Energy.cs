using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Energy : MonoBehaviour
{
    public bool solar;
    public float actualEnergy;
    public float maxEnergy, changeEnergy;
    public float timeLoad;
    public TextMeshProUGUI textEnergy, textEnergyType;
    SettingsRover settings;
    bool init;
    private void Start()
    {
        init = false;
        textEnergy.text = "";
        textEnergyType.text = "";
    }
    public void StartRover()
    {
        init = true;
        settings = FindObjectOfType<SettingsRover>();
        solar = settings.SolarEnergy;

        if (settings.amountToFuel == "Maximum") maxEnergy += changeEnergy;
        if (settings.amountToFuel == "Minimum") maxEnergy -= changeEnergy;

        actualEnergy = maxEnergy;
        if (solar)
        {
            StartCoroutine(UpEnergy(timeLoad * 2));
            textEnergyType.text = "Solar";
        }
        else textEnergyType.text = "Fuel";
    }


    IEnumerator UpEnergy(float time)
    {
        yield return new WaitForSeconds(time);
        if(actualEnergy<maxEnergy) actualEnergy ++;
        StartCoroutine(UpEnergy(time));
    }

    private void Update()
    {
        if (init)
        {
            float percentage = actualEnergy * 100 / maxEnergy;
            if (percentage > 100) percentage = 100;
            textEnergy.text = "Energy: " + percentage.ToString("") + "%";
            if (percentage > 70) textEnergy.color = Color.green;
            else if (percentage < 40) textEnergy.color = Color.red;
            else textEnergy.color = Color.yellow;
        }
    }
}

