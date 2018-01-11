using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UrgentSkillHandler : MonoBehaviour {


	//triggers
	static bool hundred = false,dadada = false;

	public GameObject light,thunder,ray;
	public GameObject OP1,OP2,OP3;

	//particle triggers
	public static bool Lightning = false,Thunder = false,ray_trigger = false;

	public GameObject attack_bar;


	// Use this for initialization
	void Start () {
		hundred = false;
		dadada = false;
		Lightning = false;
		Thunder = false;
		ray_trigger = false;
	}

	void Update(){
		if (hundred) {
			hundred = false;
			attack_bar.SetActive (true);
		}
		if (dadada) {
			dadada = false;
			UrgentSkillItem.trigger = attack_bar.GetComponent<AttackBarSet> ().getPoint ((int)Data_Skills.Skills_list [4].other);
		}
	
		//設定particle
		if (Lightning && !light.GetComponent<ParticleSystem>().isPlaying) {
			light.SetActive (true);
			light.GetComponent<ParticleSystem>().Play();
			Lightning = false;
			StartCoroutine(delayStopParticle(light.GetComponent<ParticleSystem>() ,2f));
		}
		if (Thunder ) {			
			thunder.SetActive (true);
			if(thunder.GetComponent<ParticleSystem>().isPlaying)thunder.GetComponent<ParticleSystem>().Stop();
			thunder.GetComponent<ParticleSystem>().Play();
			Thunder = false;
			StartCoroutine(delayStopParticle(thunder.GetComponent<ParticleSystem>() ,0.8f));
		}
		if (ray_trigger) {
			ray.SetActive (true);
			ray.GetComponent<ParticleSystem>().Play();
			ray_trigger = false;
			StartCoroutine(delayStopParticle(ray.GetComponent<ParticleSystem>() ,0.8f));
		}

		//設定火焰特效
		if (UrgentEventHandler.OP_power >= 0.5f) {
			OP3.SetActive (true);
		} else if (UrgentEventHandler.OP_power > 0.3f) {
			OP2.SetActive (true);
		} else if (UrgentEventHandler.OP_power > 0) {
			OP1.SetActive (true);
		} else {
			OP1.SetActive (false);
			OP2.SetActive (false);
			OP3.SetActive (false);
			//impact.SetActive(false);
		}
	
	}

	public static void on_hundred(){
		hundred = true;
	}

	public static void on_dadada(){
		dadada = true;
	}

	public static void on_Lightning(){
		Lightning = true;
	}

	public static void on_Thunder(){
		Thunder = true;
	}

	public static void on_ray_trigger(){
		ray_trigger = true;
	}

	IEnumerator delayStopParticle(ParticleSystem ps,float time){
		yield return new WaitForSeconds (time);
		ps.Stop ();	
	}


}
