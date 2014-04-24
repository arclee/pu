using UnityEngine;
using System.Collections;

public class GamePowerCountHUD : MonoBehaviour {

	tk2dTextMesh text;
	public PlayerCtrl playerctrl;

	int lastPowerLeft = 0;
	// Use this for initialization
	void Start () {
		text = (tk2dTextMesh)GetComponent<tk2dTextMesh>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		int pl = playerctrl.PowerLeft();

		if (pl != lastPowerLeft)
		{
			text.text = pl.ToString();
		}

		lastPowerLeft = pl;
	}


}
