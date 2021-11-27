using UnityEngine;

public class CombatStage_2 : GuideStageBase
{
    private bool ropeUsed = false;
    private bool arrivedTrigger = false;

    [Header("Position1Trigger"), SerializeField]
    //When player slide down to the rope bottom.
    private GameObject position1Trigger;

    [Header("Rope"), SerializeField]
    //The rope on the wooden frame
    private RopeController ropeController;

    [Header("Targets")]
    //The targets on the level 2.
    [SerializeField]
    private TargetScript firstTarget;
    [SerializeField] 
    private TargetScript secondTarget;
    [SerializeField] 
    private TargetScript thirdTarget;

    [Header("Soldiers"), SerializeField]
    //The soldiers game object
    private GameObject soldiers;
    
    private Vector3 soldiersNewPos = new Vector3(-5,0,3);
    private Vector3 soldiersNewRot = new Vector3(0,-60,0);

    protected override void StartInit()
    {
        position1Trigger.SetActive(false);
        firstTarget.Down(true);
        secondTarget.Down(true);
        thirdTarget.Down(true);
    }

    protected override void BeforeRun()
    {
        position1Trigger.SetActive(true);
    }

    protected override void Process()
    {
        if (!ropeUsed && ropeController.PlayerUsed)
        {
            ropeUsed = true;
            ShowChatText(10);
            ropeController.HideBoxCollider();
            SendMessageUpwards("StartTraining");
        }
        
        if (arrivedTrigger && firstTarget.isHit && secondTarget.isHit && thirdTarget.isHit)
        {
            ShowChatText(12);
            Over();
        }
    }

    protected override void ResetOverCondition()
    {
        ropeUsed = false;
        arrivedTrigger = false;
    }

    //ropeBottomTrigger 触发此方法
    private void ArrivedArea(GameObject trigger)
    {
        if (trigger == position1Trigger)
        {
            MoveSoldiers();
            position1Trigger.SetActive(false);
            arrivedTrigger = true;
            firstTarget.Up();
            secondTarget.Up();
            thirdTarget.Up();
            ShowChatText(11);
        }
    }

    //Move the soldiers to new position.
    private void MoveSoldiers()
    {
        soldiers.transform.localPosition = soldiersNewPos;
        soldiers.transform.localRotation = Quaternion.Euler(soldiersNewRot); 
    }
}