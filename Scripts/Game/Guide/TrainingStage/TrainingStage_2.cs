using UnityEngine;

public class TrainingStage_2 : GuideStageBase
{
    [Header("AutomaticGunScriptLPFP")]
    //AutomaticGunScriptLPFP
    public AutomaticGunScriptLPFP automaticGunScript;

    protected override void BeforeRun()
    {
        ShowChatText(1);
    }

    protected override void Process()
    {
        if (!hasShowTips && Time.time - runTime > 1)
        {
            //提示鼠标右键瞄准
            ShowTips(1);
        }

        if (hasShowTips && automaticGunScript.IsAiming)
        {
            Over();
        }
    }
}