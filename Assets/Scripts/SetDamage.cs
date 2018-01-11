using UnityEngine;
using System.Collections;

public class SetDamage : MonoBehaviour {

	public float stime = 1f;
	public float speed = 1f;
	float now;
	// Use this for initialization
	void Start () {
		now = 0f;
		//Debug.Log (gameObject.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position += new Vector3(0,Time.deltaTime * speed,0);
		now += Time.deltaTime;
		if (now > stime)
			DestroyObject (gameObject);
	}

	public void Set(int damage,bool is_hit,Vector3 panda){
		gameObject.transform.position = panda + new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f));
		gameObject.GetComponent<TextMesh> ().text = damage.ToString ();
		if (is_hit) {
			gameObject.GetComponent<TextMesh> ().color = Color.red;	
			gameObject.GetComponent<TextMesh> ().fontSize = (int)(gameObject.GetComponent<TextMesh> ().fontSize * 1.4f);
		}
	}

	public void SetUrgent(int damage,bool is_hit,Vector3 panda){
		gameObject.transform.position = panda + new Vector3(Random.Range(-12f,12f),Random.Range(-12f,12f),Random.Range(-12f,12f));
		gameObject.GetComponent<TextMesh> ().text = damage.ToString ();
		if (is_hit) {
			gameObject.GetComponent<TextMesh> ().color = Color.red;	
			gameObject.GetComponent<TextMesh> ().fontSize = (int)(gameObject.GetComponent<TextMesh> ().fontSize * 1.4f);
		}
		gameObject.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
	}

}
