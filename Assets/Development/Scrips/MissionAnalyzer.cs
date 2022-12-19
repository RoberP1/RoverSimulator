using Assets.OVR.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MissionAnalyzer : MonoBehaviour
{
    public List<Mission> missions;
    [SerializeField] TextMeshProUGUI analyzerText;
    [SerializeField] float analysisTime;
    [SerializeField] Material endMaterial;
    bool analyzable;
    Mission actualMission;
    public bool lightBool;
    public UnityEvent<bool> LightSwicht;
     
    void Start()
    {
        analyzable = false;
        foreach(Mission item in FindObjectsOfType<Mission>()) if(!item.analized) missions.Add(item);
        analyzerText.text = "";
    }

   public void Analize()
    {
        if (analyzable) StartCoroutine(Analysis(actualMission));
    }

    private void OnTriggerEnter(Collider other)
    {
        Mission reward= other.GetComponent<Mission>();
        if (reward != null)
        {
            actualMission = reward;
            analyzable= true;  
            lightBool= true;
            LightSwicht.Invoke(lightBool);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Mission>() != null)
        {
            StopAllCoroutines();
            actualMission = null;
            analyzable= false;
            lightBool = false;
            LightSwicht.Invoke(lightBool);
        }
    }

    IEnumerator Analysis(Mission reward)
    {
        analyzable = false;
        analyzerText.text = "Analizando...";
        yield return new WaitForSeconds(reward.analizysTime);
        reward.analized=true;
        missions.Remove(reward);
        lightBool = false;
        LightSwicht.Invoke(lightBool);
        analyzerText.text = "Analizado";
       reward.hologram.transform.GetChild(0).GetComponent<MeshRenderer>().material = endMaterial;
        yield return new WaitForSeconds(analysisTime);
        analyzerText.text = "";
    }
}
