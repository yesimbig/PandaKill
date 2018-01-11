using UnityEngine;
using System.Collections;

public class bg_size : MonoBehaviour {

	float iniW,iniH;

	// Use this for initialization
	void Start () {
		iniW = gameObject.GetComponent<RectTransform> ().rect.width;
		iniH = gameObject.GetComponent<RectTransform> ().rect.height;

		float screenW = Screen.width;
		float screenH = Screen.height;

		if (iniH / iniW < screenH / screenW) {
			float dd = (screenH / screenW) / (iniH / iniW);
			gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2(iniW*dd,iniH*dd);
		}


	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
