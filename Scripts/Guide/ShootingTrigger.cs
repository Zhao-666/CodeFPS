using UnityEngine;

public class ShootingTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FPSController")
        {
            Debug.Log("ShootingTrigger Enter");
            SendMessageUpwards("ArrivedShootingArea");
        }
    }
}