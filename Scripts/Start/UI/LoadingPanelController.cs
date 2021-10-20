using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPanelController : MonoBehaviour
{
    public static LoadingPanelController Instance { get; private set; }

    [Header("LoadingProgressBackground")]
    // Loading progress background
    public Image progressBackground;

    [Header("LoadingProgressBar")]
    // Loading progress bar
    public Image progressBar;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        progressBar.fillAmount = 0;
    }

    /**
     * Show and hide progress bar
     */
    public void ShowAndHideProgressBar(bool isShow)
    {
        if (isShow)
        {
            progressBackground.GetComponent<CanvasGroup>().DOFade(1, 0);
        }
        else
        {
            progressBackground.GetComponent<CanvasGroup>().DOFade(0, 1);
        }
    }

    /**
     * Set progress bar value by int. Max value is 100.
     */
    public void SetProgressBarValue(int value)
    {
        if (value > 100)
        {
            value = 100;
        }

        progressBar.fillAmount = value / 100f;
    }
}