using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

// ----- Low Poly FPS Pack Free Version -----
public class HandgunScriptLPFP : GunScriptBase {

	//Animator component attached to weapon
	Animator anim;
	
	[Header("Custom Gun Part")]
	//Use to adaptation custom gun.
	[SerializeField]
	private Transform carga;
	private Transform cargaOriginParent;
	private Vector3 cargaOriginPosition;
	private Vector3 cargaOriginRotation;
	[SerializeField]
	private Transform slider;
	[SerializeField]
	private Vector3 sliderMovePosition;
	private Vector3 sliderOriginPosition;
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

	public float aimFov = 15.0f;

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

	[Header("Weapon Settings")]

	public float sliderBackTimer = 1.58f;
	private bool hasStartedSliderBack;

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
	public float bulletForce = 400;
	[Tooltip("How long after reloading that the bullet model becomes visible " +
		"again, only used for out of ammo reload aniamtions.")]
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
		//Casing spawn point array
		public Transform casingSpawnPoint;
		//Bullet prefab spawn from this point
		public Transform bulletSpawnPoint;
		//Grenade prefab spawn from this point
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
	
	public override void Init () {
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
		sightBead.GetComponent<SightBeadPanelController>().Init(60,150);

		StartCoroutine(nameof(ReduceRotateBase));
	}
	
	private void Awake () 
	{
		//Set the animator component
		anim = GetComponent<Animator>();
		//Set current ammo to total ammo value
		currentAmmo = ammo;

		muzzleflashLight.enabled = false;
		originBulletSpawnPointRotation = Spawnpoints.bulletSpawnPoint.transform.localRotation;
		
				
		//弹匣对象不为null
		if (carga != null)
		{
			cargaOriginParent = carga.parent;
			cargaOriginPosition = carga.transform.localPosition;
			cargaOriginRotation = carga.transform.localRotation.eulerAngles;
		}

		//滑块不为null
		if (slider != null)
		{
			sliderOriginPosition = slider.transform.localPosition;
		}
	}
	
	private void OnDisable()
	{
		holstered = false;
		hasBeenHolstered = false;
		CargaReset();
		sightBead.SetActive(false);
		StopCoroutine(nameof(ReduceRotateBase));

		if (slider != null)
		{
			//自制手枪需要重置此参数切枪回来才会进行回位
			hasStartedSliderBack = false;
		}
	}

	private void LateUpdate () {
		//Weapon sway
		// 开镜状态下晃动体验不好
		if (weaponSway == true && isAiming == false){
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
		if(Input.GetButton("Fire2") && !isReloading && !isRunning && !isInspecting && !holstered
			&& Cursor.lockState == CursorLockMode.Locked) 
		{
			gunCamera.fieldOfView = Mathf.Lerp (gunCamera.fieldOfView,
				aimFov, fovSpeed * Time.deltaTime);
			
			isAiming = true;

			anim.SetBool ("Aim", true);

			if (!soundHasPlayed) 
			{
				mainAudioSource.clip = SoundClips.aimSound;
				mainAudioSource.Play ();
	
				soundHasPlayed = true;
			}
			
			//开镜状态提高子弹精度
			Spawnpoints.bulletSpawnPoint.localRotation = originBulletSpawnPointRotation;
			
			//开镜状态将枪支复位  应该抽象出一个BaseClass来实现，可是我懒，一共就几把枪
			if (weaponName == "USP")
			{
				transform.localPosition = new Vector3(0,-0.008f,0.1f);	
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
	
			anim.SetBool ("Aim", false);
		}
		if (holstered)
		{
			//隐藏准星
			sightBead.GetComponent<CanvasGroup>().DOFade(0, 0.2f);	
		}
		else
		{
			//显示准星
			sightBead.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
		}
		//Aiming end

		//If randomize muzzleflash is true, genereate random int values
		if (randomMuzzleflash == true) {
			randomMuzzleflashValue = Random.Range (minRandomValue, maxRandomValue);
		}

		//Timescale settings
		//Change timescale to normal when 1 key is pressed
		// if (Input.GetKeyDown (KeyCode.Alpha1)) 
		// {
		// 	Time.timeScale = 1.0f;
		// 	timescaleText.text = "1.0";
		// }
		// //Change timescale to 50% when 2 key is pressed
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
				StartCoroutine (AutoReload ());
			}
				
			//Set slider back
			anim.SetBool ("Out Of Ammo Slider", true);
			//Increase layer weight for blending to slider back pose
			anim.SetLayerWeight (1, 1.0f);

			//自制枪支模拟空弹动画
			if (slider != null)
			{
				slider.transform.DOLocalMove(sliderMovePosition, 0.1f);
			}
		} 
		else 
		{
			//When ammo is full, show weapon name again
			currentWeaponText.text = storedWeaponName.ToString ();
			//Toggle bool
			outOfAmmo = false;
			//anim.SetBool ("Out Of Ammo", false);
			anim.SetLayerWeight (1, 0.0f);
		}

		//Shooting 
		if (Input.GetMouseButtonDown (0) && !outOfAmmo && !isReloading && !isInspecting && !isRunning
		    && !holstered && Cursor.lockState == CursorLockMode.Locked) 
		{
			if (slider != null)
			{
				StartCoroutine(SliderMoveBack());
			}
			anim.Play ("Fire", 0, 0f);
	
			muzzleParticles.Emit (1);
				
			//Remove 1 bullet from ammo
			currentAmmo -= 1;

			shootAudioSource.clip = SoundClips.shootSound;
			shootAudioSource.Play ();

			//Light flash start
			StartCoroutine(MuzzleFlashLight());

			if (!isAiming) //if not aiming
			{
				anim.Play ("Fire", 0, 0f);
		
				muzzleParticles.Emit (1);

				if (enableSparks == true) 
				{
					//Emit random amount of spark particles
					sparkParticles.Emit (Random.Range (1, 6));
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
							sparkParticles.Emit (Random.Range (1, 6));
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
			//Spawn bullet at bullet spawnpoint
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
			
			//Expand the sight bead
			sightBead.GetComponent<SightBeadPanelController>().Expand();
		}

		//Inspect weapon when pressing T key
		if (Input.GetKeyDown (KeyCode.T)) 
		{
			anim.SetTrigger ("Inspect");
		}

		//Toggle weapon holster when pressing E key
		if (Input.GetKeyDown (KeyCode.E) && !isEventHolster) 
		{
			if (hasBeenHolstered)
			{
				Ready();	
			}
			else
			{
				Holster();
			}
		}

		//Reload 
		if (Input.GetKeyDown (KeyCode.R) && !isReloading && !isInspecting) 
		{
			if (currentAmmo != ammo)
			{
				//Reload
				Reload ();	
			}

			if (!hasStartedSliderBack) 
			{
				hasStartedSliderBack = true;
				StartCoroutine (HandgunSliderBackDelay());
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
		if (isRunning == true) {
			anim.SetBool ("Run", true);
		} else {
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

	private IEnumerator HandgunSliderBackDelay () {
		//Wait set amount of time
		yield return new WaitForSeconds (sliderBackTimer);
		//Set slider back
		anim.SetBool ("Out Of Ammo Slider", false);
		//Increase layer weight for blending to slider back pose
		anim.SetLayerWeight (1, 0.0f);
		
		//自制枪支模拟空弹动画
		if (slider != null)
		{
			slider.transform.DOLocalMove(sliderOriginPosition, 0.3f);
		}

		hasStartedSliderBack = false;
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

		if (!hasStartedSliderBack) 
		{
			hasStartedSliderBack = true;

			StartCoroutine (HandgunSliderBackDelay());
		}
		//Wait for set amount of time
		yield return new WaitForSeconds (autoReloadDelay);

		if (outOfAmmo == true)
		{
			TouchCarga();
			//Play diff anim if out of ammo
			anim.Play ("Reload Out Of Ammo", 0, 0f);

			mainAudioSource.clip = SoundClips.reloadSoundOutOfAmmo;
			mainAudioSource.Play ();

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
		
		TouchCarga();
		if (outOfAmmo == true) 
		{
			//Play diff anim if out of ammo
			anim.Play ("Reload Out Of Ammo", 0, 0f);

			mainAudioSource.clip = SoundClips.reloadSoundOutOfAmmo;
			mainAudioSource.Play ();

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

			mainAudioSource.clip = SoundClips.reloadSoundAmmoLeft;
			mainAudioSource.Play ();

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
	private IEnumerator MuzzleFlashLight () 
	{
		muzzleflashLight.enabled = true;
		yield return new WaitForSeconds (lightDuration);
		muzzleflashLight.enabled = false;
	}

	//Check current animation playing
	private void AnimationCheck () 
	{
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
	 * Let the bullet position random.
	 */
	private void RandomBulletSpawnPoint()
	{
		int rotateBase = 1 + (bulletSpawnRotateBase * 1);
		int randX = Random.Range(-1 * rotateBase, rotateBase);
		int randY = Random.Range(-1 * rotateBase, rotateBase);
		Vector3 pos = new Vector3(randX,randY,0);
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
	
	/**
	 * Knife attack animation call this function
	 */
	private void KnifeAttack()
	{
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out var raycastHit))
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
	 * 在MainAudioSource上播放音乐
	 */
	private void PlayAudioOnMainAudioSource(AudioClip audioClip, float time = 0)
	{
		mainAudioSource.clip = audioClip;
		mainAudioSource.time = time;
		mainAudioSource.Play();
	}
	
	/**
	 * Call this function when reload animation was finish. 
	 */
	private void ReloadFinish()
	{
		//Restore ammo when reloading
		//Restore ammo when reloading
		if (!outOfAmmo)
		{
			//没有打光子弹的情况下加多一发
			currentAmmo = ammo + 1;	
		}
		else
		{
			currentAmmo = ammo;
			outOfAmmo = false;	
		}
		CargaReset();
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

	/**
	 * Reload finish then reset the slider. 
	 */
	private IEnumerator SliderMoveBack()
	{
		slider.transform.DOLocalMove(sliderMovePosition, 0.1f);
		yield return new WaitForSeconds(0.1f);
		slider.transform.DOLocalMove(sliderOriginPosition, 0.1f);
	}
	
	/**
	 * Reset the carga to gun.
	 */
	private void CargaReset()
	{
		if (carga != null)
		{
			//弹匣归位
			carga.SetParent(cargaOriginParent);
			carga.transform.localPosition = cargaOriginPosition;
			carga.transform.localRotation = Quaternion.Euler(cargaOriginRotation);
		}
	}

	/**
	 * Holster the gun.
	 */
	protected override void Holster()
	{
		holstered = true;
		PlayAudioOnMainAudioSource(SoundClips.holsterSound);
		hasBeenHolstered = true;
		anim.SetBool ("Holster", true);
	}

	/**
	 * Set ready the gun.
	 */
	protected override void Ready()
	{
		holstered = false;
		PlayAudioOnMainAudioSource(SoundClips.takeOutSound);
		hasBeenHolstered = false;
		anim.SetBool ("Holster", false);
	}
}
// ----- Low Poly FPS Pack Free Version -----