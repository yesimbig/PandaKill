using UnityEngine;
using System.Collections;

public class light_one_destroy : MonoBehaviour {

	public float stime = 1f;

	float now;
	// Use this for initialization
	void Start () {
		now = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		now += Time.deltaTime;
		if (now > stime)
			DestroyObject (gameObject);
	}
}
