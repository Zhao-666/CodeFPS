using System.Collections;
using UnityEngine;

public class TrainingStage_6 : GuideStageBase
{
    private bool canShot;
    private const int MaxShotCount = 10;

    [Header("Shooting target")]
    //The target that can be shot
    public TargetScript[] targets;

    private TargetScript currentTarget;
    private int hasShotCount;

    protected override void AwakeInit()
    {
        foreach (TargetScript target in targets)
        {
            target.Down(true);
        }
    }

    protected override void BeforeRun()
    {
        StartCoroutine(ShowTwoChatText());
    }

    protected override void Process()
    {
        if (canShot)
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

    private IEnumerator ShowTwoChatText()
    {
        ShowChatText(9);
        yield return new WaitForSeconds(3);
        ShowChatText(10);
        yield return new WaitForSeconds(1);
        canShot = true;
    }
}