using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public Material fireMat;
    public Material earthMat;
    public Material airMat;
    public Material waterMat;
    public Material defMat;

    public float speed;
    public float totalDamage;
    
    private float arrowActiveTime = 4.0f;

	public GameObject managers;

	// Use this for initialization
	void Awake () {

		managers = GameObject.Find ("Managers");
	}

    // Update is called once per frame
    void Update () {
        Destroy(gameObject, arrowActiveTime);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            this.tag = other.tag;
            this.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
        }
        else if (other.gameObject.layer == 9)
        {
            managers.GetComponent<Damager> ().ApplyDamage (this.tag, other.tag, other.gameObject.GetComponent<Enemy>(), totalDamage, managers.GetComponent<Damager>().CritChance());
        }
    }
}
