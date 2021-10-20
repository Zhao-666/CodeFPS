using UnityEngine;
using UnityEngine.UI;

public class OptionPanelController : MonoBehaviour
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
        MenuPanelController.Instance.ShowMenu();
        Destroy(gameObject);
    }
}