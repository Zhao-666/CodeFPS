using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitPanelController : MonoBehaviour
{
    [Header("UI Button")]
    //Yes button
    public GameObject yesBtn;
    //No button
    public GameObject noBtn;
    
    // Start is called before the first frame update
    void Awake()
    {
        yesBtn.GetComponent<Button>().onClick.AddListener(YesBtnOnClick);
        noBtn.GetComponent<Button>().onClick.AddListener(NoBtnOnClick);
    }

    private void YesBtnOnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    private void NoBtnOnClick()
    {
        MenuPanelController.Instance.ShowMenu();
        Destroy(gameObject);
    }
}
