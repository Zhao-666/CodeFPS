using System.Collections;
using UnityEngine;

public class TrainingStage_3 : GuideStageBase
{
    [Header("Shooting target")]
    //The target that can be shot
    public TargetScript topTarget;
    public TargetScript firstTarget;

    protected override void AwakeInit()
    {
        topTarget.Down(true);
        firstTarget.Down(true);
    }

    protected override void Process()
    {
        if (!hasShowTips && Time.time - runTime > 1)
        {
            //提示鼠标左键射击
            ShowTips(2);
            ShowChatText(2);
            //上下靶子起立
            topTarget.Up();
            firstTarget.Up();
        }

        if (hasShowTips && topTarget.isHit && firstTarget.isHit)
        {
            //展示调换鼠标方向面板
            StartCoroutine(ShowPanel());
        }
    }

    private IEnumerator ShowPanel()
    {
        yield return new WaitForSeconds(0.3f);
        SettingPanelController.Instance.ShowMousePositionPanel();
        Over();
    }
}