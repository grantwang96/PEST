using UnityEngine;
using System.Collections;

public class MerpSpawner : MonoBehaviour {
    public GameObject merple;
    private int timer;
    private bool facingRight = true;
    private bool active = true;
	// Use this for initialization
	void Start () {
        timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer >= 300)
        {
            GameObject merp = Instantiate(merple);
            merp.transform.position = new Vector3(transform.position.x + 1f * 
                transform.localScale.x, transform.position.y, transform.position.z);
            merp.GetComponent<BasicMovement>().cliffyes = false;
            if (facingRight)
            {
                merp.GetComponent<BasicMovement>().Flip();
            }
            merp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(1, 20)*merp.transform.localScale.x, Random.Range(0, 5)),ForceMode2D.Impulse);
            merp.GetComponent<BasicMovement>().dumb = true;
            timer = 0;
        }
        if (active) { timer++; }
        else { timer = 0; }
	}

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            active = false;
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            active = true;
        }
    }
}
