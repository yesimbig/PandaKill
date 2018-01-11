using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MessageBoxSetting : MonoBehaviour {

	public Text Info;

	public string message = "";
	public System.Action act = null;
	public bool is_info = false;
	// Use this for initialization
	void Start () {
		Canvas canvas = (Canvas)gameObject.GetComponentsInChildren<Canvas>()[0];
		Text message_text = (Text)canvas.GetComponentsInChildren<Text>()[0];
		message_text.text = message;
		if (is_info)
			Info.text = "INFO";
		else {
			Info.text = "ERROR";
		}
	}

	public void onButtonClick(){
		DestroyObject (gameObject);
		Debug.Log ("CLK");
		if (act != null)
			act ();

	}
}
