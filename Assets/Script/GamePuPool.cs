using UnityEngine;
using System.Collections;

public class GamePuPool : MonoBehaviour
{
	static  public GamePuPool Instance;

	public GameObjPool pool = new GameObjPool();
	public string prefabName;

	public bool Grawable = false;
	public int MaxObj = 10;
	public int debugid = 0;
	void Awake()
	{
		pool.grawable = Grawable;
		pool.maxobj = MaxObj;

		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			pool.prefabName = prefabName;
			pool.PreCreate(gameObject);
		}

	}

	// Use this for initialization
	void Start ()
	{
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
