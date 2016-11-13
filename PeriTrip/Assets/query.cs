using UnityEngine;
using System;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;
//using Newtonsoft.Json.Linq;

[Serializable]
public class Query : MonoBehaviour {
	public struct Result {
		public string destination;
		public string departure_date;
		public string return_date;
		public float price;
		public string airline;
	}

	public bool exists;

	public string origin;
	public string currency;

	public List<Result> results;

	void Start() {
		results = new List<Result> ();
	}

	// checks if query led to valid results
	public bool JsonExists(string jsonString) {
		var N = SimpleJSON.JSON.Parse (jsonString);

		origin = N ["origin"].Value;

		if (origin == null) {
			exists = false;
		}

		exists = true;
		return exists;
	}

	// will only be called if the result exists
	public Query CreateFromJson(string jsonString) {
		var N = SimpleJSON.JSON.Parse (jsonString);

		origin = N ["origin"].Value;
		currency = N ["currency"].Value;

		JSONArray items = (JSONArray)N ["results"];

//		Debug.Log (items.Count);

		for (int i = 0; i < items.Count; i++)
		{
			Result r = new Result();

			r.airline = N ["results"] [i] ["airline"];
			r.departure_date = N ["results"] [i] ["departure_date"];
			r.destination = N ["results"] [i] ["destination"];
			r.return_date = N ["results"] [i] ["airline"];
			r.price = float.Parse(N ["results"] [i] ["price"]);

			results.Add (r);
		}

		return this;
	}



}
