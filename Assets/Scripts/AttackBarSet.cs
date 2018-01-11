using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackBarSet : MonoBehaviour {

	public GameObject pointer;
	public Sprite[] select;

	float allwidth;
	Vector3 iniposition;
	public float speed = 8f;
	bool dir = true;
	bool gets = false;
	// Use this for initialization
	void Start () {
		pointer.GetComponent<SpriteRenderer> ().sprite = select [0];
		iniposition = pointer.transform.localPosition;
		pointer.transform.localPosition = iniposition + new Vector3(Random.Range (-2.7f, 2.7f),0,0);
	}
	
	// Update is called once per frame
	void Update () {
		if (gets)
			return;
		if (pointer.transform.localPosition.x > iniposition.x + 3f)
			dir = false;
		else if(pointer.transform.localPosition.x < iniposition.x - 3f)	dir = true;

		if (dir)
			pointer.transform.localPosition += new Vector3 (speed * Time.deltaTime, 0, 0);
		else
			pointer.transform.localPosition -= new Vector3 (speed * Time.deltaTime, 0, 0);
	}

	public int getPoint(int cut){
		gameObject.GetComponent<AudioSource> ().Stop ();
		gets = true;
		pointer.GetComponent<SpriteRenderer> ().sprite = select [1];
		float pos = pointer.transform.localPosition.x;

		if (pos > iniposition.x + 3f) {
			pointer.transform.localPosition = new Vector3 (iniposition.x + 3f, pointer.transform.localPosition.y, pointer.transform.localPosition.z);
		} else if (pos < iniposition.x - 3f) {
			pointer.transform.localPosition = new Vector3 (iniposition.x - 3f, pointer.transform.localPosition.y, pointer.transform.localPosition.z);
		}

		float length = 6f;
		int based = 4*cut-2;

		StartCoroutine (end());

		for(int i=0;i<cut;i++){
			if( System.Math.Abs(pos - iniposition.x) <= length * (2f*(float)i+1f)/ (float)(based)){
				Debug.Log (cut-i);
				return cut-i;
			}
		}
		return 0;
	}

	IEnumerator end(){
		yield return new WaitForSeconds(0.7f);
		gameObject.SetActive (false);
		gets = false;
		pointer.transform.localPosition = iniposition + new Vector3(Random.Range (-2.7f, 2.7f),0,0);
		//Debug.Log ("HERE~~");
	}

}
