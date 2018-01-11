using UnityEngine;
using System.Collections;

public class mainButtonHandler : MonoBehaviour {


	public GameObject ItemBox;
	public GameObject SkillBox;
	public GameObject MissionBox;

	public GameObject debug,aboutBtn;
	bool de = false;

	public GameObject messageBox;
	public GameObject networkLoading;

	NetworkLoadingSet newNetworkLoading;

	public void onItemButtonClick(){
		Instantiate (ItemBox);
	}

	public void onSkillButtonClick(){
		Instantiate (SkillBox);
	}

	public void onMissionButtonClick(){
		NetworkLoading ("取得任務資料中，請稍後...");
		MissionManager.get_all_missions (UserStatementManager.id, UserStatementManager.token, onGetAllMissionSuccess, ConnErr,InvalidToken);
	}

	public void onARButtonClick(){

		StartCoroutine (LoadingScene ("_ARCamera"));
	}

	public void onBattleButtonClick(){
		if (UserStatementManager.repletion > 0) {

			StartCoroutine (LoadingScene ("_battle"));

		} else {
			ErrorMessageBox ("飽食度不足喔!!");
		}
	}

	public void onDeveloperClick(){
		StartCoroutine(LoadingScene("_developer"));
	}
	
	public void onRankButtonClick(){
		NetworkLoading ("取得排名資料中，請稍後...");
		Global.general_rank (int.Parse (UserStatementManager.id),onGetRankSuccess, ConnErr,InvalidToken);
	}

	void onGetAllMissionSuccess(){
		newNetworkLoading.Clear ();
		Instantiate (MissionBox);
	}

	void onGetRankSuccess(){
		newNetworkLoading.Clear ();
		StartCoroutine(LoadingScene("_rank"));
	}


	/*void onGotoBattle(){
		Debug.Log ("Carry");
		newNetworkLoading.Clear ();
		StartCoroutine (LoadingScene ("_battle"));
	}*/
	
	void ConnErr(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("連線失敗!!");
	}

	//不允許多登
	void InvalidToken(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("此帳號在其他地方已登入，請重登!",restart);
	}

	//重新登入
	void restart(){
		Application.LoadLevelAsync ("_title");	
	}

	//Handle Error message
	void ErrorMessageBox(string message,System.Action act = null){
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
		while (!asyncf.isDone) {
			newNetworkLoading.setMessage("載入中...請稍候..."+((int)(asyncf.progress*100)).ToString ()+"%");
			yield return null;
		}
		newNetworkLoading.Clear ();
	}


	public void onDebug(){
		if (!de) {
			debug.SetActive (true);
			aboutBtn.SetActive(true);
		} else {
			debug.SetActive (false);
			aboutBtn.SetActive(false);
		}
		de = !de;
	}

}
