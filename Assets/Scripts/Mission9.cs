using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Mission9 : MonoBehaviour {

	/*Mission9:
	 * 掃得每個贊助商的攤位logo可以增加每個技能的攻擊力，最多可翻倍
	 * 
	 * */
	
	string[] mission9str = {
		"MSI",
		"IN_WIN",
		"AHQ",
		"SS"
	};
	
	public GameObject ARMessageBox,ARMCBox;
	GameObject msg_box;
	int myid = 8;
	
	// Use this for initialization
	void Start () {
		Data_Mission.Missions_list [myid].state = PlayerPrefs.GetInt("ESport");
		if(Data_Mission.Missions_list [myid].state>4) Data_Mission.Missions_list [myid].state = 4;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 5; i++) {
			if (AREventHandler.now_track.Equals(mission9str[i]) && PlayerPrefs.GetInt(mission9str[i])==0 ) {
				PlayerPrefs.SetInt(mission9str[i],1);
				PlayerPrefs.SetInt("ESport",PlayerPrefs.GetInt("ESport")+1 );
				Data_Mission.Missions_list [myid].state++;

				if(Data_Mission.Missions_list [myid].state<4){
					MessageBox ("蒐集到"+PlayerPrefs.GetInt("ESport")+"個電競廠商LOGO!攻擊力好像快要變強啦!");
				}
				else if(Data_Mission.Missions_list [myid].state==4){
					Data_Mission.Missions_list [myid].state++;
					MessageBox ("蒐集到"+PlayerPrefs.GetInt("ESport")+"個電競廠商LOGO!攻擊力要變強啦!",MCcomplete);
				}
			}

			else if(AREventHandler.now_track.Equals(mission9str[i]) && Data_Mission.Missions_list [myid].state==4 ) {
				Data_Mission.Missions_list [myid].state++;
				MessageBox ("蒐集到"+PlayerPrefs.GetInt("ESport")+"個電競廠商LOGO!攻擊力要變強啦!",MCcomplete);
			}

		}
	}
	
	void MCcomplete(){
		//mission complete!!!
		GameObject mc_box = Instantiate (ARMCBox) as GameObject;
		MissionCompleteSet mbs = (MissionCompleteSet)(mc_box.GetComponent<MissionCompleteSet> ());
		
		mbs.missionid = Data_Mission.Missions_list[myid].server_id;
		mbs.skills = Data_Mission.Missions_list [myid].skills;
		mbs.scorenum = Data_Mission.Missions_list [myid].score;
		mbs.itemToLoad = new List<int>{1};
		mbs.numToLoad = new List<int>{1};
		mbs.setMCB (false,true);
		gameObject.GetComponent<Mission9> ().enabled = false;
	}
	
	//Handle message box
	void MessageBox(string message,System.Action act=null){
		msg_box = Instantiate (ARMessageBox) as GameObject;
		MessageSet mbs = (MessageSet)(msg_box.GetComponent<MessageSet> ());
		mbs.message = message; 
		mbs.act = act;
	}
}
