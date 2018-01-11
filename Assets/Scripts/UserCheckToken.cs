using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UserCheckToken : MonoBehaviour {

	public GameObject messageBox,networkLoading;
	NetworkLoadingSet newNetworkLoading;

	// Check Token to auto login
	void Start () {
	}

	public void onStartClick(){
		string id = PlayerPrefs.GetString("id");
		string token = PlayerPrefs.GetString("token");
		//初始化道具資料
		Global.init_all();

		if (id == null || token == null || id == "" || token == "") {
			StartCoroutine(LoadingScene("_userlogin"));
			return;
		}
		Debug.Log ("START");
		
		//check if token is correct
		NetworkLoading ("連線中，請稍後...");
		UserStatementManager.get_full_info(id, token,onGetFullInfoDone,onConnFail,onFail);
		//UserStatementManager.get_full_info(id, token,onConnFail,onGetFullInfoDone,onFail);
	}

	// Update is called once per frame
	void Update () {


	}

	void onGetFullInfoDone(){


		//上次註冊失敗
		if (PlayerPrefs.GetInt ("RegistFail") == 1) {
			newNetworkLoading.Clear ();
			NetworkLoading("初始化設定中...");
			StartCoroutine(LoadingScene("_foxSetting"));

		} else {
			Global.delayUploadItems (onUploadSuccess, onFail);
			newNetworkLoading.Clear ();
			NetworkLoading ("確認上次資料中...");
		}
	}


	void onUploadSuccess(){
		newNetworkLoading.Clear ();
		PlayerPrefs.DeleteKey ("items");
		PlayerPrefs.DeleteKey ("nums");
		StartCoroutine (LoadingScene ("_main"));
	}

	public void onReEnter(){
		newNetworkLoading.Clear ();
		NetworkLoading("初始化設定中...");

		
	}
	


	void onConnFail(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("無法與伺服器連線，請重試",Start);
	}

	void onFail(){
		newNetworkLoading.Clear ();
		StartCoroutine(LoadingScene("_userlogin"));
	}

	//Handle Error message
	void ErrorMessageBox(string message,System.Action act){
		GameObject err_msg_box = Instantiate (messageBox) as GameObject;
		MessageBoxSetting mbs = (MessageBoxSetting)(err_msg_box.GetComponent<MessageBoxSetting> ());
		mbs.message = message; 
		mbs.act = act;
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
