using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
public class allMissionSetting : MonoBehaviour {

	
	public GameObject missionPrefab, awardPrefab, ARMessageBox;
	public GameObject[] Tab_select;
	public GameObject[] Titles;
	public GameObject Mission_name;
	public GameObject complete_time;
	public GameObject award_list;
	public GameObject[] btns;
	public GameObject Score_panel;

	public Sprite[] mission_name_sp;
	public Sprite[] complete_time_sp;
	public Sprite[] award_list_sp;

	public Text missiontitle;
	public Text Time_text,Score_text;
	public Text description;

	public Button LeftArrow, RightArrow;

	public GameObject MissionContent,ScrollContent,AwardContent;

	public GameObject messageBox;
	public GameObject networkLoading;
	NetworkLoadingSet newNetworkLoading;

	public static int now_tab;//目前的分頁
	public static int now_id = -1;
	public static bool trigger; //若有換視窗則trigger跳起
	int now_server_id;
	int now_f;//儲存目前分頁
	List<GameObject> del = new List<GameObject>() , delitem = new List<GameObject>();

	float iniWidth ,iniHeight;
	Vector3 scrollContentOrigin;

	// Use this for initialization
	void Start () {
		scrollContentOrigin = ScrollContent.GetComponent<RectTransform> ().position;
		iniWidth = ScrollContent.GetComponent<RectTransform> ().rect.width;
		iniHeight = ScrollContent.GetComponent<RectTransform> ().rect.height;
		Debug.Log ("iniHeight= " + iniHeight);
		//初始化道具資料
		Data_Mission.setStates ();

		now_id = -1;
		now_tab = Global.MISSION_TOKEN;
		trigger = false;
		Tab_select [0].SetActive (true);
		Tab_select [1].SetActive (false);
		Tab_select [2].SetActive (false);

		Titles [0].SetActive (true);
		Titles [1].SetActive (false);

		Mission_name.GetComponent<Image> ().sprite = mission_name_sp [0];
		complete_time.GetComponent<Image> ().sprite = complete_time_sp [0];
		award_list.GetComponent<Image> ().sprite = award_list_sp [0];

		btns [0].SetActive (true);
		btns [1].SetActive (false);
		Score_panel.SetActive (false);

		setUpMission (Global.MISSION_TOKEN, 0);
		/*setUpMission (Contents[0],Global.MISSION_ACCEPTABLE);
		setUpMission (Contents[1],Global.MISSION_TOKEN);
		setUpMission (Contents[2],Global.MISSION_COMPLETED);*/
		trigger = true;
	}

	void Update(){

		if (trigger) {
			ScrollContent.GetComponent<RectTransform>().position = scrollContentOrigin;
			trigger = false;

			if (now_id == -1) {
				missiontitle.text = "";
				Time_text.text = "-";
				description.text = "";
				ScrollContent.GetComponent<RectTransform> ().sizeDelta = new Vector2(iniWidth, iniHeight);
				btns[0].GetComponent<Button>().interactable = false;
				btns[1].GetComponent<Button>().interactable = false;

				for(int i=0;i<delitem.Count;i++){
					DestroyObject(delitem[i]);
				}
				delitem.Clear();
				Score_panel.SetActive (false);

			} else {
				btns[0].GetComponent<Button>().interactable = true;
				btns[1].GetComponent<Button>().interactable = true;

				missiontitle.text = Data_Mission.Missions_list[now_id].name;
				description.text = Data_Mission.Missions_list[now_id].description;
				if(now_tab == Global.MISSION_TOKEN){
					if(Data_Mission.Missions_list[now_id].state < Data_Mission.Missions_list[now_id].message.Count)
						description.text =	Data_Mission.Missions_list[now_id].message[ Data_Mission.Missions_list[now_id].state ];
					else description.text = Data_Mission.Missions_list[now_id].description;
				}
				if(now_tab == Global.MISSION_COMPLETED){
					description.text =	Data_Mission.Missions_list[now_id].finish;
				}

				now_server_id = Data_Mission.Missions_list[now_id].server_id;
				int id =  MissionManager.id.IndexOf( now_server_id );


				//--------time--------------
				if(now_tab!= Global.MISSION_COMPLETED && MissionManager.close_time[id]!=""){
					DateTime d = DateTime.Parse (MissionManager.close_time[id]);
					TimeSpan tt = d - DateTime.Now ;
					Time_text.text = tt.Hours + " hr "+tt.Minutes + "m";
				}
				else if(now_tab== Global.MISSION_COMPLETED && MissionManager.finish_time[id]!=""){
					DateTime d = DateTime.Parse (MissionManager.finish_time[id]);
					Time_text.text =string.Format ("{0:0}/{1:0}/{2:0} {3:0}:{4:00}", d.Year , d.Month , d.Day ,d.Hour, d.Minute);
				}
				else{
					Time_text.text = "-";
				}
				//------------------------

				if(Data_Mission.Missions_list[now_id].score >0){
					Score_panel.SetActive (true);
					Score_text.text = "x" + Data_Mission.Missions_list[now_id].score.ToString();
				}
				else Score_panel.SetActive (false);

				for(int i=0;i<delitem.Count;i++){
					DestroyObject(delitem[i]);
				}
				delitem.Clear();

				if(Data_Mission.Missions_list[now_id].item != null)
				for(int i = 0;i<Data_Mission.Missions_list[now_id].item.Length;i++){
					GameObject item = Instantiate (awardPrefab) as GameObject;
					item.transform.SetParent (AwardContent.transform);	
					delitem .Add (item);
					ItemSetting itemsetting = (ItemSetting)(item.GetComponent<ItemSetting> ());
					itemsetting.setIcon(0,Data_Mission.Missions_list[now_id].item[i]);
				}

				if(Data_Mission.Missions_list[now_id].skills != null)
				for(int i = 0;i<Data_Mission.Missions_list[now_id].skills.Length;i++){
					GameObject item = Instantiate (awardPrefab) as GameObject;
					item.transform.SetParent (AwardContent.transform);	
					delitem .Add (item);
					ItemSetting itemsetting = (ItemSetting)(item.GetComponent<ItemSetting> ());
					itemsetting.setIcon(1,Data_Mission.Missions_list[now_id].skills[i]);
				}

				//--------------------set panel------------------------------

				//ScrollContent.GetComponent<RectTransform> ().sizeDelta = new Vector2(iniWidth, iniHeight);
			}
		}
	}

	void setUpMission(int mission_state,int f){
		now_f = f;
		//clear
		for (int i=0; i<del.Count; i++)
			DestroyObject (del [i]);
		del.Clear ();

		int cal = 0;
		for (int i = 0; i<Global.MISSION_NUM; i++) {
			int server_id = Data_Mission.Missions_list[i].server_id;
			int id =  MissionManager.id.IndexOf( server_id );

			if(  id==-1 )continue;
			if(MissionManager.mission_state[id] != mission_state) continue;
			cal++;
			if(cal<=f){
				continue;
			}
			if(cal >f+4) break;


			GameObject mission = Instantiate (missionPrefab) as GameObject;
			mission.transform.SetParent (MissionContent.transform);	

			MissionSetting missionsetting = (MissionSetting)(mission.GetComponent<MissionSetting> ());
			missionsetting.setMission (i); 
			del.Add (mission);
		}

		if (f == 0)
			LeftArrow.interactable = false;
		else LeftArrow.interactable = true;

		if (cal > f + 4)
			RightArrow.interactable = true;
		else
			RightArrow.interactable = false;

	}

	public void onExitButtonClick(){
		DestroyObject (gameObject);	
	}
	
	public void onTokenButtonClick(){
		trigger = true;
		now_id = -1;
		now_tab = Global.MISSION_TOKEN;
		Tab_select [0].SetActive (true);
		Tab_select [1].SetActive (false);
		Tab_select [2].SetActive (false);
		Titles [0].SetActive (true);
		Titles [1].SetActive (false);
		Mission_name.GetComponent<Image> ().sprite = mission_name_sp [0];
		complete_time.GetComponent<Image> ().sprite = complete_time_sp [0];
		award_list.GetComponent<Image> ().sprite = award_list_sp [0];
		btns [0].SetActive (true);
		btns [1].SetActive (false);

		setUpMission (Global.MISSION_TOKEN, 0);
	}

	public void onAcceptableButtonClick(){
		trigger = true;
		now_id = -1;
		now_tab = Global.MISSION_ACCEPTABLE;
		Tab_select [1].SetActive (true);
		Tab_select [0].SetActive (false);
		Tab_select [2].SetActive (false);
		Titles [0].SetActive (true);
		Titles [1].SetActive (false);
		Mission_name.GetComponent<Image> ().sprite = mission_name_sp [1];
		complete_time.GetComponent<Image> ().sprite = complete_time_sp [1];
		award_list.GetComponent<Image> ().sprite = award_list_sp [1];
		btns [1].SetActive (true);
		btns [0].SetActive (false);
		setUpMission (Global.MISSION_ACCEPTABLE, 0);
	}

	public void onCompletedButtonClick(){
		trigger = true;
		now_id = -1;
		now_tab = Global.MISSION_COMPLETED;
		Tab_select [2].SetActive (true);
		Tab_select [1].SetActive (false);
		Tab_select [0].SetActive (false);
		Titles [1].SetActive (true);
		Titles [0].SetActive (false);
		Mission_name.GetComponent<Image> ().sprite = mission_name_sp [2];
		complete_time.GetComponent<Image> ().sprite = complete_time_sp [2];
		award_list.GetComponent<Image> ().sprite = award_list_sp [2];
		btns [0].SetActive (false);
		btns [1].SetActive (false);
		setUpMission (Global.MISSION_COMPLETED, 0);
	}

	public void onLeftArrowClick(){
		setUpMission (now_tab,now_f-4);
	}

	public void onRightArrowClick(){
		setUpMission (now_tab,now_f+4);
	}

	//-------------------任務接取按鈕--------------------------

	int state;//目前劇情到哪邊

	public void onACbtnClick(){
		state = 0;
		missionAssign ();

	}

	public void onCCbtnClick(){
		NetworkLoading ("放棄任務中，請稍後...");
		MissionManager.quit_mission (UserStatementManager.id, UserStatementManager.token, now_server_id, onQuitDone, ConnErr, InvalidToken,onCCMissionError);
	}

	//接取任務的劇情設定
	void missionAssign(){
		if (state < Data_Mission.Missions_list [now_id].assign.Count) {
			GameObject msg_box = Instantiate (ARMessageBox) as GameObject;
			MessageSet mbs = (MessageSet)(msg_box.GetComponent<MessageSet> ());
			mbs.message = Data_Mission.Missions_list [now_id].assign[state]; 
			mbs.act = missionAssign;
			state++;

		} else {
			NetworkLoading ("接取任務中，請稍後...");
			MissionManager.take_mission (UserStatementManager.id, UserStatementManager.token, now_server_id, onTakeDone, ConnErr, InvalidToken, onACMissionError);
		}
	}

	void onTakeDone(){
		newNetworkLoading.Clear();
		onAcceptableButtonClick();
	}

	void onQuitDone(){
		newNetworkLoading.Clear();
		onTokenButtonClick();
	}

	void onACMissionError(){
		newNetworkLoading.Clear();
		ErrorMessageBox ("任務無法接取!!");
	}

	void onCCMissionError(){
		newNetworkLoading.Clear();
		ErrorMessageBox ("任務無法放棄!!");
	}

	//----------network handle--------------------------


	//不允許多登
	void InvalidToken(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("此帳號在其他地方已登入，請重登!",restart);
	}
	
	//重新登入
	void restart(){
		Application.LoadLevelAsync ("_title");	
	}

	void ConnErr(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("連線失敗!!");
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
		if (asyncf == null)
			Debug.Log ("GG");
		while (!asyncf.isDone) {
			newNetworkLoading.setMessage("載入中...請稍候..."+((int)(asyncf.progress*100)).ToString ()+"%");
			yield return null;
		}
		newNetworkLoading.Clear ();
	}

}
