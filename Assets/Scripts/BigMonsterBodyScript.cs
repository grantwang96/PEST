using UnityEngine;
using System.Collections;

public class BigMonsterBodyScript : MonoBehaviour {
    public BigMonster script;
	// Use this for initialization
	void Start () {
        script = transform.parent.GetComponent<BigMonster>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void stopMovement()
    {
        script.moving = false;
    }
    void enableHitArea()
    {
        transform.parent.FindChild("Hit Area").gameObject.SetActive(true);
    }
    void disableHitArea()
    {
        transform.parent.FindChild("Hit Area").gameObject.SetActive(false);
        script.moving = true;
    }
}
