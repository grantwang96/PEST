using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryScript : MonoBehaviour {

    public Player_Attacks info;
    private bool flip;
    public Image[] slots = new Image[6];
    public Sprite[] mons = new Sprite[4];

    // Use this for initialization
    void Start () {
        info = GameObject.Find("Net").GetComponent<Player_Attacks>();
        flip = false;
    }
	
	// Update is called once per frame
	void Update () {
        for(int i = 0; i<6; i++) { slots[i].GetComponent<Image>().sprite = mons[3]; }
        if (info.inventory.Count >= 6) { GameObject.Find("Max").GetComponent<Text>().enabled = true; }
        else { GameObject.Find("Max").GetComponent<Text>().enabled = false; }
        for (int i = 0; i<info.inventory.Count; i++)
        {
            string mon = (string)info.inventory[i];
            switch (mon)
            {
                case "Basic":
                    slots[i].GetComponent<Image>().sprite = mons[0];
                    break;
                case "Jelly":
                    slots[i].GetComponent<Image>().sprite = mons[1];
                    break;
                case "Shooty":
                    slots[i].GetComponent<Image>().sprite = mons[2];
                    break;
                default:
                    //slots[i].GetComponent<Image>().sprite = mons[3];
                    break;
            }
        }
        if (info.inventory.Count > 0)
        {
            for (int i = 0; i < 6; i++) { if (i != (info.currentMonster-1)) { slots[i].GetComponent<ParticleSystem>().Stop(); } }
            if (slots[info.currentMonster-1].GetComponent<ParticleSystem>().isStopped)
            {
                Debug.Log("This is selected!");
                slots[info.currentMonster-1].GetComponent<ParticleSystem>().Play();
            }
        }else
        {
            for(int i = 0; i<6; i++) { slots[i].GetComponent<ParticleSystem>().Stop(); }
        }
    }
}
