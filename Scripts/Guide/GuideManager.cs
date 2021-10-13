using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GuideManager : MonoBehaviour
{
    private int currentStage = 0;
    private float checkTime;

    [Header("Gun")]
    //Gun
    public GameObject assaultRifle;
    public GameObject handGun;

    [Header("Shooting target")]
    //The target that can be shot
    public TargetScript woodTarget;
    public TargetScript topTarget;
    public TargetScript[] targetList;

    [Header("GuideTips")]
    //Guide Tips
    public GameObject guideTips;
    private Text guideText;

    [Header("ShootingTrigger")]
    //Shooting Trigger
    public GameObject shootingTrigger;

    // Start is called before the first frame update
    void Start()
    {
        guideTips.GetComponent<CanvasGroup>().DOFade(0, 0);
        guideText = guideTips.transform.Find("GuideText").GetComponent<Text>();
        handGun.SetActive(false);
        shootingTrigger.SetActive(false);
        checkTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        CheckStage();
    }

    private void CheckStage()
    {
        float currentTime = Time.time;
        //阶段参考《流程规划-训练场流程规划》
        if (currentStage == 0)
        {
            if (currentTime - checkTime > 2)
            {
                ShowGuideTips(0);
                checkTime = currentTime;
            }

            if (assaultRifle == null)
            {
                //已拾取AK47
                HideGuideTips();
                currentStage = 1;
                checkTime = currentTime;
            }
        }

        if (currentStage == 1)
        {
            if (currentTime - checkTime > 1)
            {
                ShowGuideTips(1);
                checkTime = currentTime;
            }
        }
    }

    private void PlayerArrived()
    {
        topTarget.Up();
        targetList[0].Up();
    }

    private void ShowGuideTips(int index)
    {
        guideText.text = GuideTips.Tips[index];
        guideTips.GetComponent<CanvasGroup>().DOFade(1, 1);
    }

    private void HideGuideTips()
    {
        guideTips.GetComponent<CanvasGroup>().DOFade(0, 1);
    }
}