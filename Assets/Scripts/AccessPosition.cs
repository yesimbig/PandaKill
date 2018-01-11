using UnityEngine;
using System.Collections;

public class AccessPosition : MonoBehaviour {

	public GameObject LeftTop,LeftCenter,LeftBottom,RightTop,RightBottom,CenterTop,CenterBottom;
	public Camera camera;
	// Use this for initialization
	void Start () {

		if (camera.orthographic == true) {
			LeftTop.transform.position = camera.ScreenToWorldPoint (new Vector2 (0, Screen.height));

			if (LeftBottom != null) {
				LeftBottom.transform.position = camera.ScreenToWorldPoint (new Vector2 (0, 0));
			}

			if (RightTop != null) {
				RightTop.transform.position = camera.ScreenToWorldPoint (new Vector2 (Screen.width, Screen.height));
			}
			if (RightBottom != null) {
				RightBottom.transform.position = camera.ScreenToWorldPoint (new Vector2 (Screen.width, 0));
			}
			if(LeftCenter!=null){
				LeftCenter.transform.position = camera.ScreenToWorldPoint (new Vector2 (0, Screen.height/2));		
			}
			if(CenterTop!=null){
				CenterTop.transform.position = camera.ScreenToWorldPoint (new Vector2 (Screen.width/2, Screen.height));		
			}
			if(CenterBottom!=null){
				CenterBottom.transform.position = camera.ScreenToWorldPoint (new Vector2 (Screen.width/2, 0));		
			}
		}
		else {
			float differ = 0F;
			float ltz = LeftTop.transform.position.z + differ;
			float lbz = LeftBottom.transform.position.z+ differ;
			float rtz = RightTop.transform.position.z+ differ;
			float rbz = RightBottom.transform.position.z+ differ;
			float hz = 11.1F;

			Debug.LogWarning (ltz);
			Debug.LogWarning (rbz);
			LeftTop.transform.position = GetWorldPositionOnPlane (new Vector3 (0, Screen.height,0),ltz);
			LeftBottom.transform.position = GetWorldPositionOnPlane (new Vector3 (0, 0,0),lbz);
			RightTop.transform.position = GetWorldPositionOnPlane (new Vector3 (Screen.width,Screen.height,0),rtz);
			RightBottom.transform.position = GetWorldPositionOnPlane (new Vector3 (Screen.width, 0,0),rbz);

		}
	}

	public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z) {
		Ray ray = camera.ScreenPointToRay(screenPosition);
		Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
		float distance;
		xy.Raycast(ray, out distance);
		return ray.GetPoint(distance);
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
