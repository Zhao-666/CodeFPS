using UnityEngine;

public abstract class GunScriptBase : MonoBehaviour
{
    protected bool isEventHolster;

    public void EventHolster(bool setHolster)
    {
        isEventHolster = setHolster;
        if (setHolster)
        {
            Holster();
        }
        else
        {
            Ready();
        }
    }

    public virtual void Init()
    {
        
    }

    protected virtual void Holster()
    {
    }

    protected virtual void Ready()
    {
        
    }

    protected void ShowAndHideArms()
    {
        
    }
}