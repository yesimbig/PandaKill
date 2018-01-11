using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MissionCompleteSet : MonoBehaviour {

	public GameObject networkLoading,messageBox;
	NetworkLoadingSet newNetworkLoading;

	public int scorenum;
	public int missionid;
	public int[] skills;
	public List<int> itemToLoad;
	public List<int> numToLoad;

	public bool gotoScene = false;

	public GameObject reward_prefab;
	public GameObject Content;
	public GameObject Attackx2;

	public AudioClip victory_clip;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<MusicHandler> ().changeBGM (victory_clip);
	}


	public void setMCB(bool g = false,bool showatk = false){
		for(int i=0;i<itemToLoad.Count;i++){
			if(numToLoad[i]<=0)continue;
			if(Data_Items.Items_list[ itemToLoad[i]].type == 0)continue;

			GameObject item = Instantiate (reward_prefab) as GameObject;
			item.transform.SetParent (Content.transform);	

			ItemSetting itemsetting = (ItemSetting)(item.GetComponent<ItemSetting> ());
			itemsetting.setReward (0,itemToLoad[i],numToLoad[i]);
		}

		for(int i=0;i<skills.Length;i++){
			GameObject item = Instantiate (reward_prefab) as GameObject;
			item.transform.SetParent (Content.transform);	

			ItemSetting itemsetting = (ItemSetting)(item.GetComponent<ItemSetting> ());
			itemsetting.setReward (1,skills[i],1);
		}

		if (scorenum > 0) {
			GameObject item = Instantiate (reward_prefab) as GameObject;
			item.transform.SetParent (Content.transform);	

			ItemSetting itemsetting = (ItemSetting)(item.GetComponent<ItemSetting> ());
			itemsetting.setReward (2,0,scorenum);
		}
		gotoScene = g;
		if (showatk)
			Attackx2.SetActive (true);
	}

	public void onButtonClick(){

		NetworkLoading ("更新道具資料中...請稍後...");
		List<int> items = new List<int> (itemToLoad);
		List<int> nums = new List<int> (numToLoad);
		UserStatementManager.give_reward(UserStatementManager.id, UserStatementManager.token, scorenum,skills, items, nums,onChangeDone,onChangeFail,InvalidToken);


	}

	void onChangeDone(){
		newNetworkLoading.Clear ();
		//NetworkLoading ("更新任務中...請稍後...");
		NetworkLoading ("完成任務中...請稍後...");
		MissionManager.finish_mission (UserStatementManager.id, UserStatementManager.token, missionid, onMissionComplete,onMFail, InvalidToken);
	}

	void onEquipSkill(){
		if(skills[0] == 6)
	
	
	}

	void onMissionComplete(){
		newNetworkLoading.Clear ();
		DestroyObject (gameObject);
		AREventHandler.trigger = true;

		if (gotoScene) {
			StartCoroutine(LoadingScene("_main"));
		}

	}

/*	public void onReGive(){
		NetworkLoading ("重新更新道具資料中...請稍後...");
		UserStatementManager.give_reward(UserStatementManager.id, UserStatementManager.token, 0,skills,new List<int>(), new List<int>(), onChangeDone, onChangeFail,InvalidToken);
	}
*/
	void onChangeFail(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("更新道具錯誤，請重試", onButtonClick);
	}

	void onMFail(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("完成任務錯誤，請重試", onChangeDone);
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
