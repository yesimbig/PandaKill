using UnityEngine;
using System.Collections;
using MiniJSON;

public class SkillManager : MonoBehaviour {
	//Function to use:
	//> user_learn_skill:    input id, token, and skill list; access the new learned skill 
	//> user_forget_skill:   input id, token, and skill list; access the new forgotten skill
	//> set_equip_skill:	 input id, token, and skill list; access the new forgotten skill


	public static void user_learn_skill(string user_id,string token,int[] list, System.Action Done = null,System.Action Err = null,System.Action Err1 = null){
		string slist = int_list_to_string (list);
		StaticCoroutine.DoCoroutine(NetworkHandler.learn_skill(user_id, token, slist,(result)=>{					
			IDictionary search = (IDictionary) Json.Deserialize(result);
			if(search == null){
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse(search["code"].ToString());
			
			if( state == Global.ERROR_SUCCESS){ 
				string skill_current = search["skill_current"].ToString();
				UserStatementManager.skill_id_list = parseListToInt(skill_current);

				string debug = "";
				foreach(int s in UserStatementManager.skill_id_list){
					debug += s.ToString() + ",";
				}
				Debug.Log ("skill: "+ debug);
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

	public static void user_forget_skill(string user_id,string token,int[] list, System.Action Done = null,System.Action Err = null,System.Action Err1 = null){
		string slist = int_list_to_string (list);
		StaticCoroutine.DoCoroutine(NetworkHandler.forget_skill(user_id, token, slist,(result)=>{					
			IDictionary search = (IDictionary) Json.Deserialize(result);
			if(search == null){
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse(search["code"].ToString());
			
			if( state == Global.ERROR_SUCCESS){ 
				string skill_current = search["skill_current"].ToString();
				UserStatementManager.skill_id_list = parseListToInt(skill_current);
				
				string debug = "";
				foreach(int s in UserStatementManager.skill_id_list){
					debug += s.ToString() + ",";
				}
				StaticCoroutine.errLog = 0;
				Debug.Log ("skill: "+ debug);
			}
			else if(state==Global.ERROR_INVALID_TOKEN){
				Debug.Log ("Invalid token!");
				StaticCoroutine.errLog = 1;
			}	
			else if(state == Global.ERROR_USER_NOT_FOUND){
				Debug.Log ("User not found");
				StaticCoroutine.errLog = 9;
			}
		}),Done,Err,Err1);
	}

	public static void set_equip_skill(string user_id,string token,int[] list, System.Action Done = null,System.Action Err = null,System.Action Err1 = null){
		string slist = int_list_to_string (list);
		StaticCoroutine.DoCoroutine(NetworkHandler.equip_skill(user_id, token, slist,(result)=>{	
			//Debug.Log (result);
			IDictionary search = (IDictionary) Json.Deserialize(result);
			if(search == null){
				Debug.Log (result);
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse(search["code"].ToString());
			
			if( state == Global.ERROR_SUCCESS){ 
				string skill_current = search["skill_current"].ToString();
				UserStatementManager.equip_skill_id_list = parseListToInt(skill_current);
				
				string debug = "";
				foreach(int s in UserStatementManager.equip_skill_id_list){
					debug += s.ToString() + ",";
				}
				StaticCoroutine.errLog = 0;
				Debug.Log ("skill: "+ debug);
			}
			else if(state==Global.ERROR_INVALID_TOKEN){
				Debug.Log ("Invalid token!");
				StaticCoroutine.errLog = 1;
			}	
			else if(state == Global.ERROR_USER_NOT_FOUND){
				Debug.Log ("User not found");
				StaticCoroutine.errLog = 9;
			}
		}),Done,Err,Err1);
	}

//--------------------------------------------------------------------------------------------------------------------------------------------

	public static string int_list_to_string (int[] list){
		string slist = "";
		for (int i=0; i<list.Length; i++) {
			slist += list[i].ToString();
			if(i!=list.Length-1) slist+=",";
		}
		return slist;	
	}

	public static int[] parseListToInt(string text){
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
