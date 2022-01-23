using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Input
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject easyTouchControls;

        private void Start()
        {
            if (GameConfig.Environment == Env.PC)
            {
                easyTouchControls.SetActive(false);   
            }
        }

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
