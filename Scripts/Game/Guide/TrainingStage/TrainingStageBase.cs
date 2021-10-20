using UnityEngine;

public abstract class TrainingStageBase : MonoBehaviour
{
    protected bool hasShowTips = false;
    private bool isRunning = false;
    protected float runTime;

    /**
     * 当前阶段逻辑
     */
    protected abstract void Process();

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

    /**
     * 阶段运行前调用此方法
     */
    protected virtual void BeforeRun()
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

    public void Run()
    {
        BeforeRun();
        runTime = Time.time;
        isRunning = true;
    }

    protected void Over()
    {
        isRunning = false;
        SendMessageUpwards("NextStage");
        Destroy(this);
    }

    protected void ShowTips(int index)
    {
        SendMessageUpwards("ShowGuideTips", index);
        hasShowTips = true;
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