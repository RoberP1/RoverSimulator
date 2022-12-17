using Assets.OVR.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionAnalyzer : MonoBehaviour
{
    public List<Mission> missions;
    [SerializeField] TextMeshProUGUI analyzerText;
    [SerializeField] float analysisTime;
    [SerializeField] Material endMaterial;
    bool analyzable;
    Mission actualMission;
    void Start()
    {
        analyzable = false;
        foreach(Mission item in FindObjectsOfType<Mission>()) if(!item.analized) missions.Add(item); 
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
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Mission>() != null)
        {
            StopAllCoroutines();
            actualMission = null;
            analyzable= false;
        }
    }

    IEnumerator Analysis(Mission reward)
    {
        analyzable = false;
        analyzerText.text = "Analizando...";
        yield return new WaitForSeconds(reward.analizysTime);
        reward.analized=true;
        missions.Remove(reward);
        analyzerText.text = "Analizado";
       reward.hologram.transform.GetChild(0).GetComponent<MeshRenderer>().material = endMaterial;
        yield return new WaitForSeconds(analysisTime);
        analyzerText.text = "";
    }
}
