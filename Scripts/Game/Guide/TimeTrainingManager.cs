using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TimeTrainingManager : MonoBehaviour
{
    private const int TimeLimit = 10;
    private bool arrived = false;
    private bool isGameStart = false;
    private int score;
    private float currentTime;

    [Header("ShootingTrigger")]
    //ShootingTrigger
    public GameObject shootingTrigger;

    [Header("TargetGroup")]
    //TargetGroup Transform
    public Transform targetGroup;

    [Header("FirstTarget")]
    //FirstTarget
    public TargetScript firstTarget;

    [Header("UI Text")]
    //ScoreText
    public Transform scoreText;

    //TimeText
    public Transform timeText;

    // TargetScripts group
    private TargetScript[] targetScripts;

    private TargetScript currentTarget;

    void Awake()
    {
        scoreText.GetComponent<CanvasGroup>().DOFade(0, 0);
        timeText.GetComponent<CanvasGroup>().DOFade(0, 0);
        targetScripts = targetGroup.GetComponentsInChildren<TargetScript>();
    }

    void Update()
    {
        if (!isGameStart)
        {
            //射击第一个开始计时
            if (arrived && firstTarget.isHit && score == 0)
            {
                GameStart();
            }
        }

        //随机弹起靶子
        if (isGameStart && currentTarget.isHit)
        {
            score++;
            RandomUpTarget();
        }
    }

    private void OnEnable()
    {
        shootingTrigger.SetActive(true);
    }

    private void OnDisable()
    {
        shootingTrigger.SetActive(false);
    }

    private void GameStart()
    {
        isGameStart = true;
        scoreText.GetComponent<CanvasGroup>().DOFade(0, 1);
        RandomUpTarget();
        StartCoroutine(StartTiming());
    }

    private void GameOver()
    {
        isGameStart = false;
        currentTime = 0;
        scoreText.GetComponent<CanvasGroup>().DOFade(1, 1);
        timeText.GetComponent<Text>().text = "计时结束";
        scoreText.GetComponent<Text>().text = "本次得分：" + score;
        if (currentTarget != null && !currentTarget.isHit)
        {
            currentTarget.Down();
        }
    }

    //ShootingTrigger 触发此方法
    private void ArrivedArea()
    {
        timeText.GetComponent<CanvasGroup>().DOFade(1, 1);
        timeText.GetComponent<Text>().text = "射击靶子开始计时";
        arrived = true;
        firstTarget.Up();
        currentTarget = firstTarget;
    }

    //ShootingTrigger 触发此方法
    private void LeftArea()
    {
        arrived = false;
        score = 0;
        timeText.GetComponent<CanvasGroup>().DOFade(0, 1);
        scoreText.GetComponent<CanvasGroup>().DOFade(0, 1);
        if (currentTarget != null && !currentTarget.isHit)
        {
            currentTarget.Down();
        }
    }

    private void RandomUpTarget()
    {
        currentTarget = targetScripts[Random.Range(0, targetScripts.Length)];
        currentTarget.Up();
    }

    private IEnumerator StartTiming()
    {
        while (true)
        {
            yield return null;
            currentTime += Time.deltaTime;
            timeText.GetComponent<Text>().text = "剩余时间：" + Math.Round(10 - currentTime, 3);
            if (currentTime >= TimeLimit)
            {
                GameOver();
                yield return new WaitForSeconds(2);
                score = 0;
                firstTarget.Up();
                currentTarget = firstTarget;
                yield break;
            }
        }
    }
}