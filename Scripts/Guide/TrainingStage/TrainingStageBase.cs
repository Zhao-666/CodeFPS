using System;
using UnityEngine;

public abstract class TrainingStageBase : MonoBehaviour
{
    protected bool hasShowTips = false;
    private bool isRunning = false;
    protected float runTime;

    /**
     * 阶段逻辑
     */
    protected abstract void Process();

    void Awake()
    {
        AwakeInit();
    }

    void Start()
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

    public virtual void Run()
    {
        runTime = Time.time;
        isRunning = true;
    }

    protected virtual void Over()
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
    
    protected virtual void AwakeInit(){}
    protected virtual void StartInit(){}
}