using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour {

    public bool isEmpty;
    public GameObject itemInSlot;
    public Sprite emptySprite;
    public Sprite equippedSprite;
    public Transform tooltipPanel;

    private Vector3 tooltipOffset;

    private Text itemName;
    private Text itemDescription;

    private bool tooltipEnabled = false;

    static GameObject hotObject = null;
    GameObject tempSlot;


    void Start()
    {
        tooltipOffset = new Vector3(5, 5, 0);
        itemName = tooltipPanel.GetChild(0).GetComponent<Text>();
        itemDescription = tooltipPanel.GetChild(1).GetComponent<Text>();
    }

    void Update()
    {
        if (itemInSlot == null)
        {
            isEmpty = true;
            gameObject.GetComponent<Image>().sprite = emptySprite;
        }
        else
        {
            isEmpty = false;
            gameObject.GetComponent<Image>().sprite = itemInSlot.GetComponent<GameItem>().icon;
        }

        if (gameObject == hotObject && tooltipEnabled)
        {
            tooltipPanel.transform.position = Input.mousePosition + tooltipOffset;
        }



    }

    public void MouseEntered()
    {
        hotObject = gameObject;
        tooltipEnabled = true;
        if (!isEmpty)
        {
            tooltipPanel.gameObject.SetActive(true);
           
            itemName.text = itemInSlot.GetComponent<GameItem>().itemName.ToString();
            itemDescription.text = itemInSlot.GetComponent<GameItem>().description.ToString();
        }
        else
        {
            tooltipPanel.gameObject.SetActive(false);
        }
    }

    public void MouseExited()
    {
        hotObject = null;
        tooltipEnabled = false;
        tooltipPanel.gameObject.SetActive(false);
    }

    public void LeftClickToEquip()
    {
        string equipmentTag = itemInSlot.GetComponent<GameItem>().eqslot.ToString();
        GameObject targetSlot = GameObject.FindGameObjectWithTag(equipmentTag);

        if (targetSlot.GetComponent<Slot>().itemInSlot == null)
        {
            targetSlot.GetComponent<Slot>().itemInSlot = itemInSlot;
            itemInSlot = null;
        }
        else
        {
            tempSlot = targetSlot.GetComponent<Slot>().itemInSlot;
            targetSlot.GetComponent<Slot>().itemInSlot = itemInSlot;
            itemInSlot = tempSlot;
            tempSlot = null;
        }
    }
}
