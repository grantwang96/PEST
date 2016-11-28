using UnityEngine;
using System.Collections;

public class BasicMovement : MonoBehaviour {

    public int health;
    public float speed;
    public bool facingRight;
    public int delayFlip;
    public int initflip;
    public bool canFlip = false;
    public bool cliffyes = true;
    public bool dead;
    public bool dumb = false;
    public AudioClip[] sounds = new AudioClip[3];
    private bool hurt = false;
    // Use this for initialization
    void Start () {
        facingRight = true;
        speed = 5;
        delayFlip = 0;
        initflip = Random.Range(150, 200);
        dead = false;
        health = 2;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.FindChild("Hurt").GetComponent<ParticleSystem>().isPlaying)
        {
            transform.FindChild("Hurt").GetComponent<ParticleSystem>().Stop();
            hurt = false;
        }
        if (cliffyes == false) { delayFlip++; }
        if (delayFlip >= initflip) { cliffyes = true; }
        if (health <= 0 && !dead) { death(); }
        if (!dead) { transform.Translate(Vector2.right * speed * Time.deltaTime); }
        if (hurt) { transform.FindChild("Hurt").GetComponent<ParticleSystem>().Play(); }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.CompareTag("Floor") && !canFlip) { canFlip = true; }
        if(this.gameObject.CompareTag("Basic Friend"))
        {
            if ((coll.gameObject.CompareTag("Basic Enemy") || coll.gameObject.CompareTag("Shooty Enemy")
            || coll.gameObject.CompareTag("BigMonster")))
            {
                playNoise();
                if (coll.gameObject.CompareTag("Basic Enemy")) { coll.gameObject.GetComponent<BasicMovement>().health = 0; }
                else if(coll.gameObject.CompareTag("Shooty Enemy")) { coll.gameObject.GetComponent<ShootyMovement>().death(); }
                else if (coll.gameObject.CompareTag("BigMonster")) { coll.gameObject.GetComponent<BigMonster>().health -= 2; }
                if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dead"))
                {
                    health--;
                    hurt = true;
                }
            }
            if(coll.gameObject.CompareTag("Basic Friend") || coll.gameObject.CompareTag("Shooty Enemy")
               || coll.gameObject.CompareTag("Shooty Friend")) { Flip(); }
            if (coll.gameObject.CompareTag("Player") && canFlip) { Flip(); }
        }
        else if (this.gameObject.CompareTag("Basic Enemy"))
        {
            if(coll.gameObject.CompareTag("Basic Enemy") || coll.gameObject.CompareTag("Shooty Enemy")
               || coll.gameObject.CompareTag("Shooty Friend") || coll.gameObject.CompareTag("BigMonster")) { Flip(); }
        }
        if(coll.gameObject.name =="Edge of Insanity") { Destroy(this.gameObject); }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.CompareTag("Wall")) && canFlip && cliffyes) { Flip(); }
        if(coll.gameObject.CompareTag("Cliff") && canFlip && cliffyes && !dumb) { Flip(); }
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
        AudioClip playnoise = sounds[(int)Random.Range(0, 3)];
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
