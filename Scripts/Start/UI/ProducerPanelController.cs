using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProducerPanelController : MonoBehaviour
{
    [Header("Close button")]
    //Close button
    public GameObject closeBtn;
    
    // Start is called before the first frame update
    void Awake()
    {
        closeBtn.GetComponent<Button>().onClick.AddListener(CloseBtnOnClick);
    }

    private void CloseBtnOnClick()
    {
        MenuPanelController.Instance.ShowMenu();
        Destroy(gameObject);
    }
}
