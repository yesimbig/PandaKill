using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Mission1 : MonoBehaviour {
	/*
	 * 第一個任務:開幕遊行中
	 * 順序：體育館-公車站牌-管一
	 * 途中需要拿任務道具
	 * 最後可以得到抽獎券
	 * */

	public GameObject mission1_1,mission1_2,mission1_2_2,mission1_3;
	public Text Message;
	public GameObject ARMessageBox,ARMCBox;
	int myid = 0;
	public static bool is_active = true;

	void Start(){
		//set state
		if (!ItemManager.haveItemId (2) && !ItemManager.haveItemId (3) && !ItemManager.haveItemId (4)) {
			Data_Mission.Missions_list [myid].state = 0;
		} else if (ItemManager.haveItemId (2) && !ItemManager.haveItemId (3) && !ItemManager.haveItemId (4)) {
			Data_Mission.Missions_list [myid].state = 1;
		}else if (ItemManager.haveItemId (3) && !ItemManager.haveItemId (4)) {
			Data_Mission.Missions_list [myid].state = 2;
		}
	}

	// Update is called once per frame
	void Update () {
		if (AREventHandler.now_track == "tail" && Data_Mission.Missions_list [myid].state == 0) {
			mission1_1.SetActive (true);
		} else if ((AREventHandler.now_track == "popo"||AREventHandler.now_track == "popo2") && Data_Mission.Missions_list [myid].state == 1) {
			mission1_2.SetActive (true);
			mission1_2_2.SetActive (true);
		} else if (AREventHandler.now_track == "baobao" && Data_Mission.Missions_list [myid].state == 2) {
			mission1_3.SetActive (true);
		} else {
			mission1_1.SetActive (false);
			mission1_2.SetActive (false);
			mission1_3.SetActive (false);
			mission1_2_2.SetActive(false);
		}
	}

	public void onMission1_1Click(){
		List<int> id = new List<int>{2};
		List<int> num = new List<int>{1};


		//保留暫存檔
		Global.addItemPref(id,num);
		Global.findAndAddItem (UserStatementManager.item_list, UserStatementManager.item_num_list, 2, 1);

		ItemManager.user_change_item_amount (UserStatementManager.id, UserStatementManager.token, new List<int>(), new List<int>());
		Data_Mission.Missions_list [myid].state = 1;
		MessageBox ("這是我的信物，拿去尋找我的朋友吧", onMission111);
	}

	public void onMission111(){
		MessageBox ("把狐狸的尾巴拿去工五吧!!");
	}

	public void onMission1_2Click(){
		List<int> id = new List<int>{3};
		List<int> num = new List<int>{1};

		//保留暫存檔
		Global.addItemPref(id,num);
		Global.findAndAddItem (UserStatementManager.item_list, UserStatementManager.item_num_list, 3, 1);

		ItemManager.user_change_item_amount (UserStatementManager.id, UserStatementManager.token, new List<int>(), new List<int>());
		Data_Mission.Missions_list [myid].state = 2;

		MessageBox ("這是...熊貓的糞便嗎!天啊好臭喔!!",onMission121);
	}

	public void onMission121(){
		MessageBox ("快點把他拿去科三處理掉!!!");
	}

	public void onMission1_3Click(){
		Data_Mission.Missions_list [myid].state = 3;
		MessageBox ("這是我的朋友欸~~一起玩吧~~",onMissionClr);

	}

	void onMissionClr(){
		MCBox (Data_Mission.Missions_list[myid].server_id);
	}


	//Handle message box
	void MessageBox(string message ,System.Action act = null){
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
		mbs.itemToLoad = new List<int>{1,2,3,4};
		mbs.numToLoad = new List<int>{1,-1,-1,1};
		mbs.setMCB ();

		gameObject.GetComponent<Mission1> ().enabled = false;
	}



}
