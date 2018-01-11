using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
public class UrgentEventHandler : MonoBehaviour {

	//戰鬥相關全域變數-------------------------------
	public static int pandaHP = 200000;
	public static float OP_power = 0f;//爆擊機率
	public static int is_addition = 0; //是否是附加狀態
	public static bool is_animate = false; //是否在動畫僵直狀態
	public static int delta_damage;
	public static int delta_buffer;
	static Animator Fight_fox_Ani;


	//-------------------------------------------

	float time;
	float fixed_time_left;
	int ErrTime=0;
	bool error;
	public static bool is_end = false,is_start = false;
	bool is_connecting = true;//是否在連接中

	string CONNECTING_STRING = "Connecting....";

	public Text player_num_value,all_damage_value,your_damage_value,time_left;

	public Text myatk,firstText,secondText,thirdText;
	public Text firstScore,secondScore,thirdScore;

	public GameObject caution,timer,panda,players_num,frame,skill_bar;
	public GameObject fail, victory, award,emergency_title,victory_title,fail_title,calculating;
	public GameObject Exit_btn,hp_bar;


	public Text countdown_text,caution_num, calculateSec;
	public int panda_maxhp;

	public GameObject messageBox;
	public GameObject networkLoading;
	public GameObject UrgentBattleSuccess;

	public GameObject Panda;
	public GameObject HPbar,BONUS;

	//--設定背景音樂------------
	public AudioClip victory_clip, fail_clip,warning_clip;
	public MusicHandler mh;

	static public Vector3 panda_place;
	static public Quaternion panda_rotation;
	float maxHP_length;

	NetworkLoadingSet newNetworkLoading;

	//------award-------
	public Text award_num;
	public GameObject award_Panel;
	public GameObject award_prefab;
	int score;
	List<int> items = new List<int>();
	List<int> items_num = new List<int>();

	// Use this for initialization
	void Start () {
		OP_power = 0f;
		is_addition = 0;
		is_animate = false;
		time = 0;
		delta_buffer = 0;
		delta_damage = 0;
		ErrTime = 0;
		fixed_time_left = 0f;

		is_connecting = true;
		is_end = false;
		is_start = false;

		pandaHP = UrgentManager.pandaHP;
		panda_maxhp = pandaHP;
		Debug.Log ("pandaHP = " + pandaHP);
		maxHP_length = HPbar.transform.localScale.x;
		panda.GetComponent<PandaMove> ().setPandaHP (pandaHP);

		error = false;
		PlayerPrefs.SetString ("urgent_entered", UrgentManager.start_time);
		caution_num.text = "--";
		time_left.text = "-:--";

		score = 0;
		items = new List<int>();
		items_num = new List<int>();

		onCaution ();
		//入場時先確認一次時間
		UrgentManager.get_urgent_info (onCheckTime, onConnErr);	

	}

	int pre_sec = 10;

	// Update is called once per frame
	void Update () {
		if (is_end || !is_start)
			return;

		fixed_time_left -= Time.deltaTime;
		if (fixed_time_left <= 0f) {
			panda.SetActive (true);
			fixed_time_left = 0f;
		}
		if (fixed_time_left <= (float)pre_sec && pre_sec!=0) {
			mh.playOneClip(warning_clip);
			pre_sec = (int)Math.Floor(fixed_time_left);
		}


		if (!error)
			time += Time.deltaTime;

		//戰鬥尚未開始，每五秒鐘確認一次時間
		if (UrgentManager.started == false) {
			onCaution ();

			if (is_connecting){
				caution_num.text = "--";
			}
			else{
				caution_num.text = string.Format ("{0:0}", fixed_time_left>=100 ? 99: fixed_time_left);
				countdown_text.text = "戰鬥即將開始，目前玩家數: "+UrgentManager.player_count.ToString();
			}

			//更新時間
			if (time >= Global.WAITING_TIME) {
				Debug.Log ("Go test");
				UrgentManager.get_urgent_info (onCheckTime, onConnErr);	
				time = 0f;
			}
		} else {//戰鬥開始
			
			onBattleStart ();
			//設定血條
			HPbar.transform.localScale = new Vector3(maxHP_length * (float)pandaHP / (float)panda_maxhp,HPbar.transform.localScale.y);
			//設定熊貓血量
			panda.GetComponent<PandaMove> ().setPandaHP (pandaHP);

			//更新熊貓位置
			panda_place = Panda.transform.position;
			panda_rotation = Panda.transform.rotation;

			//更新時間
			if (is_connecting){
				time_left.text = "-:--";
			}
			else{
				if(fixed_time_left - Global.BUFFER_TIME < 0f){
					fixed_time_left = Global.BUFFER_TIME;
				}
				time_left.text = string.Format ("{0:0}:{1:00}", (int)(fixed_time_left - Global.BUFFER_TIME)/60, (int)(fixed_time_left - Global.BUFFER_TIME)%60);
			}

			//更新傷害
			if(fixed_time_left > Global.BUFFER_TIME){
				your_damage_value.text = (UrgentManager.my_total_damage + delta_damage + delta_buffer).ToString();
				//all_damage_value.text = (UrgentManager.total_damage + delta_damage + delta_buffer).ToString();
				pandaHP = panda_maxhp -  UrgentManager.total_damage - delta_damage - delta_buffer;
				if (pandaHP <= 0){
					BONUS.SetActive(true);
					pandaHP = 0;}
			}
			else{
				//最後一次更新數據
				is_end = true;
				UrgentManager.user_add_damage (UserStatementManager.id, UserStatementManager.token, delta_damage, onEndGame, onEndGame, onEndGame, InvalidToken);
				return;
			}

			if (time >= Global.TRIGGER_TIME) {
				delta_damage += delta_buffer;
				UrgentManager.user_add_damage (UserStatementManager.id, UserStatementManager.token, delta_damage, onAddSuccess, onConnErr, onEndGame, InvalidToken);
				delta_buffer = delta_damage;
				delta_damage = 0;
				time = 0f;
			}
		}
	}

	//----------------設定注意介面--------------------------------------------------

	void onCaution(){
		caution.SetActive (true);
		timer.SetActive (false);
		//panda.SetActive (false);
		players_num.SetActive (false);
		frame.SetActive (false);
		skill_bar.SetActive (false);
		fail.SetActive (false);
		victory.SetActive (false);
		award.SetActive (false);
		calculating.SetActive (false);

		hp_bar.SetActive (false);

		emergency_title.SetActive (true);
		victory_title.SetActive (false);
		fail_title.SetActive (false);
	}

	//----------------設定戰鬥開始介面----------------------------------------------

	void onBattleStart(){
		caution.SetActive (false);
		timer.SetActive (true);
		panda.SetActive (true);
		players_num.SetActive (true);
		frame.SetActive (true);
		skill_bar.SetActive (true);
		fail.SetActive (false);
		victory.SetActive (false);
		award.SetActive (false);

		calculating.SetActive (false);
		hp_bar.SetActive (true);

		emergency_title.SetActive (true);
		victory_title.SetActive (false);
		fail_title.SetActive (false);
	}

	//----------------設定結算介面---------------------------------------------

	void onCalculating(){
		caution.SetActive (false);
		timer.SetActive (false);
		panda.SetActive (true);
		players_num.SetActive (false);
		frame.SetActive (false);
		skill_bar.SetActive (false);

		fail.SetActive (false);
		victory.SetActive (false);
		award.SetActive (false);

		calculating.SetActive (true);

		Exit_btn.SetActive (false);
		hp_bar.SetActive (false);

		emergency_title.SetActive (true);
		victory_title.SetActive (false);
		fail_title.SetActive (false);
	}


	//----------------設定戰鬥成功介面---------------------------------------------

	void onBattleVictory(){
		caution.SetActive (false);
		timer.SetActive (false);
		panda.SetActive (true);
		players_num.SetActive (false);
		frame.SetActive (false);
		skill_bar.SetActive (false);

		fail.SetActive (false);
		victory.SetActive (true);
		award.SetActive (true);
		Exit_btn.SetActive (false);
		hp_bar.SetActive (false);
		calculating.SetActive (false);

		emergency_title.SetActive (false);
		victory_title.SetActive (true);
		fail_title.SetActive (false);

		mh.changeBGM (victory_clip);
	}

	//------------設定戰鬥失敗介面--------------------------------------------------

	void onBattleFail(){
		caution.SetActive (false);
		timer.SetActive (false);
		panda.SetActive (true);
		players_num.SetActive (false);
		frame.SetActive (false);
		skill_bar.SetActive (false);

		fail.SetActive (true);
		victory.SetActive (false);
		award.SetActive (true);
		calculating.SetActive (false);

		Exit_btn.SetActive (false);
		hp_bar.SetActive (false);

		emergency_title.SetActive (false);
		victory_title.SetActive (false);
		fail_title.SetActive (true);

		mh.changeBGM (fail_clip);
	}

	//----------------------------------------------按鈕事件----------------------------------------------------------------------

	public void onBackButtonClick(){
		if(CameraBG.is_start)
			CameraBG.pauseCamera ();
		NetworkLoading ("退場中，請稍後...");
		UrgentManager.urgent_exit (UserStatementManager.id, UserStatementManager.token, onExit, onConnErr, onExit, InvalidToken);


	}

	void onExit(){
		newNetworkLoading.Clear ();
		StartCoroutine (LoadingScene ("_main"));
	}
	public void onCompleteButtonClick(){
		NetworkLoading ("更新資料庫中，請稍後...");
		UserStatementManager.give_reward (UserStatementManager.id, UserStatementManager.token, score, new int[0], items, items_num, onGetReward, onGetRewardFail, InvalidToken);
	}

	void onGetReward(){
		newNetworkLoading.Clear ();
		StartCoroutine(LoadingScene("_main"));
	}

	void onGetRewardFail(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("無法連線至網路，請重試", onCompleteButtonClick);
	}
	//----------------------------------------------按鈕事件----------------------------------------------------------------------

	//----------------------------------------------校正時間事件---------------------------------------------------------------------
	void onCheckTime(){
		ErrTime = 0;
		is_start = true;
		is_connecting = false;

		if (UrgentManager.started) {
			player_num_value.text = "x"+UrgentManager.player_count.ToString();
			all_damage_value.text = showAllRank();
			your_damage_value.text = "0";
			pandaHP = panda_maxhp -  UrgentManager.total_damage;
		}

		//校正時間
		if (Math.Abs (fixed_time_left - UrgentManager.time_left) > 1f) {
			fixed_time_left = (float)UrgentManager.time_left;
		}
	}

	string showAllRank(){
		string s = "";
		for (int i = 0; i < UrgentManager.rank.Count; i++) {
			s += Global.fixed_string(UrgentManager.rank [i].nickname) + ": " + UrgentManager.rank [i].damage + "\n";
		}
		return s;	
	}



	void onAddSuccess(){
		delta_buffer = 0;
		ErrTime = 0;
		is_connecting = false;
		player_num_value.text = "x" + UrgentManager.player_count.ToString();
		all_damage_value.text = showAllRank();
		your_damage_value.text = UrgentManager.my_total_damage.ToString ();

		pandaHP = panda_maxhp -  UrgentManager.total_damage;
		if (pandaHP < 0)
			pandaHP = 0;

		panda.GetComponent<PandaMove> ().setPandaHP (pandaHP);

		//校正時間
		if (Math.Abs (fixed_time_left - UrgentManager.time_left) > 1f) {
			fixed_time_left = (float)UrgentManager.time_left;
		}
	}
	//----------------------------------------------校正時間事件---------------------------------------------------------------------

	//--------------------------------------------------戰鬥結束後的狀態--------------------------------------------------------------------------
	//當戰鬥結束
	void onEndGame(){

		panda.GetComponent<PandaMove> ().setDead();
		StartCoroutine (WaitForEnd());
	}

	//等待五秒後才結算成績
	IEnumerator WaitForEnd(){
		time_left.text = "0:00";
		onCalculating ();
		for (int i = 5; i >= 1; i--) {
			calculateSec.text = i.ToString ();
			yield return new WaitForSeconds (1f);
		}

		CalculateScore ();
	}

	void CalculateScore(){
		if (newNetworkLoading == null) {
			NetworkLoading ("結算成績中，請稍後...");
			UrgentManager.get_damage (UserStatementManager.id, UserStatementManager.token, onUrgentCompleted, onRecalculate, InvalidToken);
		}
	}

	//結算成功
	void onUrgentCompleted(){
		newNetworkLoading.Clear ();
		player_num_value.text = "x" + UrgentManager.player_count.ToString ();
		/*all_damage_value.text = UrgentManager.total_damage.ToString();
		your_damage_value.text = UrgentManager.my_total_damage.ToString ();*/
		CameraBG.pauseCamera ();


		myatk.text = UrgentManager.my_total_damage.ToString ();

		if (UrgentManager.rank.Count > 0) {
			firstText.text = UrgentManager.rank [0].nickname + ": ";
			firstScore.text = UrgentManager.rank [0].damage.ToString();
		}
		if (UrgentManager.rank.Count > 1) {
			secondText.text = UrgentManager.rank [1].nickname + ": ";
			secondScore.text =  UrgentManager.rank [1].damage.ToString();
		}
		if (UrgentManager.rank.Count > 2) {
			thirdText.text = UrgentManager.rank [2].nickname + ": ";
			thirdScore.text =  UrgentManager.rank [2].damage.ToString();
		}
		//secondText,thirdText
		if (pandaHP <= 0){
			score = 1000 + (int)((float)UrgentManager.my_total_damage/20);
			items = new List<int>{ UnityEngine.Random.Range(0,2),ItemManager.getRandomEquip() };
			items_num = new List<int>{ 1,1 };
			award_num.text = score.ToString ();

			for (int i = 0; i < items.Count; i++) {
				if (items_num [i] <= 0)
					continue;
				GameObject item = Instantiate (award_prefab) as GameObject;
				item.transform.SetParent (award_Panel.transform);	
				ItemSetting itemsetting = (ItemSetting)(item.GetComponent<ItemSetting> ());
				itemsetting.setIcon (0, items [i]);
			}
			onBattleVictory ();
		}

		else{
			score = 800;
			items = new List<int>{};
			items_num = new List<int>{};
			award_num.text = score.ToString ();

			for (int i = 0; i < items.Count; i++) {
				if (items_num [i] <= 0)
					continue;
				GameObject item = Instantiate (award_prefab) as GameObject;
				item.transform.SetParent (award_Panel.transform);	
				ItemSetting itemsetting = (ItemSetting)(item.GetComponent<ItemSetting> ());
				itemsetting.setIcon (0, items [i]);
			}
			onBattleFail ();
		}
		/*
		GameObject success_msg_box = Instantiate (UrgentBattleSuccess) as GameObject;
		BattleSuccessBoxSetting mbs = (BattleSuccessBoxSetting)(success_msg_box.GetComponent<BattleSuccessBoxSetting> ());
		mbs.setall ("0",1000,new List<int>(),new List<int>()); */
	}

	//結算失敗，重新結算
	void onRecalculate(){
		newNetworkLoading.Clear ();
		ErrorMessageBox("結算失敗!請重新結算",CalculateScore);
	}
	//--------------------------------------------------戰鬥結束後的狀態--------------------------------------------------------------------------


	//--------------------------------------------------錯誤訊息----------------------------------------------------------------------------------
	//超過五次連線失敗則直接跳出錯誤訊息
	void onConnErr(){
		is_connecting = true;
		ErrTime++;
		if (ErrTime > 5 && error == false) {
			error = true;
			Debug.Log ("GG!!!");
			ErrorMessageBox("已斷線!!是否重新連接?",reconnect);
		}
	}

	void reconnect(){
		error = true;
		ErrTime = 0;
	}

	//不允許多登
	void InvalidToken(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("此帳號在其他地方已登入，請重登!",restart);
	}
	
	//重新登入
	void restart(){
		if (CameraBG.is_start)
			CameraBG.pauseCamera ();
		StartCoroutine(LoadingScene("_title"));	
	}
	//--------------------------------------------------錯誤訊息----------------------------------------------------------------------------------


	//-------------------------------------------process event--------------------------------------------------	
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
