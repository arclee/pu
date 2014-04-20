using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public class GameTitleMain : MonoBehaviour {

	public int levelID = 0;
	AsyncOperation ao;
	
	GameObject gbFunc;
	GameObject gbLogin;
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
		gbFunc = GameObject.Find("GPFuncBtn");
		gbLogin = GameObject.Find("GPluseBtn");
		GPBtnCheck();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void GPBtnCheck()
	{

		if (gbFunc)
		{
			gbFunc.SetActive(Social.localUser.authenticated);
		}

		if (gbLogin)
		{
			gbLogin.SetActive(!Social.localUser.authenticated);
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
