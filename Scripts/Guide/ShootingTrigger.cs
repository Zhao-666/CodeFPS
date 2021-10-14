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
}