using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ARItemBoxSetting : MonoBehaviour {

	public GameObject itemPrefab;
	public GameObject Content;
	public GameObject LeftArrow, RightArrow;

	public static int nowId = -1;

	List<ARItemSetting> aritemsetting = new List<ARItemSetting>();
	int allItem = 0;
	float iniWidth = 0f;
	float iniHeight = 0f;
	float iniLeft = 0f;
	float iniRight = 0f;


	//目前指針
	int nowpoint = 0;

	// Use this for initialization
	void Start () {
		nowpoint = 0;
		iniWidth = Content.GetComponent<RectTransform> ().rect.width;
		iniHeight = Content.GetComponent<RectTransform> ().rect.height;

		for (int i = 0; i<UserStatementManager.item_list.Count; i++) {
			
			if(UserStatementManager.item_num_list[i]==0 || Data_Items.Items_list[UserStatementManager.item_list[i]].type!=0)continue;
			
			GameObject item = Instantiate (itemPrefab) as GameObject;			
			item.transform.SetParent (Content.transform);				

			ItemSetting itemsetting = (ItemSetting)(item.GetComponent<ItemSetting> ());
			itemsetting.setItem (UserStatementManager.item_list[i], UserStatementManager.item_num_list[i]); 

			aritemsetting.Add((ARItemSetting)(item.GetComponent<ARItemSetting> ()));
			aritemsetting[allItem].setARid(UserStatementManager.item_list[i]);
			aritemsetting[allItem].setSelection(false);

			//超過5個item要做延伸
			if(i>5){
				Content.GetComponent<RectTransform> ().sizeDelta = new Vector2(iniWidth*(allItem+1)/5, iniHeight);
			}
			allItem++;
		}

		if (allItem <= 5) {
			LeftArrow.SetActive(false);
			RightArrow.SetActive(false);
		}

		nowId = -1;
	}
	
	void Update(){	
		for (int i = 0; i<allItem; i++) {
			if(nowId == aritemsetting[i].myid) aritemsetting[i].setSelection(true);
			else aritemsetting[i].setSelection(false);
		}
	}

	public void onleftButtonClick(){
		if (nowpoint < allItem - 5) {
			nowpoint++;
			Content.GetComponent<RectTransform> ().localPosition -= new Vector3(iniWidth/5,0f,0f);
		}
	}

	public void onRightButtonClick(){
		if (nowpoint > 0) {
			nowpoint--;
			Content.GetComponent<RectTransform> ().localPosition += new Vector3(iniWidth/5,0f,0f);
		}
	}

}
