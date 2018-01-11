using UnityEngine;
using System.Collections;
using MiniJSON;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour {
	//Function to use:
	//> user_get_items:    			input id and token; access the items user has 
	//> user_change_item_amount:   	input id, token, and item list; access the newer amount of the items
	
	public static void user_get_items(string user_id,string token, System.Action Done = null,System.Action Err = null,System.Action Err1 = null){
		StaticCoroutine.DoCoroutine(NetworkHandler.get_items(user_id, token,(result)=>{					
			IDictionary search = (IDictionary) Json.Deserialize(result);
			if(search == null){
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse(search["code"].ToString());
			
			if( state == Global.ERROR_SUCCESS){ 
				IDictionary items = (IDictionary)search["items"];
				setup_items(items);
				
				string debug = "";
				for(int i=0;i<UserStatementManager.item_list.Count;i++){
					debug += UserStatementManager.item_list[i] + ":" + UserStatementManager.item_num_list[i] + ", ";
				}
				Debug.Log ("items: "+ debug);
				StaticCoroutine.errLog = 0;
			}
			else if(state==Global.ERROR_INVALID_TOKEN){
				Debug.Log ("Invalid token!");
				StaticCoroutine.errLog = 1;
			}	
			else if(state == Global.ERROR_USER_NOT_FOUND){
				Debug.Log ("User not found");
				StaticCoroutine.errLog = 1;
			}
		}),Done,Err,Err1);
	}
	
	public static void user_change_item_amount(string user_id,string token,List<int> items,List<int> items_num, System.Action Done = null,System.Action Err = null,System.Action Err1 = null){

		//先將暫存資料置入list中
		Global.addPrefToList(items,items_num);
	//	Global.addItemPref (items, items_num);
		string slist = parse_list_to_string (items,items_num);
		StaticCoroutine.DoCoroutine(NetworkHandler.change_item_amount(user_id, token, slist,(result)=>{					
			//Debug.Log (result);
			IDictionary search = (IDictionary) Json.Deserialize(result);
			if(search == null){
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse(search["code"].ToString());
			StaticCoroutine.errLog = 0;
			if( state == Global.ERROR_SUCCESS){ 
				IDictionary allitems = (IDictionary)search["items"];
				setup_items(allitems);
				PlayerPrefs.DeleteKey ("items");
				PlayerPrefs.DeleteKey ("nums");
				Debug.Log ("Change success!");
			}
			else if(state==Global.ERROR_INVALID_TOKEN){
				StaticCoroutine.errLog = 1;
				Debug.Log ("Invalid token!");
			}	
			else if(state == Global.ERROR_USER_NOT_FOUND){
				StaticCoroutine.errLog = 1;
				Debug.Log ("User not found");
			}
		}),Done,Err,Err1);
	}
//-------------------------------------------------------------------------------------------------

	public static void setup_items(IDictionary items){
		UserStatementManager.item_list.Clear();
		UserStatementManager.item_num_list.Clear();

		foreach( string key in items.Keys )  {
			UserStatementManager.item_list.Add(int.Parse(key));
			UserStatementManager.item_num_list.Add (int.Parse(items[key].ToString()));
		}   

	}

	public static string parse_list_to_string (List<int> keys, List<int> value){
		string result = "{";
		for(int i=0;i<keys.Count;i++){
			result += "\"" + keys[i].ToString() + "\":" + value[i].ToString ();
			if(i!=keys.Count-1) result += ",";
		}
		result += "}";
		return result;
	}



	//check user have the item or not
	public static bool haveItemId(int id){	
		if (UserStatementManager.item_list.IndexOf (id) == -1)
			return false;
		if (UserStatementManager.item_num_list [UserStatementManager.item_list.IndexOf (id)] > 0)
			return true;
		return false;
	}

	public static int getRandomEquip(){

		//55~78
		List<int> dd = new List<int> ();//存放目前未擁有的道具
		for (int i = 55; i <= 78; i++) {
			if (UserStatementManager.item_list.IndexOf (i) == -1) {
				dd.Add (i);
				continue;
			} else if (UserStatementManager.item_num_list [UserStatementManager.item_list.IndexOf (i)] == 0) {
				dd.Add (i);
				continue;
			}
		}

		if (dd.Count == 0)
			return 0;
		int x = Random.Range (0, dd.Count);
		return dd [x];
	}
}
