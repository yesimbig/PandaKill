using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeveloperManager : MonoBehaviour {
	
	public Animator area_move;
	
	public GameObject networkLoading;
	NetworkLoadingSet newNetworkLoading;
	
	// Use this for initialization
	void Start () {
	}
	
	public void onBackBtn(){
		StartCoroutine (LoadingScene ("_main"));
	}

	public void onAreaMove(int state){
		area_move.SetInteger ("now_state",state);
	}


	//Handle network loading
	void NetworkLoading(string message = null){
		newNetworkLoading = (Instantiate (networkLoading) as GameObject).GetComponent<NetworkLoadingSet>();
		
		if (message != null)
			newNetworkLoading.message = message;
	}
	
	//Handle loading next scene
	IEnumerator LoadingScene(string scene){
		NetworkLoading ("載入中...請稍候...");
		AsyncOperation asyncf = Application.LoadLevelAsync (scene);
		if (asyncf == null)
			Debug.Log ("GG");
		while (!asyncf.isDone) {
			newNetworkLoading.setMessage("載入中...請稍候..."+((int)(asyncf.progress*100)).ToString ()+"%");
			yield return null;
		}
		newNetworkLoading.Clear ();
	}


	
}
