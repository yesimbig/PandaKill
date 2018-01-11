using UnityEngine;
using System.Collections;

public class ARChoosingSet : MonoBehaviour {

	public GameObject ARMessageBox,ARMissionIcon;
	public GameObject Content;

	public static int now_id = -1;

	// Use this for initialization
	void Start () {
		now_id = -1;
		for (int i=0; i<AREventHandler.MissionList.Count; i++) {
			GameObject item = Instantiate (ARMissionIcon) as GameObject;
			item.transform.SetParent (Content.transform);	
			MissionSetting missionsetting = (MissionSetting)(item.GetComponent<MissionSetting> ());
			missionsetting.setARMission( AREventHandler.MissionList[i]);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (now_id != -1) {
			DestroyObject(gameObject);
			GameObject msg_box = Instantiate (ARMessageBox) as GameObject;
			MessageSet mbs = (MessageSet)(msg_box.GetComponent<MessageSet> ());
			mbs.message = Data_Mission.Missions_list [now_id].message[ Data_Mission.Missions_list [now_id].state ]; 
		}
		now_id = -1;
	}
}
