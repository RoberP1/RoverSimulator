using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    public bool solar;
    public float actualEnergy;
    public float maxEnergy;
    public float timeLoad;
    void Start()
    {
        actualEnergy = maxEnergy;
        if (solar) StartCoroutine(UpEnergy(timeLoad*2));
    }


    IEnumerator UpEnergy(float time)
    {
        yield return new WaitForSeconds(time);
        if(actualEnergy<maxEnergy) actualEnergy ++;
        StartCoroutine(UpEnergy(time));
    }
}

