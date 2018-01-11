using UnityEngine;
using System.Collections;
using System;

public class PandaMove : MonoBehaviour {

	public Animator panda;
	public float lateTrigger = 4f;
	public float distance = 6.4301f;
	public GameObject Head;

	Vector3 initrack;


	float trigger; 
	public float adjust_time = 10f;
	float time = 0f;


	// Use this for initialization
	void Start () {
	//	panda = gameObject.GetComponent<Animator> ();
		trigger = lateTrigger;


		Vector3 v = Head.transform.eulerAngles;

		if (Head.transform.eulerAngles.x > 63f && Head.transform.eulerAngles.x <= 90f) {
			v.x = 63f;
		}else if (Head.transform.eulerAngles.x > 90f && Head.transform.eulerAngles.x <= 117f) {
			v.x = 117f;
		}else if(Head.transform.eulerAngles.x > 243f && Head.transform.eulerAngles.x <= 270f){
			v.x = 243f;
		}else if(Head.transform.eulerAngles.x > 270f && Head.transform.eulerAngles.x <= 297f){
			v.x = 297f;
		}

		v.x = 0; v.z = 0;

		transform.position = polarToXYZ(v,distance);
		transform.rotation = Quaternion.Euler(v);

		initrack = v;
		Debug.Log (v);
	}




	// Update is called once per frame
	void Update () {

		time += Time.deltaTime;

		if (time >= adjust_time) {
			Debug.Log ("adjust!!");
			time = 0f;
			Vector3 v = Head.transform.eulerAngles;
			
			if (Head.transform.eulerAngles.x > 63f && Head.transform.eulerAngles.x <= 90f) {
				v.x = 63f;
			}else if (Head.transform.eulerAngles.x > 90f && Head.transform.eulerAngles.x <= 117f) {
				v.x = 117f;
			}else if(Head.transform.eulerAngles.x > 243f && Head.transform.eulerAngles.x <= 270f){
				v.x = 243f;
			}else if(Head.transform.eulerAngles.x > 270f && Head.transform.eulerAngles.x <= 297f){
				v.x = 297f;
			}
			transform.position = polarToXYZ(v,distance);
			transform.rotation = Quaternion.Euler(v);
		}

		/*
		Vector3 v = Head.transform.eulerAngles;

		if (Head.transform.eulerAngles.x > 63f && Head.transform.eulerAngles.x <= 90f) {
			v.x = 63f;
		} else if (Head.transform.eulerAngles.x > 90f && Head.transform.eulerAngles.x <= 117f) {
			v.x = 117f;
		} else if (Head.transform.eulerAngles.x > 243f && Head.transform.eulerAngles.x <= 270f) {
			v.x = 243f;
		} else if (Head.transform.eulerAngles.x > 270f && Head.transform.eulerAngles.x <= 297f) {
			v.x = 297f;
		}

		Vector3 newini = initrack;

		if (anglediffer( initrack.x,v.x) > 40f) {
			newini.x = v.x+40f;
			transform.position = polarToXYZ (newini, distance);
			transform.rotation = Quaternion.Euler (newini);
		} else if ( anglediffer(initrack.x, v.x) < -14f) {
			newini.x = v.x-14f;
			transform.position = polarToXYZ (newini, distance);
			transform.rotation = Quaternion.Euler (newini);
		} 
		if ( anglediffer(initrack.y , v.y) > 40f) {
			newini.y = v.y+40f;
			transform.position = polarToXYZ (newini, distance);
			transform.rotation = Quaternion.Euler (newini);
		} else if ( anglediffer(initrack.y , v.y)  < -40f) {
			newini.y = v.y-40f;
			transform.position = polarToXYZ (newini, distance);
			transform.rotation = Quaternion.Euler (newini);
		} 
		if ( anglediffer(initrack.z , v.z) > 40f) {
			newini.z = v.z+40f;
			transform.position = polarToXYZ (newini, distance);
			transform.rotation = Quaternion.Euler (newini);
		} else if ( anglediffer(initrack.z , v.z) < -40f) {
			newini.z = v.z-40f;
			transform.position = polarToXYZ (newini, distance);
			transform.rotation = Quaternion.Euler (newini);
		}
		initrack = newini;


		Debug.Log (newini);
		*/

		trigger -= Time.deltaTime;
		if (trigger <= 0f) {
			float x = UnityEngine.Random.Range (0f, 1f);
			if (x <= 0.2f)
				panda.SetTrigger ("attack1");
			else if (x <= 0.4f)
				panda.SetTrigger ("attack2");
			else if (x <= 0.5f)
				panda.SetTrigger ("crazy");		
		
			trigger = lateTrigger;
		
		}


	}


	public void setPandaHP(int hp){
		panda.SetInteger ("pandaHP", hp);	
	}

	public void setDead(){
		panda.SetTrigger ("dead");	
	}

	float anglediffer(float a,float b){
		if (a - b > 180f)
			return a - 360f - b;
		if (a - b < 180f)
			return a - b + 360f;
		else
			return a - b;
	
	}

	Vector3 polarToXYZ(Vector3 q, float d){ 

		double x, y, z;
		x = d * Math.Sin (q.y * Math.PI / 180f) ;
		y = -d * Math.Cos ((90f-q.x) * Math.PI / 180f);
		z = d * Math.Cos (q.y * Math.PI / 180f) * Math.Sin((90f- q.x)*  Math.PI / 180f);

		Debug.Log ("x = " + x + ",y = " + y + ",z = " + z); 

		return new Vector3 ((float)x,(float)y,(float)z);
	
	
	}
}
