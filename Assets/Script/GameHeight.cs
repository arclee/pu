using UnityEngine;
using System.Collections;

public class GameHeight : MonoBehaviour {

	public Transform player;
	Vector3 Statrpos;
	tk2dTextMesh text;
	// Use this for initialization
	void Start () {
	
		text = (tk2dTextMesh)GetComponent<tk2dTextMesh>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		text.text = player.position.y.ToString();
	}
}
