using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission8 : MonoBehaviour {

	/*Mission8:
	 * 工三(密碼學)、工四(電磁學)、工五(熱傳學)、工六(分子生物學)、綜一(運輸規劃)、科三(微積分)、科一(離散數學)、科二(物理)
	 * 蒐集越多拿越多分
	 * 
	 * */
	
	string[] mission8str = {
		"EC",
		"ED",
		"EE",
		"EF",
		"AA",
		"SC",
		"SA",
		"SB"
	};
	
	public GameObject ARMessageBox,ARMCBox;
	public GameObject[] mission8;
	
	int myid = 7;
	
	// Use this for initialization
	void Start () {
		Data_Mission.Missions_list [myid].state = 0;
		for (int i=42; i<=49; i++) {
			if(ItemManager.haveItemId(i)) Data_Mission.Missions_list [myid].state++;
			if(Data_Mission.Missions_list [myid].state>3)break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 8; i++) {
			if (AREventHandler.now_track.Equals(mission8str[i]) && !ItemManager.haveItemId (42+i) && Data_Mission.Missions_list [myid].state<=3) {
				mission8 [i].SetActive (true);
			} else
				mission8 [i].SetActive (false);
		}
	}

	int last_item = 0;

	public void onMission8Click(int input){
			
		if (Data_Mission.Missions_list [myid].state < 3) {
			Data_Mission.Missions_list [myid].state++;

			MessageBox ("學習到了" + Data_Items.Items_list [input + 42].name + "!得到積分1000!!");
			List<int> id = new List<int>{input + 42};
			List<int> num = new List<int>{1};	

			Global.findAndAddItem (UserStatementManager.item_list, UserStatementManager.item_num_list, input + 42, 1);
			Global.addItemPref(id,num);
			PlayerPrefs.SetInt ("score", PlayerPrefs.GetInt ("score") + 1000);

			UserStatementManager.give_reward (UserStatementManager.id, UserStatementManager.token, 0, new int[]{}, new List<int>(),new List<int>());
			
		} else {
			Data_Mission.Missions_list [myid].state++;
			last_item = input + 42;
			MessageBox ("學習到了" + Data_Items.Items_list [input + 42].name + "! 我變得更聰明啦!哈哈!",MCcomplete);
		}
		
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
		mbs.scorenum = 1000;
		mbs.itemToLoad = new List<int>{last_item};
		mbs.numToLoad = new List<int>{1};

		mbs.itemToLoad.Add (0);
		mbs.numToLoad.Add (1);
		mbs.setMCB ();

		gameObject.GetComponent<Mission8> ().enabled = false;
	}
	
	//Handle message box
	void MessageBox(string message,System.Action act=null){
		GameObject msg_box = Instantiate (ARMessageBox) as GameObject;
		MessageSet mbs = (MessageSet)(msg_box.GetComponent<MessageSet> ());
		mbs.message = message; 
		mbs.act = act;
	}
}
