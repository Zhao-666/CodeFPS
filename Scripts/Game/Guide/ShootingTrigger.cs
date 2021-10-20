using System;
using UnityEngine;

public class ShootingTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FPSController")
        {
            SendMessageUpwards("ArrivedShootingArea");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "FPSController")
        {
            SendMessageUpwards("LeftShootingArea");
        }
    }
}