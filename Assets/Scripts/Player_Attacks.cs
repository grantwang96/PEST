using UnityEngine;
using System.Collections;

public class Player_Attacks : MonoBehaviour {

    public ArrayList inventory = new ArrayList();
    public PolygonCollider2D netArea;
    public GameObject[] mons;
    public int currentMonster = 1;
    public int maxCapacity = 6;
    public GameObject player;
    public Sprite secretSprite;
    public bool win;
    public AudioClip[] monsterNoises = new AudioClip[6];
    public KeyCode attack = KeyCode.J;
    public KeyCode toss = KeyCode.K;
    public KeyCode leftScroll = KeyCode.U;
    public KeyCode rightScroll = KeyCode.I;

	// Use this for initialization
	void Start () {
        win = false;
        player = GameObject.Find("Player");
        netArea = GetComponent<PolygonCollider2D>();
        currentMonster = 1;
	}
	
	// Update is called once per frame
	void Update () {
        win = GameObject.Find("Player").GetComponent<Player_Movement>().win;
        transform.position = new Vector3(player.transform.position.x + 0.5f*transform.localScale.x,
            player.transform.position.y, player.transform.position.z);
        transform.localScale = player.transform.localScale;

        if (Input.GetKeyDown(attack)) {
            //GetComponent<AudioSource>().Play();
            GetComponent<Animator>().Play("Net Swing 2");
        }

        /*if (Input.GetKey(KeyCode.Return)) { GameObject.Find("EggGirl").GetComponent<SpriteRenderer>().sprite = secretSprite; }*/
        if (currentMonster >= inventory.Count) { currentMonster = inventory.Count; }
        if(currentMonster <= 0){ currentMonster = 1; }
        if (Input.GetKeyDown(toss))
        {
            if (inventory.Count == 0) { Debug.Log("Inventory empty! Capture some monsters!"); }
            else
            {
                string monsspawn = (string)inventory[currentMonster - 1];
                int monspawn = 0;
                switch (monsspawn)
                {
                    case "Basic":
                        monspawn = 0;
                        break;
                    case "Jelly":
                        monspawn = 1;
                        break;
                    case "Shooty":
                        monspawn = 2;
                        break;
                    default:
                        Debug.Log("You got nuthin'");
                        break;
                }
                GameObject newfri = Instantiate(mons[monspawn]) as GameObject;
                Vector3 playpos = GameObject.Find("Player").transform.position;
                if (GameObject.Find("Player").GetComponent<Player_Movement>().facingRight)
                {
                    if (monsspawn == "Jelly") { newfri.transform.position = new Vector3(playpos.x + 3, playpos.y, playpos.z); }
                    else
                    {
                        newfri.transform.position = new Vector3(playpos.x + 1, playpos.y + 1, playpos.z);
                        newfri.GetComponent<Rigidbody2D>().AddForce(new Vector2(5, 2), ForceMode2D.Impulse);
                    }
                }
                else
                {
                    if (monsspawn == "Jelly") { newfri.transform.position = new Vector3(playpos.x - 3, playpos.y, playpos.z); }
                    else
                    {
                        newfri.transform.position = new Vector3(playpos.x - 1, playpos.y + 1, playpos.z);
                        newfri.GetComponent<Rigidbody2D>().AddForce(new Vector2(-5, 2), ForceMode2D.Impulse);
                        if (monsspawn == "Basic")
                        {
                            newfri.GetComponent<BasicMovement>().facingRight = false;
                            newfri.transform.localScale = new Vector3(newfri.transform.localScale.x * -1, newfri.transform.localScale.y, newfri.transform.localScale.z);
                        }
                        else
                        {
                            newfri.GetComponent<ShootyMovement>().facingRight = false;
                            newfri.transform.localScale = new Vector3(newfri.transform.localScale.x * -1, newfri.transform.localScale.y, newfri.transform.localScale.z);
                        }
                    }
                }
                inventory.RemoveAt(currentMonster - 1);
                if (currentMonster > inventory.Count) { currentMonster = inventory.Count; }
                else if (currentMonster <= 0) { currentMonster = 1; }
            }
        }
        if (Input.GetKeyDown(rightScroll))
        {
            currentMonster++;
            if (inventory.Count == 0) { currentMonster = 1; }
            else if (currentMonster > inventory.Count) { currentMonster = 1; }
            Debug.Log("Selected Monster: " + currentMonster);
            GameObject.Find("Switch").GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(leftScroll))
        {
            currentMonster--;
            if (inventory.Count == 0) { currentMonster = 1; }
            else if (currentMonster == 0) { currentMonster = inventory.Count; }
            Debug.Log("Selected Monster: " + currentMonster);
            GameObject.Find("Switch").GetComponent<AudioSource>().Play();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (inventory.Count < maxCapacity)
        {
            if (coll.gameObject.tag == "Basic Enemy" || coll.gameObject.tag == "Basic Friend")
            {
                inventory.Add("Basic");
                Debug.Log("Basic Enemy Hit!");
                playMerp();
                Destroy(coll.gameObject);
                GetComponent<Animator>().Play("CapturedMerple");
            }
            if (coll.gameObject.tag == "Jelly Enemy" || coll.gameObject.tag == "Jelly Friend")
            {
                inventory.Add("Jelly");
                Debug.Log("Jelly Enemy Hit!");
                playJelly();
                Destroy(coll.gameObject.transform.parent.gameObject);
                GetComponent<Animator>().Play("CapturedJelly");
            }
            if (coll.gameObject.tag == "Shooty Enemy" || coll.gameObject.tag == "Shooty Friend")
            {
                inventory.Add("Shooty");
                Debug.Log("Shooty Enemy Hit!");
                Destroy(coll.gameObject);
                GetComponent<Animator>().Play("CapturedCactus");
                GameObject.Find("PlantHit").GetComponent<AudioSource>().Play();
            }
        }
        else { Debug.Log("Max Capacity!"); }
    }

    void disableNetArea()
    {
        netArea.enabled = false;
    }

    void enableNetArea()
    {
        netArea.enabled = true;
    }
    void playSound()
    {
        GetComponent<AudioSource>().Play();
    }
    void playMerp()
    {
        GameObject.Find("MonsterNoises").GetComponent<AudioSource>().clip = monsterNoises[(int)Random.Range(0, 2)];
        GameObject.Find("MonsterNoises").GetComponent<AudioSource>().Play();
    }
    void playJelly()
    {
        GameObject.Find("MonsterNoises").GetComponent<AudioSource>().clip = monsterNoises[(int)Random.Range(3, 5)];
        GameObject.Find("MonsterNoises").GetComponent<AudioSource>().Play();
    }
}
