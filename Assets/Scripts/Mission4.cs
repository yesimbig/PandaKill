using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission4 : MonoBehaviour {

	/*Mission4:
	 * 消滅掉所有的熊貓，即可勝利
	 * 
	 * 
	 * */
	
	int myid = 3;

	int num_of_panda;
	public GameObject ARMCBox,mission4;
	// Use this for initialization
	void Start () {
		num_of_panda = 7;
	}
	
	void Update () {
		
		if (AREventHandler.now_track == "littleroad" && !ItemManager.haveItemId (13)) {
			mission4.SetActive (true);
		} else {
			mission4.SetActive (false);
		}
	}
	
	public void onPandasDestroy(){
		num_of_panda--;
		if (num_of_panda == 0) {
			MCBox ();
		}
		
	}
	
	//Handle mission complete box
	void MCBox(){
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
		mbs.itemToLoad = new List<int>{0,13};
		mbs.numToLoad = new List<int>{1,1};
		mbs.setMCB ();
		gameObject.GetComponent<Mission4> ().enabled = false;
	}
}
