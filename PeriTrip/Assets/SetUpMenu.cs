using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetUpMenu : MonoBehaviour {
	StoreUserInfo userInfo;
	List<string> myCityList;
	List<string> myAirportList;
	BackendScript script;
	// Use this for initialization
	void Start () {
		script = GameObject.Find("BackendScript").GetComponent<BackendScript> ();
		userInfo = GameObject.Find ("UserInfo").GetComponent<StoreUserInfo> ();
		myCityList = userInfo.getCityList ();
		myAirportList = userInfo.getAirportList ();

		//myAirportList = new List<string> ();
		GameObject locationContainer = GameObject.Find (myCityList.Count.ToString () + " Locations");


		Debug.Log (myAirportList.Count);

		for( int index = 1; index <= myAirportList.Count; index++) {
			//Debug.Log ("count: " + count);
			int num = index+myAirportList.Count + 3;
			GameObject location = GameObject.Find("Location " + num.ToString());

			foreach (Transform trans in location.GetComponentInChildren<Transform>()) {
				trans.gameObject.SetActive (true);
				foreach (Transform trans2 in trans.gameObject.GetComponentInChildren<Transform>()) {
					trans2.gameObject.SetActive (true);
				}
			}

			location.GetComponentInChildren<TextMesh> ().text = myCityList[index-1];

			string vr = script.GetVrImage (myAirportList [index - 1]);
			Debug.Log (vr);
			if (vr == "") {
				vr = "www.editorial.designtaxi.com/editorial-images/news-uihistory16122015/2.jpg";
			}
			StartCoroutine(location.GetComponentInChildren<SetLocationImage> ().SetImage (vr));
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
