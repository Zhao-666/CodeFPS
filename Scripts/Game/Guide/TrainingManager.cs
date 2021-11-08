using UnityEngine;

public class TrainingManager : GuideManagerBase
{
    protected override string StagePrefix => "TrainingStage_";

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