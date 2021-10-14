using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MousePositionPanelController : MonoBehaviour
{
    [Header("NoBtn")]
    //NoBtn
    public GameObject noBtn;
    
    // Start is called before the first frame update
    void Awake()
    {
        noBtn.GetComponent<Button>().onClick.AddListener(NoBtnOnClick);
    }

    private void NoBtnOnClick()
    {
        SettingPanelController.Instance.HideSettingPanel();
    }
}
