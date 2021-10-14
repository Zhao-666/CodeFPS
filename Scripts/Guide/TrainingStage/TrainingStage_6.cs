using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingStage_6 : TrainingStageBase
{
    private const int MaxShotCount = 10;

    [Header("Shooting target")]
    //The target that can be shot
    public TargetScript[] targets;

    private TargetScript currentTarget;
    private int hasShotCount;

    protected override void Process()
    {
        if (!hasShowTips && Time.time - runTime > 2)
        {
            ShowTips(6);
        }

        if (hasShowTips)
        {
            if (currentTarget == null || currentTarget.isHit)
            {
                if (hasShotCount >= MaxShotCount)
                {
                    Over();
                }

                RandomUpTarget();
            }
        }
    }

    private void RandomUpTarget()
    {
        currentTarget = targets[Random.Range(0, targets.Length)];
        currentTarget.Up();
        hasShotCount++;
    }
}