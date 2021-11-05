using System;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    private BoxCollider ladderTrigger;

    private void Awake()
    {
        ladderTrigger = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var center = ladderTrigger.center;
        Vector3 pos = new Vector3(center.x, center.y - 0.1f, center.z - 0.1f);
        ladderTrigger.center = pos;
    }

    private void OnTriggerExit(Collider other)
    {
        
        var center = ladderTrigger.center;
        Vector3 pos = new Vector3(center.x, center.y + 0.1f, center.z + 0.1f);
        ladderTrigger.center = pos;
    }
}