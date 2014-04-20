using UnityEngine;
using System.Collections;

public class CameraFlow : MonoBehaviour {

	public Transform player;

	public Vector2 posbise = new Vector2(0, 0);
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.position.x + posbise.x, player.position.y + posbise.y, -10);
	}
}
