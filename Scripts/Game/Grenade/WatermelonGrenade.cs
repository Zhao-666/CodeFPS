using UnityEngine;

public class WatermelonGrenade : MonoBehaviour
{
    private float radius = 25.0F;
    private float power = 1500.0F;

    [Header("Explosion Prefabs")] [SerializeField]
    //Explosion prefab
    private Transform explosionPrefab;

    public void Explosion()
    {
        //Raycast downwards to check ground
        RaycastHit checkGround;
        if (Physics.Raycast(transform.position, Vector3.down, out checkGround, 50))
        {
            //Instantiate metal explosion prefab on ground
            Instantiate(explosionPrefab, checkGround.point,
                Quaternion.FromToRotation(Vector3.forward, checkGround.normal));
        }

        //Explosion force
        Vector3 explosionPos = transform.position;
        //Use overlapshere to check for nearby colliders
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            //Add force to nearby rigidbodies
            if (rb != null)
                rb.AddExplosionForce(power * 5, explosionPos, radius, 3.0F);

            //If the explosion hits "Target" tag and isHit is false
            if (hit.GetComponent<Collider>().tag == "Target"
                && hit.gameObject.GetComponent<TargetScript>().isHit == false)
            {
                //Animate the target 
                hit.gameObject.GetComponent<Animation>().Play("target_down");
                //Toggle "isHit" on target object
                hit.gameObject.GetComponent<TargetScript>().isHit = true;
            }

            //If the explosion hits "ExplosiveBarrel" tag
            if (hit.GetComponent<Collider>().tag == "ExplosiveBarrel")
            {
                //Toggle "explode" on explosive barrel object
                hit.gameObject.GetComponent<ExplosiveBarrelScript>().explode = true;
            }
        }

        //Destroy the grenade object on explosion
        Destroy(gameObject);
    }
}