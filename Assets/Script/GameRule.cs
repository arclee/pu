﻿using UnityEngine;
using System.Collections;

public class GameRule : MonoBehaviour {


	enum State
	{
		Play,
		End
	}

	public PlayerCtrl playerctrl;

	public GameObject endmenu;

	State playState;

	// Use this for initialization
	void Start () {
		playState = State.Play;
		endmenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void LateUpdate()
	{
		if (playState == State.Play)
		{
			if (playerctrl.playerState == PlayerCtrl.State.End
			    && playerctrl.IsStopMove()
			    )
			{
				playState = State.End;
				UploadRank();
				ShowFinishMenu();
			}
		}
	}

	void ShowFinishMenu()
	{
		endmenu.SetActive(true);
	}

	void UploadRank()
	{
		//Debug.Log("dist" + dist.ToString());
		//挑行到小數4位.但 report 只能傳INT, 所以要乘10000, server 後面會自動轉成小數.
		float dist4dig = playerctrl.GetDist() * 10000;

		int distI = (int)dist4dig;


		Social.ReportScore(distI, "CgkI5Yr-qp8cEAIQBg", UploadRankCB);
	}

	void UploadRankCB(bool success)
	{

	}

	void RetryGame()
	{
		StartCoroutine("RetryLoadLevel");
	}

	void GotoTitle()
	{
		StartCoroutine("TitleLoadLevel");

	}

	IEnumerator RetryLoadLevel()
	{
		AsyncOperation ao = Application.LoadLevelAsync("pu");

		while(!ao.isDone)
		{
			yield return ao;
		}
	}
	
	IEnumerator TitleLoadLevel()
	{
		AsyncOperation ao = Application.LoadLevelAsync("title");
		
		while(!ao.isDone)
		{
			yield return ao;
		}
	}

	//FB.
	void PostToFB()
	{

		string gameurl = "";
		if (Application.platform == RuntimePlatform.WindowsWebPlayer)
		{
			gameurl = "https://apps.facebook.com/252468178109148/";
		}
		else
		{
			gameurl = "https://play.google.com/apps/testing/com.ARC1212.PU1212";
		}
		FB.Feed(
			link: gameurl,
			linkName: "PU 1212",
			linkCaption: "My Score :" + playerctrl.GetDist().ToString(),
			linkDescription: "I am good!!",
			picture: "https://googledrive.com/host/0B0zQPJH0W58oYlFkdkc4ckJKb2c/pu.png",
			callback: LogCallback
			);

	}

	void LogCallback(FBResult response) {
		Debug.Log(response.Text);
	}

}
