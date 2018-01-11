using UnityEngine;
using System.Collections;
using System;

public class ShowWhichFox : MonoBehaviour {

	public GameObject Fox_normal,Fox_eat,Fox_sleep;
	public FoxAction[] fa;
	int state;

	// Use this for initialization
	void Start () {

		DateTime dt = DateTime.Now;
		if (dt.Hour >= 17 && dt.Hour <= 18) {
			int x = UnityEngine.Random.Range (0, 10);
			if (x < 7) {
				state = 1;				
			} else
				state = 0;	
		} else if (dt.Hour <= 6) {
			int x = UnityEngine.Random.Range (0, 10);
			if (x < 8) {
				state = 2;				
			} else
				state = 0;	
		} else
			state = 0;


		if (state == 0) {
			Fox_normal.SetActive (true);
			Fox_eat.SetActive (false);
			Fox_sleep.SetActive (false);
		} else if (state == 1) {
			Fox_eat.SetActive (true);
			Fox_normal.SetActive (false);
			Fox_sleep.SetActive (false);
		} else {
			Fox_eat.SetActive (false);
			Fox_normal.SetActive (false);
			Fox_sleep.SetActive (true);
		}
	}

	public void onClick(){
		fa[state].on_action(state);
	}

}
