using UnityEngine;

public class TrainingStage_1 : TrainingStageBase
{
    private bool arrived = false;

    [Header("ShootingTrigger")]
    //ShootingTrigger
    public GameObject shootingTrigger;

    protected override void StartInit()
    {
        shootingTrigger.SetActive(false);
    }

    protected override void BeforeRun()
    {
        shootingTrigger.SetActive(true);
        ShowChatText(0);
    }

    protected override void Process()
    {
        if (arrived)
        {
            //已到达触发点
            Over();
        }
    }

    //ShootingTrigger 触发此方法
    private void ArrivedArea()
    {
        if (shootingTrigger.activeSelf == false)
        {
            return;
        }

        arrived = true;
        shootingTrigger.SetActive(false);
    }
}