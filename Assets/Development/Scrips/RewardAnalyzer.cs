using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardAnalyzer : MonoBehaviour
{
    public List<Reward> _rewards;
    [SerializeField] TextMeshProUGUI analyzerText;
    [SerializeField] float analysisTime;
    [SerializeField] Material endMaterial;
    void Start()
    {
        foreach(Reward item in FindObjectsOfType<Reward>()) if(!item.analized) _rewards.Add(item); 
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Reward reward= other.GetComponent<Reward>();
        if (reward != null) StartCoroutine(Analysis(reward));
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<Reward>() != null) StopAllCoroutines();
    }

    IEnumerator Analysis(Reward reward)
    {
        analyzerText.text = "Analizando...";
        yield return new WaitForSeconds(reward.analizysTime);
        reward.analized=true;
        _rewards.Remove(reward);
        analyzerText.text = "Analizado";
        reward.hologram.GetComponent<MeshRenderer>().material = endMaterial;
        yield return new WaitForSeconds(analysisTime);
        analyzerText.text = "";
    }
}
