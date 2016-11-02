using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryMonster : MonoBehaviour {
    public Player_Attacks info;
    public Sprite[] monsters = new Sprite[4];
	// Use this for initialization
	void Start () {
        GetComponent<Image>().sprite = monsters[3];
	}
	
	// Update is called once per frame
	void Update () {

        

        if (info.inventory.Count > 0)
        {
            if ((string)info.inventory[info.currentMonster - 1] == "Basic")
            {
                GetComponent<Image>().sprite = monsters[0];
            }
            else if ((string)info.inventory[info.currentMonster - 1] == "Jelly")
            {
                GetComponent<Image>().sprite = monsters[1];
            }
            else if ((string)info.inventory[info.currentMonster - 1] == "Shooty")
            {
                GetComponent<Image>().sprite = monsters[2];
            }
        }
        else
        {
            GetComponent<Image>().sprite = monsters[3];
        }
	}
}
