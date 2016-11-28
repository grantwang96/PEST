using UnityEngine;
using System.Collections;

public class ShootyMovement : MonoBehaviour {
    public Transform player;
    public int timer;
    public bool facingRight;
    public GameObject bull;
    public GameObject friendbull;
    public bool dead;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<Transform>();
        facingRight = true;
        timer = (int)Random.Range(0, 60);
        dead = false;
	}
	
	// Update is called once per frame
	void Update () {
        float xdif = transform.position.x - player.position.x;
        if (transform.FindChild("pew").GetComponent<ParticleSystem>().isPlaying)
        { transform.FindChild("pew").GetComponent<ParticleSystem>().Stop(); }
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
                transform.FindChild("pew").GetComponent<ParticleSystem>().Play();
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
                transform.FindChild("pew").GetComponent<ParticleSystem>().Play();
            }
        }
        timer++;
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Edge of Insanity") { Destroy(this.gameObject); }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name == "View Area")
        {
            coll.transform.parent.FindChild("BigMonsterBody").GetComponent<Animator>().Play("BigMonsterSwing");
        }
        if (coll.gameObject.name == "Hit Area")
        {
            Destroy(this.gameObject);
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public void death()
    {
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Animator>().Play("Dead");
    }
    void kill()
    {
        Destroy(this.gameObject);
    }
}
