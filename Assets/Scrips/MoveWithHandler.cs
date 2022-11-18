using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithHandler : MonoBehaviour
{
    [SerializeField] private Transform _target;
    void Start()
    {
        StartCoroutine(SetAsChild());
    }
    IEnumerator SetAsChild()
    {
        yield return new WaitForEndOfFrame();
        transform.parent = _target;
    }

}
