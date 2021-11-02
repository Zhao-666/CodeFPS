using System;
using System.Collections;
using UnityEngine;

// ----- Low Poly FPS Pack Free Version -----
public class TargetScript : MonoBehaviour
{
    private bool isDown;

    public bool isHit;
    
    //设为false则不会倒下，需要手动调用Down()
    public bool isWillDown = true;

    [Header("Auto Up")]
    //Auto up the target.
    public bool autoUp = false;
    
    [Header("Audio")]
    //Set the audios
    public AudioClip upSound;
    public AudioClip downSound;

    public AudioSource audioSource;

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

    public void Down(bool silent = false)
    {
        isDown = true;
        //Animate the target "down"
        gameObject.GetComponent<Animation>().Play("target_down");
        
        if (!silent)
        {
            //Set the downSound as current sound, and play it
            audioSource.GetComponent<AudioSource>().clip = downSound;
            audioSource.Play();
        }

        if (autoUp)
        {
            StartCoroutine(AutoUpTarget());
        }
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

    private IEnumerator AutoUpTarget()
    {
        yield return new WaitForSeconds(2);
        if (isDown)
        {
            Up();
        }
    }
}
// ----- Low Poly FPS Pack Free Version -----