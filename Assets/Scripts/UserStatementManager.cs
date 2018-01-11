using UnityEngine;
using System.Collections;
using MiniJSON;
using System.Collections.Generic;
using System;

public class UserStatementManager : MonoBehaviour {

	public static int login_state = 0;
	public static string id = "";
	public static string token = "";
	public static int color;
	public static int eye;
	public static int tail;
	public static int weapon;
	public static int wearing;
	public static string nickname;
	public static string account;
	public static int score;
	public static int repletion;
	public static int[] equip_skill_id_list;
	public static int[] skill_id_list;
	public static List<int> item_list = new List<int>();
	public static List<int> item_num_list = new List<int>();



	//Function to use:
	//> user_login:    			input account and password; access id and token 
	//> user_logout:   			input id and token; destroy id and token
	//> get_user_info: 			input id and token; access eye, tail, weapon, score, repletion, equip kill ,and skill list
	//> set_user_info: 		   	input id, token, eye, tail, and weapon; access eye, tail, and weapon
	//> change_user_repletion: 	input id, token, value; access repletion to add the value
	//> change_user_score: 		input id, token, value; access score to add the value
	//> registration_request:	just get a token to check the request
	//> registration_data:		input newaccount,newpassword,token; get a new account

	public static void user_login(string account, string psw,System.Action Done = null,System.Action ConnErr = null,System.Action Err1 = null,System.Action Err2 = null){
		StaticCoroutine.DoCoroutine(NetworkHandler.users_login(account,psw,(result)=>{					

			IDictionary search = (IDictionary) Json.Deserialize(result);
			if(search == null){
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse(search["code"].ToString());
		
			if( state == Global.ERROR_SUCCESS){ 
				IDictionary user = (IDictionary)search["user"];
				id = user["id"].ToString();
				token = user["token"].ToString();
				PlayerPrefs.SetString("id", id);
				PlayerPrefs.SetString("token", token);
				StaticCoroutine.errLog = 0;
				Debug.Log ("Login success!!");
			}
			else if(state == Global.ERROR_USER_NOT_FOUND){
				StaticCoroutine.errLog = 1;
				Debug.Log ("User not found");
			}
			else if(state == Global.ERROR_PASSWORD_MISMATCH){
				StaticCoroutine.errLog = 2;
				Debug.Log ("Password error");
			}
			else{
				StaticCoroutine.errLog = 9;
			}
		}),Done,ConnErr,Err1,Err2);
	}

	public static void user_logout(string user_id,string token,System.Action Done = null,System.Action ConnErr = null){
		StaticCoroutine.DoCoroutine(NetworkHandler.user_logout (user_id, token, (result) => {					
			IDictionary search = (IDictionary)Json.Deserialize (result);
			if (search == null) {
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse (search ["code"].ToString ());

			if( state == Global.ERROR_SUCCESS){ 
				id = "";
				token = "";
				PlayerPrefs.DeleteAll();

				StaticCoroutine.errLog = 0;
				Debug.Log ("Logout success!!");
			}
			else if(state == Global.ERROR_USER_NOT_FOUND){
				StaticCoroutine.errLog = 1;
				Debug.Log ("User not found");
			}
			else if(state==Global.ERROR_INVALID_TOKEN){
				StaticCoroutine.errLog = 1;
				Debug.Log ("Invalid token!");
			}	
		}),Done,ConnErr);
	}

	public static void get_user_info(string user_id,string user_token,System.Action Done = null,System.Action ConnErr = null,System.Action Err = null){
		StaticCoroutine.DoCoroutine(NetworkHandler.get_info(user_id, user_token,(result)=>{					
			IDictionary search = (IDictionary) Json.Deserialize(result);
			if(search == null){
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse(search["code"].ToString());
				
			if( state == Global.ERROR_SUCCESS){ 
				id = user_id;
				token = user_token;
				IDictionary user = (IDictionary)search["user"];
				color = int.Parse(user["color"].ToString());
				eye = int.Parse(user["eye"].ToString());
				tail = int.Parse(user["tail"].ToString());
				weapon = int.Parse(user["weapon"].ToString());
				wearing = int.Parse(user["wearing"].ToString());
				score = int.Parse(user["score"].ToString());
				repletion = int.Parse(user["repletion"].ToString());
				nickname = user["nickname"].ToString();
				string esil = user["equip_skill_id_list"].ToString();
				equip_skill_id_list = parseListToInt(esil);
				string sil = user["skill_id_list"].ToString();
				skill_id_list = parseListToInt(sil);
				StaticCoroutine.errLog = 0;
				Debug.Log ("id = " + id);
				Debug.Log ("token = " + token);
				Debug.Log ("eye = " + eye);
				Debug.Log ("tail = " + tail);
				Debug.Log ("weapon = " + weapon);
				Debug.Log ("score = " + score);
				Debug.Log ("repletion = " + repletion);
			}
			else if(state==Global.ERROR_INVALID_TOKEN){
				StaticCoroutine.errLog = 1;	Debug.Log ("Invalid token!");
			}	
			else if(state == Global.ERROR_USER_NOT_FOUND){
				StaticCoroutine.errLog = 1;
				Debug.Log ("User not found");
			}
		}),Done,ConnErr,Err);
	}

	public static void set_user_info(string user_id,string token,int n_color,int n_eye,int n_tail,int n_wearing,int n_weapon, System.Action Done = null,System.Action ConnErr = null,System.Action Err1 = null){

		StaticCoroutine.DoCoroutine(NetworkHandler.set_info(user_id, token,n_color,n_eye, n_tail,n_wearing, n_weapon,(result)=>{					
			IDictionary search = (IDictionary) Json.Deserialize(result);
			if(search == null){
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse(search["code"].ToString());
			
			if( state == Global.ERROR_SUCCESS){ 
				StaticCoroutine.errLog = 0;
				IDictionary user = (IDictionary)search["user"];
				color = int.Parse(user["color"].ToString());
				eye = int.Parse(user["eye"].ToString());
				tail = int.Parse(user["tail"].ToString());
				weapon = int.Parse(user["weapon"].ToString());
				wearing = int.Parse(user["wearing"].ToString());
				Debug.Log ("Set info success!");
			}
			else if(state==Global.ERROR_INVALID_TOKEN){
				StaticCoroutine.errLog = 1;
				Debug.Log ("Invalid token!");
			}	
			else if(state == Global.ERROR_USER_NOT_FOUND){
				StaticCoroutine.errLog = 1;
				Debug.Log ("User not found");
			}
		}),Done,ConnErr,Err1);
	}

	public static void change_user_repletion(string user_id,string token,int value,System.Action Done = null,System.Action ConnErr = null,System.Action Err1 = null){
		StaticCoroutine.DoCoroutine(NetworkHandler.change_repletion(user_id, token,value,(result)=>{					
			IDictionary search = (IDictionary) Json.Deserialize(result);
			if(search == null){
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse(search["code"].ToString());
			
			if( state == Global.ERROR_SUCCESS){ 
				StaticCoroutine.errLog = 0;
				repletion = int.Parse(search["new_value"].ToString());
				Debug.Log ("Set repletion success!");
			}
			else if(state==Global.ERROR_INVALID_TOKEN){
				StaticCoroutine.errLog = 1;
				Debug.Log ("Invalid token!");
			}	
			else if(state==Global.ERROR_NOT_SATISFY){
				IList msgs = (IList)(search["msgs"]);
				StaticCoroutine.errLog = 9;
				Debug.Log ("Invalid repletion! " + msgs[0]);
			}	
		}),Done,ConnErr,Err1);
	}

	public static void change_user_score(string user_id,string token,int value,System.Action Done = null,System.Action ConnErr = null,System.Action Err1 = null){

		value += PlayerPrefs.GetInt ("score");
		//PlayerPrefs.SetInt ("score", value);

		StaticCoroutine.DoCoroutine(NetworkHandler.change_score(user_id, token,value,(result)=>{					
			IDictionary search = (IDictionary) Json.Deserialize(result);
			if(search == null){
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse(search["code"].ToString());

			if( state == Global.ERROR_SUCCESS){ 
				StaticCoroutine.errLog = 0;
				score = int.Parse(search["new_value"].ToString());

				PlayerPrefs.DeleteKey ("score");
				Debug.Log ("Set score success!");
			}
			else if(state==Global.ERROR_INVALID_TOKEN){
				StaticCoroutine.errLog = 1;
				Debug.Log ("Invalid token!");
			}
			else if(state==Global.ERROR_NOT_SATISFY){
				IList msgs = (IList)(search["msgs"]);
				StaticCoroutine.errLog = 9;
				Debug.Log ("Invalid scores! " + msgs[0]);
			}	
		}),Done,ConnErr,Err1);
	}

	public static void registration_request(System.Action Done = null,System.Action ConnErr = null,System.Action Err1 = null,System.Action Err2 = null){
		StaticCoroutine.DoCoroutine (NetworkHandler.registration_request ((result) => {					
			
			IDictionary search = (IDictionary)Json.Deserialize (result);
			if (search == null) {
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse (search ["code"].ToString ());
			
			if (state == Global.ERROR_SUCCESS) { 
				UserStatementManager.token = search ["token"].ToString ();

				StaticCoroutine.errLog = 0;
				Debug.Log ("Login success!!");
			} else
				StaticCoroutine.errLog = 9;
		}),Done,ConnErr);
	}

	public static void failureTest(string user_id,string token,System.Action Done = null,System.Action ConnErr = null,System.Action Err1 = null,System.Action Err2 = null){
		StaticCoroutine.DoCoroutine(NetworkHandler.failureTest(user_id,token,(result)=>{					
			
			IDictionary search = (IDictionary) Json.Deserialize(result);
			if(search == null){
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse(search["code"].ToString());
			
			if( state == Global.ERROR_SUCCESS){ 
				StaticCoroutine.errLog = 0;
				Debug.Log ("Login success!!");
			}
			else if(state==Global.ERROR_INVALID_TOKEN){
				StaticCoroutine.errLog = 1;
				Debug.Log ("Invalid token!");
			}	
			else if(state == Global.ERROR_USER_NOT_FOUND){
				StaticCoroutine.errLog = 1;
				Debug.Log ("User not found");
			}
		}));
	}

	public static void registration_data(string account,string psw,string n_nickname,string rg_token,System.Action Done = null,System.Action ConnErr = null,System.Action Err1 = null,System.Action Err2 = null){
		StaticCoroutine.DoCoroutine(NetworkHandler.registration_data(account,psw,n_nickname,rg_token,(result)=>{					
				
				IDictionary search = (IDictionary) Json.Deserialize(result);
				if(search == null){
					Debug.Log (result);
					StaticCoroutine.errLog = 9;
					return;
				}
				int state = int.Parse(search["code"].ToString());
				
				if( state == Global.ERROR_SUCCESS){ 
					IDictionary user = (IDictionary)search["user"];
					id = user["id"].ToString();
					token = user["token"].ToString();					
					if(user["nickname"]!=null)	nickname = user["nickname"].ToString();

					PlayerPrefs.SetString("id", id);
					PlayerPrefs.SetString("token", token);
					StaticCoroutine.errLog = 0;
					Debug.Log ("Registration success!!");
				}
				else if(state==Global.ERROR_INVALID_TOKEN){
					StaticCoroutine.errLog = 9;
					Debug.Log ("Invalid token!");
				}	
				else if(state == Global.ERROR_ACCOUNT_EXISTED){
					StaticCoroutine.errLog = 1;
					Debug.Log ("User has existed");
				}
				else if(state == Global.ERROR_NICKNAME_EXISTED){
					StaticCoroutine.errLog = 2;
					Debug.Log ("NickName has existed");
				}
			else{
				StaticCoroutine.errLog = 9;
				Debug.Log ("REGIST TOO MUCH!!" + state);
			}
			}),Done,ConnErr,Err1,Err2);
		}

	public static void get_full_info(string user_id,string user_token,System.Action Done = null,System.Action ConnErr = null,System.Action Err = null){
		StaticCoroutine.DoCoroutine(NetworkHandler.get_full_info(user_id, user_token,(result)=>{					
			IDictionary search = (IDictionary) Json.Deserialize(result);
			Debug.Log (result);
			if(search == null){
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse(search["code"].ToString());
			
			if( state == Global.ERROR_SUCCESS){ 

				//更新下次更新時間
				DateTime nextday = DateTime.Now.AddDays (1);
				Global.NEXT_MIDNIGHT = new DateTime(nextday.Year,nextday.Month,nextday.Day,0,10,0);
				TimeSpan TS = Global.NEXT_MIDNIGHT - DateTime.Now;
				if (TS.Days >= 1)
					Global.NEXT_MIDNIGHT = Global.NEXT_MIDNIGHT.AddDays (-1);
				
				Debug.Log (Global.NEXT_MIDNIGHT);

				id = user_id;
				token = user_token;
				//user info
				IDictionary user = (IDictionary)search["user"];
				color = int.Parse(user["color"].ToString());
				eye = int.Parse(user["eye"].ToString());
				tail = int.Parse(user["tail"].ToString());
				weapon = int.Parse(user["weapon"].ToString());
				wearing = int.Parse(user["wearing"].ToString());
				score = int.Parse(user["score"].ToString());
				repletion = int.Parse(user["repletion"].ToString());
				account = user["account"].ToString();

				if(user["nickname"]!=null)
					nickname = user["nickname"].ToString();

				string esil = user["equip_skill_id_list"].ToString();
				equip_skill_id_list = parseListToInt(esil);
				string sil = user["skill_id_list"].ToString();
				skill_id_list = parseListToInt(sil);

				Debug.Log ("id = " + id);
				Debug.Log ("token = " + token);
				Debug.Log ("eye = " + eye);
				Debug.Log ("tail = " + tail);
				Debug.Log ("weapon = " + weapon);
				Debug.Log ("score = " + score);
				Debug.Log ("repletion = " + repletion);

				//urgent info
				IDictionary urgent = (IDictionary)search["urgent"];

				Debug.Log ("Load urgents...");
				
				UrgentManager.opened = false;
				UrgentManager.started = false;
				UrgentManager.start_time = "";
				Debug.Log (urgent["opened"].ToString());
				UrgentManager.start_time = urgent["start_time"].ToString();
				Debug.Log ("Urgent start time:" + UrgentManager.start_time);
				if(String.Equals(urgent["opened"].ToString(),"True")){
					Debug.Log ("Urgent open");
					UrgentManager.opened = true;
					UrgentManager.time_left = int.Parse(urgent["time_left"].ToString());
					UrgentManager.next_start_time = urgent["next_start_time"].ToString();
					if(String.Equals(urgent["started"].ToString(),"True")){
						UrgentManager.started = true;
						UrgentManager.player_count = int.Parse(urgent["player_count"].ToString());
						UrgentManager.total_damage = int.Parse(urgent["total_damage"].ToString());
						UrgentManager.my_total_damage = 0;
					}
				}


				//missions info
				IDictionary missions = (IDictionary)search["missions"];
				Debug.Log ("Load missions...");
				foreach( string key in missions.Keys )  {
					
					string mstate = ((IDictionary)missions[key])["state"].ToString();
					string mis_daily = ((IDictionary)missions[key])["is_daily"].ToString();
					int mission_key = int.Parse(key);
					Debug.Log (key+" "+mstate+" "+mis_daily);
					MissionManager.id.Add(int.Parse(key));
					MissionManager.mission_state.Add (int.Parse(mstate));

					if( String.Equals(mis_daily,"False")){
						MissionManager.is_daily.Add(false);
					}
					else{
						MissionManager.is_daily.Add (true);
					}
					
					if(((IDictionary)missions[key])["open_time"]!=null){
						MissionManager.open_time.Add (((IDictionary)missions[key])["open_time"].ToString());
						Debug.Log ("open time: "+MissionManager.open_time[MissionManager.open_time.Count-1]);
					}
					else{
						MissionManager.open_time.Add ("");
					}
					
					if(((IDictionary)missions[key])["close_time"]!=null){
						MissionManager.close_time.Add(((IDictionary)missions[key])["close_time"].ToString());
						Debug.Log ("close time: "+MissionManager.close_time[MissionManager.close_time.Count-1]);
					}
					else{
						MissionManager.close_time.Add("");
					}		

					if(((IDictionary)missions[key])["finish_time"]!=null){
						MissionManager.finish_time.Add(((IDictionary)missions[key])["finish_time"].ToString());
						Debug.Log ("finish time: "+MissionManager.finish_time[MissionManager.finish_time.Count-1]);
					}
					else{
						MissionManager.finish_time.Add("");
					}	

				}   

				//items
				Debug.Log ("load items...");
				IDictionary items = (IDictionary)search["items"];
				UserStatementManager.item_list.Clear();
				UserStatementManager.item_num_list.Clear();
				
				foreach( string key in items.Keys )  {
					UserStatementManager.item_list.Add(int.Parse(key));
					UserStatementManager.item_num_list.Add (int.Parse(items[key].ToString()));
					Debug.Log (key + " "+items[key].ToString() );
				}   

				StaticCoroutine.errLog = 0;

			}
			else if(state==Global.ERROR_INVALID_TOKEN){
				StaticCoroutine.errLog = 1;	Debug.Log ("Invalid token!");
			}	
			else if(state == Global.ERROR_USER_NOT_FOUND){
				StaticCoroutine.errLog = 1;
				Debug.Log ("User not found");
			}
		}),Done,ConnErr,Err);
	}

	public static void give_reward(string user_id,string user_token,int score_value,int[] skills,List<int> item,List<int> item_num,System.Action Done = null,System.Action ConnErr = null,System.Action Err = null){

		//先將暫存資料置入list中
		Global.addPrefToList(item,item_num);
	//	Global.addItemPref(item, item_num);

		score_value += PlayerPrefs.GetInt ("score");
//		PlayerPrefs.SetInt ("score", score_value);

		//parsing
		string skill_list = SkillManager.int_list_to_string (skills);
		string item_pair_list = ItemManager.parse_list_to_string (item, item_num);

		StaticCoroutine.DoCoroutine(NetworkHandler.give_reward(user_id, user_token,score_value,skill_list,item_pair_list,(result)=>{					
			IDictionary search = (IDictionary) Json.Deserialize(result);
			Debug.Log (result);
			if(search == null){
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse(search["code"].ToString());
			
			if( state == Global.ERROR_SUCCESS){ 

				IDictionary score = (IDictionary)search["score"];
				if(int.Parse(score["code"].ToString ()) == Global.ERROR_SUCCESS){
					UserStatementManager.score = int.Parse(score["new_value"].ToString ());
					Debug.Log ("score:" + UserStatementManager.score);
				}

				IDictionary skill = (IDictionary)search["skill"];
				if(int.Parse(skill["code"].ToString ()) == Global.ERROR_SUCCESS){
					string skill_current = skill["skill_current"].ToString();
					UserStatementManager.skill_id_list = parseListToInt(skill_current);
					string debug = "";
					foreach(int s in UserStatementManager.skill_id_list){
						debug += s.ToString() + ",";
					}
					Debug.Log ("skill: "+ debug);
				}

				IDictionary items = (IDictionary)search["item"];
				IDictionary allitems = (IDictionary)items["items"];
				ItemManager.setup_items(allitems);

				PlayerPrefs.DeleteKey ("items");
				PlayerPrefs.DeleteKey ("nums");
				PlayerPrefs.DeleteKey ("score");

				StaticCoroutine.errLog = 0;
			}
			else if(state==Global.ERROR_INVALID_TOKEN){
				StaticCoroutine.errLog = 1;	Debug.Log ("Invalid token!");
			}	
			else if(state == Global.ERROR_USER_NOT_FOUND){
				StaticCoroutine.errLog = 1;
				Debug.Log ("User not found");
			}
		}),Done,ConnErr,Err);
	}


//--------------------------------------------------------------------------------------------------------------------------------------------

	static int[] parseListToInt(string text){
		char[] delimiterChars = {',','\n','\t','\r'}; // 使用逗號作為分隔
		string[] words = text.Split(delimiterChars);
		int len = words.Length;

		if (len == 1 && words [0].Length == 0) { // NULL list
			return new int[0];
		}

		int[] outputList = new int[len];
		for(int i=0;i<len;i++)
			outputList[i] = int.Parse(words[i]);
		return outputList;
	}


}
