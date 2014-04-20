using UnityEngine;
using System.Collections;

public class Power : MonoBehaviour {

	public Transform max;
	public Transform nowb;
	public Transform now;

	public GameObject player;
	PlayerCtrl playerctrl;

	public float uiscale = 1;

	Vector3 nowPowerScale;
	// Use this for initialization
	void Start ()
	{
		nowPowerScale = new Vector3(1, 1, 1);

		playerctrl = (PlayerCtrl)player.GetComponent<PlayerCtrl>();
		//set max;
		max.localScale = new Vector3(max.localScale.x, playerctrl.maxpower * uiscale, max.localScale.z);
		//set now
		nowb.localScale = new Vector3(nowb.localScale.x, playerctrl.maxpower * uiscale, nowb.localScale.z);
		//now.localScale = new Vector3(now.localScale.x, player.power * uiscale, now.localScale.z);
		nowPowerScale.x = now.localScale.x;
		nowPowerScale.y = playerctrl.power * uiscale;
		nowPowerScale.z = now.localScale.z;
		now.localScale = nowPowerScale;
	}
	
	// Update is called once per frame
	void Update ()
	{
		nowPowerScale.y = playerctrl.power * uiscale;
		now.localScale = nowPowerScale;
	
	}


}
