using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class MissionSetting : MonoBehaviour {

	public GameObject Icons;
	public Image icon;
	Sprite[] missions;
	public Text mission_name;
	public GameObject choose,black,warning;
	int Myid;

	// Use this for initialization
	void Start () {
		black.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		if (choose != null) {
			if (allMissionSetting.now_id == Myid) {
				choose.SetActive (true);
			} else
				choose.SetActive (false);

			if (allMissionSetting.now_tab == Global.MISSION_COMPLETED && allMissionSetting.now_id != Myid) {
				black.SetActive (true);
			} else
				black.SetActive (false);
		}
	}
	
	public void setMission(int id){
		Myid = id;
		Icons icons = Icons.GetComponent<Icons> ();
		missions = icons.missions;

		if(id < missions.Length)
			icon.sprite = missions[id];
		this.transform.localPosition = new Vector2(0,0);
		this.transform.localScale = Vector3.one;            
		
		RectTransform rt = this.GetComponent<RectTransform> ();
		rt.anchoredPosition = new Vector2(0,0);
		mission_name.text = Data_Mission.Missions_list [id].name;

		int server_id = Data_Mission.Missions_list[id].server_id;
		int sid =  MissionManager.id.IndexOf( server_id );
		
		if(MissionManager.close_time[sid]!="" && allMissionSetting.now_tab != Global.MISSION_COMPLETED){
			DateTime d = DateTime.Parse (MissionManager.close_time[id]);
			TimeSpan tt = d - DateTime.Now ;
			if(tt.Hours < 1){
				warning.SetActive(true);
			}
		}
	}

	public void setARMission(int id){
		Myid = id;
		Icons icons = Icons.GetComponent<Icons> ();
		missions = icons.missions;

		if(id < missions.Length)
			icon.sprite = missions[id];
		this.transform.localPosition = new Vector2(0,0);
		this.transform.localScale = Vector3.one;            
		
		RectTransform rt = this.GetComponent<RectTransform> ();
		rt.anchoredPosition = new Vector2(0,0);
		mission_name.text = Data_Mission.Missions_list [id].name;
	}
	
	public void onMissionClick(){
		allMissionSetting.now_id = Myid;
		allMissionSetting.trigger = true;
		ARChoosingSet.now_id = Myid;
	}
}
