using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanelController : MonoBehaviour
{
    public static StartPanelController Instance { get; private set; }

    [Header("PanelParent")] [SerializeField]
    //The panel that use to place another panel;
    private GameObject panelParent;

    [Header("UI Panel")] [SerializeField]
    //OptionPanel
    private GameObject optionPanel;

    [SerializeField]
    //LoadingPanel
    private GameObject loadingPanel;

    [SerializeField]
    //ProducerPanel
    private GameObject producerPanel;

    [SerializeField]
    //QuitPanel
    private GameObject quitPanel;

    [Header("MenuBtn")] [SerializeField]
    //Start button
    private GameObject startBtn;

    [SerializeField]
    //Options button
    private GameObject optionBtn;

    [SerializeField]
    //Producer button
    private GameObject producerBtn;

    [SerializeField]
    //Quit button
    private GameObject quitBtn;

    [Header("Audio Source")] [SerializeField]
    //Audio Source
    private AudioSource audioSource;

    [Header("Audio Clip")] [SerializeField]
    //Start button click sound
    private AudioClip startBtnClickSound;

    [SerializeField]
    //Normal button click sound
    private AudioClip normalBtnClickSound;

    [Header("Menu CanvasGroup")] [SerializeField]
    //Menu canvas group
    private CanvasGroup menuCanvasGroup;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        startBtn.GetComponent<Button>().onClick.AddListener(StartBtnOnClick);
        optionBtn.GetComponent<Button>().onClick.AddListener(OptionBtnOnClick);
        producerBtn.GetComponent<Button>().onClick.AddListener(ProducerBtnOnClick);
        quitBtn.GetComponent<Button>().onClick.AddListener(QuitBtnOnClick);
        ShowMenu();
    }

    /**
     * When any panel close, call this function
     */
    public void ShowMenu()
    {
        menuCanvasGroup.DOFade(1, 0.5f);
    }

    private void HideMenu()
    {
        menuCanvasGroup.DOFade(0, 0.2f);
    }

    private void ButtonClick(AudioClip clip = null)
    {
        if (clip == null)
        {
            clip = normalBtnClickSound;
        }

        audioSource.clip = clip;
        audioSource.Play();
        HideMenu();
    }

    private void StartBtnOnClick()
    {
        ButtonClick(startBtnClickSound);
        Instantiate(loadingPanel, panelParent.transform);
        StartCoroutine(StartGame());
    }

    private void OptionBtnOnClick()
    {
        ButtonClick();
        Instantiate(optionPanel, panelParent.transform);
    }

    private void ProducerBtnOnClick()
    {
        ButtonClick();
        Instantiate(producerPanel, panelParent.transform);
    }

    private void QuitBtnOnClick()
    {
        ButtonClick();
        Instantiate(quitPanel, panelParent.transform);
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
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
    }
}