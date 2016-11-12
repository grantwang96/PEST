using UnityEngine;
using System.Collections;

public class BigMonster : MonoBehaviour {
    public int health;
    public bool facingRight = true;
    public Animator anim;
    public bool moving = true;
	// Use this for initialization
	void Start () {
        health = 10;
        int startingNum = (int)Random.Range(1, 2);
        if(startingNum == 1)
        {
            transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
            facingRight = false;
        }
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (moving) { transform.Translate(Vector2.right * 2 * Time.deltaTime); }
        transform.FindChild("BigMonsterHealth").transform.localScale = new Vector3((float)health, 1, 1);
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
        if (coll.gameObject.CompareTag("Cliff") || coll.gameObject.CompareTag("Wall")
            || coll.gameObject.CompareTag("Basic Enemy") || coll.gameObject.CompareTag("Shooty Enemy")
            || coll.gameObject.CompareTag("Jelly Enemy") || coll.gameObject.CompareTag("Jelly Friend"))
        { Flip(); }
        if (coll.gameObject.CompareTag("Player"))
        {
            anim.Play("BigMonsterCelebrate");
        }
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
}
