using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightBeadPanelController : MonoBehaviour
{
    private const int PerNarrow = 4;

    [Header("UI Line")] [SerializeField]
    //The UI sight bead line
    private GameObject[] lines;

    //最大扩张量
    private int maxExpand = 120;

    //每次扩张量
    private int perExpand = 50;

    //默认坐标
    private int defaultPos;

    //Current line expand value
    private int currentPos;

    void OnEnable()
    {
        StartCoroutine(nameof(Narrow));
    }

    void OnDisable()
    {
        StopCoroutine(nameof(Narrow));
    }

    /**
     * 不同的枪准星不一样大
     */
    public void Init(int defaultPosValue, int maxExpandValue = 120)
    {
        defaultPos = defaultPosValue;
        maxExpand = maxExpandValue;
        currentPos = defaultPos;
        SetSightBeadLines();
    }

    /**
     * When the gun shooting has to call this function.
     */
    public void Expand()
    {
        if (currentPos > maxExpand)
        {
            return;
        }

        currentPos += perExpand;
        SetSightBeadLines();
    }

    /**
     * When the character moving call this function
     */
    public void CharacterMoving()
    {
        StopCoroutine(nameof(Narrow));
        StartCoroutine(nameof(ExpandToMax));
    }

    /**
     * When the character stop move call this function.
     */
    public void CharacterStop()
    {
        StopCoroutine(nameof(ExpandToMax));
        StartCoroutine(nameof(Narrow));
    }

    /**
     * Set all of the lines position
     */
    private void SetSightBeadLines()
    {
        foreach (GameObject line in lines)
        {
            Vector3 nextPos = new Vector3();
            Vector3 pos = line.GetComponent<RectTransform>().localPosition;
            nextPos.x = CalcCurrentPosValue(pos.x);
            nextPos.y = CalcCurrentPosValue(pos.y);
            line.GetComponent<RectTransform>().localPosition = nextPos;
        }
    }

    /**
     * Calc the current position value.
     */
    private int CalcCurrentPosValue(float axis)
    {
        int result = 0;
        if (Math.Abs(axis) > 0.1)
        {
            result = currentPos;
            if (axis < 0)
            {
                result *= -1;
            }
        }

        return result;
    }

    /**
     * Narrow the sight bead 
     */
    private IEnumerator Narrow()
    {
        while (true)
        {
            yield return null;
            if (currentPos > defaultPos)
            {
                currentPos -= PerNarrow;
                SetSightBeadLines();
            }
        }
    }

    /**
     * Expand the sight bead to max value.
     */
    private IEnumerator ExpandToMax()
    {
        while (currentPos<maxExpand)
        {
            yield return null;
            currentPos += PerNarrow;
            SetSightBeadLines();
        }
    }
}