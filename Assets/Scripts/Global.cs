using UnityEngine;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using MiniJSON;
using System;

public class Global : MonoBehaviour {

	public static float baseScreenWidth = 1920;
	public static float baseSreenHeight = 1200;
	public static float baseSreenOrthographicSize = 5;


	public static string CONN_URL = "https://nctumeichu2016.nctucs.net/";
	public static int ERROR_SUCCESS = 1; //成功
	public static int ERROR_GENERIC_ERROR = 2; //未分類的錯誤
	public static int ERROR_INVALID_TOKEN = 3;// 無效的TOKEN(重複登入或未登入)
	public static int ERROR_NOT_EXIST = 4;//不存在

	public static int ERROR_USER_NOT_FOUND = 1001;//找不到該使用者
	public static int ERROR_PASSWORD_MISMATCH = 1002;//密碼錯誤
	public static int ERROR_ACCOUNT_EXISTED = 1003;//帳號重複
	public static int ERROR_NICKNAME_EXISTED = 1004;//暱稱重複

	public static int ERROR_INVALID_PARAMETER = 2001;//參數錯誤(資料庫格式與輸入不符)
	public static int ERROR_NOT_SATISFY = 2002;//條件不滿

	/*
	 * ERROR_BANNED = 3001 # IP遭禁用
		ERROR_TOKEN_LIMIT = 3002 # 短時間內取得太多TOKEN
		ERROR_SHORT_CREATE_LIMIT = 3003 # 短時間內不可重複建立帳號
		ERROR_LONG_CREATE_LIMIT = 3004 # 長時間內不可不可在建立新帳號、取得TOKEN
	 */

	public static int MAX_STRING_LENTH = 32;
	public static float orthographicSize = 5;
	public static float timeout = 10f;

	public static int URGENT_PLAYER_LIMIT = 90; //緊急任務限制人數

	public static int MISSION_ACCEPTABLE = 1; //可接受任務
	public static int MISSION_TOKEN = 2; //已接受任務
	public static int MISSION_COMPLETED = 3; //已完成任務
	public static int MISSION_CLOSED = 4; //已關閉任務

	public static int MISSION_NUM = 10; //任務總數
	public static int SKILL_NUM = 10; //技能總數
	public static int ITEM_NUM = 10; //道具總數

	public static float WAITING_TIME = 5f;
	public static float TRIGGER_TIME = 5f;
	public static float BUFFER_TIME = 4f;

	public static string NICK_NAME = "";

	public static DateTime NEXT_MIDNIGHT;

	public class all_rank{
		public int uid;
		public string id;
		public string nickname;
		public int score;
		public string account;

		public all_rank(int u,string i,string n,int s,string a){
			uid = u;
			id = i;
			nickname = n;
			score = s;
			account = a;
		}
	}

	public static List<all_rank> GENERAL_RANK = new List<all_rank> ();
	public static int USER_RANK;


	public static bool CheckFormat(string str){
		if (str.Length > Global.MAX_STRING_LENTH)
			return false;
		return Regex.IsMatch (str, @"^\w+$");
	}

	//以中文字=2，英文字=1判斷字串長度
	public static int fixed_string_length(string strChinese){
			int bresult = 0;
			int dRange = 0;
			int dstringmax=Convert.ToInt32("9fff", 16);
			int dstringmin=Convert.ToInt32("4e00", 16);
			for (int i = 0; i < strChinese.Length; i++)
			{
				dRange = Convert.ToInt32(Convert.ToChar(strChinese.Substring(i, 1)));
				if (dRange >= dstringmin && dRange <dstringmax )
				{
					bresult += 2;
				}
				else
				{
					bresult++;
				}
			}
			
		return bresult;
	}

	//將長度一率壓至9個字元長度
	public static string fixed_string(string strChinese){
		string s = "";
		int bresult = 0;
		int dRange = 0;
		int dstringmax=Convert.ToInt32("9fff", 16);
		int dstringmin=Convert.ToInt32("4e00", 16);
		for (int i = 0; i < strChinese.Length && bresult < 9; i++)
		{
			dRange = Convert.ToInt32(Convert.ToChar(strChinese.Substring(i, 1)));
			s+= strChinese.Substring(i, 1);
			if (dRange >= dstringmin && dRange <dstringmax )
			{
				bresult += 2;
			}
			else
			{
				bresult++;
			}
		}
		
		return s;
	}


	//初始化所有道具資料
	public static void init_all(){
		Data_Skills.setSkills ();
		Data_Items.setItems ();
		Data_Mission.setMissions ();
	
	}

	//新增未上傳道具資料
	public static void addItemPref(List<int> id,List <int> num){
		string items = PlayerPrefs.GetString("items");
		string nums = PlayerPrefs.GetString("nums");
		if (items == null || items == "") {

			items = "";
			nums = "";
			for(int i=0;i<id.Count;i++){
				items = items + id[i].ToString() + ",";
				nums = nums + num[i].ToString() + ",";
			}

			PlayerPrefs.SetString ("items", items);
			PlayerPrefs.SetString ("nums", nums);
		} else {
			for(int i=0;i<id.Count;i++){
				items += id[i].ToString() + ",";
				nums += num[i].ToString() + ",";
			}
			
			PlayerPrefs.SetString ("items", items);
			PlayerPrefs.SetString ("nums", nums);
		}
		Debug.Log ("generate items = " + PlayerPrefs.GetString("items"));
	}

	//上傳暫存區的道具資料
	public static void delayUploadItems(System.Action onComplete,System.Action onFail = null){
		string items = PlayerPrefs.GetString("items");
		int score = PlayerPrefs.GetInt("score");
		Debug.Log ("items = " + items);
		Debug.Log ("score = " + score);
		if ((items == null || items.Length == 0) && score == 0) {
			onComplete ();
			return;
		}

		List<int> id = new List<int> ();
		List<int> num = new List<int> ();

		UserStatementManager.give_reward(UserStatementManager.id, UserStatementManager.token,0,new int[0], id, num, onComplete, onFail);
	}

	//分析目前暫存資料，再將其置入串列中
	public static void addPrefToList(List<int> id,List<int>num){
		string pitems = PlayerPrefs.GetString("items");
		string pnums = PlayerPrefs.GetString("nums");
		//Debug.Log ("pitems = " + pitems);
		if (pitems == null || pitems.Length == 0) {
			return;
		}
		char[] delimiterChars = { ',' };
		string[] words = pitems.Split (delimiterChars);
		string[] wnums = pnums.Split (delimiterChars);
		for (int i=0;i<words.Length;i++)
		{
			if(words[i].Length>0){
				//Debug.Log ("words = " + int.Parse (words[i]) + " nums= " + int.Parse(wnums[i]));
				findAndAddItem(id,num,int.Parse (words[i]),int.Parse(wnums[i]));
			}
		}
	}

	//新增道具至串列中
	public static void findAndAddItem(List<int> items,List<int>items_num,int id,int num){
		for (int i = 0; i < items.Count; i++) {
			Debug.Log (items[i]);
			if (items [i] == id) {
				//Debug.Log ("find!!"+ id);
				items_num[i] += num;
				return;
			}
		}
		items.Add (id);
		items_num.Add (num);
	}

	//取得任務是否被看過
	public static bool isMissionPrefExist(int id){
		string missions = PlayerPrefs.GetString("missions");
		if (missions == null || missions.Length == 0) {
			return false;
		}
		char[] delimiterChars = { ',' };
		string[] mission = missions.Split (delimiterChars);
		for (int i=0;i<mission.Length;i++)
		{
			if(int.Parse(mission[i]) == id)return true;
		}
		return false;
	}

	//新增已看過任務資料
	public static void addMissionsPref(int id){
		string missions = PlayerPrefs.GetString("missions");
		if (missions == null || missions == "") {
			PlayerPrefs.SetString ("missions", id.ToString ());
		} else {
			PlayerPrefs.SetString ("missions", missions + "," + id.ToString ());
		}
	}

	//更新全體總排名
	public static void general_rank(int id,System.Action Done = null,System.Action Err = null,System.Action Err1 = null){
		StaticCoroutine.DoCoroutine(NetworkHandler.general_rank(id,(result)=>{					
			IDictionary search = (IDictionary) Json.Deserialize(result);
			Debug.Log (result);
			if(search == null){
				StaticCoroutine.errLog = 9;
				return;
			}
			int state = int.Parse(search["code"].ToString());
			
			if( state == Global.ERROR_SUCCESS){ 

				GENERAL_RANK.Clear();

				USER_RANK = int.Parse(search["query_rank"].ToString());

				if(search["data"]!=null){
					IDictionary ranks = (IDictionary)search["data"];
					
					foreach( string key in ranks.Keys )  {
						Debug.Log(key);
						IDictionary rank_key = (IDictionary)ranks[key];
						if(rank_key!=null){
							GENERAL_RANK.Add(new all_rank( int.Parse(rank_key["uid"].ToString()),"imbig", rank_key["nickname"].ToString(), int.Parse(rank_key["score"].ToString()), rank_key["account"].ToString() ) );
						}
					}   
				}

				StaticCoroutine.errLog = 0;
				Debug.Log ("Get general rank success!!");
			}
			else{
				StaticCoroutine.errLog = 1;
			}	
			
		}),Done,Err,Err1);
	}

}
