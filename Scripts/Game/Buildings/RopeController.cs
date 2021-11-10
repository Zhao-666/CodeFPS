using DG.Tweening;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    private bool playerUsed = false;
    private BoxCollider boxCollider;
    private Material material;
    private Tween emissionTween;

    public bool PlayerUsed => playerUsed;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        material = GetComponent<MeshRenderer>().material;
        emissionTween = material
            .DOColor(new Color(255f / 255, 215f / 255, 0, 0), "_EmissionColor", 1)
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void PlayerUse()
    {
        playerUsed = true;
    }

    public void HideBoxCollider()
    {
        boxCollider.enabled = false;
        emissionTween.TogglePause();
        material.DOColor(Color.black, "_EmissionColor", 0);
    }

    public void ShowBoxCollider()
    {
        boxCollider.enabled = true;
        emissionTween.TogglePause();
        playerUsed = false;
    }
}