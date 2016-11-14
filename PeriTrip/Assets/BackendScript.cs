using UnityEngine;
using System.Collections;
using Firebase;
using Firebase.Database;
using Firebase.Unity;
using Firebase.Analytics;
using Firebase.Unity.Editor;
using System.Collections.Generic;

public class BackendScript : MonoBehaviour {


	public DatabaseReference reference;
	private static BackendScript instance = null;
	public static BackendScript Instance {
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

//	System.Net.WebClient webClient;

	// Use this for initialization
	void Start () {
		// Set this before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://peritrip.firebaseio.com/");
		reference = FirebaseDatabase.DefaultInstance.RootReference;
//		webClient = new System.Net.WebClient ();
	}

	public string GetCity(string airport) {
		DataSnapshot refer = reference.Child (airport).GetValueAsync ().Result;
		string FML = refer.Child ("city").Value.ToString();

		return FML;


//		return reference.Child (airport).Child ("city").ToString();
	}

	public string GetPreviewImage(string airport) {
//		webClient.DownloadFile (reference.Child("peritrip").Child(airport).Child("prevUrl"), localFileName);
		return reference.Child(airport).Child("prevUrl").ToString();
	}

	public string GetVrImage(string airport) {
		
//		webClient.DownloadFile (reference.Child("peritrip").Child(airport).Child("vrUrl"), localFileName);
//		DatabaseReference refer = reference.Child(airport).Child("vrUrl");
//		DataSnapshot ds = reference.Child (airport).GetValueAsync();

		//string FML = ""; // = ds.GetValue(true).ToString();

//
//
//		FirebaseDatabase.DefaultInstance
//			.GetReference("peritrip").OrderByChild("peritrip")
//			.ValueChanged += (object sender, ValueChangedEventArgs args) => {
//			if (args.DatabaseError != null) {
//				Debug.LogError(args.DatabaseError.Message);
//				return;
//			}
//			// Do something with the data in args.Snapshot
//		};
		/*
		 	string url = reference.Child (airport).Reference.ToString();
		url = url.Substring (0, url.Length - 8) + url.Substring (url.Length - 4);


		Debug.Log("THIS IS THE MOFO URL" + url);


		using (var wc = new System.Net.WebClient ()) {
			var tempJsonString = wc.DownloadString (url);
			var N = SimpleJSON.JSON.Parse (tempJsonString);


			Debug.Log ("FMLLLLLLLLLLL" + N);

			FML = N ["vrUrl"];

		}

		Debug.Log ("THIS IS THE FRIGGIN URL: " + FML);

		 * */

		//		FirebaseDatabase.DefaultInstance
		//			.GetReference(airport)
		//			.GetValueAsync().ContinueWith(task => {
		//				if (task.IsFaulted) {
		//					// Handle the error...
		//				}
		//				else if (task.IsCompleted) {
		//					DataSnapshot snapshot = task.;
		//					// Do something with snapshot...
		//					FML = snapshot.GetValue(true).ToString();
		//
		//				}
		//			});

		DataSnapshot refer = reference.Child (airport).GetValueAsync ().Result;
		string FML = refer.Child ("vrUrl").Value.ToString();

		Debug.Log ("FMLLLLLLLLLL" + refer.Child("vrUrl").Value);
		Debug.Log ("ARGHHHHHHHHH" + FML);

		return FML;
	}
	struct Locs {
		string city;
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
