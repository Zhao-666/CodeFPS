using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscPanelController : MonoBehaviour
{
    [Header("MenuBtn")]
    [SerializeField]
    //Continue button
    private GameObject continueBtn;
    [SerializeField]
    //Options button
    private GameObject optionBtn;
    [SerializeField]
    //Quit button
    private GameObject quitBtn;

    // Start is called before the first frame update
    void Start()
    {
        continueBtn.GetComponent<Button>().onClick.AddListener(ContinueBtnOnClick);
        optionBtn.GetComponent<Button>().onClick.AddListener(OptionBtnOnClick);
        quitBtn.GetComponent<Button>().onClick.AddListener(QuitBtnOnClick);
    }

    private void ContinueBtnOnClick()
    {
        SettingPanelController.Instance.HideSettingPanel();
    }  
    
    private void OptionBtnOnClick()
    {
        SettingPanelController.Instance.ShowOptionPanel();
    }

    private void QuitBtnOnClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }
}
