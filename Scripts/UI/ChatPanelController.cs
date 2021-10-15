using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChatPanelController : MonoBehaviour
{
    public static ChatPanelController Instance { get; private set; }

    [Header("ChatTextArea")]
    //Top chat text area
    public Text topText;
    //Bottom chat text area
    public Text bottomText;

    private Coroutine cleanCoroutine;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        topText.text = "";
        bottomText.text = "";
    }

    public void PublishText(string text)
    {
        if (bottomText.text != "")
        {
            topText.text = bottomText.text;
        }

        bottomText.text = text;
        if (cleanCoroutine != null)
        {
            StopCoroutine(cleanCoroutine);
        }

        cleanCoroutine = StartCoroutine(CleanChatText());
    }

    private IEnumerator CleanChatText()
    {
        yield return new WaitForSeconds(2);
        topText.text = "";
        yield return new WaitForSeconds(4);
        bottomText.text = "";
    }
}