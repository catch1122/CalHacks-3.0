using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Amadeus : MonoBehaviour {

	public string url;
	Text console;
	public string apikey = "";
	public string origin;
	public string one_way = "false";
	public string duration;
	public string direct = "true";
	public string max_price;

//	public Query q;	// CHANGE LIKE IT SHOULD BE SET UP HERE
	private Query q;
	private BackendScript bs;

	public List<string> airports;
	public List<string> cities;

	// Use this for initialization
	void Start () {
//		GetComponent<Renderer> ().material.color = new Color(0.5f, 1, 1, 0.1f); //C#
		console = GameObject.Find("Console").GetComponent<Text>();
		airports = new List<string> ();
		cities = new List<string> ();

		q = GameObject.Find("Query").GetComponent<Query>();
		bs = GameObject.Find ("BackendScript").GetComponent<BackendScript> ();

		Debug.Log ("Hello");
		//console.text += "Hello";
		// will be commenting this out later and calling it after set max price probs
//		return CallQuery();
	}

	// Update is called once per frame
	void Update () {
		
	}

//	public IEnumerator CallQuery() {
//		return InternalCallQuery ();
//	}

	public void SetOrigin(string o) {
		origin = o;
	}

	public void SetDuration(string d) {
		duration = d;
	}

	public void SetMaxPrice(string mp) {
		max_price = mp;
	}

	public void CallQuery() {
		
		console.text += "\n after gameobject find";
		//SceneManager.LoadScene ("Location Menu", LoadSceneMode.Single);
//		string monthVar = System.DateTime.Now.Month.ToString ();
//		string dateVar = System.DateTime.Now.Date.ToString ();
//		if (monthVar < 10)
//			monthVar = "0" + monthVar;
//		if (dateVar < 10)
//			dateVar = "0" + dateVar;
		//console.text += "\n PRe using";
		Debug.Log ("Pre using");

		using (var wc = new System.Net.WebClient ()) {
			Debug.Log ("Origin: " + origin);
			Debug.Log ("Duration: " + duration);
			Debug.Log ("Max Price: " + max_price);

			url = "http://api.sandbox.amadeus.com/v1.2/flights/inspiration-search?" 
				+ "apikey=" + apikey
				+ "&origin=" + origin 
				+ "&one-way=false"
				+ "&duration=" + duration 
				+ "&direct=true"
				+ "&max_price=" + max_price;

//			url = "http://api.sandbox.amadeus.com/v1.2/flights/" +
//				"inspiration-search?apikey=5ydifrkNfmgRtCgp7lGDA4GYuOtsUlHG&" +
//				"origin=LAX&one-way=false&duration=3&direct=true&max_price=100";
			
			var jsonString = wc.DownloadString (url);

//			Debug.Log (jsonString);

			q.CreateFromJson (jsonString);
			int len;
			if (q.results.Count < 4)
				len = q.results.Count;
			else
				len = 4;
			console.text += "\nlength: " + len;


		
			for (int i = 0; i < len; i++) {
				using (var wcTwo = new System.Net.WebClient ()) {
					
					string cityurl = "https://api.sandbox.amadeus.com/v1.2/location/"
					                + q.results [i].destination
						+ "?apikey=" + apikey;
//				console.text += "\n" + cityurl;
					var tempJsonString = wcTwo.DownloadString (cityurl);

//					console.text += "\njson: " + tempJsonString;
					
//				console.text += "\nha: " + q.results [i].destination;
//					console.text += "\napikey: " + apikey;
					var N = SimpleJSON.JSON.Parse (tempJsonString);

//					console.text += "\nTEMPJSON: " + N;
//				console.text += "\nINSIDE LOOP";

//				string city = "PORTLAND";//bs.GetCity (q.results [i].destination);
//				console.text += "\nCITY: " + city;
//				Debug.Log ("CITY: " + city);
					string city = N ["city"] ["name"];
//					console.text += "\n" + city;

					airports.Add (q.results [i].destination);
					console.text += "\n firebase: " + q.results [i].destination;
					if (bs.reference.Child (q.results [i].destination) == null) {
						bs.reference.Child (q.results [i].destination).Child ("city").SetValueAsync (city);
						bs.reference.Child (q.results [i].destination).Child ("vrUrl").SetValueAsync ("");
					}

					cities.Add (city);

					Debug.Log (city);
					console.text += "\ncity " + i + ": "+ city; 
				}
			}

//			WWW www = new WWW (url);
//			yield return www;

			GameObject.Find("UserInfo").GetComponent<StoreUserInfo>().setAirportList(airports);
			GameObject.Find("UserInfo").GetComponent<StoreUserInfo>().setCityList(cities);
			console.text += "\n after gameobject find";
//			SceneManager.LoadScene ("Location Menu", LoadSceneMode.Single);
			SceneManager.LoadSceneAsync("Location Menu", LoadSceneMode.Single);

		} 
	}

}