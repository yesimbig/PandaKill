using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ItemSetting : MonoBehaviour {

	public GameObject Icons;
	public Image icon;

	Sprite[] items,skills;
	public Text item_name,item_num,icon_text;
	public Text description,btn_description;
	public GameObject b;
	public GameObject networkLoading,messageBox;
	public GameObject icon_name;
	NetworkLoadingSet newNetworkLoading;
	int myid;
	Icons icons;

	public void setItem(int id, int num,bool btn = false,bool used = true){

		icons = Icons.GetComponent<Icons> ();
		items = icons.items;
		skills = icons.skills;

		if(id < items.Length)
			icon.sprite = items[id];
		this.transform.localPosition = new Vector2(0,0);
		this.transform.localScale = Vector3.one;            

		RectTransform rt = this.GetComponent<RectTransform> ();
		rt.anchoredPosition = new Vector2(0,0);
		myid = id;

		//道具選擇介面會用到
		if (description != null) {
			if(btn){
				description.text = "";
				btn_description.text = Data_Items.Items_list[id].description;
				b.SetActive(true);
				b.GetComponent<Button>().interactable = used;
			}
			else{
				btn_description.text = "";
				description.text = Data_Items.Items_list[id].description;
				b.SetActive(false);
			}
		}

		//在AR選擇畫面中會用到
		if (item_name != null) {
			item_name.text = Data_Items.Items_list[id].name;
		}
	}

	public void setReward(int type,int id,int num){

		icons = Icons.GetComponent<Icons> ();
		items = icons.items;
		skills = icons.skills;

		this.transform.localPosition = new Vector2(0,0);
		this.transform.localScale = Vector3.one;            
		
		RectTransform rt = this.GetComponent<RectTransform> ();
		rt.anchoredPosition = new Vector2(0,0);

		//一般道具
		if (type == 0) {
			icon_text.text = Data_Items.Items_list [id].name;
			if (id < items.Length)
				icon.sprite = items [id];
			item_num.text = num.ToString ();
		} else if (type == 1) {//技能
			icon_text.text = Data_Skills.Skills_list [id].name;
			if (id < skills.Length)
				icon.sprite = skills [id];
			item_num.text = num.ToString ();
		} else {
			icon_text.text = "積分";
			item_num.text = num.ToString ();
		}
	}

	public void setIcon(int type,int id){
		icons = Icons.GetComponent<Icons> ();
		items = icons.items;
		skills = icons.skills;

		this.transform.localPosition = new Vector2(0,0);
		this.transform.localScale = Vector3.one;            
		
		RectTransform rt = this.GetComponent<RectTransform> ();
		rt.anchoredPosition = new Vector2(0,0);
		//Debug.Log (id);

		//一般道具
		if (type == 0) {
			icon_text.text = Data_Items.Items_list [id].name;
			if (id < items.Length)
				icon.sprite = items [id];
		} else if (type == 1) {//技能
			icon_text.text = Data_Skills.Skills_list [id].name;
			if (id < skills.Length)
				icon.sprite = skills [id];
		}
		
	}

	public void onUseClick(){
		Debug.Log (myid);


		//體力回復藥水
		if (myid == 1) {

			if(UserStatementManager.repletion == 4){
				ErrorMessageBox ("體力已經是滿的囉!");
			}
			else{
				NetworkLoading ("設定中，請稍後...");
				b.GetComponent<Button>().interactable = false;
				UserStatementManager.change_user_repletion (UserStatementManager.id, UserStatementManager.token,1, onUsingId1, onConnErr, InvalidToken);
			}
		} else if (myid == 0) {
			if (PlayerPrefs.GetString ("extra_score") == null || PlayerPrefs.GetString ("extra_score").Length > 0) {
				ErrorMessageBox ("目前已經有使用囉!");
			} else {
				NetworkLoading ("設定中，請稍後...");
				b.GetComponent<Button>().interactable = false;
				List<int> item = new List<int>{ 0 }, num = new List<int>{ -1 };
				ItemManager.user_change_item_amount (UserStatementManager.id, UserStatementManager.token, item, num, onDeleteItem0, onConnErr, InvalidToken);
			}
		} else {

			NetworkLoading ("設定中，請稍後...");

			//眼睛
			if (myid >= 55 && myid <= 58) {
				UserStatementManager.set_user_info (UserStatementManager.id, UserStatementManager.token, UserStatementManager.color, myid - 54,
					UserStatementManager.tail, UserStatementManager.wearing, UserStatementManager.weapon, onSetEquip, onConnErr, InvalidToken);
			}
			//尾巴
			else if (myid >= 59 && myid <= 62) {
				UserStatementManager.set_user_info (UserStatementManager.id, UserStatementManager.token, UserStatementManager.color, UserStatementManager.eye,
					myid - 58, UserStatementManager.wearing, UserStatementManager.weapon, onSetEquip, onConnErr, InvalidToken);
			}
			//衣服
			else {
				UserStatementManager.set_user_info (UserStatementManager.id, UserStatementManager.token, (myid-63)/4 + 1, UserStatementManager.eye,
					UserStatementManager.tail, (myid-63) % 4+1, UserStatementManager.weapon, onSetEquip, onConnErr, InvalidToken);
			}
		
		}


	}

	void onUsingId1(){
		newNetworkLoading.Clear ();
		NetworkLoading("設定中，請稍後...");
		List<int> item = new List<int>{1}, num = new List<int>{-1};
		ItemManager.user_change_item_amount( UserStatementManager.id, UserStatementManager.token, item,num,onDeleteItem,onConnErr,InvalidToken);
	}

	void onDeleteItem0(){
		newNetworkLoading.Clear ();
		PlayerPrefs.SetString ("extra_score", "set");
		ErrorMessageBox ("已使用積分加成藥水，下次戰鬥積分將會有1.2倍加乘!!",null,true);

		allItemsSetting.trigger = true;
	}

	void onDeleteItem(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("已經將飽食度增加!!",null,true);
		allItemsSetting.trigger = true;
	}

	void onSetEquip(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("已成功換裝!",null,true);
		allItemsSetting.trigger = true;
		infoSetting.foxTrigger = true;
	}

	void onConnErr(){
		newNetworkLoading.Clear ();
		ErrorMessageBox ("連線錯誤!!");
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
	void ErrorMessageBox(string message,System.Action act = null,bool info = false){
		GameObject err_msg_box = Instantiate (messageBox) as GameObject;
		MessageBoxSetting mbs = (MessageBoxSetting)(err_msg_box.GetComponent<MessageBoxSetting> ());
		mbs.message = message; 
		mbs.act = act;
		mbs.is_info = info;
	}
	
	//Handle network loading
	void NetworkLoading(string message = null){
		newNetworkLoading = (Instantiate (networkLoading) as GameObject).GetComponent<NetworkLoadingSet>();
		
		if (message != null)
			newNetworkLoading.message = message;
	}

}
