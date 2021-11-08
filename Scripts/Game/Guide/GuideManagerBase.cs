using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class GuideManagerBase : MonoBehaviour
{
    protected abstract string StagePrefix { get; }
    protected int currentStage = 0;
    protected int runningStage = -1;
    protected int finalStage = 10;

    [Header("StageObject")]
    //StageObject
    public GameObject stageObject;

    private readonly Dictionary<string, GuideStageBase> guideStageBases
        = new Dictionary<string, GuideStageBase>();

    [Header("GuideTips")]
    //Guide Tips
    public GameObject guideTips;

    private Text guideText;
    
    /**
     * Start初始化
     */
    protected virtual void StartInit()
    {
    }

    /**
     * 流程结束
     */
    protected virtual void GuideOver()
    {
    }

    // Start is called before the first frame update
    private void Start()
    {
        guideTips.GetComponent<CanvasGroup>().DOFade(0, 0);
        guideText = guideTips.transform.Find("GuideText").GetComponent<Text>();
        SetGuideStageScript();
        StartInit();
    }

    // Update is called once per frame
    private void Update()
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
            GuideStageBase tsb;
            guideStageBases.TryGetValue(StagePrefix + currentStage, out tsb);
            if (tsb != null)
            {
                Debug.Log("Run " + StagePrefix + currentStage);
                tsb.Run();
            }
            else
            {
                Debug.Log(StagePrefix + currentStage + " script can't found.");
                currentStage++;
            }

            if (currentStage > finalStage)
            {
                GuideOver();
            }
        }
    }

    /**
     * 调用此方法进入下一阶段
     */
    protected void NextStage()
    {
        currentStage++;
        HideGuideTips();
    }

    /**
     * 阶段脚本调用
     */
    protected void ShowGuideTips(int index)
    {
        guideText.text = TrainingTips.Tips[index];
        guideTips.GetComponent<CanvasGroup>().DOFade(1, 1);
    }

    protected void HideGuideTips()
    {
        guideTips.GetComponent<CanvasGroup>().DOFade(0, 1);
    }

    /**
     * Stage call this function.
     */
    protected void PublishChatText(int index)
    {
        ChatPanelController.Instance.PublishText(TrainingChatText.ChatText[index]);
    }

    /**
     * 获取所有阶段脚本
     */
    private void SetGuideStageScript()
    {
        GuideStageBase[] tsb = stageObject.GetComponents<GuideStageBase>();
        if (tsb != null)
        {
            foreach (GuideStageBase trainingStageBase in tsb)
            {
                guideStageBases.Add(trainingStageBase.GetType().Name, trainingStageBase);
            }
        }
    }
}