using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour {

    public float Speed;
    public float tilt;
    public Rigidbody2D playerbody;
    public bool canJump;
    public bool jumped;
    public bool facingRight;
    public bool isInjured = false;
    public int currentHealth = 10;
    public int fullHealth = 10;
    public int injuryFrames = 0;
    public Image healthbar;
    private Vector3 deadzone;
    private float rotated;
    private bool leftTilt;
    private float increaser;
    public bool win;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode menuKey = KeyCode.P;
    public Vector2 vel;
	// Use this for initialization
	void Start () {
        win = false;
        increaser = 20f;
        canJump = true;
        jumped = false;
        rotated = 0;
        leftTilt = false;
        Speed = 10;
        playerbody = GetComponent<Rigidbody2D>();
        vel = new Vector2(Speed, 0);
        facingRight = true;
        currentHealth = fullHealth;
        deadzone = new Vector3(6,4,0);
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 3, Camera.main.transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
        Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        canJump = GameObject.Find("Feet").GetComponent<FeetScript>().canJump;
        if(transform.position.y > Camera.main.transform.position.y + deadzone.y)
        {
            float diff = transform.position.y - (Camera.main.transform.position.y + deadzone.y);
            Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y + diff, Camera.main.transform.position.z);
        }
        if (transform.position.y < Camera.main.transform.position.y - deadzone.y)
        {
            float diff = (Camera.main.transform.position.y - deadzone.y) - transform.position.y;
            Camera.main.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y - diff, Camera.main.transform.position.z);
        }
        healthbar.fillAmount = (float)currentHealth / fullHealth;
        if (currentHealth <= 0 )
        {
            Debug.Log("YOU DEAD!");
            lose();
        }
        if (!win)
        {
            /*if (Input.GetKeyDown(rightKey) && touchingGrass) { GameObject.Find("GroundWalk").GetComponent<ParticleSystem>().Play(); }
            if (Input.GetKeyUp(rightKey) || !touchingGrass) { GameObject.Find("GroundWalk").GetComponent<ParticleSystem>().Stop(); }*/
            if (Input.GetKey(rightKey))
            {
                if (!facingRight) { Flip(); }
                transform.Translate(Vector2.right * Time.smoothDeltaTime * Speed);
                transform.eulerAngles = new Vector3(0, 0, rotated);
                if (rotated >= 5 || rotated <= -5)
                {
                    if (rotated >= 5) { rotated = 5; }
                    else { rotated = -5; }
                    increaser *= -1;
                    leftTilt = !leftTilt;
                }
                rotated += increaser * Time.deltaTime;
                if (!GameObject.Find("WalkingSound").GetComponent<AudioSource>().isPlaying)
                {
                    GameObject.Find("WalkingSound").GetComponent<AudioSource>().Play();
                }
                //playerbody.rotation = Quaternion.Euler(0.0f, 0.0f, playerbody.velocity.x * -tilt);
            }
            /* if (Input.GetKeyDown(leftKey) && touchingGrass) { GameObject.Find("GroundWalk").GetComponent<ParticleSystem>().Play(); }
             if (Input.GetKeyUp(leftKey) || !touchingGrass) { GameObject.Find("GroundWalk").GetComponent<ParticleSystem>().Stop(); }*/
            if (Input.GetKey(leftKey))
            {
                if (facingRight) { Flip(); }
                transform.Translate(Vector2.right * Time.smoothDeltaTime * Speed);
                transform.eulerAngles = new Vector3(0, 0, rotated);
                if (rotated >= 5 || rotated <= -5)
                {
                    if (rotated >= 5) { rotated = 5; }
                    else { rotated = -5; }
                    increaser *= -1;
                    leftTilt = !leftTilt;
                }
                rotated += increaser * Time.smoothDeltaTime;
                if (!GameObject.Find("WalkingSound").GetComponent<AudioSource>().isPlaying)
                {
                    GameObject.Find("WalkingSound").GetComponent<AudioSource>().Play();
                }
            }
            if (Input.GetKeyUp(leftKey) || Input.GetKeyUp(rightKey) || !canJump)
            {
                GameObject.Find("WalkingSound").GetComponent<AudioSource>().Pause();
            }
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                playerbody.velocity = new Vector2(playerbody.velocity.x, 0);
                playerbody.AddForce(Vector2.up * 17.5f, ForceMode2D.Impulse);
                //canJump = false;
                jumped = true;
                GameObject.Find("Jump").GetComponent<AudioSource>().Play();
                rotated = 0;
                leftTilt = !leftTilt;
                increaser = Mathf.Abs(increaser);
                GameObject.Find("Body").GetComponent<Animator>().Play("Jump");
            }
            if (Input.GetKey(KeyCode.P)) { SceneManager.LoadScene(0); }
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        /*if (coll.gameObject.tag=="Floor" && jumped){
            GetComponentInChildren<Animator>().Play("Land");
            jumped = false;
        }*/
        if(coll.gameObject.name=="Edge of Insanity")
        {
            //coll.gameObject.GetComponent<AudioSource>().Play();
            lose();
        }
        if (coll.gameObject.CompareTag("Basic Enemy") || coll.gameObject.CompareTag("Shooty Enemy") && !isInjured)
        {
            Vector3 dir = transform.localScale;
            if (transform.localScale.x == coll.transform.localScale.x) { playerbody.AddForce(new Vector2(dir.x * 10, 5), ForceMode2D.Impulse); }
            else  { playerbody.AddForce(new Vector2(-dir.x * 10, 5), ForceMode2D.Impulse); }
            if (coll.gameObject.CompareTag("Basic Enemy")) { coll.gameObject.GetComponent<BasicMovement>().Flip(); } 
            currentHealth--;
            Debug.Log("You got hit! Health: " + currentHealth);
            GameObject.Find("Hurt").GetComponent<AudioSource>().Play();
            isInjured = true;
            GameObject.Find("Body").GetComponent<Animator>().SetBool("Injured", true);
            GameObject.Find("Body").GetComponent<Animator>().Play("Injuredv2");
        }
        if (coll.gameObject.CompareTag("BigMonster"))
        {
            Vector3 dir = transform.localScale;
            if (transform.localScale.x == coll.transform.localScale.x) { playerbody.AddForce(new Vector2(dir.x * 10, 5), ForceMode2D.Impulse); }
            else { playerbody.AddForce(new Vector2(-dir.x * 10, 5), ForceMode2D.Impulse); }
            //What the BigMonster Does...
            currentHealth--;
            Debug.Log("You got hit! Health: " + currentHealth);
            GameObject.Find("Hurt").GetComponent<AudioSource>().Play();
            isInjured = true;
            GameObject.Find("Body").GetComponent<Animator>().SetBool("Injured", true);
            GameObject.Find("Body").GetComponent<Animator>().Play("Injuredv2");
        }
        if (coll.gameObject.name == "EggGirl")
        {
            win = true;
            coll.gameObject.GetComponent<EggGirl>().winState = true;
            GameObject.Find("EggHeart").GetComponent<ParticleSystem>().Play();
        }
    }

    /*void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Floor") { canJump = false; }
    }*/

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Jelly Enemy") && !isInjured)
        {
            Vector3 dir = transform.localScale;
            if (transform.localScale.x == coll.transform.localScale.x) {  playerbody.AddForce(new Vector2(dir.x * 10, 5), ForceMode2D.Impulse);  }
            else {  playerbody.AddForce(new Vector2(-dir.x * 10, 5), ForceMode2D.Impulse); }
            currentHealth--;
            Debug.Log("You got hit! Health: " + currentHealth);
            GameObject.Find("Hurt").GetComponent<AudioSource>().Play();
            isInjured = true;
            GameObject.Find("Body").GetComponent<Animator>().SetBool("Injured", true);
            GameObject.Find("Body").GetComponent<Animator>().Play("Injuredv2");
        }
        if (coll.gameObject.CompareTag("MeanBullet") && !isInjured)
        {
            Vector3 dir = transform.localScale;
            if (transform.localScale.x == coll.transform.localScale.x) { playerbody.AddForce(new Vector2(dir.x * 10, 5), ForceMode2D.Impulse); }
            else { playerbody.AddForce(new Vector2(-dir.x * 10, 5), ForceMode2D.Impulse); }
            currentHealth--;
            Debug.Log("You got hit! Health: " + currentHealth);
            GameObject.Find("Hurt").GetComponent<AudioSource>().Play();
            isInjured = true;
            GameObject.Find("Body").GetComponent<Animator>().SetBool("Injured", true);
            GameObject.Find("Body").GetComponent<Animator>().Play("Injuredv2");
        }
        if (coll.gameObject.name == "View Area")
        {
            GameObject munster = coll.gameObject.transform.parent.gameObject;
            munster.transform.FindChild("BigMonsterBody").gameObject.GetComponent<Animator>().Play("BigMonsterSwing");
        }
        if (coll.gameObject.CompareTag("Heal"))
        {
            currentHealth = 10;
            GameObject.Find("SandvichBite").GetComponent<AudioSource>().Play();
            Destroy(coll.gameObject);
        }
        if(coll.gameObject.name == "Hit Area")
        {
            if (!isInjured)
            {
                Vector3 dir = transform.localScale;
                if (transform.localScale.x == coll.transform.localScale.x) { playerbody.AddForce(new Vector2(-dir.x * 10, 5), ForceMode2D.Impulse); }
                else { playerbody.AddForce(new Vector2(dir.x * 10, 5), ForceMode2D.Impulse); }
                //What the BigMonster Does...
                currentHealth -= 3;
                Debug.Log("You got hit! Health: " + currentHealth);
                GameObject.Find("Hurt").GetComponent<AudioSource>().Play();
                isInjured = true;
                GameObject.Find("Body").GetComponent<Animator>().SetBool("Injured", true);
                GameObject.Find("Body").GetComponent<Animator>().Play("Injuredv2");
            }
        }
    }

    void lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void juuke()
    {
        playerbody.AddForce(Vector2.up * 17.5f, ForceMode2D.Impulse);
    }
}
