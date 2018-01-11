using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission3 : MonoBehaviour {

	public GameObject ARMessageBox,ARMCBox;
	public GameObject[] mission3;
	int myid = 2;

	// Use this for initialization
	void Start () {

		Data_Mission.Missions_list [myid].state = 0;

		for (int i=5; i<=12; i++) {
			if(ItemManager.haveItemId(i)) Data_Mission.Missions_list [myid].state++;
			if (Data_Mission.Missions_list [myid].state == 2)
				break;
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		string[] mission3str = {
			"FamilyMart",
			"milk",
			"Mc",
			"7-11",
			"HiLife",
			"restaurant1",
			"restaurant2",
			"IHateThisPlace"
		};
		for (int i = 0; i < 8; i++) {
			if (AREventHandler.now_track.Equals(mission3str[i]) && !ItemManager.haveItemId (5+i) && Data_Mission.Missions_list [myid].state <= 2) {
				mission3 [i].SetActive (true);
			} else
				mission3 [i].SetActive (false);
		}
	}

	int last_get = 0;

	public void onMission3Click(int input){
		List<int> id = new List<int>{input};
		List<int> num = new List<int>{1};


		Data_Mission.Missions_list [myid].state++;

		if (Data_Mission.Missions_list [myid].state < 3) {
			//保留暫存檔
			Global.addItemPref (id, num);
			Global.findAndAddItem (UserStatementManager.item_list, UserStatementManager.item_num_list, input, 1);
			ItemManager.user_change_item_amount (UserStatementManager.id, UserStatementManager.token, new List<int>(), new List<int>(), onUploadDone);
			MessageBox ("已獲得" + Data_Items.Items_list [input].name + "!!!");
		} else if (Data_Mission.Missions_list [myid].state == 3){

			last_get = input;
			MessageBox ("已獲得" + Data_Items.Items_list [input].name + "!!!",MCcomplete);
		}

	}

	void MCcomplete(){
		//Data_Mission.Missions_list [myid].state++;
		//mission complete!!!
		GameObject mc_box = Instantiate (ARMCBox) as GameObject;
		MissionCompleteSet mbs = (MissionCompleteSet)(mc_box.GetComponent<MissionCompleteSet> ());
		
		mbs.missionid = Data_Mission.Missions_list[myid].server_id;

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

		mbs.scorenum = Data_Mission.Missions_list [myid].score;
		mbs.itemToLoad = new List<int>{0,last_get};
		mbs.numToLoad = new List<int>{1,1};

		mbs.setMCB ();

		gameObject.GetComponent<Mission3> ().enabled = false;
	}

	void onUploadDone(){
		PlayerPrefs.DeleteKey ("items");
		PlayerPrefs.DeleteKey ("nums");
	}
	
	//Handle message box
	void MessageBox(string message,System.Action act=null){
		GameObject msg_box = Instantiate (ARMessageBox) as GameObject;
		MessageSet mbs = (MessageSet)(msg_box.GetComponent<MessageSet> ());
		mbs.message = message; 
		mbs.act = act;
	}
}
