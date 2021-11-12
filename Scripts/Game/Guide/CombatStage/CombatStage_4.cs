using System.Collections;
using UnityEngine;

public class CombatStage_4 : GuideStageBase
{
    private bool arrivedTrigger3 = false;
    private bool arrivedTrigger4 = false;
    private bool targetsUp = false;
    private GameObject flash;

    [Header("PositionTrigger"), SerializeField]
    //When player move to the stair case.
    private GameObject position3Trigger;

    [SerializeField] private GameObject position4Trigger;

    [Header("Targets")]
    //The target in the stair case.
    [SerializeField]
    private TargetScript target1;
    [SerializeField]
    private TargetScript target2;

    protected override void StartInit()
    {
        position3Trigger.SetActive(false);
        position4Trigger.SetActive(false);
        target1.Down(true);
        target2.Down(true);
    }

    protected override void BeforeRun()
    {
        position3Trigger.SetActive(true);
    }

    protected override void Process()
    {
        if (arrivedTrigger4 && flash == null && !targetsUp)
        {
            StartCoroutine(ShowChatText());
            targetsUp = true;
            target1.Up();
            target2.Up();
        }

        if (flash == null && arrivedTrigger3 && arrivedTrigger4 && target1.isHit && target2.isHit)
        {
            Over();
        }
    }

    protected override void ResetOverCondition()
    {
        base.ResetOverCondition();
        arrivedTrigger3 = false;
        arrivedTrigger4 = false;
        targetsUp = false;
    }

    //trigger 触发此方法
    private void ArrivedArea(GameObject trigger)
    {
        if (trigger == position3Trigger)
        {
            ShowTips(1);
            ShowChatText(14);
            position3Trigger.SetActive(false);
            position4Trigger.SetActive(true);
            arrivedTrigger3 = true;
        }
        else if (trigger == position4Trigger)
        {
            position4Trigger.SetActive(false);
            flash = trigger.GetComponent<GuideTrigger>().TriggerObject;
            arrivedTrigger4 = true;
        }
    }

    private IEnumerator ShowChatText()
    {
        ShowChatText(15);
        yield return new WaitForSeconds(2);
        ShowChatText(16);
    }
}