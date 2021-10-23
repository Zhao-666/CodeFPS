using System;
using UnityEngine;
using UnityEngine.Events;

public class GlobalSettingData : MonoBehaviour
{
    public enum VSyncCount
    {
        EveryVBlank,
        EverySecondVBlank
    }
    private event UnityAction GlobalEvent;
    
    public static GlobalSettingData Instance { get; private set; }

    [Header("V Sync Count")] [SerializeField]
    // The FPS limit of this game
    public VSyncCount vSync = VSyncCount.EveryVBlank;

    [Header("Look Settings")]
    //鼠标灵敏度，临时解决方案，后期需要改成保存到本地
    public float mouseSensitivity = 7f;

    void Awake()
    {
        Instance = this;
        QualitySettings.vSyncCount = (int)vSync + 1;
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
    
    public void SetVSyncCount(VSyncCount value)
    {
        vSync = value;
        QualitySettings.vSyncCount = (int)vSync + 1;
    }

    public void SetMouseSensitivity(float value)
    {
        mouseSensitivity = value;
    }

}