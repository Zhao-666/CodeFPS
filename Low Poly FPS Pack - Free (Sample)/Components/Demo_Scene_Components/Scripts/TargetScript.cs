using System;
using UnityEngine;

// ----- Low Poly FPS Pack Free Version -----
public class TargetScript : MonoBehaviour
{
    private bool isDown;

    public bool isHit;
    
    //设为false则不会倒下，需要手动调用Down()
    public bool isWillDown = true;

    [Header("Audio")]
    //Set the audios
    public AudioClip upSound;
    public AudioClip downSound;

    public AudioSource audioSource;

    private void Awake()
    {
        // isHit = true;
    }

    private void Update()
    {
        //If the target is hit
        if (isHit == true)
        {
            if (isWillDown && isDown == false)
            {
                Down();
            }
        }
    }

    public void Down()
    {
        isDown = true;
        //Animate the target "down"
        gameObject.GetComponent<Animation>().Play("target_down");

        //Set the downSound as current sound, and play it
        audioSource.GetComponent<AudioSource>().clip = downSound;
        audioSource.Play();
    }

    public void Up()
    {
        //Animate the target "up" 
        gameObject.GetComponent<Animation>().Play("target_up");

        //Set the upSound as current sound, and play it
        audioSource.GetComponent<AudioSource>().clip = upSound;
        audioSource.Play();

        //Target is no longer hit
        isHit = false;
        isDown = false;
    }
}
// ----- Low Poly FPS Pack Free Version -----