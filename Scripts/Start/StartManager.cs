using System.Collections;
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
    //Gun list
    public GameObject[] guns;

    [Header("UI SceneMask")]
    //SceneMask
    public Image sceneMask;

    void Start()
    {
        foreach (GameObject gun in guns)
        {
            StartCoroutine(RotateGun(gun));
        }

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
        Vector3 nextPos = new Vector3(0, localPosition.y, localPosition.z);
        int nextIndex = 1;
        while (true)
        {
            yield return new WaitForSeconds(6f);
            sceneMask.DOFade(1, 1);
            yield return new WaitForSeconds(1.5f);

            //反复横跳
            if (nextIndex >= guns.Length)
            {
                nextIndex = 0;
            }

            nextPos.x = nextIndex++ * GunDistance;
            
            mainCamera.transform.localPosition = nextPos;
            sceneMask.DOFade(0, 1);
        }
    }
}