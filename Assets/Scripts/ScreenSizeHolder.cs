using UnityEngine;
using System.Collections;

public class ScreenSizeHolder : MonoBehaviour {

	public float baseWidth = Global.baseScreenWidth;
	public float baseHeight = Global.baseSreenHeight;
	public float baseOrthographicSize = Global.baseSreenOrthographicSize;

	void Awake(){		
		float newOrthographicSize = (float)Screen.height / (float)Screen.width * this.baseWidth / this.baseHeight * this.baseOrthographicSize;
		Camera.main.orthographicSize = Mathf.Max(newOrthographicSize , this.baseOrthographicSize);
		Global.orthographicSize = Mathf.Max(newOrthographicSize , this.baseOrthographicSize);
	}
}
