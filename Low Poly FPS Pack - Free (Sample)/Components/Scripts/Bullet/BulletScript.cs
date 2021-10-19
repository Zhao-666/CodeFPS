using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

// ----- Low Poly FPS Pack Free Version -----
public class BulletScript : MonoBehaviour {

	[Range(5, 100)]
	[Tooltip("After how long time should the bullet prefab be destroyed?")]
	public float destroyAfter;
	[Tooltip("If enabled the bullet destroys on impact")]
	public bool destroyOnImpact = false;
	[Tooltip("Minimum time after impact that the bullet is destroyed")]
	public float minDestroyTime;
	[Tooltip("Maximum time after impact that the bullet is destroyed")]
	public float maxDestroyTime;

	[Header("Impact Effect Prefabs")]
	public Transform [] metalImpactPrefabs;

	private Ray ray;
	private RaycastHit[] raycastHits = new RaycastHit[3];

	private void Awake()
	{
		CheckPermeableTarget();
	}

	private void Start () 
	{
		//Start destroy timer
		StartCoroutine (DestroyAfter ());
	}

	//If the bullet collides with anything
	private void OnCollisionEnter (Collision collision) 
	{
		//If destroy on impact is false, start 
		//coroutine with random destroy timer
		if (!destroyOnImpact) 
		{
			StartCoroutine (DestroyTimer ());
		}
		//Otherwise, destroy bullet on impact
		else 
		{
			Destroy (gameObject);
		}

		//If bullet collides with "Metal" tag
		if (collision.transform.tag == "Metal"
		|| collision.transform.tag == "Target") 
		{
			//Instantiate random impact prefab from array
			Instantiate (metalImpactPrefabs [Random.Range(0, metalImpactPrefabs.Length)],
				transform.position, 
				Quaternion.LookRotation (collision.contacts [0].normal),
				collision.gameObject.transform);
			//Destroy bullet object
			Destroy(gameObject);
		}

		//If bullet collides with "Target" tag
		if (collision.transform.tag == "Target") 
		{
			//Toggle "isHit" on target object
			collision.transform.gameObject.GetComponent
				<TargetScript>().isHit = true;
			//Destroy bullet object
			Destroy(gameObject);
		}
			
		//If bullet collides with "ExplosiveBarrel" tag
		if (collision.transform.tag == "ExplosiveBarrel") 
		{
			//Toggle "explode" on explosive barrel object
			collision.transform.gameObject.GetComponent
				<ExplosiveBarrelScript>().explode = true;
			//Destroy bullet object
			Destroy(gameObject);
		}
	}

	private IEnumerator DestroyTimer () 
	{
		//Wait random time based on min and max values
		yield return new WaitForSeconds
			(Random.Range(minDestroyTime, maxDestroyTime));
		//Destroy bullet object
		Destroy(gameObject);
	}

	private IEnumerator DestroyAfter () 
	{
		//Wait for set amount of time
		yield return new WaitForSeconds (destroyAfter);
		//Destroy bullet object
		Destroy (gameObject);
	}

	private void CheckPermeableTarget()
	{
		ray = new Ray(transform.position,transform.forward);
		int hitsCount = Physics.RaycastNonAlloc(ray, raycastHits);
		if (hitsCount > 0)
		{
			foreach (RaycastHit raycastHit in raycastHits)
			{
				if (raycastHit.collider != null)
				{
					GameObject go = raycastHit.collider.gameObject;
					if (go != null && go.CompareTag("WoodTarget"))
					{
						Instantiate (metalImpactPrefabs [Random.Range(0, metalImpactPrefabs.Length)],
							raycastHit.point, Quaternion.LookRotation (raycastHit.normal),
							go.transform);
					}
				}
			}
		}
	}
}
// ----- Low Poly FPS Pack Free Version -----