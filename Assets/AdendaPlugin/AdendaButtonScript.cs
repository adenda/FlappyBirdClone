using UnityEngine;
using System.Collections;

public class AdendaButtonScript : MonoBehaviour 
{
	// Constants
	const string OPT_IN_TXT = "Opt In";
	const string OPT_OUT_TXT = "Opt Out";
	const string OPT_ERROR_TXT = "ERROR!";
	
	// Member variables
	private int mOptedInState;
	private string mButtonText;
	private float buttonHeight;
	private float buttonWidth;
	public GUIStyle adendaStyle;
	
	void OnEnable()
	{
		print("Registering for Adenda Events");
		AdendaPlugin.OnPreLockscreenStarted += handlePreLockscreenStarted;
		AdendaPlugin.OnPostLockscreenStarted += handlePostLockscreenStarted;
		AdendaPlugin.OnPreLockscreenStopped += handlePreLockscreenStopped;
		AdendaPlugin.OnPostLockscreenStopped += handlePostLockscreenStopped;
	}
	
	void OnDisable()
	{
		print("Unregistering for Adenda Events");
		AdendaPlugin.OnPreLockscreenStarted -= handlePreLockscreenStarted;
		AdendaPlugin.OnPostLockscreenStarted -= handlePostLockscreenStarted;
		AdendaPlugin.OnPreLockscreenStopped -= handlePreLockscreenStopped;
		AdendaPlugin.OnPostLockscreenStopped -= handlePostLockscreenStopped;
	}
	
	// Use this for initialization
	void Start () 
	{
		buttonHeight = Screen.height / 12;
		buttonWidth = buttonHeight *3.54f;

		// Make sure we grab the right initial state	
		mOptedInState = AdendaPlugin.initializeState();
		print("Initialized State: " + mOptedInState);
		
		// Set the right button text
		if (mOptedInState == AdendaPlugin.OPTED_ERROR_STATE)
			mButtonText = OPT_ERROR_TXT;
		else if (mOptedInState == AdendaPlugin.OPTED_IN_STATE)
			mButtonText = OPT_OUT_TXT;
		else
			mButtonText = OPT_IN_TXT;

		// Turn ads on
		AdendaPlugin.setEnableAds(true);
		AdendaPlugin.setUnlockType(AdendaPlugin.UNLOCK_TYPE_DEFAULT);
	}
	
	// Update is called once per frame
	void Update () {	
	}
	
	private void OnGUI()
	{
		GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
		buttonStyle.fontSize = 50;

		bool bPressed = GUI.Button(new Rect(200, 300, 600, 150), mButtonText, buttonStyle);
		
		if (bPressed && mOptedInState == AdendaPlugin.OPTED_OUT_STATE)
		{
			// Click event happened -- Opt the user IN
			AdendaPlugin.startAdendaLockscreen("9876543210", "m", "19900102");
		}
		else if (bPressed && mOptedInState == AdendaPlugin.OPTED_IN_STATE)
		{
			// Click event happened -- Opt the user OUT
			AdendaPlugin.stopAdendaLockscreen();
		}
	}
	
	public void handlePreLockscreenStarted()
	{
		print("Handling onPreLockscreenStarted!");
	}
	
	public void handlePostLockscreenStarted()
	{
		print("Handling onPostLockscreenStarted!");
		// Set button text
		mButtonText = OPT_OUT_TXT;
		// Set opted in state
		mOptedInState = 1;
	}
	
	public void handlePreLockscreenStopped()
	{
		print("Handling onPreLockscreenStopped!");
	}
	
	public void handlePostLockscreenStopped()
	{
		print("Handling onPostLockscreenStopped!");
		// Set button text
		mButtonText = OPT_IN_TXT;
		// Set opted in state
		mOptedInState = 0;
	}
}
