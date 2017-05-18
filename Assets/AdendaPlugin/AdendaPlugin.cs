using System;
using UnityEngine;
using System.Collections.Generic;

public class AdendaPlugin : MonoBehaviour {
	
	public const int OPTED_IN_STATE = 1;
	public const int OPTED_OUT_STATE = 0;
	public const int OPTED_ERROR_STATE = -1;
	public const int UNLOCK_TYPE_SLIDE = 1;
	public const int UNLOCK_TYPE_GLOWPAD = 2;
	public const int UNLOCK_TYPE_GESTURE = 3;
	public const int UNLOCK_TYPE_DEFAULT = 0;
	
	// Adenda Callback that can be hooked into
	public static event Action OnPreLockscreenStarted = delegate {};
	public static event Action OnPostLockscreenStarted = delegate {};
	public static event Action OnPreLockscreenStopped = delegate {};
	public static event Action OnPostLockscreenStopped = delegate {};
	public static event Action<String, long> onUserNewReward = delegate{};
	
	void Awake()
	{
		print ("gameObject Name: " + gameObject.name);
		setCallbackHandlerName(gameObject.name);
	}
	
	// Sets callback for lockscreen launch functions
	public static void setCallbackHandlerName(string callbackHandlerName)
	{
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.widget.UnityHelper");
		pluginClass.CallStatic("setCallbackHandlerName", new object[1] {callbackHandlerName});
	}
	
	// Initialize state of Adenda object
	public static int initializeState()
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.widget.UnityHelper");
		return pluginClass.CallStatic<int>("initializeState", new object[1] {activity});
	}
	
	// Launches dialog to confirm starting Adenda lockscreen
	public static void startAdendaLockscreen(bool bConfirm = true)
	{
		startAdendaLockscreen(null, null, null, bConfirm);
	}
	
	// Launches dialog to confirm starting Adenda lockscreen
	public static void startAdendaLockscreen(String userId, String gender, String dob, bool bConfirm = true)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.widget.UnityHelper");
		if (bConfirm)
			pluginClass.CallStatic("launchStartLockscreenConfirmDialog", new object[4] {activity, userId, gender, dob});
		else
			pluginClass.CallStatic("startLockscreen", new object[4] {activity, userId, gender, dob});
	}
	
	// Launches dialog to confirm stopping Adenda lockscreen 
	public static void stopAdendaLockscreen(bool bConfirm = true)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.widget.UnityHelper");
		if (bConfirm)
			pluginClass.CallStatic("launchStopLockscreenConfirmDialog", new object[1] {activity});
		else
			pluginClass.CallStatic("stopLockscreen", new object[1] {activity});
	}
	
	// Set unlock thype
	public static void setUnlockType(int type)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		pluginClass.CallStatic("setUnlockType", new object[2] {activity, type});
	}
	
	// Get unlock type
	public static int getUnlockType()
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<int>("getUnlockType", new object[1] {activity});
	}
	
	// Set impression tracking for custom content on or off
	public static void setEnableCustomContentTracking(bool bEnable)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		pluginClass.CallStatic("setEnableCustomContentTracking", new object[2] {activity, bEnable});
	}
	
	// Get enabled state of impression tracking for custom content
	public static bool getEnableCustomContentTracking()
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<bool>("getEnableCustomContentTracking", new object[1] {activity});
	}
	
	// Set server ads on or off
	public static void setEnableAds(bool bEnable)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		pluginClass.CallStatic("setEnableAds", new object[2] {activity, bEnable});
	}
	
	// Get enabled state of impression tracking for custom content
	public static bool getEnableAds()
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<bool>("getEnableAds", new object[1] {activity});
	}
	
	public static void setAdendaConfirmationImage(string imageName)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		pluginClass.CallStatic("setAdendaConfirmationImage", new object[2] {activity, imageName});
	}
	
	public static void setAdendaConfirmationText(string sAdendaNotificationText)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		pluginClass.CallStatic("setAdendaConfirmationText", new object[2] {activity, sAdendaNotificationText});
	}
	
	// Add local image resource to custom content queue
	public static long addImageResource(int img_res_id, string sIdentifier, bool bGTC)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<long>("addImageResource", new object[4]{activity, img_res_id, sIdentifier, bGTC});
	}
	
	// Ad image resource with text
	public static long addImageResource(int img_res_id, string sText, string sIdentifier, bool bGTC)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<long>("addImageResource", new object[5]{activity, img_res_id, sText, sIdentifier, bGTC});
	}
	
	// Add image resource with image and url
	public static long addImageResource(int img_res_id, string sText, string sUrl, string sIdentifier, bool bGTC)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<long>("addImageResource", new object[6]{activity, img_res_id, sText, sUrl, sIdentifier, bGTC});
	}
	
	//-----
	// Add local image resource to custom content queue
	public static long addImageResource(string img_res_name, string sIdentifier, bool bGTC)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<long>("addImageResource", new object[4]{activity, img_res_name, sIdentifier, bGTC});
	}
	
	// Ad image resource with text
	public static long addImageResource(string img_res_name, string sText, string sIdentifier, bool bGTC)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<long>("addImageResource", new object[5]{activity, img_res_name, sText, sIdentifier, bGTC});
	}
	
	// Add image resource with image and url
	public static long addImageResource(string img_res_name, string sText, string sUrl, string sIdentifier, bool bGTC)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<long>("addImageResource", new object[6]{activity, img_res_name, sText, sUrl, sIdentifier, bGTC});
	}
	//--------
	
	// Add remote image to custom content queue
	public static long addRemoteImageResource(string sImgUri, string sIdentifier, bool bGTC)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<long>("addRemoteImageResource", new object[4]{activity, sImgUri, sIdentifier, bGTC});
	}
	
	// Add remote image with text to custom content queue
	public static long addRemoteImageResource(string sImgUri, string sText, string sIdentifier, bool bGTC)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<long>("addRemoteImageResource", new object[5]{activity, sImgUri, sText, sIdentifier, bGTC});
	}
	
	// Add remote image with text and click url to custom content queue
	public static long addRemoteImageResource(string sImgUri, string sText, string sUrl, string sIdentifier, bool bGTC)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<long>("addRemoteImageResource", new object[6]{activity, sImgUri, sText, sUrl, sIdentifier, bGTC});
	}
	
	// Add html custom content
	public static long addCustomHtmlContent(string sUrl, string sIdentifier, bool bGTC)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<long>("addCustomHtmlContent", new object[4]{activity, sUrl, sIdentifier, bGTC});
	}
	
	// Add html custom content with Uri
	public static long addCustomHtmlContent(string sUrl, string sActionUri, string sIdentifier, bool bGTC)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<long>("addCustomHtmlContent", new object[5]{activity, sUrl, sActionUri, sIdentifier, bGTC});
	}
	
	// Add video custom content
	public static long addCustomVideoContent(string sUrl, string sIdentifier, bool bExpandOnRotation, bool bGTC)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<long>("addCustomVideoContent", new object[5]{activity, sUrl, sIdentifier, bExpandOnRotation, bGTC});
	}
	
	// Add video custom content with Uri
	public static long addCustomVideoContent(string sUrl, string sActionUri, string sIdentifier, bool bExpandOnRotation, bool bGTC)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<long>("addCustomVideoContent", new object[6]{activity, sUrl, sActionUri, sIdentifier, bExpandOnRotation, bGTC});
	}
	
	public static long addCustomFragmentContent (string sActionUri, string sClassName, Dictionary<string,string> arguments, string sIdentifier, bool bGTC, bool bInsertAtHead)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		
		// Get arguments bundle
		AndroidJavaObject argBundle = new AndroidJavaObject("android.os.Bundle");
		if (arguments != null)
		{
			foreach(KeyValuePair<string, string> arg in arguments)
			{
				argBundle.Call("putString", new object[2]{arg.Key, arg.Value});
			}
		}
		
		return pluginClass.CallStatic<long>("addCustomFragmentContent", new object[7]{activity, sActionUri, sClassName, argBundle, sIdentifier, bGTC, bInsertAtHead});
	}

	public static long addUnityFragment (string sActionUri, string sIdentifier, bool bGTC, bool bInsertAtHead)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaUnityAgent");
		
		return pluginClass.CallStatic<long>("addUnityFragmentContent", new object[5]{activity, sActionUri, sIdentifier, bGTC, bInsertAtHead});
	}

	public static long addUnityFragment (string sActionUri, string sIdentifier, bool bGTC, bool bInsertAtHead, String sGameObjectName, String sMethodName, String sMethodParam)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaUnityAgent");

		return pluginClass.CallStatic<long>("addUnityFragmentContent", new object[8]{activity, sActionUri, sIdentifier, bGTC, bInsertAtHead, sGameObjectName, sMethodName, sMethodParam});
	}
	
	// Requests an ad to be inserted into the custom content queue
	public static long customContentAdRequest()
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<long>("customContentAdRequest", new object[1]{activity});
	}
	
	// Removes content from the custom content queue
	public static void removeCustomContent(long id)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		pluginClass.CallStatic("removeCustomContent", new object[2]{activity, id});
	}
	
	public static void removeAllCustomContent()
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		pluginClass.CallStatic("removeAllCustomContent", new object[1]{activity});
	}
	
	public static void flushContentCache()
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		pluginClass.CallStatic("flushContentCache", new object[1]{activity});
	}
	
	public static void trackInstall(String sAdvertiserId)
	{
		print("Tracking Install!");
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject adendaTracker = new AndroidJavaObject("sdk.adenda.tracker.AdendaTracker", new object[2]{activity, sAdvertiserId});
		adendaTracker.Call("trackInstall");
	}
	
	public static long getTotalUserReward (String sUserId)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<long>("getTotalUserReward", new object[2] {activity, sUserId});
	}
	
	// Return whether or not App is opted in
	public static bool isOptedIn()
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<bool>("isOptedIn", new object[1] {activity});
	}
	
	// Return number of customContent entries
	public static long getNumberCustomEntries()
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<long>("getNumberCustomEntries", new object[1] {activity});
	}
	
	// Gets the id of the first entry in the custom content queue
	public static long getFirstRowId()
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<long>("getFirstRowId", new object[1] {activity});
	}
	
	// Gets the id of the last entry in the custom content queue
	public static long getLastRowId()
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<long>("getLastRowId", new object[1] {activity});
	}
	
	// Sets the probability of the app's lock screen appearing instead of the system's default lock screen
	// WARNING: Setting this value to anything other than 100 will cause the app's lock screen to not appear all of the time
	public static void setAdendaRatio(int ratio)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		pluginClass.CallStatic("setAdendaRatio", new object[2] {activity, ratio});
	}
	
	// Gets the probability of the app's lock screen appearing instead of the system's default lock screen
	public static int getAdendaRatio()
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<int>("getAdendaRatio", new object[1] {activity});
	}
	
	public static void setEnableForegroundService(bool bEnable)
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		pluginClass.CallStatic("setEnableForegroundService", new object[2] {activity, bEnable});
	}
	
	public static bool getEnableForegroundService()
	{
		AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaClass pluginClass = new AndroidJavaClass("sdk.adenda.lockscreen.AdendaAgent");
		return pluginClass.CallStatic<bool>("getEnableForegroundService", new object[1] {activity});
	}
	
	// Launch callback methods
	public void onPreStartLockscreen(string unusedMessage)
	{
		OnPreLockscreenStarted();
	}
	
	public void onPostStartLockscreen(string unusedMessage)
	{
		OnPostLockscreenStarted();
	}
	
	public void onPreStopLockscreen(string unusedMessage)
	{
		OnPreLockscreenStopped();
	}
	
	public void onPostStopLockscreen(string unusedMessage)
	{
		OnPostLockscreenStopped();
	}
	
	public void onUserReward(String sUserRewards)
	{
		string[] results = sUserRewards.Split(':');
		onUserNewReward(results[0], Convert.ToInt64(results[1]));
	}	
}