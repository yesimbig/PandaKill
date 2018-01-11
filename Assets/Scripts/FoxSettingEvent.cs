using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class FoxSettingEvent : MonoBehaviour {


	static int color,eye,tail,weapon,wearing;
	public GameObject[] btn_part;
	public Sprite[] btn_part_sprite;
	public Sprite[] btn_part_sprite_selected;

	public GameObject[] selected_panel;


	public GameObject[] btn_eye;
	public Sprite[] btn_eye_sprite;
	public Sprite[] btn_eye_sprite_selected;

	public GameObject[] btn_tail;
	public Sprite[] btn_tail_sprite;
	public Sprite[] btn_tail_sprite_selected;

	public GameObject[] btn_clothes;
	Sprite[][] btn_clothes_sprite = new Sprite[4][];
	Sprite[][] btn_clothes_sprite_selected = new Sprite[4][];

	public Sprite[] btn_clothes1_sprite;
	public Sprite[] btn_clothes1_sprite_selected;

	public Sprite[] btn_clothes2_sprite;
	public Sprite[] btn_clothes2_sprite_selected;

	public Sprite[] btn_clothes3_sprite;
	public Sprite[] btn_clothes3_sprite_selected;

	public Sprite[] btn_clothes4_sprite;
	public Sprite[] btn_clothes4_sprite_selected;

	public GameObject[] btn_color;
	public GameObject[] selected_color;
	public Sprite[] color_sprite;
	public Sprite color_sprite_disabled;

	public GameObject color_disabled_text;

	public GameObject messageBox;
	public GameObject networkLoading;

	public FoxSkinSetting fss;

	NetworkLoadingSet newNetworkLoading;

	// Use this for initialization
	void Start () {
		color = 1;
		eye = 1;
		tail = 1;
		wearing = 1;
		weapon = 1;
		fss.set (eye, tail, color, wearing);
		selected_panel [0].SetActive (true);
		selected_panel [1].SetActive (false);
		selected_panel [2].SetActive (false);

		btn_part [0].GetComponent<Image> ().sprite = btn_part_sprite_selected[0];
		color_disabled_text.SetActive (true);

		btn_clothes_sprite [0] = btn_clothes1_sprite;
		btn_clothes_sprite_selected [0] = btn_clothes1_sprite_selected;
		btn_clothes_sprite [1] = btn_clothes2_sprite;
		btn_clothes_sprite_selected [1] = btn_clothes2_sprite_selected;
		btn_clothes_sprite [2] = btn_clothes3_sprite;
		btn_clothes_sprite_selected [2] = btn_clothes3_sprite_selected;
		btn_clothes_sprite [3] = btn_clothes4_sprite;
		btn_clothes_sprite_selected [3] = btn_clothes4_sprite_selected;

		setSelected (btn_eye,btn_eye_sprite,btn_eye_sprite_selected,0);
		setSelected (btn_tail, btn_tail_sprite, btn_tail_sprite_selected, 0);
		setSelected (btn_clothes, btn_clothes_sprite[color-1], btn_clothes_sprite_selected[color-1], 0);
		setColorButtons (false);
	}

	//----------------------------------------button event----------------------------------------------------------
	public void onChooseEyeClick(){
		setSelected (btn_part,btn_part_sprite,btn_part_sprite_selected,0);
		selected_panel [0].SetActive (true);
		selected_panel [1].SetActive (false);
		selected_panel [2].SetActive (false);
		color_disabled_text.SetActive (true);
		setColorButtons (false);

	}

	public void onChooseTailClick(){
		setSelected (btn_part,btn_part_sprite,btn_part_sprite_selected,1);
		selected_panel [1].SetActive (true);
		selected_panel [0].SetActive (false);
		selected_panel [2].SetActive (false);
		color_disabled_text.SetActive (true);
		setColorButtons (false);
	}

	public void onChooseClothesClick(){
		setSelected (btn_part,btn_part_sprite,btn_part_sprite_selected,2);
		selected_panel [2].SetActive (true);
		selected_panel [0].SetActive (false);
		selected_panel [1].SetActive (false);
		color_disabled_text.SetActive (false);
		setColorButtons (true);
	}

	public void onEye1Click(){
		setSelected (btn_eye,btn_eye_sprite,btn_eye_sprite_selected,0);
		eye = 1;
		fss.set (eye, tail, color, wearing);
	}

	public void onEye2Click(){
		setSelected (btn_eye,btn_eye_sprite,btn_eye_sprite_selected,1);
		eye = 2;		
		fss.set (eye, tail, color, wearing);
	}
	public void onEye3Click(){
		setSelected (btn_eye,btn_eye_sprite,btn_eye_sprite_selected,2);
		eye = 3;		fss.set (eye, tail, color, wearing);
	}
	public void onEye4Click(){
		setSelected (btn_eye,btn_eye_sprite,btn_eye_sprite_selected,3);
		eye = 4;		fss.set (eye, tail, color, wearing);
	}

	public void onTail1Click(){
		setSelected (btn_tail,btn_tail_sprite,btn_tail_sprite_selected,0);
		tail = 1;		fss.set (eye, tail, color, wearing);
	}
	
	public void onTail2Click(){
		setSelected (btn_tail,btn_tail_sprite,btn_tail_sprite_selected,1);
		tail = 2;		fss.set (eye, tail, color, wearing);
	}
	public void onTail3Click(){
		setSelected (btn_tail,btn_tail_sprite,btn_tail_sprite_selected,2);
		tail = 3;		fss.set (eye, tail, color, wearing);
	}
	public void onTail4Click(){
		setSelected (btn_tail,btn_tail_sprite,btn_tail_sprite_selected,3);
		tail = 4;		fss.set (eye, tail, color, wearing);
	}

	public void onClothes1Click(){
		setSelected (btn_clothes,btn_clothes_sprite[color-1],btn_clothes_sprite_selected[color-1],0);
		wearing = 1;		fss.set (eye, tail, color, wearing);
	}

	public void onClothes2Click(){
		setSelected (btn_clothes,btn_clothes_sprite[color-1],btn_clothes_sprite_selected[color-1],1);
		wearing = 2;		fss.set (eye, tail, color, wearing);
	}

	public void onClothes3Click(){
		setSelected (btn_clothes,btn_clothes_sprite[color-1],btn_clothes_sprite_selected[color-1],2);
		wearing = 3;		fss.set (eye, tail, color, wearing);
	}

	public void onClothes4Click(){
		setSelected (btn_clothes,btn_clothes_sprite[color-1],btn_clothes_sprite_selected[color-1],3);
		wearing = 4;		fss.set (eye, tail, color, wearing);
	}

	public void onRedClick(){
		color = 1;		fss.set (eye, tail, color, wearing);
		showBtn (selected_color,0);
		setSelected (btn_clothes,btn_clothes_sprite[color-1],btn_clothes_sprite_selected[color-1],wearing-1);
	}

	public void onYellowClick(){
		color = 2;		fss.set (eye, tail, color, wearing);
		showBtn (selected_color,1);
		setSelected (btn_clothes,btn_clothes_sprite[color-1],btn_clothes_sprite_selected[color-1],wearing-1);
	}

	public void onGreenClick(){
		color = 3;		fss.set (eye, tail, color, wearing);
		showBtn (selected_color,2);
		setSelected (btn_clothes,btn_clothes_sprite[color-1],btn_clothes_sprite_selected[color-1],wearing-1);
	}

	public void onBlueClick(){
		color = 4;		fss.set (eye, tail, color, wearing);
		showBtn (selected_color,3);
		setSelected (btn_clothes,btn_clothes_sprite[color-1],btn_clothes_sprite_selected[color-1],wearing-1);
	}

	void setSelected(GameObject[] b,Sprite[] ori,Sprite[] sel,int index){
		for(int i=0;i<b.Length;i++){
			if(i==index){
				b[i].GetComponent<Image> ().sprite = sel[i];
			}
			else b[i].GetComponent<Image> ().sprite = ori[i];
		}
	}

	void showBtn (GameObject[] g,int c){
		for (int i = 0; i < 4; i++) {
			if(i==c)
				g[i].SetActive (true);
			else g[i].SetActive (false);
		}
	}

	void setColorButtons(bool b){
		for (int i=0; i<btn_color.Length; i++) {
			btn_color [i].GetComponent<Button>().interactable = b; 
			if(b){
				selected_color[i].GetComponent<Image>().color = Color.white;
				btn_color[i].GetComponent<Image>().sprite = color_sprite[i];
			}
			else {
				selected_color[i].GetComponent<Image>().color = Color.black;
				btn_color[i].GetComponent<Image>().sprite = color_sprite_disabled;
			}
		}
		
	}

	public void onCheckButtonClicked(){
		if (color != 0 && weapon != 0) {
			Debug.Log (Global.NICK_NAME);
			UserStatementManager.set_user_info(UserStatementManager.id,UserStatementManager.token,color,eye,tail,wearing,weapon,onSetSuccess,onConnErr,InvalidToken);
			NetworkLoading("狐狸資訊設定中...");
		}
	}

	public void onSetSuccess(){
		newNetworkLoading.Clear ();
		NetworkLoading("初始化設定中...");
		int[] skill = {0,1,2};
		List<int> item = new List<int>{54+eye,58+tail,62+(color-1)*4+wearing};
		List<int> item_num = new List<int>{1,1,1};
		UserStatementManager.give_reward (UserStatementManager.id, UserStatementManager.token, 0, skill, item, item_num, onRewardSuccess, onConnErr, InvalidToken);

	}

	public void onRewardSuccess(){
		newNetworkLoading.Clear ();
		NetworkLoading("初始化設定中...");
		int[] skill = {0,1,2};
		SkillManager.set_equip_skill (UserStatementManager.id, UserStatementManager.token, skill, onToMainScene, onConnErr, InvalidToken);


	}

	public void onToMainScene(){
		newNetworkLoading.Clear ();
		PlayerPrefs.SetInt ("RegistFail", 0);
		StartCoroutine (LoadingScene("_tutorial"));
		//Application.LoadLevel("_main");	
	}

	public void onConnErr(){
		ErrorMessageBox("連線失敗!");
		newNetworkLoading.Clear ();
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
		while (!asyncf.isDone) {
			newNetworkLoading.setMessage("載入中...請稍候..."+((int)(asyncf.progress*100)).ToString ()+"%");
			yield return null;
		}
		newNetworkLoading.Clear ();
	}
}
