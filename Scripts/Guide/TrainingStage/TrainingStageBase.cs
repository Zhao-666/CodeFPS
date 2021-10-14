using UnityEngine;

public abstract class TrainingStageBase : MonoBehaviour
{
    protected bool hasShowTips = false;
    private bool isRunning = false;
    protected float runTime;

    protected abstract void Process();

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
}