using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MessageSet : MonoBehaviour {

	public string message = "";
	public Text message_text;
	public System.Action act = null;
	// Use this for initialization
	void Start () {
		message_text.text = message;
	}

	public void onButtonClick(){
		DestroyObject (gameObject);
		if (act != null)
			act ();
	}
}
