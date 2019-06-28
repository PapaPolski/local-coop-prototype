using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour {


    public Slider pHealthSlider;
    public Text pHealthText;

    public int health;
    private int maxHealth;
    private string playerNumber;

    public Slider pXPSlider;
    public Text pLvText;

    int currentXP;
    int xpToLevelUp;
    int currentLevel;

	// Use this for initialization
	
    void Start () {
		health = 250;
        maxHealth = health;
        //pHealthSlider.maxValue = maxHealth;
        if (gameObject.name == "Player1")
        {
            playerNumber = "1";
        }
        else
        {
            playerNumber = "2";
        }

        currentLevel = 1;
        currentXP = 0;
        OnLevelUp();
	}
	
	// Update is called once per frame
	void Update () {

		if (health <= 0)
			Destroy (this.gameObject);

		if (currentXP >= xpToLevelUp) {
			currentLevel++;
			OnLevelUp ();
		}
	}

    public void PTakeDamage(int damage)
    {
        health -= damage;
        HealthUpdate();
    }

    void HealthUpdate()
    {
        pHealthText.text = "Player " + playerNumber + ": " + health + "/" + maxHealth;
        pHealthSlider.value = health;
    }

    void OnLevelUp()
    {
        xpToLevelUp = (currentLevel * 100);
        pXPSlider.maxValue = xpToLevelUp;
        currentXP = 0;
        pLvText.text = "LV: " + currentLevel;
        pXPSlider.value = currentXP;
    }

    public void AddXP(int xpToAdd)
    {
        currentXP += xpToAdd;
        pXPSlider.value = currentXP;
    }
}