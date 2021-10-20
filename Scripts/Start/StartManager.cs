using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    private const int GunDistance = 2;

    [Header("MainCamera")]
    //Main Camera
    public GameObject mainCamera;

    [Header("Guns")]
    //Assault Rifle Gun
    public GameObject assaultRifle;

    //Handgun
    public GameObject handGun;

    [Header("UI SceneMask")]
    //SceneMask
    public Image sceneMask;

    void Start()
    {
        StartCoroutine(RotateGun(assaultRifle));
        StartCoroutine(RotateGun(handGun));
        StartCoroutine(SceneChange());
    }

    private IEnumerator RotateGun(GameObject gun)
    {
        while (true)
        {
            gun.transform.DORotate(new Vector3(0, 30, 0), 5);
            yield return new WaitForSeconds(5f);
            gun.transform.DORotate(new Vector3(0, -60, 0), 5);
            yield return new WaitForSeconds(5f);
        }
    }

    private IEnumerator SceneChange()
    {
        var localPosition = mainCamera.transform.localPosition;
        Vector3 nextPos = new Vector3(GunDistance, localPosition.y, localPosition.z);
        while (true)
        {
            yield return new WaitForSeconds(6f);
            sceneMask.DOFade(1, 1);
            yield return new WaitForSeconds(1.5f);
            mainCamera.transform.localPosition = nextPos;
            sceneMask.DOFade(0, 1);
            //反复横跳
            nextPos.x = Math.Abs(nextPos.x - GunDistance) < 0.1f ? 0 : GunDistance;
        }
    }
}