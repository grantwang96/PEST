using UnityEngine;
using System.Collections;

public class BasicMovement : MonoBehaviour {

    public int health = 2;
    public float speed;
    public bool facingRight;
    private int delayFlip;
    private int initflip;
    public bool canFlip = false;
    public bool cliffyes = true;
    public bool dead;
    public AudioClip[] sounds = new AudioClip[3];
    // Use this for initialization
    void Start () {
        facingRight = true;
        speed = 5;
        delayFlip = 0;
        initflip = Random.Range(240, 300);
        dead = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (cliffyes == false) { delayFlip++; }
        if (delayFlip == initflip) { cliffyes = true; }
        if (health <= 0 && !dead)
        {
            death();
        }
        if (!dead)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Floor")) { canFlip = true; }
        if ((coll.gameObject.CompareTag("Basic Enemy") || coll.gameObject.CompareTag("Shooty Enemy")) && this.transform.CompareTag("Basic Friend"))
        {
            health--;
            playNoise();
            if(coll.gameObject.CompareTag("Basic Enemy")) { coll.gameObject.GetComponent<BasicMovement>().health = 0; }
            else { coll.gameObject.GetComponent<ShootyMovement>().death(); }
        }
        else if (coll.gameObject.CompareTag("Basic Enemy") || coll.gameObject.CompareTag("Shooty Enemy") || coll.gameObject.CompareTag("Shooty Friend")) { Flip(); }
        else if(coll.gameObject.CompareTag("Basic Friend") || coll.gameObject.CompareTag("Shooty Friend") && this.transform.CompareTag("Basic Friend")) { Flip(); }
        if(coll.gameObject.CompareTag("Player") && this.transform.CompareTag("Basic Friend") && canFlip) { Flip(); }
        if(coll.gameObject.name=="Edge of Insanity") { Destroy(this.gameObject); }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.CompareTag("Cliff") || coll.gameObject.CompareTag("Wall")) && canFlip && cliffyes) { Flip(); }
    }
    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void playNoise()
    {
        AudioClip playnoise = sounds[(int)Random.Range(0, 2)];
        GetComponent<AudioSource>().clip = playnoise;
        GetComponent<AudioSource>().Play();
    }
    void kill()
    {
        Destroy(this.gameObject);
    }
    public void death()
    {
        Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        dead = true;
        GetComponent<BoxCollider2D>().enabled = false;
        //rbody.isKinematic = true;
        rbody.constraints = RigidbodyConstraints2D.FreezeAll;
        if (this.transform.CompareTag("Basic Friend"))
        {
            Destroy(transform.FindChild("Floor").gameObject);
        }
        GetComponent<Animator>().Play("Dead");
    }
}
