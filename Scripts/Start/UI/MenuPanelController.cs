using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanelController : MonoBehaviour
{
    public static MenuPanelController Instance { get; private set; }

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
    //QuitPanel
    public GameObject quitPanel;

    [Header("MenuBtn")]
    //Start button
    public GameObject startBtn;
    //Options button
    public GameObject optionBtn;
    //Producer button
    public GameObject producerBtn;
    //Quit button
    public GameObject quitBtn;

    [Header("Audio Source")]
    //Audio Source
    public AudioSource audioSource;
    
    [Header("Audio Clip")]
    //Start button click sound
    public AudioClip startBtnClickSound;
    //Normal button click sound
    public AudioClip normalBtnClickSound;

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
        quitBtn.GetComponent<Button>().onClick.AddListener(QuitBtnOnClick);
    }

    /**
     * When any panel close, call this function
     */
    public void ShowMenu()
    {
        canvasGroup.DOFade(1, 0.5f);
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

    private void HideMenu()
    {
        canvasGroup.DOFade(0, 0.2f);
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