using UnityEngine;

public class TrainingStage_0 : TrainingStageBase
{
    [Header("Gun")]
    //Gun
    public GameObject assaultRifle;

    protected override void Process()
    {
        if (!hasShowTips && Time.time - runTime > 2)
        {
            SendMessageUpwards("ShowGuideTips", value: 0);
            hasShowTips = true;
        }

        if (assaultRifle == null)
        {
            //已拾取AK47，进入下一阶段
            Over();
        }
    }
}