using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisruptionEffect : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        target = GetComponent<Transform>();
    }
}
