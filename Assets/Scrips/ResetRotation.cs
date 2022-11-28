using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class ResetRotation : MonoBehaviour
{
    Quaternion _rotation;
    OneGrabRotateTransformer _transformer;

    private void Awake()
    {
        _rotation = transform.rotation;
        _transformer = GetComponent<OneGrabRotateTransformer>();
    }
    public void ReSetRotation()
    {
        transform.rotation = _rotation;
        _transformer._relativeAngle = 0;
        _transformer._constrainedRelativeAngle = 0;
    }
}
