using UnityEngine;

public abstract class GuideStageBase : MonoBehaviour
{
    protected bool hasShowTips = false;
    protected float runTime;
    protected bool overOnce; //是否结束了一次
    private bool isRunning = false;

    /**
     * 当前阶段逻辑
     */
    protected abstract void Process();

    /**
     * 阶段运行前调用此方法
     */
    protected virtual void BeforeRun()
    {
    }
    
    /**
     * 重置结束判断条件
     */
    protected virtual void ResetOverCondition()
    {
    }
    
    /**
     * Awake初始化
     */
    protected virtual void AwakeInit()
    {
    }

    /**
     * Start初始化
     */
    protected virtual void StartInit()
    {
    }
    
    private void Awake()
    {
        AwakeInit();
    }

    private void Start()
    {
        StartInit();
    }

    void Update()
    {
        if (isRunning)
        {
            Process();
        }
    }

    public void Run(bool over = false)
    {
        BeforeRun();
        runTime = Time.time;
        isRunning = true;
        overOnce = over;
    }

    protected void Over()
    {
        isRunning = false;
        SendMessageUpwards("NextStage");
        StopAllCoroutines();
        ResetOverCondition();
    }

    protected void ShowTips(int index, int delayTime = 0)
    {
        SendMessageUpwards("ShowGuideTips", index);
        hasShowTips = true;
        if (delayTime != 0)
        {
            Invoke(nameof(HideTips), delayTime);
        }
    }

    protected void HideTips()
    {
        SendMessageUpwards("HideGuideTips");
    }

    protected void ShowChatText(int index)
    {
        SendMessageUpwards("PublishChatText", index);
    }
}