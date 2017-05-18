using UnityEngine;
using System.Collections;

public class RewardReceiver : MonoBehaviour 
{
	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	void OnEnable()
	{
		print("Registering for Adenda Reward Events");
		AdendaPlugin.onUserNewReward += handleOnUserNewReward;
	}

	void onDisable()
	{
		print ("Unregistering for Adenda Reward Events");
		AdendaPlugin.onUserNewReward -= handleOnUserNewReward;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void handleOnUserNewReward(string sUser, long amount)
	{
		print ("HANDLED Adenda Reward Event: " + amount);
		int totalBottles = PlayerPrefs.GetInt("TotalBottles");
		PlayerPrefs.SetInt("TotalBottles",totalBottles + (int)amount);
	}
}
