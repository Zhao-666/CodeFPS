using System;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    [Header("Ladder Trigger"), SerializeField]
    private BoxCollider ladderTrigger;

    private void OnCollisionEnter(Collision other)
    {
        var center = ladderTrigger.center;
        Vector3 pos = new Vector3(center.x, center.y + 0.5f, center.z - 0.2f);
        ladderTrigger.center = pos;
    }

    private void OnCollisionExit(Collision other1)
    {
        var center = ladderTrigger.center;
        Vector3 pos = new Vector3(center.x, center.y - 0.5f, center.z + 0.2f);
        ladderTrigger.center = pos;
    }
}