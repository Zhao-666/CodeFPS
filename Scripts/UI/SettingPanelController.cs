using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanelController : MonoBehaviour
{
    public static SettingPanelController Instance { get; private set; }

    [Header("MousePositionPanel")]
    //MousePositionPanel
    public GameObject mousePositionPanel;

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

    public void ShowMousePositionPanel()
    {
        UnlockMouse();
        gameObject.SetActive(true);
        currentShowPanel = Instantiate(mousePositionPanel, transform);
    }

    public void HideSettingPanel()
    {
        if (currentShowPanel != null)
        {
            Destroy(currentShowPanel);
        }

        gameObject.SetActive(false);
        LockMouse();
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