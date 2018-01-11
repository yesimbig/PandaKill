using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class allSkillSetting : MonoBehaviour {
	
	public GameObject itemPrefab;
	public GameObject Content;
	public GameObject networkLoading,messageBox;
	public Text description,damage_cd;
	public Sprite[] used;
	public GameObject b;

	NetworkLoadingSet newNetworkLoading;

	//--------------全域傳入資訊--------------------
	public static int now_id;
	public static GameObject go;
	public static bool is_equip;
	//--------------------------------------------

	float iniWidth ,iniHeight;

	List<int> temp = new List<int>();
	int equip;
	// Use this for initialization
	void Start () {
		equip = 0;
		now_id = UserStatementManager.skill_id_list[0];
		iniWidth = Content.GetComponent<RectTransform> ().rect.width;
		iniHeight = Content.GetComponent<RectTransform> ().rect.height;
		set();
	}
	
	public void set(){
		int id,id_num;
		for (int i = 0; i<UserStatementManager.skill_id_list.Length; i++) {
			id = UserStatementManager.skill_id_list[i];

			if(id == 5 && i!= UserStatementManager.skill_id_list.Length-1 && UserStatementManager.skill_id_list[i+1]==6)continue;
			if(id == 6 && i!= UserStatementManager.skill_id_list.Length-1 && UserStatementManager.skill_id_list[i+1]==7)continue;

			//id = i;
			bool is_equip = false;
			if( having(id) != -1){
				temp.Add (id);
				equip++;
				is_equip = true;
			}

			GameObject item = Instantiate (itemPrefab) as GameObject;
			item.transform.SetParent (Content.transform);
			
			SkillSetting skillsetting = (SkillSetting)(item.GetComponent<SkillSetting> ());
			skillsetting.setSkill(id,is_equip);
		}



	}
	
	void Update(){	


		//戰鬥攻擊加成-------------------------
		float multi = 1f;
		int server_id = Data_Mission.Missions_list[8].server_id;
		int id =  MissionManager.id.IndexOf( server_id );
		if (MissionManager.mission_state [id] == Global.MISSION_COMPLETED)
			multi = 2f;
		int damage = (int)(Data_Skills.Skills_list [now_id].damage * multi);
		//-------------------------------------

		description.text = Data_Skills.Skills_list [now_id].description;

		if(Data_Skills.Skills_list [now_id].type==0)	
			damage_cd.text = "傷害: "+ damage.ToString() +"，CD: "+Data_Skills.Skills_list[now_id].cd.ToString() + "秒";
		else if(Data_Skills.Skills_list [now_id].type==1)
			damage_cd.text = "CD: "+Data_Skills.Skills_list[now_id].cd.ToString() + "秒";
		else if(Data_Skills.Skills_list [now_id].type==2)	
			damage_cd.text = "傷害: "+damage.ToString()+"("+ (damage*2).ToString() +")，CD: "+Data_Skills.Skills_list[now_id].cd + "秒";
		else if(Data_Skills.Skills_list [now_id].type==3)
			damage_cd.text = "每秒"+damage.ToString()+"傷害，CD: "+Data_Skills.Skills_list[now_id].cd.ToString() + "秒";
		else if(Data_Skills.Skills_list [now_id].type==4)
			damage_cd.text = "蓄力最高"+(damage*(int)Data_Skills.Skills_list [now_id].other).ToString()+"傷害，CD: "+Data_Skills.Skills_list[now_id].cd.ToString() + "秒";

		if(is_equip)
			b.GetComponent<Image> ().sprite = used [1];
		else b.GetComponent<Image> ().sprite = used [0];
	}

	public void onSelectionBtnClick(){
		Debug.Log (now_id);
		Debug.Log (is_equip);

		if(!is_equip){
			if(equip >=4){
				ErrorMessageBox("最多只能選四個技能!!");
			}
			else{
				SkillSetting skillsetting = (SkillSetting)(go.GetComponent<SkillSetting> ());
				skillsetting.changeUsed(true);
				is_equip = true;
				temp.Add(now_id);
				equip++;
			}
		}
		else{
			SkillSetting skillsetting = (SkillSetting)(go.GetComponent<SkillSetting> ());
			skillsetting.changeUsed(false);
			temp.Remove(now_id);
			is_equip = false;
			equip--;
		}	
	}

	int having(int id){
		for(int i=0;i<UserStatementManager.equip_skill_id_list.Length;i++){
			if(UserStatementManager.equip_skill_id_list[i] == id) return i;
		}
		return -1;
	}

	public void onExitButtonClick(){

		if (temp.Count != 4 && UserStatementManager.skill_id_list.Length != equip) {
			ErrorMessageBox ("你還有技能可以使用喔!");
		} else {
			int[] list = new int[temp.Count];
			for (int i=0; i<temp.Count; i++) {
				list [i] = temp [i];
			}

			if (!checkequip ()) {
				NetworkLoading ("設定技能中，請稍後...");
				SkillManager.set_equip_skill (UserStatementManager.id, UserStatementManager.token, list, onSetDone, onSetFail, InvalidToken);
			} else {
				DestroyObject (gameObject);
			}
		}
	}

	bool checkequip(){

		if (temp.Count != UserStatementManager.equip_skill_id_list.Length)
			return false;

		for (int i = 0; i < temp.Count; i++) {
			if (temp.IndexOf (UserStatementManager.equip_skill_id_list [i]) == -1)
				return false;
		}
		return true;
	}

	void onSetDone(){
		newNetworkLoading.Clear ();
		DestroyObject (gameObject);
	}

	void onSetFail(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("設定失敗!!");
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

	//Handle Error message
	void ErrorMessageBox(string message,System.Action act=null){
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

}
