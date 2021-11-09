using System.Collections;
using UnityEngine;

public class CombatStage_0 : GuideStageBase
{
    private bool arrivedRopeTop = false;
    private bool chatOver = false;
    
    [Header("DoorTrigger"), SerializeField]
    //When player near door that the stage start.
    private GameObject doorTrigger;

    [Header("LadderTopTrigger"), SerializeField]
    //When player climb up to the top of the ladder.
    private GameObject ladderTopTrigger;

    protected override void Process()
    {
        if (chatOver && arrivedRopeTop)
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
        else if (trigger == ladderTopTrigger)
        {
            arrivedRopeTop = true;
            ladderTopTrigger.SetActive(false);
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