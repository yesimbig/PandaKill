using UnityEngine;
using System.Collections;

public class Mission7 : MonoBehaviour {

	bool loading = false;
	public GameObject networkLoading;
	NetworkLoadingSet newNetworkLoading;

	// Use this for initialization
	void Start () {
		loading = false;
	}



	// Update is called once per frame
	void Update () {
		if (AREventHandler.now_track.Equals("NCTU_LOGO") && !ItemManager.haveItemId (41) && loading == false) {
			loading = true;
			StartCoroutine(LoadingScene("_answer"));
		}
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
