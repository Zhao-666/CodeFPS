using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    Transform[] weapons;
    float rotateSpeed = 100f;

	void Start () {
        weapons = new Transform[transform.childCount];
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = transform.GetChild(i);
        }
	}
	
	void Update () {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].Rotate(Vector3.up * Time.deltaTime * rotateSpeed, Space.World);
        }
	}
}
