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

	private Rigidbody rigidbody;
	private Vector3 velocity;
	private Ray ray;

	private void Awake()
	{
		// CheckPermeableTarget();
		rigidbody = GetComponent<Rigidbody>();
	}

	private void Start () 
	{
		//Start destroy timer
		StartCoroutine (DestroyAfter ());
		velocity = rigidbody.velocity;
	}

	private void Update()
	{
		velocity = rigidbody.velocity;
	}

	//If the bullet collides with anything
	private void OnCollisionEnter (Collision collision)
	{
		if (collision.transform.CompareTag("WoodTarget"))
		{
			Instantiate (metalImpactPrefabs [Random.Range(0, metalImpactPrefabs.Length)],
				transform.position, 
				Quaternion.LookRotation (collision.contacts [0].normal),
				collision.gameObject.transform);
			transform.Translate(new Vector3(0,0,0.1f),Space.Self);
			rigidbody.velocity = velocity;
			return;
		}

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
		if (collision.transform.CompareTag("Metal")
		|| collision.transform.CompareTag("Target")) 
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
		if (collision.transform.CompareTag("Target")) 
		{
			//Toggle "isHit" on target object
			collision.transform.gameObject.GetComponent
				<TargetScript>().isHit = true;
			//Destroy bullet object
			Destroy(gameObject);
		}
			
		//If bullet collides with "ExplosiveBarrel" tag
		if (collision.transform.CompareTag("ExplosiveBarrel")) 
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
}
// ----- Low Poly FPS Pack Free Version -----