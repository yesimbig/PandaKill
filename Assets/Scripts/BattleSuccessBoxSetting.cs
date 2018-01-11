using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class BattleSuccessBoxSetting : MonoBehaviour {

	public Text TimeSpend,getscore;

	public GameObject Content, reward_prefab;

	public GameObject messageBox;
	public GameObject networkLoading;
	public GameObject victory_title, fail_title;
	public AudioClip victory_clip,fail_clip;

	NetworkLoadingSet newNetworkLoading;

	int scorenum;
	int[] skills;
	List<int> itemTL;
	List<int> numTL;

	public void setall(string time,int get,List<int> itemToLoad, List<int> numToLoad,bool plus = false,bool fail = false){
		TimeSpend.text = time;
		getscore.text = "x"+get.ToString();
		if (plus) {
			getscore.color = Color.red;
			getscore.text += " (x1.2)";
		}
		scorenum = get;
		itemTL = itemToLoad;
		numTL = numToLoad;


		if (fail) {
			victory_title.SetActive (false);
			fail_title.SetActive (true);
			gameObject.GetComponent<MusicHandler>().changeBGM(fail_clip);
		}else gameObject.GetComponent<MusicHandler>().changeBGM(victory_clip);

		for(int i=0;i<itemToLoad.Count;i++){
			if(numToLoad[i]<=0)continue;
			
			GameObject item = Instantiate (reward_prefab) as GameObject;
			item.transform.SetParent (Content.transform);	
			
			ItemSetting itemsetting = (ItemSetting)(item.GetComponent<ItemSetting> ());
			itemsetting.setReward (0,itemToLoad[i],numToLoad[i]);
		}
	}

	public void onButtonClick(){
		NetworkLoading ("連線中，請稍後...");
		UserStatementManager.change_user_repletion (UserStatementManager.id, UserStatementManager.token, -1, giveReward, onConnErr,InvalidToken);
	}

	void giveReward(){
		newNetworkLoading.Clear ();
		NetworkLoading("連線中，請稍後...");
		UserStatementManager.give_reward(UserStatementManager.id, UserStatementManager.token, scorenum,new int[]{},itemTL,numTL, onSubmitScoreDone, onConnErr2,InvalidToken);
	}

	void onSubmitScoreDone(){
		newNetworkLoading.Clear ();
		if (CameraBG.is_start)
			CameraBG.pauseCamera ();
		StartCoroutine(LoadingScene("_main"));
		DestroyObject (gameObject);
	}

	void onConnErr(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("連線失敗!請重試!",onButtonClick);
	}

	void onConnErr2(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("連線失敗!請重試!",giveReward);
	}


	//不允許多登
	void InvalidToken(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("此帳號在其他地方已登入，請重登!",restart);
	}
	
	//重新登入
	void restart(){
		if (CameraBG.is_start)
			CameraBG.pauseCamera ();
		StartCoroutine(LoadingScene("_title"));	
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
}
