using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableReward", menuName = "Scriptables/Reward")]
public class Reward : MonoBehaviour
{
    public string nameReward;
    public float analizysTime;
    public GameObject hologram;
    public bool analized;
    private void Start()
    {
        hologram.transform.localPosition = transform.localPosition;
        hologram.transform.localRotation = transform.localRotation;
        hologram.transform.localScale = transform.localScale;
    }
}
