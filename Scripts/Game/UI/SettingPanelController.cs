using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanelController : MonoBehaviour
{
    public static SettingPanelController Instance { get; private set; }

    [Header("UI Panel")]
    [SerializeField]
    //MousePositionPanel
    private GameObject mousePositionPanel;
    [SerializeField]
    //EscPanel
    private GameObject escPanel;
    [SerializeField]
    //EscPanel
    private GameObject optionPanel;

    //上一个展示的Panel，后期可改为栈
    private GameObject lastShowPanel;
    //当前展示的Panel
    private GameObject currentShowPanel;
    public GameObject CurrentShowPanel => currentShowPanel;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    /**
     * 展示选项面板
     */
    public void ShowOptionPanel()
    {
        if (currentShowPanel != null)
        {
            currentShowPanel.SetActive(false);
            lastShowPanel = currentShowPanel;
        }
        ShowSettingPanel(optionPanel);
    }

    /**
     * 展示游戏中ESC面板
     */
    public void ShowEscPanel()
    {
        //按ESC键时如果有正在显示的窗口则关闭
        if (currentShowPanel != null)
        {
            HideSettingPanel();
        }
        else
        {
            ShowSettingPanel(escPanel);
        }
    }

    /**
     * 展示鼠标移动修改面板
     */
    public void ShowMousePositionPanel()
    {
        ShowSettingPanel(mousePositionPanel);
    }
    
    public void HideSettingPanel()
    {
        if (currentShowPanel != null)
        {
            Destroy(currentShowPanel);
        }

        if (lastShowPanel != null)
        {
            //上一层面板
            lastShowPanel.SetActive(true);
            currentShowPanel = lastShowPanel;
            lastShowPanel = null;
        }
        else
        {
            gameObject.SetActive(false);
            LockMouse();
            Time.timeScale = 1;
        }
    }

    private void ShowSettingPanel(GameObject showPanel)
    {
        gameObject.SetActive(true);
        currentShowPanel = Instantiate(showPanel, transform);
        Time.timeScale = 0;
        UnlockMouse();
    }

    private void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}