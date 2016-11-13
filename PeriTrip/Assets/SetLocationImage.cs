using UnityEngine;
using System.Collections;

public class SetLocationImage : MonoBehaviour {
	public string url = "";
	// Use this for initialization
	void Start () {
		//test


	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public IEnumerator SetImage(string imgurl){
		
		url = imgurl;
		Debug.Log (gameObject.name+" set url: " + url);
		WWW www = new WWW(imgurl);
		yield return www;
		Renderer renderer= GetComponent<Renderer>();
		renderer.material.mainTexture = www.texture;
	}

}
