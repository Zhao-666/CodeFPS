using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using Random = UnityEngine.Random;

// ----- Low Poly FPS Pack Free Version -----
public class AutomaticGunScriptLPFP : MonoBehaviour {

	//Animator component attached to weapon
	Animator anim;

	[Header("Arms")]
	//Arm Transform
	public Transform arms;
	
	[Header("Custom Gun Part")]
	//Use to adaptation custom gun.
	[SerializeField]
	private Transform carga;
	private Transform cargaOriginParent;
	private Vector3 cargaOriginPosition;
	[SerializeField]
	private Transform leftHand;

	[Header("Main Camera")]
	//Main gun camera
	public Camera mainCamera;

	[Header("Gun Camera")]
	//Main gun camera
	public Camera gunCamera;

	[Header("Sight bead")] [SerializeField]
	//Sight bead in the canvas
	private GameObject sightBead;

	[Header("Gun Camera Options")]
	//How fast the camera field of view changes when aiming 
	[Tooltip("How fast the camera field of view changes when aiming.")]
	public float fovSpeed = 15.0f;
	//Default camera field of view
	[Tooltip("Default value for camera field of view (40 is recommended).")]
	public float defaultFov = 40.0f;

	public float aimFov = 25.0f;

	[Header("UI Weapon Info")]
	[Tooltip("Name of the current weapon, shown in the game UI.")]
	public string weaponName;
	public Sprite weaponIcon;
	private string storedWeaponName;

	[Header("Weapon Sway")]
	//Enables weapon sway
	[Tooltip("Toggle weapon sway.")]
	public bool weaponSway;

	public float swayAmount = 0.02f;
	public float maxSwayAmount = 0.06f;
	public float swaySmoothValue = 4.0f;

	private Vector3 initialSwayPosition;

	//Used for fire rate
	private float lastFired;
	[Header("Weapon Settings")]
	//How fast the weapon fires, higher value means faster rate of fire
	[Tooltip("How fast the weapon fires, higher value means faster rate of fire.")]
	public float fireRate;
	//Eanbles auto reloading when out of ammo
	[Tooltip("Enables auto reloading when out of ammo.")]
	public bool autoReload;
	//Delay between shooting last bullet and reloading
	public float autoReloadDelay;
	//Check if reloading
	private bool isReloading;

	//Holstering weapon
	private bool hasBeenHolstered = false;
	//If weapon is holstered
	private bool holstered;
	//Check if running
	private bool isRunning;
	//Check if aiming
	private bool isAiming;
	public bool IsAiming
	{
		get => isAiming;
	}
	
	//Check if walking
	private bool isWalking;
	//Check if inspecting weapon
	private bool isInspecting;

	//How much ammo is currently left
	private int currentAmmo;
	//Totalt amount of ammo
	[Tooltip("How much ammo the weapon should have.")]
	public int ammo;
	//Check if out of ammo
	private bool outOfAmmo;

	[Header("Bullet Settings")]
	//Bullet
	[Tooltip("How much force is applied to the bullet when shooting.")]
	public float bulletForce = 400.0f;
	[Tooltip("How long after reloading that the bullet model becomes visible " +
		"again, only used for out of ammo reload animations.")]
	public float showBulletInMagDelay = 0.6f;
	[Tooltip("The bullet model inside the mag, not used for all weapons.")]
	public SkinnedMeshRenderer bulletInMagRenderer;

	[Header("Grenade Settings")]
	public float grenadeSpawnDelay = 0.35f;

	[Header("Muzzleflash Settings")]
	public bool randomMuzzleflash = false;
	//min should always bee 1
	private int minRandomValue = 1;

	[Range(2, 25)]
	public int maxRandomValue = 5;

	private int randomMuzzleflashValue;

	public bool enableMuzzleflash = true;
	public ParticleSystem muzzleParticles;
	public bool enableSparks = true;
	public ParticleSystem sparkParticles;
	public int minSparkEmission = 1;
	public int maxSparkEmission = 7;

	[Header("Muzzleflash Light Settings")]
	public Light muzzleflashLight;
	public float lightDuration = 0.02f;

	[Header("Audio Source")]
	//Main audio source
	public AudioSource mainAudioSource;
	//Audio source used for shoot sound
	public AudioSource shootAudioSource;

	[Header("UI Components")]
	public Text timescaleText;
	public Text currentWeaponText;
	public Text currentAmmoText;
	public Text totalAmmoText;
	public Image currentWeaponIcon;

	[System.Serializable]
	public class prefabs
	{  
		[Header("Prefabs")]
		public Transform bulletPrefab;
		public Transform casingPrefab;
		public Transform grenadePrefab;
	}
	public prefabs Prefabs;
	
	[System.Serializable]
	public class spawnpoints
	{  
		[Header("Spawnpoints")]
		//Array holding casing spawn points 
		//(some weapons use more than one casing spawn)
		//Casing spawn point array
		public Transform casingSpawnPoint;
		//Bullet prefab spawn from this point
		public Transform bulletSpawnPoint;

		public Transform grenadeSpawnPoint;
	}
	public spawnpoints Spawnpoints;

	[System.Serializable]
	public class soundClips
	{
		public AudioClip shootSound;
		public AudioClip takeOutSound;
		public AudioClip holsterSound;
		public AudioClip reloadSoundOutOfAmmo;
		public AudioClip reloadSoundAmmoLeft;
		public AudioClip aimSound;
		public AudioClip cutWatermelon;
	}
	public soundClips SoundClips;

	private bool soundHasPlayed = false;

	private Quaternion originBulletSpawnPointRotation;

	private int bulletSpawnRotateBase;

	//普通状态下后坐力
	private const float NormalRecoilForce = 0.8f;
	//开镜状态下后坐力
	private const float AimingRecoilForce = 1.5f;
	
	//后坐力归位力
	private const float NormalBackupForce = 0.8f;
	//射击时后坐力归位力
	private const float ShootingBackupForce = 0.1f;
	
	//当前后坐力
	private float recoilForce = NormalRecoilForce;
	//后坐力计数
	private float recoilForceCount;

	public void Init () {
		//Save the weapon name
		storedWeaponName = weaponName;
		//Get weapon name from string to text
		currentWeaponText.text = weaponName;
		//Set total ammo text from total ammo int
		totalAmmoText.text = ammo.ToString();

		//Weapon sway
		//fixbug: 枪支晃动的情况下有可能造成永久性偏移
		initialSwayPosition = Vector3.zero;

		//Set the shoot sound to audio source
		shootAudioSource.clip = SoundClips.shootSound;
		
		//设置武器图标
		currentWeaponIcon.sprite = weaponIcon;
		
		//设置准星
		sightBead.SetActive(true);
		sightBead.GetComponent<SightBeadPanelController>().Init(35);

		if (carga != null)
		{
			cargaOriginParent = carga.parent;
			cargaOriginPosition = carga.transform.localPosition;
		}
		
		//播放上膛声音
		PlayAudioOnMainAudioSource(SoundClips.reloadSoundOutOfAmmo, 1.5f);

		StartCoroutine(nameof(ReduceRotateBase));
		StartCoroutine(nameof(ReduceRecoilForce));
	}
	
	private void Awake () {
		
		//Set the animator component
		anim = GetComponent<Animator>();
		//Set current ammo to total ammo value
		currentAmmo = ammo;

		muzzleflashLight.enabled = false;
		originBulletSpawnPointRotation = Spawnpoints.bulletSpawnPoint.transform.localRotation;
	}

	private void OnDisable()
	{
		sightBead.SetActive(false);
		StopCoroutine(nameof(ReduceRotateBase));
		StopCoroutine(nameof(ReduceRecoilForce));
	}

	private void LateUpdate () {
		
		//Weapon sway
		// 开镜状态下晃动体验不好
		if (weaponSway == true && isAiming == false) 
		{
			float movementX = Input.GetAxis ("Mouse X") * swayAmount;
			float movementY = Input.GetAxis ("Mouse Y") * swayAmount;
			//Clamp movement to min and max values
			movementX = Mathf.Clamp 
				(movementX, -maxSwayAmount, maxSwayAmount);
			movementY = Mathf.Clamp 
				(movementY, -maxSwayAmount, maxSwayAmount);
			//Lerp local pos
			Vector3 finalSwayPosition = new Vector3 
				(movementX, movementY, 0);
			transform.localPosition = Vector3.Lerp 
				(transform.localPosition, finalSwayPosition + 
					initialSwayPosition, Time.deltaTime * swaySmoothValue);
		}
	}
	
	private void Update () {

		//Aiming
		//Toggle camera FOV when right click is held down
		if(Input.GetButton("Fire2") && !isReloading && !isRunning && !isInspecting
		&& Cursor.lockState == CursorLockMode.Locked) 
		{
			isAiming = true;
			//Start aiming
			anim.SetBool ("Aim", true);

			//When right click is released
			gunCamera.fieldOfView = Mathf.Lerp(gunCamera.fieldOfView,
				aimFov,fovSpeed * Time.deltaTime);

			if (!soundHasPlayed) 
			{
				PlayAudioOnMainAudioSource(SoundClips.aimSound);
	
				soundHasPlayed = true;
			}

			//开镜状态提高子弹精度
			Spawnpoints.bulletSpawnPoint.localRotation = originBulletSpawnPointRotation;
			
			//开镜状态降低后坐力
			recoilForce = AimingRecoilForce;
			
			//开镜状态将枪支复位  应该抽象出一个BaseClass来实现，可是我懒，一共就几把枪
			if (weaponName == "G36C")
			{
				transform.localPosition = new Vector3(0,-0.035f,0.1f);	
			}
			else
			{
				transform.localPosition = Vector3.zero;
			}

			//隐藏准星
			sightBead.GetComponent<CanvasGroup>().DOFade(0, 0.2f);
		} 
		else 
		{
			//When right click is released
			gunCamera.fieldOfView = Mathf.Lerp(gunCamera.fieldOfView,
				defaultFov,fovSpeed * Time.deltaTime);

			isAiming = false;
			//Stop aiming
			anim.SetBool ("Aim", false);
				
			soundHasPlayed = false;
			recoilForce = NormalRecoilForce;
			
			//显示准星
			sightBead.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
		}
		//Aiming end

		//If randomize muzzleflash is true, genereate random int values
		if (randomMuzzleflash == true) 
		{
			randomMuzzleflashValue = Random.Range (minRandomValue, maxRandomValue);
		}

		// //Timescale settings
		// //Change timescale to normal when 1 key is pressed
		// if (Input.GetKeyDown (KeyCode.Alpha1)) 
		// {
		// 	Time.timeScale = 1.0f;
		// 	timescaleText.text = "1.0";
		// }
		// //Change timesccale to 50% when 2 key is pressed
		// if (Input.GetKeyDown (KeyCode.Alpha2)) 
		// {
		// 	Time.timeScale = 0.5f;
		// 	timescaleText.text = "0.5";
		// }
		// //Change timescale to 25% when 3 key is pressed
		// if (Input.GetKeyDown (KeyCode.Alpha3)) 
		// {
		// 	Time.timeScale = 0.25f;
		// 	timescaleText.text = "0.25";
		// }
		// //Change timescale to 10% when 4 key is pressed
		// if (Input.GetKeyDown (KeyCode.Alpha4)) 
		// {
		// 	Time.timeScale = 0.1f;
		// 	timescaleText.text = "0.1";
		// }
		// //Pause game when 5 key is pressed
		// if (Input.GetKeyDown (KeyCode.Alpha5)) 
		// {
		// 	Time.timeScale = 0.0f;
		// 	timescaleText.text = "0.0";
		// }

		//Set current ammo text from ammo int
		currentAmmoText.text = currentAmmo.ToString ();

		//Continosuly check which animation 
		//is currently playing
		AnimationCheck ();

		//Play knife attack 1 animation when Q key is pressed
		if (Input.GetKeyDown (KeyCode.Q) && !isInspecting) 
		{
			anim.Play ("Knife Attack 1", 0, 0f);
		}
		//Play knife attack 2 animation when F key is pressed
		if (Input.GetKeyDown (KeyCode.V) && !isInspecting) 
		{
			anim.Play ("Knife Attack 2", 0, 0f);
		}
			
		//Throw grenade when pressing G key
		if (Input.GetKeyDown (KeyCode.G) && !isInspecting) 
		{
			StartCoroutine (GrenadeSpawnDelay ());
			//Play grenade throw animation
			anim.Play("GrenadeThrow", 0, 0.0f);
		}

		//If out of ammo
		if (currentAmmo == 0) 
		{
			//Show out of ammo text
			currentWeaponText.text = "OUT OF AMMO";
			//Toggle bool
			outOfAmmo = true;
			//Auto reload if true
			if (autoReload == true && !isReloading) 
			{
				//Fixbug: 按住右键时自动装弹会立刻装满 
				isReloading = true;
				StartCoroutine (AutoReload ());
			}
		} 
		else 
		{
			//When ammo is full, show weapon name again
			currentWeaponText.text = storedWeaponName.ToString ();
			//Toggle bool
			outOfAmmo = false;
			//anim.SetBool ("Out Of Ammo", false);
		}
			
		//AUtomatic fire
		//Left click hold 
		if (Input.GetMouseButton (0) && !outOfAmmo && !isReloading && !isInspecting && !isRunning
		&& Cursor.lockState == CursorLockMode.Locked) 
		{
			//Shoot automatic
			if (Time.time - lastFired > 1 / fireRate) 
			{
				lastFired = Time.time;

				//Remove 1 bullet from ammo
				currentAmmo -= 1;

				shootAudioSource.clip = SoundClips.shootSound;
				shootAudioSource.Play ();

				if (!isAiming) //if not aiming
				{
					anim.Play ("Fire", 0, 0f);
					//If random muzzle is false
					if (!randomMuzzleflash && 
						enableMuzzleflash == true) 
					{
						muzzleParticles.Emit (1);
						//Light flash start
						StartCoroutine(MuzzleFlashLight());
					} 
					else if (randomMuzzleflash == true)
					{
						//Only emit if random value is 1
						if (randomMuzzleflashValue == 1) 
						{
							if (enableSparks == true) 
							{
								//Emit random amount of spark particles
								sparkParticles.Emit (Random.Range (minSparkEmission, maxSparkEmission));
							}
							if (enableMuzzleflash == true) 
							{
								muzzleParticles.Emit (1);
								//Light flash start
								StartCoroutine (MuzzleFlashLight ());
							}
						}
					}
				} 
				else //if aiming
				{
					
					anim.Play ("Aim Fire", 0, 0f);

					//If random muzzle is false
					if (!randomMuzzleflash) {
						muzzleParticles.Emit (1);
					//If random muzzle is true
					} 
					else if (randomMuzzleflash == true) 
					{
						//Only emit if random value is 1
						if (randomMuzzleflashValue == 1) 
						{
							if (enableSparks == true) 
							{
								//Emit random amount of spark particles
								sparkParticles.Emit (Random.Range (minSparkEmission, maxSparkEmission));
							}
							if (enableMuzzleflash == true) 
							{
								muzzleParticles.Emit (1);
								//Light flash start
								StartCoroutine (MuzzleFlashLight ());
							}
						}
					}
				}

				if (!isAiming)
				{
					//Let the bullet spawn random
					if (bulletSpawnRotateBase < 5)
					{
						bulletSpawnRotateBase++;	
					}
					RandomBulletSpawnPoint();	
				}
				
				//Spawn bullet from bullet spawnpoint
				var bullet = (Transform)Instantiate (
					Prefabs.bulletPrefab,
					Spawnpoints.bulletSpawnPoint.position,
					Spawnpoints.bulletSpawnPoint.rotation);

				//Add velocity to the bullet
				bullet.GetComponent<Rigidbody>().velocity = 
					bullet.transform.forward * bulletForce;
				
				//Spawn casing prefab at spawnpoint
				Instantiate (Prefabs.casingPrefab, 
					Spawnpoints.casingSpawnPoint.position, 
					Spawnpoints.casingSpawnPoint.rotation);
				
				AddRecoilForce();
				
				//Expand the sight bead
				sightBead.GetComponent<SightBeadPanelController>().Expand();
			}
		}

		//Inspect weapon when T key is pressed
		if (Input.GetKeyDown (KeyCode.T)) 
		{
			anim.SetTrigger ("Inspect");
		}

		//Toggle weapon holster when E key is pressed
		if (Input.GetKeyDown (KeyCode.E) && !hasBeenHolstered) 
		{
			holstered = true;

			PlayAudioOnMainAudioSource(SoundClips.holsterSound);

			hasBeenHolstered = true;
		} 
		else if (Input.GetKeyDown (KeyCode.E) && hasBeenHolstered) 
		{
			holstered = false;

			PlayAudioOnMainAudioSource(SoundClips.takeOutSound);

			hasBeenHolstered = false;
		}
		//Holster anim toggle
		if (holstered == true) 
		{
			anim.SetBool ("Holster", true);
		} 
		else 
		{
			anim.SetBool ("Holster", false);
		}

		//Reload 
		if (Input.GetKeyDown (KeyCode.R) && !isReloading && !isInspecting) 
		{
			if (currentAmmo != ammo)
			{
				//Reload
				Reload ();
			}
		}

		//Walking when pressing down WASD keys
		if (Input.GetKey (KeyCode.W) && !isRunning || 
			Input.GetKey (KeyCode.A) && !isRunning || 
			Input.GetKey (KeyCode.S) && !isRunning || 
			Input.GetKey (KeyCode.D) && !isRunning) 
		{
			anim.SetBool ("Walk", true);
			isWalking = true;
		} else {
			anim.SetBool ("Walk", false);
			isWalking = false;
		}

		//Running when pressing down W and Left Shift key
		if ((Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.LeftShift))) 
		{
			isRunning = true;
		} else {
			isRunning = false;
		}
		
		//Run anim toggle
		if (isRunning == true) 
		{
			anim.SetBool ("Run", true);
		} 
		else 
		{
			anim.SetBool ("Run", false);
		}

		if (isWalking || isRunning)
		{
			sightBead.GetComponent<SightBeadPanelController>().CharacterMoving();
		}
		else
		{
			sightBead.GetComponent<SightBeadPanelController>().CharacterStop();
		}
	}

	private IEnumerator GrenadeSpawnDelay () {
		
		//Wait for set amount of time before spawning grenade
		yield return new WaitForSeconds (grenadeSpawnDelay);
		//Spawn grenade prefab at spawnpoint
		Instantiate(Prefabs.grenadePrefab, 
			Spawnpoints.grenadeSpawnPoint.transform.position, 
			Spawnpoints.grenadeSpawnPoint.transform.rotation);
	}

	private IEnumerator AutoReload () {
		//Wait set amount of time
		yield return new WaitForSeconds (autoReloadDelay);

		if (outOfAmmo == true) 
		{
			//Play diff anim if out of ammo
			anim.Play ("Reload Out Of Ammo", 0, 0f);

			PlayAudioOnMainAudioSource(SoundClips.reloadSoundOutOfAmmo);

			//If out of ammo, hide the bullet renderer in the mag
			//Do not show if bullet renderer is not assigned in inspector
			if (bulletInMagRenderer != null) 
			{
				bulletInMagRenderer.GetComponent
				<SkinnedMeshRenderer> ().enabled = false;
				//Start show bullet delay
				StartCoroutine (ShowBulletInMag ());
			}
		}
	}

	//Reload
	private void Reload () {
		
		if (outOfAmmo == true) 
		{
			//Play diff anim if out of ammo
			anim.Play ("Reload Out Of Ammo", 0, 0f);

			PlayAudioOnMainAudioSource(SoundClips.reloadSoundOutOfAmmo);

			//If out of ammo, hide the bullet renderer in the mag
			//Do not show if bullet renderer is not assigned in inspector
			if (bulletInMagRenderer != null) 
			{
				bulletInMagRenderer.GetComponent
				<SkinnedMeshRenderer> ().enabled = false;
				//Start show bullet delay
				StartCoroutine (ShowBulletInMag ());
			}
		} 
		else 
		{
			//Play diff anim if ammo left
			anim.Play ("Reload Ammo Left", 0, 0f);

			PlayAudioOnMainAudioSource(SoundClips.reloadSoundAmmoLeft);

			//If reloading when ammo left, show bullet in mag
			//Do not show if bullet renderer is not assigned in inspector
			if (bulletInMagRenderer != null) 
			{
				bulletInMagRenderer.GetComponent
				<SkinnedMeshRenderer> ().enabled = true;
			}
		}
	}

	//Enable bullet in mag renderer after set amount of time
	private IEnumerator ShowBulletInMag () {
		
		//Wait set amount of time before showing bullet in mag
		yield return new WaitForSeconds (showBulletInMagDelay);
		bulletInMagRenderer.GetComponent<SkinnedMeshRenderer> ().enabled = true;
	}

	//Show light when shooting, then disable after set amount of time
	private IEnumerator MuzzleFlashLight () {
		
		muzzleflashLight.enabled = true;
		yield return new WaitForSeconds (lightDuration);
		muzzleflashLight.enabled = false;
	}

	//Check current animation playing
	private void AnimationCheck () {
		
		//Check if reloading
		//Check both animations
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Reload Out Of Ammo") || 
			anim.GetCurrentAnimatorStateInfo (0).IsName ("Reload Ammo Left")) 
		{
			isReloading = true;
		} 
		else 
		{
			isReloading = false;
		}

		//Check if inspecting weapon
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Inspect")) 
		{
			isInspecting = true;
		} 
		else 
		{
			isInspecting = false;
		}
	}

	/**
	 * 在MainAudioSource上播放音乐
	 */
	private void PlayAudioOnMainAudioSource(AudioClip audioClip, float time = 0)
	{
		mainAudioSource.clip = audioClip;
		mainAudioSource.time = time;
		mainAudioSource.Play();
	}

	/**
	 * Let the bullet position random.
	 */
	private void RandomBulletSpawnPoint()
	{
		int rotateBase = 1 + (bulletSpawnRotateBase * 1);
		int randX = Random.Range(-1 * rotateBase, rotateBase);
		int randY = Random.Range(-1 * rotateBase - 1, rotateBase);// 模型偏右，往左补偿 1
		Vector3 pos = new Vector3(randX, randY,0);
		Spawnpoints.bulletSpawnPoint.localRotation = Quaternion.Euler(pos);
	}

	private IEnumerator ReduceRotateBase()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.3f);
			if (bulletSpawnRotateBase > 0)
			{
				bulletSpawnRotateBase--;
			}
		}
	}

	private IEnumerator ReduceRecoilForce()
	{
		float backupForce;
		while (true)
		{
			yield return null;
			if (recoilForceCount >= 0)
			{
				if (Input.GetMouseButton(0))
				{
					backupForce = ShootingBackupForce;
				}
				else
				{
					backupForce = NormalBackupForce;
				}
				arms.transform.localRotation = 
					Quaternion.Euler(arms.transform.localRotation.eulerAngles +
					                 new Vector3(backupForce,0,0));
				recoilForceCount -= backupForce;
			}
		}
	}

	private void AddRecoilForce()
	{
		float yAxisForce = recoilForce / 2;
		if (recoilForceCount < recoilForce * 10)//这个值影响多少颗子弹达到最高
		{
			int rand = Random.Range(-1, 2);
			recoilForceCount += recoilForce;
			arms.transform.localRotation = 
				Quaternion.Euler(arms.transform.localRotation.eulerAngles - 
				                 new Vector3(recoilForce,yAxisForce * rand,0));
		}
	}

	/**
	 * Knife attack animation call this function
	 */
	private void KnifeAttack()
	{
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out var raycastHit,2))
		{
			GameObject go = raycastHit.collider.gameObject;
			if (go.CompareTag("WatermelonGrenade"))
			{
				go.GetComponent<WatermelonGrenade>().Explosion();
			}
			
			if (go.CompareTag("Watermelon"))
			{
				PlayAudioOnMainAudioSource(SoundClips.cutWatermelon,0.1f);
				go.GetComponent<Watermelon>().Explode();
			}
		}
	}
	
	/**
	 * Call this function when reload animation was finish. 
	 */
	private void ReloadFinish()
	{
		//Restore ammo when reloading
		currentAmmo = ammo;
		outOfAmmo = false;
		if (carga != null)
		{
			carga.SetParent(cargaOriginParent);
			carga.transform.localPosition = cargaOriginPosition;
			carga.transform.localRotation = Quaternion.identity;
		}
		// carga.transform.localPosition = Vector3.zero;
	}

	/**
	 * Call this function when reload animation touch the carga.
	 * Bind the carga and hand.
	 */
	private void TouchCarga()
	{
		if (leftHand != null && carga != null)
		{
			carga.SetParent(leftHand);
		}
	}
}
// ----- Low Poly FPS Pack Free Version -----