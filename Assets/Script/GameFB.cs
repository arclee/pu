using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameFB : MonoBehaviour {
	
	private bool isInit = false;
	//private string lastResponse = "";

	public tk2dTextMesh UserID;

	Texture UserTexture;

	
	void Awake()
	{
		CallFBInit();


	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void CallFBInit()
	{	
		FB.Init(SetInit, OnHideUnity);
	}
	
	private void SetInit()
	{
		isInit = true;
		Util.Log("SetInit");
		enabled = true; // "enabled" is a property inherited from MonoBehaviour
		if (FB.IsLoggedIn) 
		{
			Util.Log("Already logged in");
			OnLoggedIn();
		}
		else
		{
			//WEB 自動登入, 要求權限.
			//手機版做成按鈕給玩家選按.
			if (Application.platform == RuntimePlatform.WindowsWebPlayer)
			{
				CallFBLogin();
			}
		}
	}

	
	private void OnHideUnity(bool isGameShown)
	{
		Debug.Log("Is game showing? " + isGameShown);
	}
	
	private void CallFBLogin()
	{
		if (!FB.IsLoggedIn)                                                                                              
		{
			FB.Login("email,publish_actions,publish_stream", LoginCallback);
		}
	}

	void LoginCallback(FBResult result)
	{
		if (result.Error != null)
		{
			//lastResponse = "Error Response:\n" + result.Error;
		}

		if (FB.IsLoggedIn)
		{
			OnLoggedIn();
			//lastResponse = "Login was successful!";
		}
		else
		{
			//lastResponse = "Login cancelled by Player";
			//UserID.text = lastResponse;

		}
	}

	
	void OnLoggedIn()
	{
		Util.Log("Logged in. ID: " + FB.UserId);
		
		//UserID.text = FB.UserId;
		// Reqest player info and profile picture		
		//FB.API("/me?fields=id,first_name,friends.limit(100).fields(first_name,id)", Facebook.HttpMethod.GET, APICallback);  
		LoadPicture(Util.GetPictureURL("me", 128, 128),MyPictureCallback);    

		//自動發文.
		var automsg = 
		new Dictionary<string, string>() {
			{"message", "Message"},
			{"description", "description"},
			{"caption", "caption"}
		};

		FB.API("/me/feed", Facebook.HttpMethod.POST, API_ME_FEED_Callback, automsg);
	}
	
	void API_ME_FEED_Callback(FBResult result)                                                                                              
	{

	}

	void APICallback(FBResult result)                                                                                              
	{                                                                                                                              
		Util.Log("APICallback");                                                                                                
		if (result.Error != null)                                                                                                  
		{                                                                                                                          
			Util.LogError(result.Error);                                                                                           
			// Let's just try again                                                                                                
			FB.API("/me?fields=id,first_name,friends.limit(100).fields(first_name,id)", Facebook.HttpMethod.GET, APICallback);     
			return;                                                                                                                
		}                                                                                                                          
		
		//profile = Util.DeserializeJSONProfile(result.Text);                                                                        
		//GameStateManager.Username = profile["first_name"];                                                                         
		//friends = Util.DeserializeJSONFriends(result.Text);                                                                        
	}

	
	delegate void LoadPictureCallback (Texture texture);

	void MyPictureCallback(Texture texture)
	{
		Util.Log("MyPictureCallback");

		if (texture ==  null)
		{
			// Let's just try again
			Util.LogError("Error loading user picture");
			LoadPicture(Util.GetPictureURL("me", 128, 128),MyPictureCallback);
			
			return;
		}
		
		UserTexture = texture;
	}
	IEnumerator LoadPictureEnumerator(string url, LoadPictureCallback callback)    
	{
		WWW www = new WWW(url);
		yield return www;
		callback(www.texture);
	}

	void LoadPicture (string url, LoadPictureCallback callback)
	{
		FB.API(url,Facebook.HttpMethod.GET,result =>
		       {
			if (result.Error != null)
			{
				Util.LogError(result.Error);
				return;
			}
			
			var imageUrl = Util.DeserializePictureURLString(result.Text);
			
			StartCoroutine(LoadPictureEnumerator(imageUrl,callback));
		});
	}

	private void CallFBLogout()
	{
		FB.Logout();
		UserTexture = null;

	}

	void  OnGUI()
	{
		if (UserTexture != null)
		{
			GUI.DrawTexture( (new Rect(8,10, 150, 150)), UserTexture);
		}
	}

}
