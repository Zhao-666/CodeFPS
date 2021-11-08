using UnityEngine;

public class TrainingStage_0 : GuideStageBase
{
    [Header("Gun")]
    //Gun
    public GameObject assaultRifle;

    protected override void Process()
    {
        if (!hasShowTips && Time.time - runTime > 2)
        {
            ShowTips(0);
        }

        if (assaultRifle == null)
        {
            //已拾取AK47，进入下一阶段
            Over();
        }
    }
}