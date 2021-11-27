using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ActualCombatManager : GuideManagerBase
{
    [Header("Combat UI")]
    //Show the training info.
    [SerializeField]
    private Transform timeText;

    [SerializeField] private Transform scoreText;

    private float currentTime;
    private float bestTime;

    protected override string StagePrefix => "CombatStage_";
    protected override List<string> ChatTexts => CombatChatText.ChatTexts;
    protected override List<string> GuideTips => CombatTips.Tips;

    // protected override int CurrentStage { get; set; } = 7;    //Debug用

    protected override void Awake()
    {
        base.Awake();
        timeText.GetComponent<CanvasGroup>().DOFade(0, 0);
        scoreText.GetComponent<CanvasGroup>().DOFade(0, 0);
    }

    protected override void GuideOver()
    {
        base.GuideOver();
        StartCoroutine(ShowChatText());
    }

    private IEnumerator ShowChatText()
    {
        PublishChatText(24);
        yield return new WaitForSeconds(2);
        PublishChatText(25);
        yield return new WaitForSeconds(2);
        PublishChatText(26);
        RestartGuide(1);
    }

    private void StartTraining()
    {
        currentTime = 0;
        scoreText.GetComponent<CanvasGroup>().DOFade(0, 0);
        timeText.GetComponent<CanvasGroup>().DOFade(1, 1);
        StartCoroutine(nameof(StartTiming));
    }

    private void FinishTraining()
    {
        StopCoroutine(nameof(StartTiming));
        if (Math.Abs(bestTime) < 0.1f || currentTime < bestTime)
        {
            bestTime = currentTime;
        }

        timeText.GetComponent<CanvasGroup>().DOFade(0, 0);
        scoreText.GetComponent<CanvasGroup>().DOFade(1, 1);
        scoreText.GetComponent<Text>().text = $"最终时间：{currentTime:N1}\n历史最佳：{bestTime:N1}";
    }

    private IEnumerator StartTiming()
    {
        while (true)
        {
            yield return null;
            currentTime += Time.deltaTime;
            timeText.GetComponent<Text>().text = "当前时间：" + Math.Round(currentTime, 1);
        }
    }
}