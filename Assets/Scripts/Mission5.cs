using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission5 : MonoBehaviour {

	/*Mission5:
	 * 1.浩然圖書館.工三到工六.藝文空間.游泳館、計中
	 * 2.找到五個之後，回活中結束任務
	 * 
	 * */


	public GameObject ARMessageBox,ARMCBox;
	public GameObject[] mission5;
	public GameObject mission5_c;
	int myid = 4;
	
	// Use this for initialization
	void Start () {
		
		Data_Mission.Missions_list [myid].state = 0;
		for (int i=14; i<=21; i++) {
			if(ItemManager.haveItemId(i)) Data_Mission.Missions_list [myid].state++;
			if(Data_Mission.Missions_list [myid].state>=5)break;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		string[] mission5str = {
			"library",
			"EC",
			"ED",
			"EE",
			"EF",
			"artspace",
			"swimmingpool",
			"CScenter"
		};

		for (int i = 0; i < 8; i++) {
			if (AREventHandler.now_track.Equals(mission5str[i]) && !ItemManager.haveItemId (14+i) && Data_Mission.Missions_list [myid].state < 5) {
				mission5 [i].SetActive (true);
			} else
				mission5 [i].SetActive (false);
		}

		if (AREventHandler.now_track.Equals ("activitycenter") && Data_Mission.Missions_list [myid].state == 5) {
			mission5_c.SetActive (true);
		}else mission5_c.SetActive (false);

	}

	public void onMission5Click(int input){
		List<int> id = new List<int>{input + 14};
		List<int> num = new List<int>{1};

		//保留暫存檔

		Global.findAndAddItem (UserStatementManager.item_list, UserStatementManager.item_num_list, input + 14, 1);
		Global.addItemPref(id,num);

		ItemManager.user_change_item_amount (UserStatementManager.id, UserStatementManager.token, new List<int>(), new List<int>());

		if (Data_Mission.Missions_list [myid].state < 4) {
			Data_Mission.Missions_list [myid].state++;
			MessageBox ("已學習了" + Data_Items.Items_list [input + 14].name + "!!!");
		} else {
			Data_Mission.Missions_list [myid].state++;
			MessageBox ("已學習了" + Data_Items.Items_list [input + 14].name + "!!!",goback);
		}
		
	}

	void goback(){
		MessageBox ("學得差不多了，請去活中找狐狸長老展現學習的成果吧!!");
	
	}

	//活中的按鈕
	public void onMissionCClick(){
		Data_Mission.Missions_list [myid].state++;
		MessageBox ("狐狸長老大人，在此展現我的訓練成果~",MCcomplete);	
	}

	void MCcomplete(){
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
		mbs.itemToLoad = new List<int>{1};
		mbs.numToLoad = new List<int>{1};
		mbs.setMCB ();
		gameObject.GetComponent<Mission5> ().enabled = false;
	}

	//Handle message box
	void MessageBox(string message,System.Action act=null){
		GameObject msg_box = Instantiate (ARMessageBox) as GameObject;
		MessageSet mbs = (MessageSet)(msg_box.GetComponent<MessageSet> ());
		mbs.message = message; 
		mbs.act = act;
	}
}
