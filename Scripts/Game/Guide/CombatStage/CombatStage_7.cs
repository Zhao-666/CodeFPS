using System.Collections;
using UnityEngine;

public class CombatStage_7 : GuideStageBase
{
    [Header("PositionTrigger"), SerializeField]
    //When player move to the enter.
    private GameObject position7Trigger;

    protected override void StartInit()
    {
        position7Trigger.SetActive(false);
    }

    protected override void BeforeRun()
    {
        position7Trigger.SetActive(true);
        ShowTips(2);
        StartCoroutine(ShowChatText());
    }

    protected override void Process()
    {
        
    }

    //trigger 触发此方法
    private void ArrivedArea(GameObject trigger)
    {
        if (trigger == position7Trigger)
        {
            position7Trigger.SetActive(false);
            SendMessageUpwards("FinishTraining");
            Over();
        }
    }

    private IEnumerator ShowChatText()
    {
        ShowChatText(22);
        yield return new WaitForSeconds(2);
        ShowChatText(23);
    }
}