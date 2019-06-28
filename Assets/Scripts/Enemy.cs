using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

	public Material fireMat;
	public Material earthMat;
	public Material airMat;
	public Material waterMat;

    public Canvas healthCanvas;
    public Slider enemyHealthSlider;
    public GameObject damageText;

    private float maxHealth;
    private string[] elementsList = { "Fire", "Earth", "Air", "Water" };

    public float health;
    private string enemyElement;

    NavMeshAgent nav;
    GameObject target;
    GameObject levelManager;

	void Awake()
	{
        health = 100;
        maxHealth = health;
        levelManager = GameObject.FindGameObjectWithTag("Manager");
        enemyElement = elementsList[Mathf.FloorToInt(Random.Range(0, 4))];

        this.tag = enemyElement;

        switch (gameObject.tag)
        {
            case "Fire":
                gameObject.GetComponent<Renderer>().material = fireMat;
                break;

            case "Earth":
                gameObject.GetComponent<Renderer>().material = earthMat;
                break;

            case "Water":
                gameObject.GetComponent<Renderer>().material = waterMat;
                break;

            case "Air":
                gameObject.GetComponent<Renderer>().material = airMat;
                break;
        }
        nav = GetComponent<NavMeshAgent>();
	}

	// Use this for initialization
	void Start () {
        enemyHealthSlider.maxValue = health;
        enemyHealthSlider.value = health;
	}

	void Update ()
	{

        if (levelManager.GetComponent<LevelManager>().playersAlive == true)
        {
            nav.SetDestination(PickTarget(target).gameObject.transform.position);
        }
        else
        {
            Destroy(gameObject);
        }
	}


    GameObject PickTarget(GameObject closest)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        closest = null;

        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject gO in players)
        {
            Vector3 diff = gO.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = gO;
                distance = curDistance;
            }
        }
        return closest;
    }

    public void TakeDamage(float damage, bool isCrit)
	{
		health -= damage;
        enemyHealthSlider.value -= damage;
        CombatText(damage, isCrit, true);

        if (health <= 0)
        {
            Death();
        }
	}

    public void TakeHealth(float healed, bool isCrit)
	{
		health += healed;
        enemyHealthSlider.value += healed;
        CombatText(healed, isCrit, false);
        if (health >= maxHealth)
            health = maxHealth;
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<Damager>().DamagePlayer(other.gameObject, 50);
        }
    }

    void CombatText(float damageNum, bool isCrit, bool damaged)
    {
        GameObject dmgTex = Instantiate(damageText) as GameObject;
        //RectTransform rctTransform = dmgTex.GetComponent<RectTransform>();
        if (!isCrit)
            dmgTex.GetComponent<Animator>().SetTrigger("Hit");
        else
            dmgTex.GetComponent <Animator>().SetTrigger("Crit");
        dmgTex.transform.SetParent(transform.Find("EnemyCanvas"));
        dmgTex.transform.localPosition = damageText.transform.localPosition;
        dmgTex.transform.localScale = damageText.transform.localScale;
        dmgTex.transform.localRotation = damageText.transform.localRotation;
        if (!damaged)
            dmgTex.GetComponent<Text>().text = "+ " + damageNum;
        else
            dmgTex.GetComponent<Text>().text = "- " + damageNum;
        Destroy(dmgTex.gameObject, 1.5f);
    }

    void Death()
    {
        levelManager.GetComponent<LevelManager>().OnEnemyDeath();
        Destroy(this.gameObject);
    }
}
