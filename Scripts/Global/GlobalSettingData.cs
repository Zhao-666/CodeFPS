using System;
using UnityEngine;
using UnityEngine.Events;

public class GlobalSettingData : MonoBehaviour
{
    private event UnityAction GlobalEvent;
    
    public static GlobalSettingData Instance { get; private set; }

    [Header("FPS Limit")] [SerializeField]
    // The FPS limit of this game
    public int fps = 60;

    [Header("Look Settings")]
    //鼠标灵敏度，临时解决方案，后期需要改成保存到本地
    public float mouseSensitivity = 7f;

    void Awake()
    {
        Instance = this;
        Application.targetFrameRate = fps;
    }

    /**
     * 注册事件
     */
    public void RegisterGlobalEvent(UnityAction action)
    {
        GlobalEvent += action;
    }

    /**
     * 提交更改，调用所有事件
     */
    public void SubmitGlobalSetting()
    {
        GlobalEvent?.Invoke();
    }
    
    public void SetApplicationFrameRate(int value)
    {
        fps = value;
        Application.targetFrameRate = fps;
    }

    public void SetMouseSensitivity(float value)
    {
        mouseSensitivity = value;
    }

}