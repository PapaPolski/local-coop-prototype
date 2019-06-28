using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BowController : MonoBehaviour {

    public bool isFiring;

    public Rigidbody arrow;
    public Bow equippedBow;
    public Transform firePoint;
	public Slider aimSlider;
	public Text ammoText;

	public float minFireForce = 15f;
	public float maxFireForce = 30f;
	public float maxChargeTime = 0.75f;
	public float timeBetweenShots;
    public float arrowSpeed;

	private float currentLaunchForce;
	private float chargeSpeed;
	private bool fired;
    private float currentTime;
	private int totalAmmo;
	private int currentAmmo;
	private bool isReloading;
	

	public Trap equippedTrap;
    public int currentlyPlacedTraps = 0;

	private bool trapPlaced;

	private void OnEnable()
	{
		currentLaunchForce = minFireForce;
		aimSlider.value = minFireForce;
        currentTime = 0;
		totalAmmo = equippedBow.ammoCount;
	}

	// Use this for initialization
	void Start () {
        equippedBow = GetComponent<Inventory>().weaponEQ.GetComponent<Bow>();
		equippedTrap = GetComponent<Inventory> ().trapEQ.GetComponent<Trap>();
		chargeSpeed = (maxFireForce - minFireForce) / maxChargeTime;
		currentAmmo = totalAmmo;
		isReloading = false;
	}
	
	// Update is called once per frame
	void Update()
	{
		aimSlider.value = minFireForce;
		
		if (currentAmmo > 0 && !isReloading)
		{

			if (currentLaunchForce >= maxFireForce && !fired)
			{
				currentLaunchForce = maxFireForce;
				Fire(true);
			}

			else if (Input.GetMouseButtonDown(0))
			{
				fired = false;
				currentLaunchForce = minFireForce;
			}

			else if (Input.GetMouseButton(0) && !fired)
			{
				currentLaunchForce += chargeSpeed * Time.deltaTime;
				aimSlider.value = currentLaunchForce;
			}

			else if (Input.GetMouseButtonUp(0) && !fired)
			{
				Fire(false);
			}
		}
		
		if (Input.GetMouseButtonDown(0) && currentAmmo == 0)
		{
			StartCoroutine(Reload());
		}
		
		if (Input.GetMouseButtonDown(1) && Time.time > currentTime && currentlyPlacedTraps < equippedTrap.maxConcurrentTraps)
		{
			PlaceTrap();
		}

		if (currentAmmo < totalAmmo && Input.GetKeyDown(KeyCode.R) && !isReloading)
		{
			StartCoroutine(Reload());
		}
	}

	private void Fire(bool maximumPower)
	
	{
		fired = true;

        Rigidbody newarrow = Instantiate(arrow, firePoint.position, Quaternion.Euler(0, 0, 0)) as Rigidbody;
		
		if (maximumPower)
		{
			float totalDamage = equippedBow.damageModifier * equippedBow.baseDamage * equippedBow.maxChargeDamageMod;
			newarrow.GetComponent<Arrow>().totalDamage = totalDamage;
		}
		else
		{
			float totalDamage = equippedBow.damageModifier * equippedBow.baseDamage;
			newarrow.GetComponent<Arrow>().totalDamage = totalDamage;
		}
		
		newarrow.velocity = currentLaunchForce * firePoint.forward;

			currentLaunchForce = minFireForce;
			currentAmmo--;
			UpdateAmmoText();
	}

	IEnumerator Reload()
	{
		isReloading = true;
		yield return new WaitForSeconds(equippedBow.reloadSpeed);
		isReloading = false;
		currentAmmo = totalAmmo;
		UpdateAmmoText();
		isReloading = false;
	}

	void PlaceTrap()
	{
		//Destroy (GameObject.Find(equippedTrap.name.ToString() + "(Clone)"));
		Instantiate (equippedTrap, firePoint.position, Quaternion.identity);
		currentTime = Time.time + equippedTrap.trapCooldown;
        currentlyPlacedTraps++;
	}

	void UpdateAmmoText()
	{
		ammoText.text = "Ammo: " + currentAmmo + "/" + totalAmmo;
	}
}
