using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission6 : MonoBehaviour {

	/*Mission6:
	 * a華、comebuy、鼎天、麥當勞、夯米包、元氣、一餐燒臘、萊爾富、一餐水餃、一餐麵店、二餐小七、八方、米克Q、姊妹、雙葉、二餐魯味
	 * 蒐集越多拿越多分
	 * 
	 * */

	string[] mission6str = {
		"AHua",
		"COMEBUY",
		"dintien",
		"Mc",
		"hangmibao",
		"oganki",
		"restaurant1rise",
		"HiLife",
		"dumkins",
		"noodles",
		"7-11",
		"bafang",
		"mikoQ6",
		"chicken",
		"doubleleaf",
		"lulu",
		"FamilyMart",
		"milk",
		"littlewood"
	};
	
	public GameObject ARMessageBox,ARMCBox;
	public GameObject[] mission6;

	int myid = 5;

	// Use this for initialization
	void Start () {
		Data_Mission.Missions_list [myid].state = 0;
		for (int i=22; i<=40; i++) {
			if(ItemManager.haveItemId(i)) Data_Mission.Missions_list [myid].state++;
			if(Data_Mission.Missions_list [myid].state==9)break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 19; i++) {
			if (AREventHandler.now_track.Equals(mission6str[i]) && !ItemManager.haveItemId (22+i) && Data_Mission.Missions_list [myid].state <= 9) {
				mission6 [i].SetActive (true);
			} else
				mission6 [i].SetActive (false);
		}
	}

	int last_get = 0;

	public void onMission6Click(int input){
		List<int> id = new List<int>{input + 22};
		List<int> num = new List<int>{1};

		if (Data_Mission.Missions_list [myid].state < 9) {
			Data_Mission.Missions_list [myid].state++;

			//保留暫存檔
			Global.findAndAddItem (UserStatementManager.item_list, UserStatementManager.item_num_list, input + 22, 1);
			Global.addItemPref (id, num);
			ItemManager.user_change_item_amount (UserStatementManager.id, UserStatementManager.token, new List<int>(), new List<int>());

			if(Data_Mission.Missions_list [myid].state==4)
				MessageBox ("哇~是" + Data_Items.Items_list [input + 22].name + "耶!真好吃!!",on4foodgets);
			else if(Data_Mission.Missions_list [myid].state==6)
				MessageBox ("哇~是" + Data_Items.Items_list [input + 22].name + "耶!真好吃!!",on6foodgets);
			else if(Data_Mission.Missions_list [myid].state==8)
				MessageBox ("哇~是" + Data_Items.Items_list [input + 22].name + "耶!真好吃!!",on8foodgets);
			else MessageBox ("哇~是" + Data_Items.Items_list [input + 22].name + "耶!真好吃!!");

		} else if(Data_Mission.Missions_list [myid].state==9){
			last_get = input + 22;
			MessageBox ("哇~是" + Data_Items.Items_list [input + 22].name + "耶!真好吃!我覺得吃的好飽喔~真是感謝你><!",MCcomplete);
		}
		
	}

	void on4foodgets(){
		MessageBox ("好開心喔~哼哼哼~送你禮物吧!!",on4foodgets2);
	}

	void on4foodgets2(){
		MessageBox ("得到了2000分及積分加成藥水!");

		List<int> items = new List<int>{0};
		List<int> nums = new List<int>{1};
		Global.findAndAddItem (UserStatementManager.item_list, UserStatementManager.item_num_list, 0, 1);
		Global.addItemPref(items,nums);
		PlayerPrefs.SetInt ("score", PlayerPrefs.GetInt ("score") + 2000);

		UserStatementManager.give_reward (UserStatementManager.id, UserStatementManager.token, 0, new int[]{}, new List<int>(),new List<int>());
	}

	void on6foodgets(){
		MessageBox ("好開心喔~哼哼哼~送你禮物吧!!",on6foodgets2);
	}

	void on6foodgets2(){

		int item = ItemManager.getRandomEquip ();

		List<int> items = new List<int>{item};
		List<int> nums = new List<int>{1};

		Global.findAndAddItem (UserStatementManager.item_list, UserStatementManager.item_num_list, item, 1);
		Global.addItemPref(items,nums);
		PlayerPrefs.SetInt ("score", PlayerPrefs.GetInt ("score") + 1000);


		MessageBox ("得到了1000分及"+Data_Items.Items_list[item].name+"!");

		UserStatementManager.give_reward (UserStatementManager.id, UserStatementManager.token, 0, new int[]{}, new List<int>(),new List<int>());
	}


	void on8foodgets(){
		MessageBox ("天啊你對我好好喔QQ~吃了好多東西~送你禮物吧!!",on8foodgets2);
	}
	
	void on8foodgets2(){
		MessageBox ("得到了1000分及體力回復藥水!");
		List<int> items = new List<int>{1};
		List<int> nums = new List<int>{1};

		Global.findAndAddItem (UserStatementManager.item_list, UserStatementManager.item_num_list, 1, 1);
		Global.addItemPref(items,nums);
		PlayerPrefs.SetInt ("score", PlayerPrefs.GetInt ("score") + 1000);

		UserStatementManager.give_reward (UserStatementManager.id, UserStatementManager.token, 0, new int[]{}, new List<int>(), new List<int>());
	}

	void MCcomplete(){
		//mission complete!!!
		Data_Mission.Missions_list [myid].state = 10;
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
		mbs.itemToLoad = new List<int>{last_get};
		mbs.numToLoad = new List<int>{1};

		mbs.setMCB ();
		/*
		for (int i=22; i<=40; i++) {
			mbs.itemToLoad.Add (i);
			mbs.numToLoad.Add (-1);
		}*/

	}
	
	//Handle message box
	void MessageBox(string message,System.Action act=null){
		GameObject msg_box = Instantiate (ARMessageBox) as GameObject;
		MessageSet mbs = (MessageSet)(msg_box.GetComponent<MessageSet> ());
		mbs.message = message; 
		mbs.act = act;
	}
}
