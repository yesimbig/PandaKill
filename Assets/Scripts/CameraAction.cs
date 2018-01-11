using UnityEngine;
using System.Collections;

public class CameraAction : MonoBehaviour {
	
	//public GameObject g;
	
	static public WebCamTexture c;
	
	void Start () {
		c = new WebCamTexture (WebCamTexture.devices[0].name);
		GetComponent<Renderer>().material.mainTexture = c;
		c.Play ();
		
	}

	static public void pauseCamera(){
		c.Pause ();
	}
	
	static public void restartCamera(){
		c.Play ();
	}
		
}