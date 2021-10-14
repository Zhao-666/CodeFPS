using System.Collections;
using UnityEngine;

public class TrainingStage_7 : TrainingStageBase
{
    [Header("Handgun")]
    //Handgun
    public GameObject handGun;

    protected override void StartInit()
    {
        handGun.SetActive(false);
    }

    protected override void Process()
    {
        if (!hasShowTips)
        {
            StartCoroutine(ShowTwoTips());
        }

        if (handGun == null)
        {
            Over();
        }
        else if (!handGun.activeSelf)
        {
            handGun.SetActive(true);
        }
    }

    private IEnumerator ShowTwoTips()
    {
        yield return new WaitForSeconds(1);
        ShowTips(7);
        yield return new WaitForSeconds(2);
        HideTips();
        yield return new WaitForSeconds(2);
        ShowTips(8);
    }
}