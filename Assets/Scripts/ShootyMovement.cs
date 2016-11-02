using UnityEngine;
using System.Collections;

public class ShootyMovement : MonoBehaviour {
    public Transform player;
    public int timer;
    public bool facingRight;
    public GameObject bull;
    public GameObject friendbull;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<Transform>();
        facingRight = true;
        timer = (int)Random.Range(0, 60);
	}
	
	// Update is called once per frame
	void Update () {
        float xdif = transform.position.x - player.position.x;
        if(this.CompareTag("Shooty Enemy"))
        {
            if (xdif >= 0) { if (facingRight) { Flip(); } }
            else { if (!facingRight) { Flip(); } }
        }
        if(this.CompareTag("Shooty Friend"))
        {
            if (timer >= 60)
            {
                GameObject bullet = Instantiate(friendbull) as GameObject;
                GetComponent<AudioSource>().Play();
                bullet.transform.position = new Vector3(transform.position.x + 2f * transform.localScale.x, transform.position.y, transform.position.z);
                bullet.transform.localScale = new Vector3(transform.localScale.x, bullet.transform.localScale.y, bullet.transform.localScale.z);
                timer = 0;
            }
        }
        else
        {
            if (timer >= 120)
            {
                GameObject bullet = Instantiate(bull) as GameObject;
                GetComponent<AudioSource>().Play();
                bullet.transform.position = new Vector3(transform.position.x + 2f * transform.localScale.x, transform.position.y, transform.position.z);
                bullet.transform.localScale = new Vector3(transform.localScale.x, bullet.transform.localScale.y, bullet.transform.localScale.z);
                timer = 0;
            }
        }
        timer++;
	}

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
