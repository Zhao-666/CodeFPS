using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Gun Arms")]
    // AssaultRifle Gun
    public GameObject assaultRifle;
    // Handgun
    public GameObject handGun;

    private bool hasAssaultRifle;
    private bool hasHandGun;

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
    private GameObject rayWeapon;

    void Awake()
    {
        pickUpText.enabled = false;
        pickUpIcon.enabled = false;
        gunInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && hasAssaultRifle)
        {
            ShowAndHideHandgun(false);
            ShowAndHideAssaultRifle();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && hasHandGun)
        {
            ShowAndHideAssaultRifle(false);
            ShowAndHideHandgun();
        }

        if (Input.GetKeyDown(KeyCode.F) && rayWeapon != null)
        {
            PickUpWeapon();
        }

        CheckPickUpWeapon();
    }

    private void PickUpWeapon()
    {
        if (rayWeapon==null)
        {
            return;
        }
        
        if (rayWeapon.name == "Assault_Rifle")
        {
            hasAssaultRifle = true;
            gunInfo.SetActive(true);
            ShowAndHideHandgun(false);
            ShowAndHideAssaultRifle();
        }
        else if (rayWeapon.name == "Handgun")
        {
            hasHandGun = true;
            gunInfo.SetActive(true);
            ShowAndHideAssaultRifle(false);
            ShowAndHideHandgun();
        }

        Destroy(rayWeapon);
    }

    private void CheckPickUpWeapon()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 2))
        {
            if (raycastHit.collider.gameObject.CompareTag("Weapon"))
            {
                rayWeapon = raycastHit.collider.gameObject;
                GunBase gb = rayWeapon.GetComponent<GunBase>();
                pickUpText.enabled = true;
                pickUpIcon.enabled = true;
                pickUpText.text = pickUpStr + gb.gunName;
                pickUpIcon.sprite = gb.gunIcon;
            }
            else
            {
                rayWeapon = null;
                pickUpText.enabled = false;
                pickUpIcon.enabled = false;
            }
        }
        else
        {
            rayWeapon = null;
            pickUpText.enabled = false;
            pickUpIcon.enabled = false;
        }
    }

    private void ShowAndHideAssaultRifle(bool show = true)
    {
        assaultRifle.SetActive(show);
        if (show)
        {
            assaultRifle.GetComponent<AutomaticGunScriptLPFP>().Init();
        }
    }

    private void ShowAndHideHandgun(bool show = true)
    {
        handGun.SetActive(show);
        if (show)
        {
            handGun.GetComponent<HandgunScriptLPFP>().Init();
        }
    }
}