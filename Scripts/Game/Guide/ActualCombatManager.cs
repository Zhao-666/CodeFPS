using System.Collections.Generic;

public class ActualCombatManager : GuideManagerBase
{
    protected override string StagePrefix => "CombatStage_";
    protected override List<string> ChatTexts => CombatChatText.ChatTexts;
    protected override List<string> GuideTips => CombatTips.Tips;
}