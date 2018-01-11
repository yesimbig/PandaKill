using UnityEngine;
using System.Collections;
using System;

public class infoSetting : MonoBehaviour {

	public TextMesh score; 
	public TextMesh hour,minute,second,urgent_info;
	public GameObject[] eat_empty;
	public GameObject timeText;

	public GameObject emergencyBox;
	public GameObject messageBox;
	public GameObject networkLoading;
	NetworkLoadingSet newNetworkLoading;
	public GameObject urgent_info_box;
	public GameObject bg;
	public FoxSkinSetting[] fss;

	GameObject emergency_obj;

	bool emergencyBox_open = false; //確認緊急任務方格是否開啟
	bool battlechk = false; //是否已確認下次戰鬥時間
	bool updatechk = false; //是否已確認下次更新時間
	bool trigger = false;
	bool check = false;

	DateTime nextTime,startTime;//確認下次重新計時時間
	DateTime now,start;
	TimeSpan MM;
	Animator BGAni;

	//設定狐狸換裝
	public static bool foxTrigger = false;

	// Use this for initialization
	void Start () {
		battlechk = false;
		updatechk = false;
		trigger = false;
		foxTrigger = false;
		score.text = UserStatementManager.score.ToString();
		BGAni = bg.GetComponent<Animator> ();
		setBG (true);

		int eye = UserStatementManager.eye;
		int tail = UserStatementManager.tail;
		int color = UserStatementManager.color;
		int wearing = UserStatementManager.wearing;
		if (eye < 1 || eye > 4)
			eye = 1;
		if (tail < 1 || tail > 4)
			tail = 1;
		if(color < 1 || color > 4)
			color = 1;
		if(wearing < 1 ||wearing > 4)
			wearing = 1;

		for(int i=0;i<3;i++)
			fss[i].set (eye, tail, color, wearing);

		foxTrigger = true;

		//Debug.Log (UrgentManager.start_time);
	}

	void Update () {

	//	Debug.Log (updateTime.Ticks);

		TimeSpan updateTime = Global.NEXT_MIDNIGHT - DateTime.Now;
		if (updateTime.Ticks < 0 && battlechk == false && updatechk == false) {
			updatechk = true;
			NetworkLoading("更新時間中，請稍後...");
			UserStatementManager.get_full_info(UserStatementManager.id,UserStatementManager.token,onChangeDone,ConnErr,InvalidToken);
		}

		//狐狸換裝
		if (foxTrigger) {
			int eye = UserStatementManager.eye;
			int tail = UserStatementManager.tail;
			int color = UserStatementManager.color;
			int wearing = UserStatementManager.wearing;
			if (eye < 1 || eye > 4)
				eye = 1;
			if (tail < 1 || tail > 4)
				tail = 1;
			if(color < 1 || color > 4)
				color = 1;
			if(wearing < 1 ||wearing > 4)
				wearing = 1;
			for(int i=0;i<3;i++)
				fss[i].set (eye, tail, color, wearing);
			foxTrigger = false;
		}

		//重設飽食度
		int repletion = UserStatementManager.repletion;
		for (int i=0; i<4; i++) {
			if(i<repletion)eat_empty[i].SetActive(false);
			else eat_empty[i].SetActive(true);
		}


		setBG (false);

		//設定緊急任務之時間
		if (UrgentManager.start_time == null || UrgentManager.start_time.Length == 0) {
			urgent_info.text = "戰 鬥 未 載 入";
			timeText.SetActive (false);

		} else {
			now = System.DateTime.Now;
			start = DateTime.Parse (UrgentManager.start_time);
			MM = start - now;

			//if(trigger == true)
			//Debug.Log ((nextTime - now).TotalSeconds);

			if ( (nextTime - now).TotalSeconds > 0) {
				//Debug.Log("nextTime =" + nextTime); 
				trigger = true;
			}
			else if( (nextTime - now).TotalSeconds <= 0 && trigger == true){
				UrgentManager.started = false;
				UrgentManager.opened = false;
				UrgentManager.start_time = UrgentManager.next_start_time; 
				trigger = false;
			}



			if(UrgentManager.started || MM.TotalSeconds < 30){
				urgent_info.text = "戰 鬥 即 將 開 始!!";
				urgent_info_box.SetActive(true);
				timeText.SetActive (false);

				//確認下次戰鬥時間
				if(emergency_obj==null && !battlechk){
					battlechk = true;
					checkUrgentTime();
				}
			}
			else{
				PlayerPrefs.DeleteKey("urgent_entered");
				battlechk = false;
				emergencyBox_open = false;
				if(emergency_obj!=null){
					DestroyObject(emergency_obj);
				}
				timeText.SetActive (true);
				urgent_info_box.SetActive(false);

				TimeSpan minus30 = MM.Subtract(TimeSpan.FromSeconds(30));


				timeText.SetActive (true);
				hour.text = (minus30.Days * 24 + minus30.Hours).ToString ("00");
				minute.text = minus30.Minutes.ToString ("00");	
				second.text = minus30.Seconds.ToString ("00");
			}
		}
	}

	//設定時間及背景
	void setBG (bool start){
		int now_hour = int.Parse (System.DateTime.Now.ToString ("HH"));
		if (now_hour >= 6 && now_hour <= 16) {
			BGAni.SetBool ("is_morning", true);
			BGAni.SetBool ("is_dusk", false);
			BGAni.SetBool ("is_night", false);
		}
		else if ( now_hour >= 17 && now_hour < 19 ) {
			BGAni.SetBool ("is_morning", false);
			BGAni.SetBool ("is_dusk", true);
			BGAni.SetBool ("is_night", false);
		}
		else{
			BGAni.SetBool ("is_morning", false);
			BGAni.SetBool ("is_dusk", false);
			BGAni.SetBool ("is_night", true);
		}

		if (start)
			BGAni.SetTrigger ("start");
	}


	void onChangeDone(){
		newNetworkLoading.Clear ();
		updatechk = false;
	}

	//確認緊急任務房間是否開啟
	public void checkUrgent(){
		NetworkLoading ("連線中，請稍後...");
		UrgentManager.get_urgent_info (onCheckDone, ConnErr,InvalidToken);
	}

	//確認資料
	void onCheckDone(){
		//如果房間開啟，進入房間
		newNetworkLoading.Clear ();
		if (!UrgentManager.opened) {
			ErrorMessageBox ("房間尚未開放...請稍後重試");
		} else if (UrgentManager.player_count >= UrgentManager.player_limit) {
			DestroyObject(emergency_obj);
			ErrorMessageBox ("目前入場人數已達到上限!");
		}else{
			DestroyObject(emergency_obj);
			NetworkLoading ("入場中，請稍後...");
			UrgentManager.user_add_damage(UserStatementManager.id,UserStatementManager.token,0,onGotoUrgent,ConnErr,InvalidToken);


			//Application.LoadLevelAsync ("_urgent");
			//Debug.Log ("enter!!");
		} 

	}

	void onGotoUrgent(){
		StartCoroutine (LoadingScene("_urgent"));
	}

	void checkUrgentTime(){
		NetworkLoading("取得下次戰鬥時間中...請稍後");
		UrgentManager.get_urgent_info(checkSuccess,checkErr);

	}

	//確認戰鬥時間成功
	void checkSuccess(){
		newNetworkLoading.Clear ();
		//trigger = true;

		//顯示出警告畫面
		if(!emergencyBox_open && UrgentManager.opened && PlayerPrefs.GetString("urgent_entered")!=null && PlayerPrefs.GetString("urgent_entered")!=UrgentManager.start_time){
			emergencyBox_open = true;
			PlayerPrefs.SetString ("urgent_entered", UrgentManager.start_time);
			emergency_obj = Instantiate (emergencyBox) as GameObject;
			EmergencyBoxSetting ebs = (EmergencyBoxSetting)(emergency_obj.GetComponent<EmergencyBoxSetting> ());
			ebs.act = checkUrgent;
		}

		if (UrgentManager.started) {
			nextTime = System.DateTime.Now.AddSeconds (UrgentManager.time_left - 30);
		} else if (UrgentManager.opened) {
			startTime = System.DateTime.Now.AddSeconds (UrgentManager.time_left);
			nextTime = System.DateTime.Now.AddSeconds (UrgentManager.time_left + 60);
			Debug.Log (nextTime);
		} else {
			startTime = DateTime.Parse (UrgentManager.start_time);
		}
	}

	//確認戰鬥時間失敗，重試
	void checkErr(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("確認時間失敗，請重試!!",checkUrgentTime);
	}


	//不允許多登
	void InvalidToken(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("此帳號在其他地方已登入，請重登!",restart);
	}
	
	//重新登入
	void restart(){
		Application.LoadLevelAsync ("_title");	
	}


	void ConnErr(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("連線失敗!!");
	}

	//Handle Error message
	void ErrorMessageBox(string message,System.Action act = null){
		GameObject err_msg_box = Instantiate (messageBox) as GameObject;
		MessageBoxSetting mbs = (MessageBoxSetting)(err_msg_box.GetComponent<MessageBoxSetting> ());
		mbs.message = message; 
		mbs.act = act;
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
