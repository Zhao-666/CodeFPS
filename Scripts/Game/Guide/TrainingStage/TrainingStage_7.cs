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

    protected override void BeforeRun()
    {
        StartCoroutine(ShowTwoChatText());
        handGun.SetActive(true);
    }

    protected override void Process()
    {
        if (handGun == null)
        {
            Over();
        }
    }

    private IEnumerator ShowTwoChatText()
    {
        yield return new WaitForSeconds(1);
        ShowChatText(11);
        yield return new WaitForSeconds(2);
        ShowChatText(12);
    }
}