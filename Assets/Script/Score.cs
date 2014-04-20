using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	
	public PlayerCtrl playerctrl;

	bool keep0 = true;
	tk2dTextMesh text;
	// Use this for initialization
	void Start () {
		text = (tk2dTextMesh)GetComponent<tk2dTextMesh>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		float dis = playerctrl.GetDist();

		if (keep0)
		{
			if ( dis >= 1)
			{
				keep0 = false;
			}
		}

		if (!keep0)
		{
			text.text = dis.ToString();
		}
	}
}
