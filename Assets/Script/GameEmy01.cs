using UnityEngine;
using System.Collections;

public class GameEmy01 : MonoBehaviour {

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
		Destroy(gameObject);
	}

}
