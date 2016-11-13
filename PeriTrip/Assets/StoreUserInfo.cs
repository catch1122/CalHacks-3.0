using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreUserInfo : MonoBehaviour {
	List<string> myCityList;
	List<string> myAirportList;
	private static StoreUserInfo instance = null;
	public static StoreUserInfo Instance {
		get { return instance; }
	}
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setCityList(List<string> cityList){
		myCityList = cityList;
	}
	public void setAirportList(List<string> airportList){
		myAirportList = airportList;
	}
	public List<string> getCityList(){
		return myCityList;
	}
	public List<string> getAirportList(){
		return myAirportList;
	}
}
