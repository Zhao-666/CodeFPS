using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Gun Arms")]
    // AssaultRifle Gun
    public GameObject assaultRifle;
    // Handgun
    public GameObject handGun;
    //当前枪支
    private GameObject currentGun;

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
            ShowAssaultRifle();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && hasHandGun)
        {
            ShowHandgun();
        }

        if (Input.GetKeyDown(KeyCode.F) && rayWeapon != null)
        {
            PickUpWeapon();
        }

        CheckPickUpWeapon();
    }

    private void PickUpWeapon()
    {
        if (rayWeapon == null)
        {
            return;
        }

        if (rayWeapon.name == "Assault_Rifle")
        {
            hasAssaultRifle = true;
            gunInfo.SetActive(true);
            ShowAssaultRifle();
        }
        else if (rayWeapon.name == "Handgun")
        {
            hasHandGun = true;
            gunInfo.SetActive(true);
            ShowHandgun();
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

    private void ShowAssaultRifle()
    {
        HideCurrentGun();
        assaultRifle.SetActive(true);
        assaultRifle.GetComponent<AutomaticGunScriptLPFP>().Init();
        currentGun = assaultRifle;
    }

    private void ShowHandgun()
    {
        HideCurrentGun();
        handGun.SetActive(true);
        handGun.GetComponent<HandgunScriptLPFP>().Init();
        currentGun = handGun;
    }

    private void HideCurrentGun()
    {
        if (currentGun != null)
        {
            currentGun.SetActive(false);
        }
    }
}