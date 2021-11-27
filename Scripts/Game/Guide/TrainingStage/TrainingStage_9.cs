using System.Collections;
using DG.Tweening;
using UnityEngine;

public class TrainingStage_9 : GuideStageBase
{
    private bool useGrenade = false;
    private bool showChatOver = false;
    private bool processOver = false;
    private bool watermelonIsNull = false;

    [Header("RagdollSoldier")] [SerializeField]
    //A ragdoll soldier prefab
    private GameObject ragdollSoldier;

    [Header("Soldier")] [SerializeField]
    // The soldier stand in the stands.
    private GameObject soldier;

    [Header("Watermelon")] [SerializeField]
    //Watermelon game object
    private GameObject watermelon;

    [Header("WatermelonGrenade")] [SerializeField]
    //WatermelonGrenade game object
    private GameObject watermelonGrenade;

    [Header("WatermelonTrigger")] [SerializeField]
    //WatermelonTrigger
    private GameObject watermelonTrigger;

    [Header("RagdollTrigger")] [SerializeField]
    //RagdollTrigger
    private GameObject ragdollTrigger;

    [Header("Door")] [SerializeField]
    //Door
    private GameObject door;

    private GameObject currentWatermelon;

    protected override void AwakeInit()
    {
        watermelon.SetActive(false);
        watermelonGrenade.SetActive(false);
        watermelonTrigger.SetActive(false);
        ragdollTrigger.SetActive(false);

        if (useGrenade)
        {
            currentWatermelon = watermelonGrenade;
        }
        else
        {
            currentWatermelon = watermelon;
        }
    }

    protected override void BeforeRun()
    {
        currentWatermelon.SetActive(true);
        watermelonTrigger.SetActive(true);
        StartCoroutine(ShowStartChatText());
    }

    protected override void Process()
    {
        if (!watermelonIsNull && currentWatermelon == null)
        {
            watermelonIsNull = true;
        }

        if (watermelonIsNull && !processOver && showChatOver)
        {
            processOver = true;
            if (useGrenade)
            {
                soldier.SetActive(false);
                StartCoroutine(ShowRagdollTrigger());
            }
            else
            {
                StartCoroutine(ShowOverChatText());
            }
        }
    }

    //WatermelonTrigger/RagdollTrigger 触发此方法
    private void ArrivedArea()
    {
        if (watermelonTrigger.activeSelf == false
            && ragdollTrigger.activeSelf == false)
        {
            //其他脚本的Trigger也会触发这里
            return;
        }

        if (!watermelonIsNull)
        {
            //第一次触发
            ShowTips(5);
            watermelonTrigger.SetActive(false);
        }
        else
        {
            //第二次触发
            ragdollTrigger.SetActive(false);
            Instantiate(ragdollSoldier, new Vector3(14, 14, 8), Quaternion.identity);
            StartCoroutine(ShowOverChatText());
        }
    }

    private IEnumerator ShowStartChatText()
    {
        ShowTips(4);
        ShowChatText(16);
        yield return new WaitForSeconds(2);
        ShowChatText(17);
        yield return new WaitForSeconds(2);
        ShowChatText(18);
        showChatOver = true;
    }

    private IEnumerator ShowOverChatText()
    {
        watermelonTrigger.SetActive(false);
        HideTips();
        ShowChatText(19);
        yield return new WaitForSeconds(3);
        ShowChatText(20);
        door.transform.DORotate(new Vector3(0, -90, 0), 2);
        Over();
    }

    private IEnumerator ShowRagdollTrigger()
    {
        yield return new WaitForSeconds(2);
        ragdollTrigger.SetActive(true);
    }
}