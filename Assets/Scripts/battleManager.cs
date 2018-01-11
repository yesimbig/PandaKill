using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class battleManager : MonoBehaviour {

	//戰鬥相關全域變數-------------------------------
	public static int pandaHP;
	public static float OP_power = 0f;//爆擊機率
	public static int is_addition = 0; //是否是附加狀態
	public static bool is_animate = false; //是否在動畫僵直狀態
	//-------------------------------------------

	public int max_pandaHP;
	public Text timer;
	public GameObject ButtleSuccess;
	public GameObject HPbar;
	public GameObject Panda,Fight_fox ,big_fire1,big_fire2,big_fire3,attack_bar,impact;

	public GameObject light,thunder,ray;

	public GameObject networkLoading;
	NetworkLoadingSet newNetworkLoading;

	float time_run = 0;
	float time_left = 90f;
	bool is_battle_complete = false;
	float maxHP_length;
	static Animator PandaAni,Fight_fox_Ani;

	//triggers
	static bool hundred = false,dadada = false;

	//particle triggers
	public static bool Lightning = false,Thunder = false,ray_trigger = false;



	// Use this for initialization
	void Start () {
		pandaHP = max_pandaHP;
		time_run = 0;
		maxHP_length = HPbar.transform.localScale.x;
		is_battle_complete = false;
		PandaAni = Panda.GetComponent<Animator> ();
		Fight_fox_Ani = Fight_fox.GetComponent<Animator>();

		OP_power = 0f;
		is_addition = 0;
		is_animate = false;

		hundred = false;
		dadada = false;
		Lightning = false;
		Thunder = false;
		ray_trigger = false;
		light.GetComponent<ParticleSystem>().Stop ();
		thunder.GetComponent<ParticleSystem>().Stop ();
		ray.GetComponent<ParticleSystem>().Stop ();

	}
	
	// Update is called once per frame
	void Update () {



		PandaAni.SetInteger ("PandaHP",pandaHP);

		HPbar.transform.localScale = new Vector3(maxHP_length * (float)pandaHP / (float)max_pandaHP,HPbar.transform.localScale.y);
		//戰鬥成功
		if (pandaHP == 0 && !is_battle_complete) {
			is_battle_complete = true;
			GameObject success_msg_box = Instantiate (ButtleSuccess) as GameObject;
			BattleSuccessBoxSetting mbs = (BattleSuccessBoxSetting)(success_msg_box.GetComponent<BattleSuccessBoxSetting> ());

			if(time_run<10f) time_run = 10f;

			int reward = 600 + (int)(900f*((90f-time_run)/80f));
			bool plus = false;
			//積分加成
			if (PlayerPrefs.GetString ("extra_score") != null && PlayerPrefs.GetString ("extra_score").Length>0) {
				reward = (int)((float)reward * 1.2f);
				PlayerPrefs.SetString ("extra_score","");
				plus = true;
			}

			//隨機拿服裝
			int x = Random.Range(0,5);
			List<int> items = new List<int>{};
			List<int> nums = new List<int>{}; 
			if (x == 0) {
				items.Add (ItemManager.getRandomEquip ());
				nums.Add (1);
			}


			mbs.setall (timer.text,reward,items,nums,plus);
		} 
		//戰鬥失敗
		else if (time_left == 0 && !is_battle_complete) {
			is_battle_complete = true;
			GameObject success_msg_box = Instantiate (ButtleSuccess) as GameObject;
			BattleSuccessBoxSetting mbs = (BattleSuccessBoxSetting)(success_msg_box.GetComponent<BattleSuccessBoxSetting> ());

			int reward = 600;
			bool plus = false;
			//積分加成
			if (PlayerPrefs.GetString ("extra_score") != null && PlayerPrefs.GetString ("extra_score").Length>0) {
				reward = (int)((float)reward * 1.2f);
				PlayerPrefs.SetString ("extra_score","");
				plus = true;
			}

			mbs.setall (timer.text,reward,new List<int>(),new List<int>(),plus,true); 
		}
		else if(!is_battle_complete){
			time_run += Time.deltaTime;
			time_left = 90f - time_run;

			if(time_left < 0) time_left = 0;
			timer.text = string.Format ("{0:00}:{1:00}", (int)time_left / 60, (int)(time_left) % 60);
		}

		//設定火焰特效
		if (OP_power >= 0.5f) {
			big_fire3.SetActive (true);
			impact.SetActive(true);
		} else if (OP_power > 0.3f) {
			big_fire2.SetActive (true);
			impact.SetActive(true);
		} else if (OP_power > 0) {
			big_fire1.SetActive (true);
			impact.SetActive(true);
		} else {
			big_fire1.SetActive (false);
			big_fire2.SetActive (false);
			big_fire3.SetActive (false);
			impact.SetActive(false);
		}
		//設定技能4的trigger
		if (hundred) {
			hundred = false;
			attack_bar.SetActive (true);
		}
		if (dadada) {
			dadada = false;
			BattleSkillItem.trigger = attack_bar.GetComponent<AttackBarSet> ().getPoint ((int)Data_Skills.Skills_list [4].other);
		}

		//設定particle
		if (Lightning && !light.GetComponent<ParticleSystem>().isPlaying) {
			light.SetActive (true);
			light.GetComponent<ParticleSystem>().Play();
			Lightning = false;
			StartCoroutine(delayStopParticle(light.GetComponent<ParticleSystem>() ,2f));
		}
		if (Thunder ) {			
			thunder.SetActive (true);
			if(thunder.GetComponent<ParticleSystem>().isPlaying)thunder.GetComponent<ParticleSystem>().Stop();
			thunder.GetComponent<ParticleSystem>().Play();
			Thunder = false;
			StartCoroutine(delayStopParticle(thunder.GetComponent<ParticleSystem>() ,0.8f));
		}
		if (ray_trigger) {
			ray.SetActive (true);
			ray.GetComponent<ParticleSystem>().Play();
			ray_trigger = false;
			StartCoroutine(delayStopParticle(ray.GetComponent<ParticleSystem>() ,0.8f));
		}


	}

	public static void triggerHit(int h){
		if(h==0)
			PandaAni.SetTrigger ("PandaHit");
		else PandaAni.SetTrigger ("PandaBigHit");
	}

	public static void on_normal_punch(int x){
		Fight_fox_Ani.SetTrigger ("punch");
		Fight_fox_Ani.SetInteger ("punchtype",x);
	}

	public static void on_heavy_punch(){
		Fight_fox_Ani.SetTrigger ("heavy_punch");
	}

	public static void on_dead_punch(){
		Fight_fox_Ani.SetTrigger ("dead_punch");
	}
	
	public static void on_curse(){

		Fight_fox_Ani.SetTrigger ("curse");
	}

	public static void on_eye(){
		Fight_fox_Ani.SetTrigger ("eye");
	}

	public static void on_hundred(){
		Fight_fox_Ani.SetTrigger ("hundred");
		hundred = true;
	}

	public static void on_dadada(){
		dadada = true;
		Fight_fox_Ani.SetTrigger ("dadada");
	}

	public void onExitClick(){
		StopCoroutine ("delayStopParticle");
		hundred = false;
		dadada = false;
		Lightning = false;
		Thunder = false;
		ray_trigger = false;

		StartCoroutine (LoadingScene ("_main"));
		//Application.LoadLevelAsync ("_main");	
	}

	IEnumerator delayStopParticle(ParticleSystem ps,float time){
		yield return new WaitForSeconds (time);
		ps.Stop ();	
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
