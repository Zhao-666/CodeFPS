using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionPanelController : MonoBehaviour
{
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
        frameRate.GetComponent<Dropdown>().value
            = (int) GlobalSettingData.Instance.vSync;
    }

    /**
     * 注册选项事件
     */
    private void SyncOptionsValue()
    {
        GlobalSettingData.Instance.SetMouseSensitivity(
            mouseSensitivity.GetComponent<Slider>().value);
        
        GlobalSettingData.Instance.SetVSyncCount(
            (GlobalSettingData.VSyncCount)frameRate.GetComponent<Dropdown>().value);
    }

    private void SubmitBtnOnClick()
    {
        //Sync options value to GlobalSettingData instance.
        SyncOptionsValue();
        
        //Call GlobalSettingData submit all changes.
        GlobalSettingData.Instance.SubmitGlobalSetting();

        Close();
    }

    private void CloseBtnOnClick()
    {
        Close();
    }

    private void Close()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Start")
        {
            //Start界面弹出菜单
            StartPanelController.Instance.ShowMenu();   
        }
        else if (currentScene == "Game")
        {
            SettingPanelController.Instance.HideSettingPanel();
        }
        Destroy(gameObject);
    }
}