using UnityEngine;
using System.Collections;

public class BigMonster : MonoBehaviour {
    public int health;
    public bool facingRight = true;
    public Animator anim;
    public bool moving = true;
    public Sprite[] healthbars = new Sprite[11];
    public bool hurt;
    public int hurtframes;
    private Color initColor;
    public SpriteRenderer BigMonsterBodySprite;
    public bool dead;
	// Use this for initialization
	void Start () {
        BigMonsterBodySprite = transform.FindChild("BigMonsterBody").gameObject.GetComponent<SpriteRenderer>();
        initColor = BigMonsterBodySprite.color;
        hurtframes = 0;
        hurt = false;
        health = 10;
        int startingNum = (int)Random.Range(1, 2);
        if(startingNum == 1)
        {
            transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
            facingRight = false;
        }
        anim = transform.FindChild("BigMonsterBody").GetComponent<Animator>();
        BigMonsterBodySprite.color = initColor;
        dead = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BigMonsterWalk") && moving == false)
        {
            moving = true;
        }
        if (health < 0)
        {
            health = 0;
        }
        transform.FindChild("BigMonsterHealth").GetComponent<SpriteRenderer>().sprite = healthbars[health];
        if (health <= 0 && !dead)
        {
            dead = true;
            moving = false;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<Rigidbody2D>().mass = 0;
            transform.FindChild("View Area").gameObject.SetActive(false);
            transform.FindChild("BigMonsterHealth").gameObject.SetActive(false);
            transform.FindChild("BigMonsterBody").GetComponent<Animator>().Play("Dead");
            Debug.Log("Killed Big Enemy!");
        }
        if (moving) { transform.Translate(Vector2.right * 2 * Time.deltaTime); }
	}
    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("FriendBullet"))
        {
            health--;
            Destroy(coll.gameObject);
            getHurt();
        }
        if (coll.gameObject.CompareTag("Heal") || coll.gameObject.CompareTag("Cliff") && anim.GetCurrentAnimatorStateInfo(0).IsName("BigMonsterWalk"))
        {
            Flip();
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Basic Friend"))
        {
            health -= 2;
            coll.gameObject.GetComponent<BasicMovement>().health--;
            coll.gameObject.GetComponent<BasicMovement>().Flip();
            getHurt();
        }
        if (coll.gameObject.CompareTag("Wall") || coll.gameObject.CompareTag("Basic Enemy")
            || coll.gameObject.CompareTag("Shooty Enemy") || coll.gameObject.CompareTag("Jelly Enemy")
            || coll.gameObject.CompareTag("Jelly Friend") && moving)
        { Flip(); }
    }
    void stopMovement()
    {
        moving = false;
    }
    void enableHitArea()
    {
        this.transform.FindChild("Hit Area").gameObject.SetActive(true);
        
    }
    void disableHitArea()
    {
        this.transform.FindChild("Hit Area").gameObject.SetActive(false);
        moving = true;
    }
    void getHurt()
    {
        hurt = true;
        hurtframes = 0;
        Debug.Log("He got hurt!");
        BigMonsterBodySprite.color = new Color(1f, 0, 0);
    }
}
