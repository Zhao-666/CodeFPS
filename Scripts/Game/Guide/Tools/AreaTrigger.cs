using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "FPSController")
        {
            SendMessageUpwards("ArrivedArea", gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "FPSController")
        {
            SendMessageUpwards("LeftArea", gameObject);
        }
    }
}