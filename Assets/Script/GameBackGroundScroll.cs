using UnityEngine;
using System.Collections;

public class GameBackGroundScroll : MonoBehaviour {

	public Vector2 Speed = new Vector2(1, 1);
	public Transform player;
	Vector3 Statrpos;
	// Use this for initialization
	void Start () {
		Statrpos = player.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		Vector3 dis = player.position - Statrpos;
		renderer.material.mainTextureOffset = new Vector2((dis.x * Speed.x)%1, (dis.y * Speed.y)%1);
	}
}
