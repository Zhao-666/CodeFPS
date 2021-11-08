using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingStage_5 : GuideStageBase
{
    private bool canShot;

    [Header("Wood target")]
    //The target that can be shot
    public TargetScript woodTarget;

    [Header("Shooting target")]
    //The target that can be shot
    public TargetScript firstTarget;

    public TargetScript secondTarget;
    public TargetScript thirdTarget;

    protected override void AwakeInit()
    {
        woodTarget.Down(true);
        firstTarget.Down(true);
        secondTarget.Down(true);
        thirdTarget.Down(true);
    }

    protected override void BeforeRun()
    {
        StartCoroutine(ShowChatText());
    }

    protected override void Process()
    {
        if (canShot && firstTarget.isHit && secondTarget.isHit && thirdTarget.isHit)
        {
            canShot = false;
            ShowChatText(7);
            StartCoroutine(WaitOver());
        }
    }

    private IEnumerator WaitOver()
    {
        yield return new WaitForSeconds(2);
        ShowChatText(8);
        yield return new WaitForSeconds(3);
        woodTarget.Down();
        Over();
    }

    private IEnumerator ShowChatText()
    {
        yield return new WaitForSeconds(2);
        ShowChatText(5);
        yield return new WaitForSeconds(2);
        canShot = true;
        ShowChatText(6);
        woodTarget.isWillDown = false;
        woodTarget.Up();
        firstTarget.Up();
        secondTarget.Up();
        thirdTarget.Up();
    }
}