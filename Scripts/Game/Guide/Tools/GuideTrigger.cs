using System;
using UnityEngine;

public class GuideTrigger : MonoBehaviour
{
    [Header("Trigger object name"),SerializeField]
    // Trigger object name
    private string triggerObjectName = "FPSController";

    private GameObject triggerObject;

    public GameObject TriggerObject => triggerObject;

    private void OnTriggerEnter(Collider other)
    {
        string objName = FilterName(other.gameObject.name);
        if (objName == triggerObjectName)
        {
            triggerObject = other.gameObject;
            SendMessageUpwards("ArrivedArea", gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string objName = FilterName(other.gameObject.name);
        if (objName == triggerObjectName)
        {
            SendMessageUpwards("LeftArea", gameObject);
        }
    }

    private string FilterName(string objectName)
    {
        int index = objectName.IndexOf("(Clone)");
        if (index != -1)
        {
            return objectName.Substring(0, index);
        }

        return objectName;
    }
}