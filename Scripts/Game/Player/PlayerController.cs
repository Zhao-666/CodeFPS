using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private readonly Dictionary<string, GameObject> arms = new Dictionary<string, GameObject>();
    private readonly Dictionary<string, GameObject> models = new Dictionary<string, GameObject>();

    [Header("Gun Arms")]
    // AssaultRifle Gun AK47
    public GameObject ak47;

    // Handgun Glock
    public GameObject glock;

    // G36C
    public GameObject g36c;

    [Header("Gun Models")]
    //AK47
    public GameObject ak47Model;

    //Glock
    public GameObject glockModel;

    //G36C
    public GameObject g36cModel;

    //当前枪支
    private GameObject currentGun;

    //主武器
    private string mainGunName;

    private GameObject mainGun;

    //副武器
    private string subGunName;
    private GameObject subGun;

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
    private GameObject rayGun;

    void Awake()
    {
        pickUpText.enabled = false;
        pickUpIcon.enabled = false;
        gunInfo.SetActive(false);
        arms.Add("AK47", ak47);
        arms.Add("Glock", glock);
        arms.Add("G36C", g36c);
        models.Add("AK47", ak47Model);
        models.Add("Glock", glockModel);
        models.Add("G36C", g36cModel);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currentGun == mainGun && subGun != null)
            {
                ShowGun(subGunName);
            }
            else if (currentGun == subGun && mainGun != null)
            {
                ShowGun(mainGunName);
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && rayGun != null)
        {
            PickUpWeapon();
        }

        CheckPickUpWeapon();
    }

    private void PickUpWeapon()
    {
        if (rayGun == null)
        {
            return;
        }

        GunBase gb = rayGun.GetComponent<GunBase>();

        //新拾取的枪支
        if (mainGun == null)
        {
            mainGunName = gb.gunName;
            mainGun = arms[gb.gunName];
        }
        else if (subGun == null)
        {
            subGunName = gb.gunName;
            subGun = arms[gb.gunName];
        }

        //交换枪支
        else if (currentGun == mainGun)
        {
            ChangeRayGunModel(mainGunName);
            mainGunName = gb.gunName;
            mainGun = arms[gb.gunName];
        }
        else if (currentGun == subGun)
        {
            ChangeRayGunModel(subGunName);
            subGunName = gb.gunName;
            subGun = arms[gb.gunName];
        }

        gunInfo.SetActive(true);
        ShowGun(gb.gunName);
        Destroy(rayGun);
    }

    private void CheckPickUpWeapon()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out raycastHit, 2))
        {
            if (raycastHit.collider.gameObject.CompareTag("Weapon"))
            {
                rayGun = raycastHit.collider.gameObject;
                GunBase gb = rayGun.GetComponent<GunBase>();
                pickUpText.enabled = true;
                pickUpIcon.enabled = true;
                pickUpText.text = pickUpStr + gb.gunName;
                pickUpIcon.sprite = gb.gunIcon;
            }
            else
            {
                rayGun = null;
                pickUpText.enabled = false;
                pickUpIcon.enabled = false;
            }
        }
        else
        {
            rayGun = null;
            pickUpText.enabled = false;
            pickUpIcon.enabled = false;
        }
    }

    private void ShowGun(string gunName)
    {
        HideCurrentGun();
        GameObject go = arms[gunName];
        go.SetActive(true);
        if (go.GetComponent<AutomaticGunScriptLPFP>())
        {
            go.GetComponent<AutomaticGunScriptLPFP>().Init();
        }
        else
        {
            go.GetComponent<HandgunScriptLPFP>().Init();
        }

        currentGun = go;
    }

    private void HideCurrentGun()
    {
        if (currentGun != null)
        {
            currentGun.SetActive(false);
        }
    }

    private void ChangeRayGunModel(string gunName)
    {
        Instantiate(models[gunName], rayGun.transform.position, models[gunName].transform.rotation);
    }
}