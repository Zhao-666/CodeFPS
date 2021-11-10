using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualCombatManager : GuideManagerBase
{
    private bool finishCombat = false;
    
    protected override string StagePrefix => "CombatStage_";
    protected override List<string> ChatTexts => CombatChatText.ChatTexts;
    protected override List<string> GuideTips => CombatTips.Tips;

    protected override int CurrentStage { get; set; } = 6;

    protected override void GuideOver()
    {
        StartCoroutine(ShowChatText());
        finishCombat = true;
    }

    private IEnumerator ShowChatText()
    {
        PublishChatText(24);
        yield return new WaitForSeconds(2);
        PublishChatText(25);
        yield return new WaitForSeconds(2);
        PublishChatText(26);
    }
}