using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Vuforia;

public class AREventHandler : MonoBehaviour {

	public Text now;
	public static string now_track = "";
	public GameObject itemPrefab,Content;
	public GameObject arhandler;

	public GameObject ARMessageBox, ARChoosingBox;

	public GameObject networkLoading;
	NetworkLoadingSet newNetworkLoading;

	bool is_box_open;
	GameObject items;
	public static List<int> MissionList = new List<int>();
	public static bool trigger = false;

	public static bool m_bIsFocus;

	public GameObject[] ArPic;

	public MusicHandler mh;
	public AudioClip coin_clip;


	List<int> ArList1 = new List<int>{39,40,41,48};
	List<int> ArList2 = new List<int>{42};
	List<int> ArList3 = new List<int>{0,7,8,14,15,27,28,31};
	List<int> ArList4 = new List<int>{5};
	List<int> ArList5 = new List<int>{1,2,6,9,10,12,20,21,22};
	List<int> ArList6 = new List<int>{0,3,4,7,13,14,15,16,17,18,25,26,29,30,31,32,33,34,35};
	List<int> ArList7 = new List<int>{47};
	List<int> ArList8 = new List<int>{1,11,20,21,22,24,36,38};
	List<int> ArList9 = new List<int>{43,44,45,46};

	// Use this for initialization
	void Start () {
		//關閉BGM
		mh.StopBGM ();

		now_track = "";
		is_box_open = false;
		arhandler.SetActive (true);
		MissionList.Clear ();
		//判定各項任務狀態

		m_bIsFocus = false;
		CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		setAllMission ();
	}
	
	// Update is called once per frame
	void Update () {

		if (now.text == "" && now_track != "")
			mh.playOneClip (coin_clip);

		now.text = now_track;



		if (trigger) {
			setAllMission();
			trigger = false;
		}

		//if (m_bIsFocus)
		#if UNITY_EDITOR
		if(Input.GetMouseButtonUp(0))
			#elif UNITY_ANDROID || UNITY_IPHONE
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
				#endif
		{
			CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		}

	}

	void setAllMission (){

		gameObject.GetComponent<Mission1>().enabled = false;
		gameObject.GetComponent<Mission2>().enabled = false;
		gameObject.GetComponent<Mission3>().enabled = false;
		gameObject.GetComponent<Mission4>().enabled = false;
		gameObject.GetComponent<Mission5>().enabled = false;
		gameObject.GetComponent<Mission6>().enabled = false;
		gameObject.GetComponent<Mission7>().enabled = false;
		gameObject.GetComponent<Mission8>().enabled = false;
		gameObject.GetComponent<Mission9>().enabled = false;
		MissionList.Clear ();

		for (int i = 0; i<Global.MISSION_NUM; i++) {
			int server_id = Data_Mission.Missions_list[i].server_id;
			int id =  MissionManager.id.IndexOf( server_id );
			
			if(  id==-1 )continue;
			if(MissionManager.mission_state[id] != Global.MISSION_TOKEN) continue;
			MissionList.Add (i);
			if (i == 0) {
				gameObject.GetComponent<Mission1> ().enabled = true;
				ARActive (ArList1);
			}
			if (i == 1) {
				gameObject.GetComponent<Mission2> ().enabled = true;
				ARActive (ArList2);
			}
			if (i == 2) {
				gameObject.GetComponent<Mission3> ().enabled = true;
				ARActive (ArList3);
			}
			if(i==3) {
				gameObject.GetComponent<Mission4> ().enabled = true;
				ARActive (ArList4);
			}
			if(i==4) {
				gameObject.GetComponent<Mission5> ().enabled = true;
				ARActive (ArList5);
			}
			if(i==5){
				gameObject.GetComponent<Mission6> ().enabled = true;
				ARActive (ArList6);
			}
			if(i==6){
				gameObject.GetComponent<Mission7> ().enabled = true;
				ARActive (ArList7);
			}
			if(i==7){
				gameObject.GetComponent<Mission8> ().enabled = true;
				ARActive (ArList8);
			}
			if(i==8){
				gameObject.GetComponent<Mission9> ().enabled = true;
				ARActive (ArList9);
			}
		}
		
	}

	void ARActive(List<int> l){
		for (int i = 0; i < l.Count; i++) {
			ArPic [l [i]].SetActive (true);		
		}	
	}

	public void onBackButtonClick(){
		StartCoroutine(LoadingScene("_main"));
	}

	public void onItemButtonClick(){
		if (!is_box_open) {
			items = Instantiate (itemPrefab) as GameObject;
			items.transform.SetParent (Content.transform);
			items.transform.localScale = new Vector3(1,1,1);
			items.transform.localPosition = new Vector3(0,0,0);
			is_box_open = true;

		} else {			
				DestroyObject(items);
			is_box_open = false;
		}
	}

	public void onMissionButtonClick(){
		if (MissionList.Count > 1) {
			Instantiate (ARChoosingBox);
		} else if (MissionList.Count == 1) {
			GameObject msg_box = Instantiate (ARMessageBox) as GameObject;
			MessageSet mbs = (MessageSet)(msg_box.GetComponent<MessageSet> ());
			mbs.message = Data_Mission.Missions_list [MissionList [0]].message [Data_Mission.Missions_list [MissionList [0]].state]; 
		} else {
			GameObject msg_box = Instantiate (ARMessageBox) as GameObject;
			MessageSet mbs = (MessageSet)(msg_box.GetComponent<MessageSet> ());
			mbs.message = "你目前還沒有接取任何任務喔!快回到主畫面的任務欄確認看看吧!"; 
		}
	}

	//Handle network loading
	void NetworkLoading(string message = null){
		newNetworkLoading = (Instantiate (networkLoading) as GameObject).GetComponent<NetworkLoadingSet>();
		
		if (message != null)
			newNetworkLoading.message = message;
	}
	
	//Handle loading next scene
	IEnumerator LoadingScene(string scene){
		NetworkLoading ("載入中...請稍候...");
		AsyncOperation asyncf = Application.LoadLevelAsync (scene);
		if (asyncf == null)
			Debug.Log ("GG");
		while (!asyncf.isDone) {
			newNetworkLoading.setMessage("載入中...請稍候..."+((int)(asyncf.progress*100)).ToString ()+"%");
			yield return null;
		}
		newNetworkLoading.Clear ();
	}

}
