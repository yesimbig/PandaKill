using UnityEngine;
using System.Collections;

public class setHit : MonoBehaviour {

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
	
	public void Set(Vector3 panda){
		gameObject.transform.position = panda + new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f));
	}

	public void SetUrgent(int type,Vector3 panda){
		gameObject.transform.position = panda + new Vector3(Random.Range(-12f,12f),Random.Range(-12f,12f),Random.Range(-12f,12f));
		gameObject.transform.localScale = new Vector3 (7f, 7f, 7f);
	}

}
