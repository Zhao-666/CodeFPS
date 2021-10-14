using UnityEngine;

public class TrainingStage_4 : TrainingStageBase
{
    private bool settingPanelClosed;

    [Header("Shooting target")]
    //The target that can be shot
    public TargetScript firstTarget;

    public TargetScript secondTarget;
    public TargetScript thirdTarget;

    protected override void Process()
    {
        if (!settingPanelClosed && SettingPanelController.Instance.CurrentShowPanel == null)
        {
            settingPanelClosed = true;
        }

        if (settingPanelClosed && !hasShowTips)
        {
            ShowTips(4);
            firstTarget.Up();
            secondTarget.Up();
            thirdTarget.Up();
        }

        if (hasShowTips && firstTarget.isHit && secondTarget.isHit && thirdTarget.isHit)
        {
            Over();
        }
    }
}