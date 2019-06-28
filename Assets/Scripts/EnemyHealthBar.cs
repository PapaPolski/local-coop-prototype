using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour {

    Quaternion startRotation;

	// Use this for initialization
	void Start () {
        startRotation = this.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.rotation = startRotation;

	}
}
