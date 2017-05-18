using UnityEngine;
using System.Collections;

public class AdendaCustomContentTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AdendaPlugin.setUnlockType(AdendaPlugin.UNLOCK_TYPE_GLOWPAD);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void OnGUI()
	{
		GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
		buttonStyle.fontSize = 50;

		if(GUI.Button(new Rect(200, 500, 600, 150), "Fire Remote Img Notif", buttonStyle))
		{
			AdendaPlugin.addRemoteImageResource("http://fc06.deviantart.net/images/i/2002/46/f/5/RedCube.jpg", "Swipe right to play!", "Remote!", false);
		}

		if(GUI.Button(new Rect(200, 700, 600, 150), "Fire HTML Notif!", buttonStyle))
		{
			AdendaPlugin.addCustomHtmlContent("http://www.cnn.com", "HTML!", false);
		}

		if(GUI.Button(new Rect(200, 900, 600, 150), "Fire Native!", buttonStyle))
		{
			AdendaPlugin.addUnityFragment(null, "Unity from Unity!", false, false, "AdendaObject", "onUnityStarted", "Callback Worked!");
		}
	}

	public void onUnityStarted(string message)
	{
		Application.LoadLevel(1);
	}
}
