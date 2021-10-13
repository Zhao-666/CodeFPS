using UnityEngine;

public class GuideManager : MonoBehaviour
{
    private int currentStage = 0;

    [Header("Gun")]
    public GameObject assaultRifle;
    public GameObject handGun;

    [Header("Shooting target")]
    //The target that can be shot
    public TargetScript woodTarget;

    public TargetScript topTarget;
    public TargetScript[] targetList;

    // Start is called before the first frame update
    void Start()
    {
        handGun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void PlayerArrived()
    {
        topTarget.Up();
        targetList[0].Up();
    }
}