using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionPanelController : MonoBehaviour
{
    private event UnityAction OptionEvents;
    
    [Header("UI Button")]
    [SerializeField]
    private Button submitBtn;
    [SerializeField]
    private Button closeBtn;

    [Header("UI Options")]
    [SerializeField]
    // Control the mouse sensitivity
    private GameObject mouseSensitivity;
    [SerializeField]
    // Control the FPS
    private GameObject frameRate;

    // Start is called before the first frame update
    void Awake()
    {
        submitBtn.onClick.AddListener(SubmitBtnOnClick);
        closeBtn.onClick.AddListener(CloseBtnOnClick);
        InitOptionsValue();
    }

    /**
     * 
     */
    private void InitOptionsValue()
    {
        mouseSensitivity.GetComponent<Slider>().value 
            = GlobalSettingData.Instance.mouseSensitivity;

        frameRate.GetComponent<InputField>().text 
            = GlobalSettingData.Instance.fps.ToString();
    }

    /**
     * 注册选项事件
     */
    private void SyncOptionsValue()
    {
        GlobalSettingData.Instance.SetMouseSensitivity(
            mouseSensitivity.GetComponent<Slider>().value);
        
        GlobalSettingData.Instance.SetApplicationFrameRate(
                Int32.Parse(frameRate.GetComponent<InputField>().text));
    }

    private void SubmitBtnOnClick()
    {
        //Sync options value to GlobalSettingData instance.
        SyncOptionsValue();
        
        //Call GlobalSettingData submit all changes.
        GlobalSettingData.Instance.SubmitGlobalSetting();
        
        MenuPanelController.Instance.ShowMenu();
        Destroy(gameObject);
    }

    private void CloseBtnOnClick()
    {
        MenuPanelController.Instance.ShowMenu();
        Destroy(gameObject);
    }

}