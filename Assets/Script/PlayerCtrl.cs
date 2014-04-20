using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour {

	public enum State
	{
		Ready = 0,
		Moving,
		End,
	}

	public bool debugNoEnd = false;
	
	[HideInInspector]
	public State playerState = State.Ready;

	bool isCharge = false;

	public float powerscale = 100;


	public float chargepower = 10;

	[HideInInspector]
	public float power = 0;

	public float maxpower = 50;
	public int maxPuTime = 2;
	int currentPuTime = 0;
	bool isAddforce = false;

	public Transform forcepos;
	public Transform forcetarget;
	public Transform pu2pos;
	Vector3 forcedir;

	
	Vector3 Statrpos;
	// Use this for initialization
	void Start ()
	{
		Statrpos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (playerState == State.End
		    || (currentPuTime >= maxPuTime && (maxPuTime > 0)))
		{
			return;
		}
	
		//if (Input.touchCount > 0)
		{
			if (!isCharge)
			{
				if (Input.GetMouseButtonDown(0))
				//if (Input.GetTouch(0).phase == TouchPhase.Began)
				{
					isCharge = true;
					playerState = State.Moving;
				}
			}
			else
			{
				if (Input.GetMouseButtonUp(0))
				//if (Input.GetTouch(0).phase == TouchPhase.Ended
				//    || Input.GetTouch(0).phase == TouchPhase.Canceled)
				{
					isCharge = false;
				}
			}
		}

		if (isCharge)
		{
			power += Time.deltaTime * chargepower;
			if (power > maxpower)
			{
				power = maxpower;
			}
		}

		if (!isAddforce)
		{
			if (!isCharge && (power > 0))
			{
				isAddforce = true;
				currentPuTime ++;
			}
		}

	}


	void FixedUpdate()
	{
		if (isAddforce)
		{
			forcedir = forcetarget.position - forcepos.position;
			forcedir.Normalize();

			rigidbody2D.AddForceAtPosition(forcedir * power * powerscale, forcepos.position);
			power = 0;
			isAddforce = false;
			//effect.
			GameObject pu2 = GamePuPool.Instance.pool.GetObject();
			pu2.transform.position = transform.position;
			pu2.transform.rotation = transform.rotation;

		}

	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Ground")
		{
			if (debugNoEnd)
			{
				return;
			}
			playerState = State.End;
			//coll.gameObject.SendMessage("ApplyDamage", 10);
		}		
		else if (coll.gameObject.tag == "Emy01")
		{

			//在頭上.
			Vector3 hitdir = transform.position - coll.gameObject.transform.position;
			hitdir.Normalize();
			float val = Mathf.Abs(Mathf.Acos(Vector3.Dot(Vector3.up , hitdir)));
			if (val >= 0 && val <= 90)
			{
				rigidbody2D.AddForceAtPosition(new Vector3(1, 1, 0).normalized * maxpower * 0.5f * powerscale, forcepos.position);
				
				coll.gameObject.SendMessage("Dead");

			}
		}
	}

	public bool IsStopMove()
	{
		bool sleep = rigidbody2D.IsSleeping();

		return sleep;
	}

	public float GetDist()
	{
		Vector3 dis = transform.position - Statrpos;
		float mult = Mathf.Pow(10, (float)4);
		return Mathf.Round((int)(dis.x * mult)) / mult;
	}

}
