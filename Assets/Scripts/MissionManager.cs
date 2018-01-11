using UnityEngine;
using System.Collections;
using MiniJSON;
using System;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour {

	public static List<int> id = new List<int>();
	public static List<bool> is_daily = new List<bool>();
	public static List<int> mission_state = new List<int>();
	public static List<int> mission_server_id = new List<int>();
	public static List<string> open_time = new List<string>() ,close_time = new List<string>(),finish_time = new List<string>();

	// Use this for initialization
	void Start () {
	}

	public static void get_all_missions(string user_id,string token,System.Action Done = null,System.Action ConnErr = null,System.Action Err1 = null){
		StaticCoroutine.DoCoroutine(NetworkHandler.all_missions(user_id, token, (result) => {					
			IDictionary search = (IDictionary)Json.Deserialize (result);
			if (search == null) {
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			Debug.Log(result);

			int state = int.Parse (search ["code"].ToString ());
			
			if( state == Global.ERROR_SUCCESS){ 
				IDictionary missions = (IDictionary)search["missions"];
				id.Clear();
				is_daily.Clear();
				mission_state.Clear ();
				mission_server_id.Clear ();
				open_time.Clear ();
				close_time.Clear ();
				finish_time.Clear ();

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

				StaticCoroutine.errLog = 0;
				Debug.Log ("Get all missions success!!");
			}
			else if(state == Global.ERROR_USER_NOT_FOUND){
				StaticCoroutine.errLog = 1;
				Debug.Log ("User not found");
			}
			else if(state==Global.ERROR_INVALID_TOKEN){
				StaticCoroutine.errLog = 1;
				Debug.Log ("Invalid token!");
			}	
		}),Done,ConnErr,Err1);
	}

	public static void take_mission(string user_id,string token,int mission_id,System.Action Done = null,System.Action ConnErr = null,System.Action Err1 = null,System.Action Err2 = null){
		StaticCoroutine.DoCoroutine(NetworkHandler.take_mission(user_id, token,mission_id, (result) => {					
			IDictionary search = (IDictionary)Json.Deserialize (result);
			if (search == null) {
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			
			int state = int.Parse (search ["code"].ToString ());
			
			if( state == Global.ERROR_SUCCESS){ 
				mission_state[MissionManager.id.IndexOf( mission_id )] = Global.MISSION_TOKEN;
				StaticCoroutine.errLog = 0;
				Debug.Log ("Take mission"+mission_id+" success!!");
			}
			else if(state == Global.ERROR_NOT_SATISFY){
					StaticCoroutine.errLog = 2;
					Debug.Log ("Mission not available");
			}
			else if(state == Global.ERROR_INVALID_TOKEN){
				StaticCoroutine.errLog = 1;
				Debug.Log ("Invalid token!!");
			}

		}),Done,ConnErr,Err1,Err2);
	}

	public static void quit_mission(string user_id,string token,int mission_id,System.Action Done = null,System.Action ConnErr = null,System.Action Err1 = null,System.Action Err2 = null){
		StaticCoroutine.DoCoroutine(NetworkHandler.quit_mission(user_id, token,mission_id, (result) => {					
			IDictionary search = (IDictionary)Json.Deserialize (result);
			if (search == null) {
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			
			int state = int.Parse (search ["code"].ToString ());
			
			if( state == Global.ERROR_SUCCESS){ 
				mission_state[MissionManager.id.IndexOf( mission_id )] = Global.MISSION_ACCEPTABLE;
				StaticCoroutine.errLog = 0;
				Debug.Log ("Quit mission"+mission_id+" success!!");
			}
			else if(state == Global.ERROR_NOT_SATISFY){
				StaticCoroutine.errLog = 2;
				Debug.Log ("Mission not available");
			}
			else if(state == Global.ERROR_INVALID_TOKEN){
				StaticCoroutine.errLog = 1;
				Debug.Log ("Invalid token!!");
			}
			
		}),Done,ConnErr,Err1,Err2);
	}

	public static void finish_mission(string user_id,string token,int mission_id,System.Action Done = null,System.Action ConnErr = null,System.Action Err1 = null){
		StaticCoroutine.DoCoroutine(NetworkHandler.finish_mission(user_id, token,mission_id, (result) => {					
			IDictionary search = (IDictionary)Json.Deserialize (result);
			if (search == null) {
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			
			int state = int.Parse (search ["code"].ToString ());
			
			if( state == Global.ERROR_SUCCESS){ 
				mission_state[MissionManager.id.IndexOf( mission_id )] = Global.MISSION_COMPLETED;

				if(search["finish_time"]!=null){
					MissionManager.finish_time[MissionManager.id.IndexOf( mission_id )] = search["finish_time"].ToString();
				}


				StaticCoroutine.errLog = 0;
				Debug.Log ("Finish mission"+mission_id+" success!!");
			}
			else if(state == Global.ERROR_NOT_SATISFY){
				StaticCoroutine.errLog = 2;
				Debug.Log ("Mission not available");
			}
			else if(state == Global.ERROR_INVALID_TOKEN){
				StaticCoroutine.errLog = 1;
				Debug.Log ("Invalid token!!");
			}
			
		}),Done,ConnErr,Err1);
	}
	
}
