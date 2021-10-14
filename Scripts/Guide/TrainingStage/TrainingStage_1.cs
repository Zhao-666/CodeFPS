using UnityEngine;

public class TrainingStage_1 : TrainingStageBase
{
    private bool arrived = false;

    [Header("ShootingTrigger")]
    //ShootingTrigger
    public GameObject shootingTrigger;

    void Start()
    {
        shootingTrigger.SetActive(false);
    }

    // Update is called once per frame
    protected override void Process()
    {
        if (!hasShowTips && Time.time - runTime > 1)
        {
            SendMessageUpwards("ShowGuideTips", 1);
            hasShowTips = true;
        }

        if (arrived)
        {
            //已到达触发点
            Over();
        }
    }

    //ShootingTrigger 触发此方法
    private void ArrivedShootingArea()
    {
        arrived = true;
    }

    public override void Run()
    {
        base.Run();
        shootingTrigger.SetActive(true);
    }
}