using UnityEngine;

public class Mission : MonoBehaviour
{
    public string nameReward;
    public float analizysTime;
    public GameObject hologram;
    public bool analized;
    private void Start()
    {
        hologram.transform.localPosition = transform.localPosition;
        hologram.transform.localRotation = transform.localRotation;
    }
}
