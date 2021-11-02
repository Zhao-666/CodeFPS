using UnityEngine;

public class TrainingStage_4 : TrainingStageBase
{
    private bool canShot;
    private bool settingPanelClosed;

    [Header("Shooting target")]
    //The target that can be shot
    public TargetScript firstTarget;
    public TargetScript secondTarget;
    public TargetScript thirdTarget;

    protected override void AwakeInit()
    {
        firstTarget.Down(true);
        secondTarget.Down(true);
        thirdTarget.Down(true);
    }

    protected override void BeforeRun()
    {
        ShowChatText(3);
    }

    protected override void Process()
    {
        if (!settingPanelClosed && SettingPanelController.Instance.CurrentShowPanel == null)
        {
            settingPanelClosed = true;
        }

        if (settingPanelClosed && !canShot)
        {
            ShowChatText(4);
            firstTarget.Up();
            secondTarget.Up();
            thirdTarget.Up();
            canShot = true;
        }

        if (canShot && firstTarget.isHit && secondTarget.isHit && thirdTarget.isHit)
        {
            Over();
        }
    }
}