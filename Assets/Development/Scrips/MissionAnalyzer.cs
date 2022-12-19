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
    bool isResearching;
    public UnityEvent<bool> LightSwicht;
    [SerializeField] AudioClip unFinish, finish, research;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        analyzable = false;
        foreach(Mission item in FindObjectsOfType<Mission>()) if(!item.analized) missions.Add(item);
        analyzerText.text = "";
    }

   public void Analize()
    {
        if (analyzable && !actualMission.analized) StartCoroutine(Analysis(actualMission));
    }

    private void OnTriggerEnter(Collider other)
    {
        Mission reward= other.GetComponent<Mission>();
        if (reward != null)
        {
            if (!reward.analized)
            {
                actualMission = reward;
                analyzable = true;
                lightBool = true;
                LightSwicht.Invoke(lightBool);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Mission>() != null)
        {
            if (isResearching)
            {
                audioSource.Stop();
                audioSource.clip = unFinish;
                audioSource.Play();
                analyzerText.text = "";
            }
            StopAllCoroutines();
            actualMission = null;
            analyzable= false;
            lightBool = false;
            LightSwicht.Invoke(lightBool);
        }
    }

    IEnumerator Analysis(Mission reward)
    {
        audioSource.clip = research;
        audioSource.Play();
        isResearching = true;
        analyzable = false;
        analyzerText.text = "Analizando...";
        yield return new WaitForSeconds(research.length);
        reward.analized=true;
        missions.Remove(reward);
        lightBool = false;
        LightSwicht.Invoke(lightBool);
        analyzerText.text = "";
        isResearching = false;
        audioSource.clip = finish;
        audioSource.Play();
        reward.hologram.transform.GetChild(0).GetComponent<MeshRenderer>().material = endMaterial;     
    }
}
