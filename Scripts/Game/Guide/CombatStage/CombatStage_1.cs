using System.Collections;
using UnityEngine;

public class CombatStage_1 : GuideStageBase
{
    private bool chatOver = false;

    [Header("MP5 Gun Model"),SerializeField]
    //MP5 Gun Model
    private GameObject mp5Model;

    [Header("WoodenFrameTopTrigger"), SerializeField]
    //When player climb up to the top of the rope.
    private GameObject woodenFrameTopTrigger;

    [Header("Rope"), SerializeField]
    //The rope on the wooden frame
    private RopeController ropeController;

    protected override void StartInit()
    {
        woodenFrameTopTrigger.SetActive(false);
        ropeController.HideBoxCollider();
    }

    protected override void BeforeRun()
    {
        woodenFrameTopTrigger.SetActive(true);
    }

    protected override void Process()
    {
        if (chatOver && mp5Model == null)
        {
            Over();
        }
    }

    //DoorTrigger 触发此方法
    private void ArrivedArea(GameObject trigger)
    {
        if (trigger == woodenFrameTopTrigger)
        {
            woodenFrameTopTrigger.SetActive(false);
            StartCoroutine(ShowGuideText());
        }
    }

    private IEnumerator ShowGuideText()
    {
        int max = 10;
        for (int i = 5; i < max; i++)
        {
            ShowChatText(i);
            yield return new WaitForSeconds(3);
        }

        chatOver = true;
        ropeController.ShowBoxCollider();
    }
}