using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingStage_3 : TrainingStageBase
{
    [Header("Shooting target")]
    //The target that can be shot
    public TargetScript topTarget;

    public TargetScript firstTarget;

    protected override void Process()
    {
        if (!hasShowTips && Time.time - runTime > 1)
        {
            //提示鼠标左键射击
            SendMessageUpwards("ShowGuideTips", 3);
            //上下靶子起立
            topTarget.Up();
            firstTarget.Up();
            hasShowTips = true;
        }

        if (hasShowTips && topTarget.isHit && firstTarget.isHit)
        {
            //展示调换鼠标方向面板
            StartCoroutine(ShowPanel());
        }
    }

    private IEnumerator ShowPanel()
    {
        yield return new WaitForSeconds(0.5f);
        SettingPanelController.Instance.ShowMousePositionPanel();
        Over();
    }
}