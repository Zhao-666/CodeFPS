using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Gun Arms")]
    // AssaultRifle Gun
    public GameObject assaultRifle;

    private bool haveAssaultRifle = false;

    [Header("Main Camera")]
    //Main camera
    public Camera mainCamera;

    [Header("UI Components")]
    //UI Components
    public Text pickUpText;
    public Image pickUpIcon;
    public GameObject gunInfo;
    
    private string pickUpStr = "按下 <color=orange>F</color> 拾取 ";

    private Ray ray;
    private RaycastHit raycastHit;
    private GameObject pickUpWeapon;

    void Awake()
    {
        pickUpText.enabled = false;
        pickUpIcon.enabled = false;
        gunInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && haveAssaultRifle)
        {
            ShowAssaultRifle();
        }

        if (Input.GetKeyDown(KeyCode.F) && pickUpWeapon != null)
        {
            if (pickUpWeapon.name == "Assault_Rifle")
            {
                haveAssaultRifle = true;
                gunInfo.SetActive(true);
                ShowAssaultRifle();
            }

            Destroy(pickUpWeapon);
        }

        CheckPickUpWeapon();
    }

    private void CheckPickUpWeapon()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 2))
        {
            if (raycastHit.collider.gameObject.CompareTag("Weapon"))
            {
                pickUpWeapon = raycastHit.collider.gameObject;
                GunBase gb = pickUpWeapon.GetComponent<GunBase>();
                pickUpText.enabled = true;
                pickUpIcon.enabled = true;
                pickUpText.text = pickUpStr + gb.gunName;
                pickUpIcon.sprite = gb.gunIcon;
            }
            else
            {
                pickUpWeapon = null;
                pickUpText.enabled = false;
                pickUpIcon.enabled = false;
            }
        }
        else
        {
            pickUpWeapon = null;
            pickUpText.enabled = false;
            pickUpIcon.enabled = false;
        }
    }

    private void ShowAssaultRifle()
    {
        assaultRifle.SetActive(true);
        assaultRifle.GetComponent<AutomaticGunScriptLPFP>().Init();
    }
}