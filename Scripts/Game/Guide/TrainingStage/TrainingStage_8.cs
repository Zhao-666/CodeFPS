using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingStage_8 : TrainingStageBase
{
    private bool switchAssaultRifle = false;

    [Header("Guns Arms")]
    //Guns Arms
    public GameObject assaultRifleArms;

    public GameObject handgunArms;

    protected override void BeforeRun()
    {
        ShowChatText(13);
    }

    protected override void Process()
    {
        if (!switchAssaultRifle && assaultRifleArms.activeSelf)
        {
            switchAssaultRifle = true;
            ShowChatText(14);
        }

        if (switchAssaultRifle && handgunArms.activeSelf)
        {
            ShowChatText(15);
            Over();
        }
    }
}