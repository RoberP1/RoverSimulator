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
    void Start()
    {
        foreach(Mission item in FindObjectsOfType<Mission>()) if(!item.analized) missions.Add(item); 
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Mission reward= other.GetComponent<Mission>();
        if (reward != null) StartCoroutine(Analysis(reward));
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Mission>() != null) StopAllCoroutines();
    }

    IEnumerator Analysis(Mission reward)
    {
        analyzerText.text = "Analizando...";
        yield return new WaitForSeconds(reward.analizysTime);
        reward.analized=true;
        missions.Remove(reward);
        analyzerText.text = "Analizado";
        reward.hologram.GetComponent<MeshRenderer>().material = endMaterial;
        yield return new WaitForSeconds(analysisTime);
        analyzerText.text = "";
    }
}
