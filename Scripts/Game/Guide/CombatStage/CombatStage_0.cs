using System.Collections;
using UnityEngine;

public class CombatStage_0 : GuideStageBase
{
    private bool arrivedRopeTop = false;
    private bool chatOver = false;
    
    [Header("DoorTrigger"), SerializeField]
    //When player near door that the stage start.
    private GameObject doorTrigger;

    [Header("RopeTopTrigger"), SerializeField]
    //When player climb up to the top of the rope.
    private GameObject ropeTopTrigger;

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
        else if (trigger == ropeTopTrigger)
        {
            arrivedRopeTop = true;
            ropeTopTrigger.SetActive(false);
        }
    }

    private IEnumerator ShowGuideText()
    {
        int max = 5;
        for (int i = 0; i < max; i++)
        {
            yield return new WaitForSeconds(3);
            ShowChatText(i);
        }

        chatOver = true;
    }
}