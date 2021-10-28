using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermelon : MonoBehaviour
{
    private float explosionRadius = 1;
    private float explosionForce = 40;
    
    [Header("Prefab")] [SerializeField]
    // The destroyed watermelon
    private GameObject destroyedWatermelon;
    
    public void Explode () {

        //Spawn the destroyed watermelon prefab
        Instantiate (destroyedWatermelon, transform.position, 
            transform.rotation); 

        //Explosion force
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders) {
            Rigidbody rb = hit.GetComponent<Rigidbody> ();
			
            //Add force to nearby rigidbodies
            if (rb != null)
                rb.AddExplosionForce (explosionForce * 50, explosionPos, explosionRadius);

        }

        //Destroy the current barrel object
        Destroy (gameObject);
    }
}
