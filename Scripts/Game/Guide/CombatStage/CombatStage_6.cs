using UnityEngine;

public class CombatStage_6 : GuideStageBase
{
    private bool arrivedTrigger = false;
    private bool targetsUp = false;
    private GameObject flash;

    [Header("PositionTrigger"), SerializeField]
    //When player move to the enter.
    private GameObject position6Trigger;

    [Header("Targets")]
    //The target in the stair case.
    [SerializeField]
    private TargetScript target1;
    [SerializeField] 
    private TargetScript target2;

    protected override void StartInit()
    {
        position6Trigger.SetActive(false);
        target1.Down(true);
        target2.Down(true);
    }

    protected override void BeforeRun()
    {
        position6Trigger.SetActive(true);
    }

    protected override void Process()
    {
        if (arrivedTrigger && flash == null && !targetsUp)
        {
            targetsUp = true;
            target1.Up();
            target2.Up();
        }
        
        if (flash == null && arrivedTrigger && target1.isHit && target2.isHit)
        {
            ShowChatText(21);
            Over();
        }
    }

    //trigger 触发此方法
    private void ArrivedArea(GameObject trigger)
    {
        if (trigger == position6Trigger)
        {
            flash = trigger.GetComponent<GuideTrigger>().TriggerObject;
            arrivedTrigger = true;
        }
    }
}