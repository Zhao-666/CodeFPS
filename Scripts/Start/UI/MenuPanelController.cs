using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MenuPanelController : MonoBehaviour
{
    public static MenuPanelController Instance;
    
    [Header("PanelParent")]
    //The panel that use to place another panel;
    public GameObject panelParent;

    [Header("UI Panel")]
    //OptionPanel
    public GameObject optionPanel;
    //LoadingPanel
    public GameObject loadingPanel;
    //ProducerPanel
    public GameObject producerPanel;

    [Header("MenuBtn")]
    //Start button
    public GameObject startBtn;
    //Options button
    public GameObject optionBtn;
    //Producer button
    public GameObject producerBtn;
    //Quit button
    public GameObject quitBtn;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        startBtn.GetComponent<Button>().onClick.AddListener(StartBtnOnClick);
        optionBtn.GetComponent<Button>().onClick.AddListener(OptionBtnOnClick);
        producerBtn.GetComponent<Button>().onClick.AddListener(ProducerBtnOnClick);
    }

    /**
     * When any panel close, call this function
     */
    public void ShowMenu()
    {
        canvasGroup.DOFade(1, 0.5f);
    }

    private void HideMenu()
    {
        canvasGroup.DOFade(0, 0.2f);
    }
    
    private void StartBtnOnClick()
    {
        Instantiate(loadingPanel, panelParent.transform);
        HideMenu();
        StartCoroutine(StartGame());
    }

    private void OptionBtnOnClick()
    {
        HideMenu();
        Instantiate(optionPanel, panelParent.transform);
    }
    private void ProducerBtnOnClick()
    {
        HideMenu();
        Instantiate(producerPanel, panelParent.transform);
    }

    private IEnumerator StartGame()
    {
        int loadingValue = 0;
        LoadingPanelController.Instance.ShowAndHideProgressBar(true);
        while (loadingValue < 100)
        {
            loadingValue += Random.Range(0, 5);
            LoadingPanelController.Instance.SetProgressBarValue(loadingValue);
            yield return new WaitForSeconds(0.1f);
        }
        LoadingPanelController.Instance.ShowAndHideProgressBar(false);
    }
}