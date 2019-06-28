using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Trap : Weapon {

    public float trapCooldown;
	public float trapDuration;
    public float totalDamage;
	public int maxConcurrentTraps;

    public bool trapIsInUse = false;

    public GameObject managers;

	
    void Awake()
    {
        managers = GameObject.Find("Managers");
    }

    public void OnTriggerEnter(Collider col)
	{
		if (col.GetComponent<Enemy> () && trapIsInUse == false) 
		{
			col.transform.position = this.transform.position;
			col.GetComponent<NavMeshAgent> ().speed = 0;
			StartCoroutine(DestroyTrap(col.gameObject, trapDuration));
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            trapIsInUse = true;
		}
	}

		private IEnumerator DestroyTrap(GameObject enemy, float delay)
	{
		yield return new WaitForSeconds (delay);
		enemy.GetComponent<NavMeshAgent> ().speed = 1;
        managers.GetComponent<Damager> ().ApplyDamage (this.tag, enemy.tag, enemy.gameObject.GetComponent<Enemy>(), totalDamage, managers.GetComponent<Damager>().CritChance());
        GameObject player1 = GameObject.Find("Player1");
        player1.GetComponent<BowController>().currentlyPlacedTraps--;
		Destroy(this.gameObject);
	}
}
