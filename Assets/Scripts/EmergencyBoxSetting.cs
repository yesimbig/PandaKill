using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EmergencyBoxSetting : MonoBehaviour {

	public Text messageText;
	public string message = "超大型熊貓出沒! 是否擊退牠?";
	public System.Action act = null,esc = null;

	// Use this for initialization
	void Start () {
		if (message.Length > 0) {
			messageText.text = message;
		}
	}
	
	public void onAttackBtnClick(){
		//DestroyObject (gameObject);
		if (act != null)
			act();
	}

	public void onEscapeBtnClick(){
		DestroyObject (gameObject);
		if (esc != null)
			esc();
	}
}