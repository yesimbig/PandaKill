using UnityEngine;
using System.Collections;

public class ARItemSetting : MonoBehaviour {

	//just handle the button item in AR

	public int myid;
	public GameObject sObj;

	bool is_select = false;

	public void onARItemClick(){
		Debug.Log (myid);
		ARItemBoxSetting.nowId = myid;
	}

	public void setARid(int id){
		myid = id;
	}

	public void setSelection(bool se){
		is_select = se;
		if (is_select) {
			sObj.SetActive (true);
		} else {
			sObj.SetActive (false);
		}
	}

}
