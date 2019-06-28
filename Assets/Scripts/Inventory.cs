using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public GameObject weaponEQ;
	public GameObject trapEQ;

    public Canvas inventoryCanvas;
    private bool inventoryOpen;

	// Use this for initialization
	void Start () {
        inventoryOpen = false;
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.I))
            {
                InventoryActivator();
            }

        switch (inventoryOpen)
        {
            case true:
                inventoryCanvas.gameObject.SetActive(true);
                GameObject.Find("Player1").GetComponent<Player1>().canFire = false;
                break;

            case false:
                inventoryCanvas.gameObject.SetActive(false);
                GameObject.Find("Player1").GetComponent<Player1>().canFire = true;
                break;
        }
    }

    void InventoryActivator()
    {
        if (inventoryOpen)
        {
            inventoryOpen = false;      
        }
        else if (!inventoryOpen)
        {
            inventoryOpen = true;
        }
    }
}
