using UnityEngine;

namespace Game.Input
{
    public class InputManager : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if (UnityEngine.Input.GetKeyUp(KeyCode.Escape))
            {
                SettingPanelController.Instance.ShowEscPanel();
            }
        }
    }
}
