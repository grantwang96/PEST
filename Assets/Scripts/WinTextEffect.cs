using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinTextEffect : MonoBehaviour {
    public Text winner;
    private int counter;
	// Use this for initialization
	void Start () {
        counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (counter == 3)
        {
            counter = 0;
            winner.GetComponent<GUIText>().color = new Color(Random.Range(0, 256), Random.Range(0, 256), Random.Range(0, 256));
        }
        else { counter++; }
	}
}
