using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class answerManager : MonoBehaviour {

	public GameObject ARMessageBox,ARMCBox;
	public Text question;
	public Text[] answer_text;
	public Image[] answer_block;
	public Button[] answer_btn;
	public Button submit;
	public GameObject[] correct,wrong;

	string[] questions = {
		"Q1：本次名人演講由哪一個社團所舉辦的呢？",
		"Q2：本次名人演講在何處舉行呢？",
		"Q3：本次名人演講邀請到哪位演講者呢？",
		"Q4: 本次名人演講的演講主題是哪一個呢？",
		"Q5: 參加本次名人演講有甚麼好康呢？"
	};

	string[,] ans = {
		{ "梅竹後援會", "北友會", "熱舞社", "吉他社" },
		{ "中正堂", "國際會議廳", "合勤講堂", "實驗劇場" },
		{ "易智言", "Janet", "柯文哲", "宋少卿" },
		{ "平步卿雲", "說話的幸福呢","卿聲戲語", "戲說人生" },
		{ "可蓋藝文護照", "掌握成功的秘訣", "不懼絲毫的失敗", "以上皆是" }
	};

	int[] ans_num = { 0, 1, 3, 2, 3 };

	int state = 0;
	int send = -1;

	Color32 white = new Color32 (255, 255, 255, 240);
	Color32 yellow = new Color32 (255, 255, 85, 240);
	Color32 unchoose = new Color32 (255, 255, 255, 70);

	int myid = 6;

	// Use this for initialization
	void Start () {
		state = 0;
		send = -1;
		set_question (state);
	}


	void set_question(int s){
		send = -1;
		question.text = questions [s];
		question.color = Color.white;
		for (int i = 0; i < 4; i++) {
			answer_btn [i].interactable = true;
			submit.interactable = true;
			answer_text [i].text = ans [s, i];
			answer_block [i].color = white;
			correct [i].SetActive (false);
			wrong [i].SetActive (false);
		}
	}

	public void onAnsbtnClick(int n){
		send = n;
		for (int i = 0; i < 4; i++) {
			if (i == n)
				answer_block [i].color = yellow;
			else answer_block [i].color = white;
		}
	}

	public void onSubmitClick(){
		if (send == -1)
			return;
		question.color = unchoose;
		for (int i = 0; i < 4; i++) {
			answer_btn [i].interactable = false;
			submit.interactable = false;
			if(i!=send)	answer_block[i].color = unchoose;
		}

		if (ans_num [state] == send) {
			correct [send].SetActive (true);
			StartCoroutine(delay(2f));
		} else {
			wrong [send].SetActive (true);
			StartCoroutine(delaywrong(2f));
		}
	}

	IEnumerator delay(float delay){
		yield return new WaitForSeconds (delay);
		state++;

		if (state == 5) {
			MessageBox ("恭喜你!你已經答對了所有問題!", onComplete);
		
		} else {
			set_question (state);
		}
	}

	IEnumerator delaywrong(float delay){
		yield return new WaitForSeconds (delay);
		MessageBox ("很抱歉!你答錯了嗚嗚嗚，請重新開始吧!", onWrong);
	}


	void onComplete(){
		MCBox (Data_Mission.Missions_list [myid].server_id);	
	}

	void onWrong(){
		state = 0;
		set_question (0);
	}


	//Handle message box
	void MessageBox(string message ,System.Action act = null){
		GameObject msg_box = Instantiate (ARMessageBox) as GameObject;
		MessageSet mbs = (MessageSet)(msg_box.GetComponent<MessageSet> ());
		mbs.message = message; 
		mbs.act = act;
	}

	//Handle mission complete box
	void MCBox(int missionid){

		GameObject mc_box = Instantiate (ARMCBox) as GameObject;
		MissionCompleteSet mbs = (MissionCompleteSet)(mc_box.GetComponent<MissionCompleteSet> ());

		mbs.missionid = missionid;

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

		mbs.scorenum = Data_Mission.Missions_list[myid].score;
		mbs.itemToLoad = new List<int>{1,41};
		mbs.numToLoad = new List<int>{1,1};
		mbs.setMCB (true);
	}
}
