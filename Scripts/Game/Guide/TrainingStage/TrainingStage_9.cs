using System.Collections;
using UnityEngine;

public class TrainingStage_9 : TrainingStageBase
{
    private bool isOver = false;
    
    [Header("Watermelon")] [SerializeField]
    //Watermelon game object
    private GameObject watermelon;

    [Header("WatermelonTrigger")] [SerializeField]
    //WatermelonTrigger
    private GameObject watermelonTrigger;

    protected override void AwakeInit()
    {
        watermelon.SetActive(false);
    }

    protected override void BeforeRun()
    {
        watermelon.SetActive(true);
        StartCoroutine(ShowStartChatText());
    }

    protected override void Process()
    {
        if (!isOver && watermelon == null)
        {
            isOver = true;
            StartCoroutine(ShowOverChatText());
        }
    }
    
    //WatermelonTrigger 触发此方法
    private void ArrivedArea()
    {
        ShowTips(5);
        watermelonTrigger.SetActive(false);
    }

    private IEnumerator ShowStartChatText()
    {
        ShowTips(4);
        ShowChatText(16);
        yield return new WaitForSeconds(2);
        ShowChatText(17);
        yield return new WaitForSeconds(2);
        ShowChatText(18);
    }

    private IEnumerator ShowOverChatText()
    {
        HideTips();
        ShowChatText(19);
        yield return new WaitForSeconds(3);
        ShowChatText(20);
        Over();
    }
}