using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class allItemsSetting : MonoBehaviour {

	public GameObject itemPrefab;
	public GameObject[] Content;
	public GameObject[] tabs_select;
	public GameObject[] Scrolls;
	float iniWidth ,iniHeight;
	List<GameObject> items = new List<GameObject>();


	public static bool trigger;
	// Use this for initialization
	void Start () {
		trigger = false;
		iniWidth = Content[0].GetComponent<RectTransform> ().rect.width;
		iniHeight = Content[0].GetComponent<RectTransform> ().rect.height;
		Scrolls [1].SetActive (true);
		Scrolls [0].SetActive (false);
		Scrolls [2].SetActive (false);
		set ();
	}

	public void set(){

		int id,id_num;
		int[] num = new int[3];
		for (int i = 0; i<UserStatementManager.item_list.Count; i++) {
			
			id = UserStatementManager.item_list [i];
			id_num = UserStatementManager.item_num_list [i];
			
			if (id_num == 0)
				continue;

			for (int j = 0; j<id_num; j++) {

				int type = 0;
				if (Data_Items.Items_list [id].type == 2 || Data_Items.Items_list [id].type == 3)
					type = 1;
				else if (Data_Items.Items_list [id].type == 1)
					type = 2;
			
				GameObject item = Instantiate (itemPrefab) as GameObject;
				item.transform.SetParent (Content [type].transform);	

				ItemSetting itemsetting = (ItemSetting)(item.GetComponent<ItemSetting> ());
			
				items.Add (item);

				if (type == 1) {
					//設定裝備
					if (id >= 55 && id <= 58 && id - 54 == UserStatementManager.eye)
						itemsetting.setItem (id, id_num, true, false);
					else if (id >= 59 && id <= 62 && id - 58 == UserStatementManager.tail)
						itemsetting.setItem (id, id_num, true, false);
					else if (id >= 63 && id <= 78 && id - 62 == (UserStatementManager.color - 1) * 4 + UserStatementManager.wearing)
						itemsetting.setItem (id, id_num, true, false);
					else
						itemsetting.setItem (id, id_num, true); 
				} else {
					itemsetting.setItem (id, id_num, false); 

				}
				if (num [type] > 2) {
					Content [type].GetComponent<RectTransform> ().sizeDelta = new Vector2 (iniWidth, iniHeight * ((((float)num [type] + 1f) / 3f)));
				}
				num [type]++;
			}
		}
	}

	void reset(){
		for (int i=0; i<items.Count; i++)
			DestroyObject (items [i]);
		items.Clear ();
	}

	void Update(){	

		if (trigger) {
			//DestroyObject(gameObject);
			reset ();
			set ();
			trigger = false;
		}

	}

	public void onExitButtonClick(){
		DestroyObject (gameObject);	
	}

	public void onAwardTabClick(){
		tabs_select [2].SetActive (true);
		tabs_select [1].SetActive (false);
		tabs_select [0].SetActive (false);
		Scrolls [2].SetActive (true);
		Scrolls [1].SetActive (false);
		Scrolls [0].SetActive (false);
	}

	public void onEqTabClick(){
		tabs_select [1].SetActive (true);
		tabs_select [0].SetActive (false);
		tabs_select [2].SetActive (false);
		Scrolls [1].SetActive (true);
		Scrolls [2].SetActive (false);
		Scrolls [0].SetActive (false);
	}

	public void onNormalTabClick(){
		tabs_select [0].SetActive (true);
		tabs_select [1].SetActive (false);
		tabs_select [2].SetActive (false);
		Scrolls [0].SetActive (true);
		Scrolls [1].SetActive (false);
		Scrolls [2].SetActive (false);
	}

}
