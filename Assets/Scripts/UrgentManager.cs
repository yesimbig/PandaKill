using UnityEngine;
using System.Collections;
using MiniJSON;
using System;
using System.Collections.Generic;
public class UrgentManager : MonoBehaviour {

	public class rank_people{
		public int uid;
		public string nickname;
		public int damage;

		public rank_people(int u,string s,int d){
			uid = u;
			nickname = s;
			damage = d;
		}
	}


	public static bool opened = false;
	public static bool started = false;
	public static string start_time;
	public static string next_start_time;
	public static int player_count;
	public static int total_damage;
	public static int time_left;
	public static int my_total_damage;
	public static int pandaHP;
	public static List<rank_people> rank = new List<rank_people>();
	public static int player_limit = 90;


	public static void get_urgent_info(System.Action Done = null,System.Action Err = null,System.Action Err1 = null){
		StaticCoroutine.DoCoroutine(NetworkHandler.Urgent_info((result)=>{					
			IDictionary search = (IDictionary) Json.Deserialize(result);
			Debug.Log (result);
			if(search == null){
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse(search["code"].ToString());
			
			if( state == Global.ERROR_SUCCESS){ 
				Debug.Log ("Load urgents...");
				Debug.Log (search["opened"].ToString());

				UrgentManager.opened = false;
				UrgentManager.started = false;
				UrgentManager.start_time = search["start_time"].ToString();
				Debug.Log ("Urgent start time:" + UrgentManager.start_time);
				if(String.Equals(search["opened"].ToString(),"True")){
					Debug.Log ("Urgent open");
					UrgentManager.opened = true;
					UrgentManager.time_left = int.Parse(search["time_left"].ToString());
					UrgentManager.next_start_time = search["next_start_time"].ToString();

					if(search["player_count"]!=null){
						UrgentManager.player_count = int.Parse(search["player_count"].ToString());
					}else UrgentManager.player_count = 0;

					if(String.Equals(search["started"].ToString(),"True")){
						UrgentManager.started = true;
						UrgentManager.total_damage = int.Parse(search["total_damage"].ToString());
						UrgentManager.my_total_damage = 0;
					}
				}

				rank.Clear();
				if(search["rank"]!=null){
					IDictionary ranks = (IDictionary)search["rank"];

					foreach( string key in ranks.Keys )  {
						Debug.Log(key);
						IDictionary rank_key = (IDictionary)ranks[key];
						if(rank_key!=null){
							rank.Add(new rank_people( int.Parse(rank_key["uid"].ToString()), rank_key["nickname"].ToString(), int.Parse(rank_key["damage"].ToString()) ) );
							Debug.Log (key + " "+rank_key["uid"].ToString() + " " + rank_key["nickname"].ToString() + " " + rank_key["damage"].ToString() );
						}
					}   
				}

				pandaHP = int.Parse( ((IDictionary)search["boss_info"])["hp"].ToString());
				Debug.Log(pandaHP);
				player_limit = int.Parse( ((IDictionary)search["boss_info"])["player_limit"].ToString());

				StaticCoroutine.errLog = 0;
				Debug.Log ("Get urgent info success!!");
			}
			else{
				StaticCoroutine.errLog = 1;
			}	

		}),Done,Err,Err1);
	}

	public static void user_add_damage(string user_id,string user_token,int damage,System.Action Done = null,System.Action ConnErr = null,System.Action Err1 = null,System.Action Err2 = null){
		StaticCoroutine.DoCoroutine(NetworkHandler.add_damage(user_id,user_token,damage,(result)=>{			
			Debug.Log (result);
			IDictionary search = (IDictionary) Json.Deserialize(result);
			if(search == null){
				//Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse(search["code"].ToString());
			
			if( state == Global.ERROR_SUCCESS){ 
				player_count = int.Parse(search["player_count"].ToString());
				total_damage = int.Parse(search["total_damage"].ToString());
				time_left = int.Parse(search["time_left"].ToString());
				my_total_damage = int.Parse(search["my_total_damage"].ToString());

				rank.Clear();
				if(search["rank"]!=null){
					IDictionary ranks = (IDictionary)search["rank"];

					foreach( string key in ranks.Keys )  {
						Debug.Log(key);
						IDictionary rank_key = (IDictionary)ranks[key];

						if(rank_key!=null){
							rank.Add(new rank_people( int.Parse(rank_key["uid"].ToString()), rank_key["nickname"].ToString(), int.Parse(rank_key["damage"].ToString()) ) );
							Debug.Log (key + " "+rank_key["uid"].ToString() + " " + rank_key["nickname"].ToString() + " " + rank_key["damage"].ToString() );
						}
					}   
				}


				StaticCoroutine.errLog = 0;
				Debug.Log ("Add damage success!!");
			}
			else if( state==Global.ERROR_NOT_SATISFY){
				StaticCoroutine.errLog = 1;
				Debug.Log ("Mission isn't start");
			}	
			else if( state == Global.ERROR_INVALID_TOKEN){
				StaticCoroutine.errLog = 2;
				Debug.Log ("Invalid token");
			}
			
		}),Done,ConnErr,Err1,Err2);
	}

	public static void get_damage(string user_id,string user_token,System.Action Done = null,System.Action ConnErr = null,System.Action Err1 = null,System.Action Err2 = null){
		StaticCoroutine.DoCoroutine(NetworkHandler.get_damage(user_id,user_token,(result)=>{					
			IDictionary search = (IDictionary) Json.Deserialize(result);
			if(search == null){
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse(search["code"].ToString());

			if( state == Global.ERROR_SUCCESS){ 
				player_count = int.Parse(search["player_count"].ToString());
				total_damage = int.Parse(search["total_damage"].ToString());
				time_left = int.Parse(search["time_left"].ToString());
				my_total_damage = int.Parse(search["my_total_damage"].ToString());

				rank.Clear();
				if(search["rank"]!=null){
					IDictionary ranks = (IDictionary)search["rank"];

					foreach( string key in ranks.Keys )  {
						Debug.Log(key);
						IDictionary rank_key = (IDictionary)ranks[key];

						if(rank_key!=null){
							rank.Add(new rank_people( int.Parse(rank_key["uid"].ToString()), rank_key["nickname"].ToString(), int.Parse(rank_key["damage"].ToString()) ) );
							Debug.Log (key + " "+rank_key["uid"].ToString() + " " + rank_key["nickname"].ToString() + " " + rank_key["damage"].ToString() );
						}
					}   
				}


				StaticCoroutine.errLog = 0;
				Debug.Log ("Get damage success!!");
			}
			else if( state==Global.ERROR_NOT_SATISFY){
				StaticCoroutine.errLog = 1;
				Debug.Log ("Mission isn't start");
			}	
			else if( state == Global.ERROR_INVALID_TOKEN){
				StaticCoroutine.errLog = 2;
				Debug.Log ("Invalid token");
			}

		}),Done,ConnErr,Err1,Err2);
	}

	public static void urgent_exit(string user_id,string user_token,System.Action Done = null,System.Action ConnErr = null,System.Action Err1 = null,System.Action Err2 = null){
		StaticCoroutine.DoCoroutine(NetworkHandler.urgent_exit(user_id,user_token,(result)=>{					
			IDictionary search = (IDictionary) Json.Deserialize(result);
			if(search == null){
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse(search["code"].ToString());
			
			if( state == Global.ERROR_SUCCESS){ 

				StaticCoroutine.errLog = 0;
				Debug.Log ("Exit success!!");
			}
			else if( state==Global.ERROR_NOT_SATISFY){
				StaticCoroutine.errLog = 1;
				Debug.Log ("Mission not open!");
			}	
			else if( state == Global.ERROR_NOT_EXIST){
				StaticCoroutine.errLog = 1;
				Debug.Log ("NOT JOINED!!");
			}else if(state == Global.ERROR_INVALID_TOKEN){
				StaticCoroutine.errLog = 2;
			}
			
		}),Done,ConnErr,Err1,Err2);
	}
}
