using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CameraBG : MonoBehaviour {

	static public WebCamTexture c;
	static public bool is_start = false;
	void Start () {
		is_start = true;
		if (c == null) {
			c = new WebCamTexture (WebCamTexture.devices [0].name);
		}
			GetComponent<RawImage> ().texture = c;
		
		c.Play ();
	}



	static public void pauseCamera(){
		is_start = false;
		c.Pause ();
	}

	static public void restartCamera(){
		is_start = true;
		c.Play ();
	}

}
