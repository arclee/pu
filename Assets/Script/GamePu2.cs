using UnityEngine;
using System.Collections;

public class GamePu2 : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EndPu()
	{
		StartCoroutine(WaitDisable());
	}

	private IEnumerator WaitDisable()
	{
		yield return new WaitForEndOfFrame();
		
		GamePuPool.Instance.pool.Restore(transform.parent.gameObject);
	}
}
