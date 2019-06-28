using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2 : MonoBehaviour {

    public Material fireMat;
    public Material earthMat;
    public Material airMat;
    public Material waterMat;
    public Material defMat;

    public GameObject elementalBall;
	public Slider manaSlider;
    public Image triggerImage;

	public float moveSpeed;
	public float baseMana = 100;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

	private float totalMana;
	private float currentMana;
    private bool readyToImbue = false;

    private GameObject Element;
    private GameObject trap;
    private Rigidbody rb;

	void Start () {
        elementalBall.SetActive(false);
        rb = GetComponent<Rigidbody>();
		totalMana = baseMana;
		currentMana = totalMana;
		manaSlider.maxValue = totalMana;
		manaSlider.minValue = 0;
	}
	
	// Update is called once per frame
	void Update () {

		moveInput = new Vector3(Input.GetAxisRaw("XHorizontal"), 0f, Input.GetAxisRaw("XVertical"));
		moveVelocity = moveInput * moveSpeed;

		//Vector3 playerDirection = Vector3.right * Input.GetAxis("XRHorizontal") + Vector3.forward * -Input.GetAxis("XRVertical");

		if (moveInput != Vector3.zero) 
		{
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (moveInput), 0.15f);
		}


		if (Input.GetAxis("MageOrb") > 0.0f && currentMana > 0)
        {
			OrbActivation ();
        }
        else
        {
			OrbDeactivation ();
        } 
            
		manaSlider.value = currentMana;

        if (readyToImbue)
        {
            if (Input.GetAxis("MageTrap") > 0.0f && trap.GetComponent<Trap>().trapIsInUse == false)
            {
                ImbueTrap();
            }
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Trap>() && col.gameObject.tag == "Untagged")
        {
            if (currentMana > 20)
            {
                triggerImage.enabled = true;
                readyToImbue = true;
                trap = col.gameObject;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<Trap>())
        {
            triggerImage.enabled = false;
            readyToImbue = false;
            trap = null;
        }
    }


    public void ActiveElement(string activeElement, GameObject target)
    {

        if (activeElement == null)
        {
            activeElement = "null";
        }

        switch (activeElement)
        {
            case "earth":
                target.tag = "Earth";
                target.GetComponent<Renderer>().material = earthMat;
                break;

            case "fire":
                target.tag = "Fire";
                target.GetComponent<Renderer>().material = fireMat;   
                break;

            case "water":
                target.tag = "Water";
                target.GetComponent<Renderer>().material = waterMat;
                break;

            case "air":
                target.tag = "Air";
                target.GetComponent<Renderer>().material = airMat;
                break;

            case "null":
                target.tag = "Untagged";
                target.GetComponent<Renderer>().material = defMat;
                break;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveVelocity;
    }

	void OrbActivation()
	{

		currentMana -= Time.deltaTime * 10;

		elementalBall.SetActive(true);

		if(Input.GetButton("XboxA"))
		{
            ActiveElement("earth", elementalBall);
		}

		if(Input.GetButton("XboxB"))
		{
            ActiveElement("fire", elementalBall);
		}

		if(Input.GetButton("XboxX"))
		{
            ActiveElement("water", elementalBall);
		}

		if(Input.GetButton("XboxY"))
		{
            ActiveElement("air", elementalBall);
		}

		if (Input.GetButtonUp("XboxA") || Input.GetButtonUp("XboxB") || Input.GetButtonUp("XboxX") || Input.GetButtonUp("XboxY"))
		{
            ActiveElement("null", elementalBall);
		}

		if (currentMana <= 0) 
		{
			OrbDeactivation ();
		}
	}

	void OrbDeactivation()
	{
		elementalBall.SetActive(false);
        ActiveElement("null", elementalBall);

		currentMana += 10 * Time.deltaTime * 10;
		if (currentMana > totalMana) {
			currentMana = totalMana;
		}
	}

    void ImbueTrap()
    {
        if(Input.GetButton("XboxA"))
        {
            ActiveElement("earth", trap);
            readyToImbue = false;
            currentMana -= 20;
        }

        if(Input.GetButton("XboxB"))
        {
            ActiveElement("fire", trap);
            readyToImbue = false;
            currentMana -= 20;
        }

        if(Input.GetButton("XboxX"))
        {
            ActiveElement("water", trap);
            readyToImbue = false;
            currentMana -= 20;
        }

        if(Input.GetButton("XboxY"))
        {
            ActiveElement("air", trap);
            readyToImbue = false;
            currentMana -= 20;
        }
    }
}
