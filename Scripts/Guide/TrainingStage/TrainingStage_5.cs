using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingStage_5 : TrainingStageBase
{
    [Header("Wood target")]
    //The target that can be shot
    public TargetScript woodTarget;

    protected override void Process()
    {
        if (!hasShowTips && Time.time - runTime > 1)
        {
            ShowTips(5);
            woodTarget.isWillDown = false;
            woodTarget.Up();
        }

        if (hasShowTips && woodTarget.isHit)
        {
            StartCoroutine(WaitOver());
        }
    }

    private IEnumerator WaitOver()
    {
        yield return new WaitForSeconds(3);
        woodTarget.Down();
        Over();
    }
}