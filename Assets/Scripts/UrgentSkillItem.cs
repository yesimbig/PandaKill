using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UrgentSkillItem : MonoBehaviour {

	public GameObject Icons;
	public Text SkillSec;
	public int skill_id = 0;

	public GameObject damage_ani,hit_ani,skill_sustain;
	public Vector3 panda;

	public AudioClip[] audio;

	Sprite[] icon,icon_cd;

	Sprite my_icon,my_icon_cd;
	int skill_type = 0;
	float skill_Cd = 5F;
	float skill_Sustain = 0f;
	int skill_Damage = 0;
	float skill_delay = 0f;

	float skill_timeleft = 0;//技能CD運行時間
	bool is_sustain_running = false;//是否有附加效果
	Image im;
	GameObject new_damage_ani;

	bool is_hold = false;//是否有成功按下按鈕

	public static int trigger =-1 ;

	float multi = 1f;

	// Use this for initialization
	void Start () {
		//----------將按紐位置設定正確------------
		this.transform.localPosition = new Vector2(0,0);
		this.transform.localScale = Vector3.one;            
		RectTransform rt = this.GetComponent<RectTransform> ();
		rt.anchoredPosition = new Vector2(0,0);
		//---------------------------------------

		//戰鬥攻擊加成-------------------------
		int server_id = Data_Mission.Missions_list[8].server_id;
		int id =  MissionManager.id.IndexOf( server_id );
		if (MissionManager.mission_state [id] == Global.MISSION_COMPLETED)
			multi = 2f;
		//-------------------------------------

		im = this.GetComponent<Image>();
		skill_sustain.SetActive (false);
		trigger = -1;
		is_hold = false;
	}

	// Update is called once per frame
	void Update () {
		//-----設定按鈕的時間以及圖片------------------
		if (skill_timeleft <= 0F&&!UrgentEventHandler.is_animate && !UrgentEventHandler.is_end) {
			skill_timeleft = 0F;
			im.sprite = my_icon;
			SkillSec.text = "";
		} else {

			im.sprite = my_icon_cd;

			//設定CD時間與顯示文字---------------------------
			if(skill_timeleft>0f){
				SkillSec.text = skill_timeleft.ToString ("F1");
				skill_timeleft -= Time.deltaTime;
			}
			else SkillSec.text = "";
			//-------------------------------


			if (is_sustain_running) {    //續傷型技能啟動中
				skill_sustain.SetActive (true);
			} else {
				skill_sustain.SetActive (false);
			}
		}
		//--------------------------------------------

		//設定trigger條件
		if (trigger != -1 && skill_id == 4) {
			StartCoroutine( doContinuedDamage(skill_delay, trigger ) );
			trigger = -1;
		}


	}

	public void onButtonDown(){
		if (skill_id == 4 && skill_timeleft <= 0F && !UrgentEventHandler.is_animate && !UrgentEventHandler.is_end) {
			UrgentSkillHandler.on_hundred();
			UrgentEventHandler.is_animate = true;
			is_hold = true;

		}

	}

	public void onButtonUp(){
		if (skill_id == 4 && is_hold) {
			skill_timeleft = skill_Cd;
			UrgentSkillHandler.on_dadada();
			is_hold = false;
		}
	}

	public void onButtonClicked(){

		if (skill_timeleft <= 0F && !UrgentEventHandler.is_animate && !UrgentEventHandler.is_end && skill_id!=4) {
			skill_timeleft = skill_Cd;

			if(skill_id == 0){
			//	UrgentEventHandler.on_normal_punch();
				int x = Random.Range (0,2);
				play (audio[x]);
			}
			else if(skill_id == 1){
			//	UrgentEventHandler.on_heavy_punch();
				play (audio[2]);
			}
			else if(skill_id==2){
			//	UrgentEventHandler.on_dead_punch();
				play (audio[4]);
			}
			else if(skill_id==3){
			//	UrgentEventHandler.on_curse();
				play (audio [6]);
			}
			else{

				//battleManager.on_eye();
			}

			if(skill_type == 3 || skill_type == 1) //續傷型
				StartCoroutine(SustainFunction((int)skill_Sustain,skill_delay));
			else StartCoroutine(doDamage(skill_delay));	//直傷型
		}
	}

	public void setId(int id){

		Icons icons = Icons.GetComponent<Icons> ();
		icon = icons.skills;
		icon_cd = icons.skills_cd;



		skill_id = id;
		if (id < icon.Length) {
			my_icon = icon [skill_id];
			my_icon_cd = icon_cd [skill_id];

		} else {
			my_icon = icon [0];
			my_icon_cd = icon_cd [0];
		}

		skill_Cd = 		 Data_Skills.Skills_list[skill_id].cd;
		skill_Damage =	 Data_Skills.Skills_list[skill_id].damage;
		skill_Sustain =  Data_Skills.Skills_list[skill_id].sustain;
		skill_type =	 Data_Skills.Skills_list[skill_id].type;	 
		skill_delay =    Data_Skills.Skills_list[skill_id].delay;	
	}



	IEnumerator SustainFunction(int s,float delay){  //續傷型技能，每秒鐘扣一次傷害

		if (delay > 0f) {
			UrgentEventHandler.is_animate = true;
			yield return new WaitForSeconds (delay);
			UrgentEventHandler.is_animate = false;
		}

		//詛咒時有閃電特效
		if (skill_type == 3) {
			UrgentSkillHandler.Lightning = true;
			UrgentEventHandler.is_addition++;
		} else {
			play (audio[10]);
			UrgentSkillHandler.ray_trigger = true;
			UrgentEventHandler.OP_power = Data_Skills.Skills_list[skill_id].other;
		}
		is_sustain_running = true;

		//持續傷害開始--------------------
		for (int i=0; i<s; i++) {
			yield return new WaitForSeconds(1);

			if(skill_type == 3){
				play (audio[7]);
				UrgentSkillHandler.Thunder = true;
			}
			StartCoroutine( doDamage(0f));
		}
		//持續傷害結束--------------------

		if(skill_type==1)//結束爆擊機率
			UrgentEventHandler.OP_power = 0F;
		is_sustain_running = false;

		if(skill_type == 3)
			UrgentEventHandler.is_addition--;
	}

	IEnumerator doDamage(float delay){

		//等待延遲
		if (delay > 0f) {
			UrgentEventHandler.is_animate = true;
			yield return new WaitForSeconds (delay);
			UrgentEventHandler.is_animate = false;
		}

		//撥放音效
		if (skill_id == 1) {
			play (audio [3]);
		} else if (skill_id == 2) {
			play (audio[5]);
		}

		if (skill_Damage> 0 && !UrgentEventHandler.is_end) {
			//觸發熊貓被打到的動作
		/*	if(skill_id==0)
				battleManager.triggerHit (0);
			else battleManager.triggerHit (1);
*/

			//計算爆擊倍率
			int y = Random.Range (0f, 1f) >= UrgentEventHandler.OP_power ? 1 : 2;
			//計算技能效果倍率
			y *= (skill_type==2 && UrgentEventHandler.is_addition>0) ? 2: 1;


			new_damage_ani = Instantiate(damage_ani,UrgentEventHandler.panda_place,UrgentEventHandler.panda_rotation) as GameObject;
			//未爆擊，數字黃色，反之紅色
			if(y==1)new_damage_ani.GetComponent<SetDamage>().SetUrgent( (int)(skill_Damage * y * multi) ,false,UrgentEventHandler.panda_place);
			else new_damage_ani.GetComponent<SetDamage>().SetUrgent( (int)(skill_Damage * y * multi) ,true,UrgentEventHandler.panda_place);

			GameObject new_hit_ani = Instantiate(hit_ani,panda, Quaternion.identity) as GameObject;
			new_hit_ani.GetComponent<setHit>().SetUrgent (skill_id, UrgentEventHandler.panda_place + new Vector3(1,-1,0) );


			UrgentEventHandler.pandaHP -= (int)(skill_Damage * y * multi);
			UrgentEventHandler.delta_damage += (int)(skill_Damage * y * multi);
		}
		if (UrgentEventHandler.pandaHP < 0)
			UrgentEventHandler.pandaHP = 0;
		yield return null;
	}

	IEnumerator doContinuedDamage(float delay,int x){

		//等待延遲
		if (delay > 0f) {
			UrgentEventHandler.is_animate = true;
			yield return new WaitForSeconds (delay);

		}

		//撥放音效
		play (audio[9]);


		if (!UrgentEventHandler.is_end) {
			//觸發熊貓被打到的動作
		//	battleManager.triggerHit (1);

			yield return new WaitForSeconds(0.15f *  Data_Skills.Skills_list[skill_id].other - x);

			for(int i=0;i<x;i++){
				yield return new WaitForSeconds(0.15f);
				//計算爆擊倍率
				int y = Random.Range (0f, 1f) >= UrgentEventHandler.OP_power ? 1 : 2;

				new_damage_ani = Instantiate(damage_ani,UrgentEventHandler.panda_place, UrgentEventHandler.panda_rotation) as GameObject;
				//未爆擊，數字黃色，反之紅色
				if(y==1)new_damage_ani.GetComponent<SetDamage>().SetUrgent((int)(skill_Damage * y * multi) ,false,UrgentEventHandler.panda_place);
				else new_damage_ani.GetComponent<SetDamage>().SetUrgent( (int)(skill_Damage * y * multi) ,true,UrgentEventHandler.panda_place);

				GameObject new_hit_ani = Instantiate(hit_ani,UrgentEventHandler.panda_place, UrgentEventHandler.panda_rotation) as GameObject;
				new_hit_ani.GetComponent<setHit>().SetUrgent (skill_id ,UrgentEventHandler.panda_place );			
				UrgentEventHandler.pandaHP -= (int)(skill_Damage * y * multi);
				UrgentEventHandler.delta_damage +=(int)(skill_Damage * y * multi);

				if (UrgentEventHandler.pandaHP < 0)
					UrgentEventHandler.pandaHP = 0;
			}
		}

		yield return new WaitForSeconds(0.1f);

		UrgentEventHandler.is_animate = false;


		yield return null;
	}

	//撥放音效
	void play(AudioClip a){
		gameObject.GetComponent<MusicHandler>().playOneClip(a);	
	}
}
