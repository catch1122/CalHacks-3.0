using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections.Generic;

public class BackendScript : MonoBehaviour {

	// Set this before calling into the realtime database.
	FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://peritrip.firebaseio.com/");
	public DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

//	System.Net.WebClient webClient;

	// Use this for initialization
	void Start () {
//		webClient = new System.Net.WebClient ();
	}

	string GetCity(string airport) {
		return reference.Child ("peritrip").Child (airport).Child ("city");
	}

	string GetPreviewImage(string airport, string localFileName) {
//		webClient.DownloadFile (reference.Child("peritrip").Child(airport).Child("prevUrl"), localFileName);
		return reference.Child("peritrip").Child(airport).Child("prevUrl");
	}

	string GetVrImage(string airport, string localFileName) {
//		webClient.DownloadFile (reference.Child("peritrip").Child(airport).Child("vrUrl"), localFileName);
		return reference.Child("peritrip").Child(airport).Child("vrUrl");
	}

//	void WriteLocation(string airport, string city, string imgPrev, string imgVR) {
//		Location loc = new Location(city, imgPrev, imgVR);
//		string json = JsonUtility.ToJson(user);
//
//		reference.Child("peritrip").Child(airport).SetRawJsonValueAsync(json);
//	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
