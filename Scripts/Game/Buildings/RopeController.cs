using UnityEngine;

public class RopeController : MonoBehaviour
{
    private BoxCollider boxCollider;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    public void HideBoxCollider()
    {
        boxCollider.enabled = false;
    }

    public void ShowBoxCollider()
    {
        boxCollider.enabled = true;
    }

    public bool IsHideBoxCollider()
    {
        return boxCollider.enabled;
    }
}