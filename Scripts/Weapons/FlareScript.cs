using UnityEngine;
using System.Collections;

public class FlareScript : MonoBehaviour {

	[Header("Timer")]
	//Time before the grenade explodes
	[Tooltip("Time before the grenade explodes")]
	public float grenadeTimer = 5.0f;

	[Header("Explosion Prefabs")]
	//Explosion prefab
	public Transform explosionPrefab;

	[Header("Explosion Options")]
	//Radius of the explosion
	[Tooltip("The radius of the explosion force")]
	public float radius = 25.0F;

	[Header("Throw Force")]
	[Tooltip("Minimum throw force")]
	public float minimumForce = 1500.0f;
	[Tooltip("Maximum throw force")]
	public float maximumForce = 2500.0f;
	private float throwForce;

	[Header("Audio")]
	public AudioSource impactSound;

	private void Awake () 
	{
		//Generate random throw force
		//based on min and max values
		throwForce = Random.Range
			(minimumForce, maximumForce);

		//Random rotation of the grenade
		GetComponent<Rigidbody>().AddRelativeTorque 
		   (Random.Range(500, 1500), //X Axis
			Random.Range(0,0), 		 //Y Axis
			Random.Range(0,0)  		 //Z Axis
			* Time.deltaTime * 5000);
	}

	private void Start () 
	{
		//Launch the projectile forward by adding force to it at start
		GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * throwForce);

		//Start the explosion timer
		StartCoroutine (ExplosionTimer ());
	}

	private void OnCollisionEnter (Collision collision) 
	{
		//Play the impact sound on every collision
		impactSound.Play ();
	}

	private IEnumerator ExplosionTimer () 
	{
		//Wait set amount of time
		yield return new WaitForSeconds(grenadeTimer);

		//Raycast downwards to check ground
		RaycastHit checkGround;
		if (Physics.Raycast(transform.position, Vector3.down, out checkGround, 50))
		{
			//Instantiate metal explosion prefab on ground
			Instantiate (explosionPrefab, checkGround.point, 
				Quaternion.FromToRotation (Vector3.forward, checkGround.normal)); 
		}

		//Explosion force
		Vector3 explosionPos = transform.position;
		//Use overlapshere to check for nearby colliders
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders) {
			//TODO 闪光弹效果
		}

		//Destroy the grenade object on explosion
		Destroy (gameObject);
	}
}