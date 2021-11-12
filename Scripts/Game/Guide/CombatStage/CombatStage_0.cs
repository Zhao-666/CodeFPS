using System.Collections;
using UnityEngine;

public class CombatStage_0 : GuideStageBase
{
    private bool arrivedRopeBottom = false;
    private bool chatOver = false;
    
    [Header("DoorTrigger"), SerializeField]
    //When player near door that the stage start.
    private GameObject doorTrigger;

    [Header("LadderBottomTrigger"), SerializeField]
    //When player walk to the ladder bottom.
    private GameObject ladderBottomTrigger;

    protected override void AwakeInit()
    {
        base.AwakeInit();
        doorTrigger.SetActive(false);
        ladderBottomTrigger.SetActive(false);
    }

    protected override void BeforeRun()
    {
        base.BeforeRun();
        doorTrigger.SetActive(true);
        ladderBottomTrigger.SetActive(true);
    }

    protected override void Process()
    {
        if (chatOver && arrivedRopeBottom)
        {
            Over();
        }
    }

    //DoorTrigger 触发此方法
    private void ArrivedArea(GameObject trigger)
    {
        if (trigger == doorTrigger)
        {
            doorTrigger.SetActive(false);
            StartCoroutine(ShowGuideText());
        }
        else if (trigger == ladderBottomTrigger)
        {
            ShowTips(0,2);
            arrivedRopeBottom = true;
            ladderBottomTrigger.SetActive(false);
        }
    }

    private IEnumerator ShowGuideText()
    {
        int max = 5;
        for (int i = 0; i < max; i++)
        {
            ShowChatText(i);
            yield return new WaitForSeconds(3);
        }

        chatOver = true;
    }
}