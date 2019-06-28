using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {

  

    public void ApplyDamage(string incomingElement, string enemyElement, Enemy target, float totalDamage, bool isCrit)
	{
		if (incomingElement == enemyElement) {
            if (!isCrit)
                target.TakeHealth(totalDamage * 2, isCrit);
            else
                target.TakeHealth(totalDamage * 3, isCrit);
		}

		else if (incomingElement == "Water" && enemyElement == "Fire" || incomingElement == "Earth" && enemyElement == "Water" || incomingElement == "Air" && enemyElement == "Earth" || incomingElement == "Fire" && enemyElement == "Air")
		{
            if (!isCrit)
                target.TakeDamage(totalDamage * 5, isCrit);
            else
                target.TakeDamage(totalDamage * 6, isCrit);
		}

		else
		{
            if (!isCrit)
                target.TakeDamage(totalDamage, isCrit);
            else
                target.TakeDamage(totalDamage * 1.2f, isCrit);
		}
	}

    public void DamagePlayer(GameObject target, int damage)
    {
        gameObject.GetComponent<Movement>().PTakeDamage(damage);
    }

    public bool CritChance()
    {
        
        int critRoll = Random.Range(0, 100);
        if (critRoll <= GameObject.Find("Player1").GetComponent<BowController>().equippedBow.critChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
