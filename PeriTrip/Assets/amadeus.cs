using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class Amadeus : MonoBehaviour {

	public string url;

	public string apikey = "5ydifrkNfmgRtCgp7lGDA4GYuOtsUlHG";
	public string origin;
	public string one_way = "false";
	public string duration;
	public string direct = "true";
	public string max_price;

//	public Query q;	// CHANGE LIKE IT SHOULD BE SET UP HERE
	private Query q;
	private BackendScript bs;

	public List<string> airports;
//	public List<string> cities;

	// Use this for initialization
	IEnumerator Start () {
		GetComponent<Renderer> ().material.color = new Color(0.5f, 1, 1, 0.1f); //C#

		airports = new List<string> ();
//		cities = new List<string> ();

		q = GameObject.Find("Query").GetComponent<Query>();
		bs = GameObject.Find ("BackendScript").GetComponent<BackendScript> ();

//		Debug.Log ("Hello");

		// will be commenting this out later and calling it after set max price probs
		return CallQuery();
	}

	// Update is called once per frame
	void Update () {
		
	}

	void SetOrigin(string o) {
		origin = o;
	}

	void SetDuration(string d) {
		duration = d;
	}

	void SetMaxPrice(string mp) {
		max_price = mp;
	}

	IEnumerator CallQuery() {
//		string monthVar = System.DateTime.Now.Month.ToString ();
//		string dateVar = System.DateTime.Now.Date.ToString ();
//		if (monthVar < 10)
//			monthVar = "0" + monthVar;
//		if (dateVar < 10)
//			dateVar = "0" + dateVar;

//		Debug.Log ("Pre using");

		using (var wc = new System.Net.WebClient ()) {
//			Debug.Log ("Post using");

			url = "http://api.sandbox.amadeus.com/v1.2/flights/inspiration-search?" 
				+ "apikey=" + apikey
				+ "&origin=" + origin 
				+ "&one-way=" + one_way 
				+ "&duration=" + duration 
				+ "&direct=" + direct
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
		
			for (int i = 0; i < len; i++) {
				string cityurl = "https://api.sandbox.amadeus.com/v1.2/location/" 
					+ q.results [i].destination
					+ "?apikey="
					+ apikey;

				var tempJsonString = wc.DownloadString (cityurl);
				var N = SimpleJSON.JSON.Parse (tempJsonString);

				string city = N ["city"] ["name"];

				airports.Add (q.results [i].destination);
				bs.reference.Child("peritrip").Child(q.results[i].destination).Child("city").SetValueAsync(city);
//				cities.Add (city);
			}

			WWW www = new WWW (url);
			yield return www;

		} 
	}

}