using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RankManager : MonoBehaviour {

	public Text[] point;
	public Text[] nickname;
	public Text[] id;
	public Text userMessage;


	public GameObject networkLoading;
	NetworkLoadingSet newNetworkLoading;

	// Use this for initialization
	void Start () {

		userMessage.text = "HI, "+UserStatementManager.nickname +"(UID: "+UserStatementManager.id+")! 目前分數: "+UserStatementManager.score.ToString()+" 排名: "+Global.USER_RANK; 

		for (int i=0; i<Global.GENERAL_RANK.Count; i++) {
			point[i].text = Global.GENERAL_RANK[i].score.ToString();
			nickname[i].text = Global.GENERAL_RANK[i].nickname;
			id[i].text = Global.GENERAL_RANK[i].account;
		}

		for (int i= Global.GENERAL_RANK.Count; i <5; i++) {
			point[i].text = "0";
			nickname[i].text = "-";
			id[i].text = "-";
		}
	}

	public void onBackBtn(){
		StartCoroutine (LoadingScene ("_main"));
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
