using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class GameTitleMain : MonoBehaviour {

	public int levelID = 0;
	AsyncOperation ao;

	//g+.
	GameObject gbGPFunc;
	GameObject gbGPLogin;
	//fb.
	GameObject gbFBFunc;
	GameObject gbFBLogin;


	void Awake()
	{
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();
		
		//((PlayGamesPlatform)Social.Active).SignOut();
	}

	// Use this for initialization
	void Start ()
	{
		gbGPFunc = GameObject.Find("GPFuncBtn");
		gbGPLogin = GameObject.Find("GPluseBtn");
		
		gbFBFunc = GameObject.Find("FBFuncBtn");
		gbFBLogin = GameObject.Find("FBBtn");
		GPBtnCheck();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void GPBtnCheck()
	{

		if (gbGPFunc)
		{
			gbGPFunc.SetActive(Social.localUser.authenticated);
		}

		if (gbGPLogin)
		{
			gbGPLogin.SetActive(!Social.localUser.authenticated);
		}

		if (Application.platform != RuntimePlatform.WindowsWebPlayer)
		{
			if (gbFBFunc)
			{
				gbFBFunc.SetActive(FB.IsLoggedIn);
			}

			if (gbFBLogin)
			{
				gbFBLogin.SetActive(!FB.IsLoggedIn);
				
			}
		}
		else
		{
			if (gbFBFunc)
			{
				gbFBFunc.SetActive(false);
			}
			
			if (gbFBLogin)
			{
				gbFBLogin.SetActive(false);
				
			}

		}

	}

	void PlayGame()
	{
		StartCoroutine("LoadLevelWithProgress");

	}

	IEnumerator LoadLevelWithProgress()
	{
		ao = Application.LoadLevelAsync(levelID);

		while(!ao.isDone)
		{
			yield return ao;
		}
	}

	void LogingGooglePlus()
	{
		StartCoroutine("ILogingGooglePlus");

	}

	IEnumerator ILogingGooglePlus()
	{
		if (!Social.localUser.authenticated)
		{
			Social.localUser.Authenticate(LoginCallBack);
		}

		yield return null;
	}

	void LoginCallBack(bool success)
	{
		GPBtnCheck();

	}

	void ShowRank()
	{
		Social.ShowLeaderboardUI();
	}

	void LogOut()
	{
		((PlayGamesPlatform)Social.Active).SignOut();
		GPBtnCheck();
	}

	void ShowAchievement()
	{
		Social.ShowAchievementsUI();
	}

	void OnDestroy()
	{
		Debug.Log("OnDestroy");
		GamePuPool.Instance.pool.DisableAll();
	}

}
