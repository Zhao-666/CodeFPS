using UnityEngine;

public class CombatStage_3 : GuideStageBase
{
    private bool arrivedTrigger = false;

    [Header("Position2Trigger"), SerializeField]
    //When player move to the stair case.
    private GameObject position2Trigger;

    [Header("Targets")]
    //The target in the stair case.
    [SerializeField]
    private TargetScript target;

    protected override void StartInit()
    {
        position2Trigger.SetActive(false);
        target.Down(true);
    }

    protected override void BeforeRun()
    {
        position2Trigger.SetActive(true);
    }

    protected override void Process()
    {
        if (arrivedTrigger && target.isHit)
        {
            Over();
        }
    }

    //trigger 触发此方法
    private void ArrivedArea(GameObject trigger)
    {
        if (trigger == position2Trigger)
        {
            position2Trigger.SetActive(false);
            arrivedTrigger = true;
            target.Up();
            ShowChatText(13);
        }
    }
}