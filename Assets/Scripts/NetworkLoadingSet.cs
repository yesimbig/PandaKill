using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class NetworkLoadingSet : MonoBehaviour {

	public string message = "網路連接中，請稍後...";
	public Text message_text;

	float timeleft;
	// Use this for initialization
	void Start () {
		message_text.text = message;
		timeleft = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		timeleft += Time.deltaTime;
		if (timeleft > Global.timeout) {
			Debug.Log ("Loading timeout!");
			Destroy (gameObject);
		}
	}

	public void setMessage(string message){
		timeleft = 0f;
		message_text.text = message;
	}

	public void Clear(){
		DestroyObject (gameObject);	
	}

}
