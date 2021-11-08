using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingStage_8 : GuideStageBase
{
    private bool isOver = false;
    private bool switchAssaultRifle = false;

    [Header("Guns Arms")]
    //Guns Arms
    public GameObject assaultRifleArms;

    public GameObject handgunArms;

    protected override void BeforeRun()
    {
        ShowTips(3);
        ShowChatText(13);
    }

    protected override void Process()
    {
        if (!switchAssaultRifle && assaultRifleArms.activeSelf)
        {
            switchAssaultRifle = true;
            ShowChatText(14);
        }

        if (switchAssaultRifle && handgunArms.activeSelf && !isOver)
        {
            isOver = true;
            HideTips();
            StartCoroutine(ShowChatText());
        }
    }

    private IEnumerator ShowChatText()
    {
        ShowChatText(15);
        yield return new WaitForSeconds(2);
        Over();
    }
}