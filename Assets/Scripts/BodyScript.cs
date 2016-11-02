using UnityEngine;
using System.Collections;

public class BodyScript : MonoBehaviour {

    public Sprite norm;
    public Sprite injured;
    public Player_Movement condition;
	// Use this for initialization
	void Start () {
        condition = GameObject.Find("Player").GetComponent<Player_Movement>();
	}
	
	// Update is called once per frame
	void Update () {
        if (condition.isInjured)
        {
            GetComponent<SpriteRenderer>().sprite = injured;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = norm;
        }
        /*if (Input.GetKey(KeyCode.Space))
        {
            if (GetComponentInParent<Player_Movement>().canJump)
            {
                GetComponent<Animator>().SetBool("Landing", true);
            }
        }*/
    }

    void disableLanding()
    {
        GetComponent<Animator>().SetBool("Landing", false);
    }

    void juuke()
    {
        GetComponentInParent<Player_Movement>().juuke();
    }
}
