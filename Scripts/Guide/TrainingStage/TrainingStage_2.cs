using UnityEngine;

public class TrainingStage_2 : TrainingStageBase
{
    [Header("AutomaticGunScriptLPFP")]
    //AutomaticGunScriptLPFP
    public AutomaticGunScriptLPFP automaticGunScript;

    protected override void Process()
    {
        if (!hasShowTips && Time.time - runTime > 1)
        {
            //提示鼠标右键瞄准
            ShowTips(2);
        }

        if (hasShowTips && automaticGunScript.IsAiming)
        {
            Over();
        }
    }
}