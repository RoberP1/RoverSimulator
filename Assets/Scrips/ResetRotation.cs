using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRotation : MonoBehaviour
{
    Quaternion _rotation;
    private void Awake()
    {
        _rotation = transform.rotation;
    }
    public void ReSetRotation()
    {
        transform.rotation = _rotation;
    }
}
