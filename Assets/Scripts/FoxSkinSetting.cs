using UnityEngine;
using System.Collections;

public class FoxSkinSetting : MonoBehaviour {

	public GameObject[] clothes_blue;
	public GameObject[] clothes_yellow;
	public GameObject[] clothes_red;
	public GameObject[] clothes_green;

	public GameObject[] eyes;
	public GameObject[] tail;

	public static int ncolor,nwearing,neye,ntail;

	public int fight = 0;

	bool trigger = false;

	// Use this for initialization
	void Start () {
		ncolor = 0;
		nwearing = 0;
		neye = 0;
		ntail = 0;
		//trigger = true;
	}

	void Update(){
	/*
		if (trigger) {
			for (int i = 0; i < 4; i++) {
				if (i == nwearing) {
					clothes_blue [i].SetActive (true);
					clothes_yellow [i].SetActive (true);
					clothes_red [i].SetActive (true);
					clothes_green [i].SetActive (true);
				} else {
					clothes_blue [i].SetActive (false);
					clothes_yellow [i].SetActive (false);
					clothes_red [i].SetActive (false);
					clothes_green [i].SetActive (false);
				}
			
				if (i == neye)
					eyes [i].SetActive (true);
				else
					eyes [i].SetActive (false);

				if (fight == 1 && (neye == 0 || neye == 3))
					eyes [3].SetActive (true);

				if(fight == 2 && (neye == 0 || neye == 3 || neye == 1)){
					eyes [3].SetActive (true);
				}


				if (i == ntail)
					tail [i].SetActive (true);
				else
					tail [i].SetActive (false);
			}

			if (ncolor == 0) {
				clothes_blue [nwearing].SetActive (false);
				clothes_yellow [nwearing].SetActive (false);
				clothes_red [nwearing].SetActive (true);
				clothes_green [nwearing].SetActive (false);
			} else if (ncolor == 1) {
				clothes_blue [nwearing].SetActive (false);
				clothes_yellow [nwearing].SetActive (true);
				clothes_red [nwearing].SetActive (false);
				clothes_green [nwearing].SetActive (false);
			} else if (ncolor == 2) {
				clothes_blue [nwearing].SetActive (false);
				clothes_yellow [nwearing].SetActive (false);
				clothes_red [nwearing].SetActive (false);
				clothes_green [nwearing].SetActive (true);
			} else if (ncolor == 3) {
				clothes_blue [nwearing].SetActive (true);
				clothes_yellow [nwearing].SetActive (false);
				clothes_red [nwearing].SetActive (false);
				clothes_green [nwearing].SetActive (false);
			}
			trigger = false;
		}
*/

	}

	public void set(int e,int t,int c,int w){
		neye = e-1;
		ntail = t-1;
		ncolor = c-1;
		nwearing = w-1;

		for (int i = 0; i < 4; i++) {
			if (i == nwearing) {
				clothes_blue [i].SetActive (true);
				clothes_yellow [i].SetActive (true);
				clothes_red [i].SetActive (true);
				clothes_green [i].SetActive (true);
			} else {
				clothes_blue [i].SetActive (false);
				clothes_yellow [i].SetActive (false);
				clothes_red [i].SetActive (false);
				clothes_green [i].SetActive (false);
			}
			
			if (i == neye)
				eyes [i].SetActive (true);
			else
				eyes [i].SetActive (false);
			
			if (fight == 1 && (neye == 0 || neye == 3))
				eyes [3].SetActive (true);
			
			if(fight == 2 && (neye == 0 || neye == 3 || neye == 1)){
				eyes [3].SetActive (true);
			}
			
			
			if (i == ntail)
				tail [i].SetActive (true);
			else
				tail [i].SetActive (false);
		}
		
		if (ncolor == 0) {
			clothes_blue [nwearing].SetActive (false);
			clothes_yellow [nwearing].SetActive (false);
			clothes_red [nwearing].SetActive (true);
			clothes_green [nwearing].SetActive (false);
		} else if (ncolor == 1) {
			clothes_blue [nwearing].SetActive (false);
			clothes_yellow [nwearing].SetActive (true);
			clothes_red [nwearing].SetActive (false);
			clothes_green [nwearing].SetActive (false);
		} else if (ncolor == 2) {
			clothes_blue [nwearing].SetActive (false);
			clothes_yellow [nwearing].SetActive (false);
			clothes_red [nwearing].SetActive (false);
			clothes_green [nwearing].SetActive (true);
		} else if (ncolor == 3) {
			clothes_blue [nwearing].SetActive (true);
			clothes_yellow [nwearing].SetActive (false);
			clothes_red [nwearing].SetActive (false);
			clothes_green [nwearing].SetActive (false);
		}



		//trigger = true;
	}
}
