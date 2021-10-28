using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GuideManager : MonoBehaviour
{
    private const string TrainingStagePrefix = "TrainingStage_";
    private int currentStage = 0;
    private int runningStage = -1;
    private int finalStage = 10;

    [Header("TrainingStageManager")]
    //TrainingStageManager
    public GameObject trainingStageManager;

    [Header("TimeTrainingManager")]
    //TimeTrainingManager
    public GameObject timeTrainingManager;

    private readonly Dictionary<string, TrainingStageBase> trainingStageBases
        = new Dictionary<string, TrainingStageBase>();

    [Header("GuideTips")]
    //Guide Tips
    public GameObject guideTips;

    private Text guideText;

    // Start is called before the first frame update
    void Start()
    {
        guideTips.GetComponent<CanvasGroup>().DOFade(0, 0);
        guideText = guideTips.transform.Find("GuideText").GetComponent<Text>();
        timeTrainingManager.SetActive(false);
        SetTrainingStageScript();
    }

    // Update is called once per frame
    void Update()
    {
        CheckStage();
    }

    /**
     * 检测流程阶段
     */
    private void CheckStage()
    {
        if (currentStage <= finalStage && currentStage != runningStage)
        {
            runningStage = currentStage;
            //阶段参考《流程规划-训练场流程规划》
            TrainingStageBase tsb;
            trainingStageBases.TryGetValue(TrainingStagePrefix + currentStage, out tsb);
            if (tsb != null)
            {
                Debug.Log("Run " + TrainingStagePrefix + currentStage);
                tsb.Run();
            }
            else
            {
                Debug.Log(TrainingStagePrefix + currentStage + " script can't found.");
                currentStage++;
            }
            
            if (currentStage > finalStage)
            {
                timeTrainingManager.SetActive(true);
                ShowGuideTips(GuideTips.Tips.Count - 1);
                Invoke(nameof(HideGuideTips),3);
            }
        }
    }

    /**
     * 调用此方法进入下一阶段
     */
    private void NextStage()
    {
        currentStage++;
        HideGuideTips();
    }

    /**
     * 阶段脚本调用
     */
    private void ShowGuideTips(int index)
    {
        guideText.text = GuideTips.Tips[index];
        guideTips.GetComponent<CanvasGroup>().DOFade(1, 1);
    }

    private void HideGuideTips()
    {
        guideTips.GetComponent<CanvasGroup>().DOFade(0, 1);
    }

    private void PublishChatText(int index)
    {
        ChatPanelController.Instance.PublishText(GuideChatText.ChatText[index]);
    }

    /**
     * 获取所有阶段脚本
     */
    private void SetTrainingStageScript()
    {
        TrainingStageBase[] tsb = trainingStageManager.GetComponents<TrainingStageBase>();
        if (tsb != null)
        {
            foreach (TrainingStageBase trainingStageBase in tsb)
            {
                trainingStageBases.Add(trainingStageBase.GetType().Name, trainingStageBase);
            }
        }
    }
}