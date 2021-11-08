using System.Collections.Generic;
using UnityEngine;

public class TrainingManager : GuideManagerBase
{
    protected override string StagePrefix => "TrainingStage_";
    protected override List<string> ChatTexts => TrainingChatText.ChatTexts;
    protected override List<string> GuideTips => TrainingTips.Tips;

    [Header("TimeTrainingManager")]
    //TimeTrainingManager
    public GameObject timeTrainingManager;

    protected override void StartInit()
    {
        timeTrainingManager.SetActive(false);
    }

    protected override void GuideOver()
    {
        timeTrainingManager.SetActive(true);
        ShowGuideTips(TrainingTips.Tips.Count - 1);
        Invoke(nameof(HideGuideTips), 3);
    }
}