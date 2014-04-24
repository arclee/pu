using UnityEngine;
using System.Collections;

public class GameEmy01 : MonoBehaviour {

	public GameObject MainObj;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Dead()
	{
		ReUse();
	}

	void ReUse()
	{
		Destroy(MainObj);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			col.rigidbody2D.SendMessage("JumpByEmy");
			Dead();
		}
	}
}
