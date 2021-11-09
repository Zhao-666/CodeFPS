using UnityEngine;

public class CombatStage_5 : GuideStageBase
{
    private bool arrivedTrigger = false;
    private bool leftTrigger = false;
    private bool targetUp = false;
    
    [Header("PositionTrigger"), SerializeField]
    //When player move to the enter.
    private GameObject position5Trigger;

    [Header("Targets")]
    //The target in the stair case.
    [SerializeField]
    private TargetScript target1;
    [SerializeField]
    private TargetScript target2;

    protected override void StartInit()
    {
        position5Trigger.SetActive(false);
        target1.Down(true);
        target2.Down(true);
    }

    protected override void BeforeRun()
    {
        position5Trigger.SetActive(true);
    }

    protected override void Process()
    {
        if (arrivedTrigger && target1.isHit && target2.isHit && !leftTrigger && targetUp)
        {
            ShowChatText(19);
        }

        if (arrivedTrigger && leftTrigger)
        {
            ShowChatText(20);
            Over();
        }
    }

    //trigger 触发此方法
    private void ArrivedArea(GameObject trigger)
    {
        if (trigger == position5Trigger)
        {
            ShowChatText(18);
            arrivedTrigger = true;
            target1.Up();
            target2.Up();
            targetUp = true;
        }
    }

    private void LeftArea(GameObject trigger)
    {
        if (trigger == position5Trigger)
        {
            position5Trigger.SetActive(false);
            leftTrigger = true;
        }
    }
}