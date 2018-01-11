using UnityEngine;
using System.Collections;

public class cloudMotion : MonoBehaviour {

	public float speed = 0.1f;
	public Camera camera;
	float screenWidth;
	float randomize = 1f;
	// Use this for initialization
	void Start () {
		screenWidth = camera.ScreenToWorldPoint(new Vector2(Screen.width,0)).x - camera.ScreenToWorldPoint(new Vector2(0,0)).x;
		gameObject.transform.position += new Vector3 (Random.Range(-randomize, randomize), Random.Range (-randomize, randomize),0); 


	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position -= new Vector3 (speed, 0, 0);
		if (camera.WorldToScreenPoint (gameObject.transform.position).x < -3f) {
			Debug.Log (screenWidth);
			gameObject.transform.position += new Vector3(screenWidth+3,0,0);
		}
	}
}
