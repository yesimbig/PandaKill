using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Mission2 : MonoBehaviour {

	public GameObject ARMessageBox,ARMCBox;
	public GameObject[] mission2_1_raw,mission2_1_heat;
	public GameObject[] mission2_2_raw,mission2_2_heat;
	public GameObject mission2_1,mission2_2;


	int myid = 1;

	bool[] used; 
	bool state = false;

	float heatTime = 0f;
	int find = 0;
	int now_food = 0;

	string[] mission2str = {
		"吐司",
		"培根",
		"甜不辣",
		"百頁豆腐",
		"米血",
		"里肌",
		"羊小排",
		"秋刀魚"
	};

	// Use this for initialization
	void Start(){
		now_food = 0;
		find = 0;
		Data_Mission.Missions_list [myid].state = 0;
		used = new bool[8];
		for (int i=0; i<8; i++)
			used [i] = false;
		state = false;
			
		for (int i=0; i<8; i++) {
			if (used [i]) {
				mission2_1_heat [i].SetActive (true);
				mission2_1_raw [i].SetActive (false);
			} else {
				mission2_1_heat [i].SetActive (false);
				mission2_1_raw [i].SetActive (true);
			}
		}

	}

	void Update(){

		if (AREventHandler.now_track == "fire" && find < 4) {
			//mission2_1
			if (state == false) {
				mission2_1.SetActive(true);
				mission2_2.SetActive(false);
			} else {
				mission2_1.SetActive(false);
				mission2_2.SetActive(true);		
				heatTime += Time.deltaTime;

				if(heatTime >= 3f){
					mission2_2_heat[now_food].SetActive(true);
					mission2_2_raw[now_food].SetActive(false);
				}
			}
		} else {
			mission2_1.SetActive(false);
			mission2_2.SetActive(false);
			heatTime = 0f;
		}
	}

	public void onMission2_1Click(int a){
		state = true;
		now_food = a;
		heatTime = 0f;
		for (int i=0; i<8; i++) {
			if(i==a)
				mission2_2_raw [a].SetActive (true);
			else mission2_2_raw[i].SetActive(false);
			mission2_2_heat[i].SetActive(false);
		}
	}

	public void onMission2_2Click(int a){

		if (a == 5 || a == 2 || a == 0 || a == 1) {
			MessageBox ("哇~是" + mission2str [a] + "耶!!找到正確的食材了!!", onFindSuccess);
			used [a] = true;
			for (int i=0; i<8; i++) {
				if (used [i]) {
					mission2_1_heat [i].SetActive (true);
					mission2_1_raw [i].SetActive (false);
				} else {
					mission2_1_heat [i].SetActive (false);
					mission2_1_raw [i].SetActive (true);
				}
			}
		} else {
			MessageBox ("雖然" + mission2str [a] + "也很好吃，不過他並不是正確的食材", onFindFail);
			for (int i=0; i<8; i++) {
				used [i] = false;
				mission2_1_heat [i].SetActive (false);
				mission2_1_raw [i].SetActive (true);
			}
		}
	}

	void onFindSuccess(){
		state = false;
		find++;
		if (find >= 4) {
			MessageBox ("感謝你~找到全部正確的食材了!!快來領取獎勵吧~~",onMissionComplete);
		}

	}

	void onFindFail(){
		state = false;
		find = 0;
	}

	void onMissionComplete(){
		MCBox( Data_Mission.Missions_list[myid].server_id);
	}

	//Handle message box
	void MessageBox(string message,System.Action act){
		GameObject msg_box = Instantiate (ARMessageBox) as GameObject;
		MessageSet mbs = (MessageSet)(msg_box.GetComponent<MessageSet> ());
		mbs.message = message; 
		mbs.act = act;
	}

	//Handle mission complete box
	void MCBox(int missionid){
		GameObject mc_box = Instantiate (ARMCBox) as GameObject;
		MissionCompleteSet mbs = (MissionCompleteSet)(mc_box.GetComponent<MissionCompleteSet> ());
		
		mbs.missionid = missionid;

		//技能升級
		if (Data_Mission.Missions_list [myid].skills.Length>0 &&  Data_Mission.Missions_list [myid].skills[0] == 8) {
			int[] skill_list = new int[1];
			skill_list[0] = Data_Mission.Missions_list [myid].skills[0];
			skill_list[0] = 5;
			for (int i = 0; i < UserStatementManager.skill_id_list.Length; i++) {
				if(UserStatementManager.skill_id_list[i]==5)skill_list[0] = 6;
				else if(UserStatementManager.skill_id_list[i]==6)skill_list[0] = 7;
			}
			mbs.skills = skill_list;
		}
		else mbs.skills = Data_Mission.Missions_list [myid].skills;
		mbs.scorenum = Data_Mission.Missions_list[myid].score;
		mbs.itemToLoad = new List<int>{ItemManager.getRandomEquip ()};
		mbs.numToLoad = new List<int>{1};
		mbs.setMCB ();

		gameObject.GetComponent<Mission2> ().enabled = false;
	}

}
