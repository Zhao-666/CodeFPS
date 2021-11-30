using UnityEngine;

public class RafaleController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * 0.3f,Space.Self);
    }
}