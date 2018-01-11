using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UItest : MonoBehaviour {

	void Start(){
		Debug.Log (PlayerPrefs.GetString("id"));
		Debug.Log (PlayerPrefs.GetString("token"));
	
	}

	void OnGUI() {

		if(GUI.Button (new Rect (50, 30, 100, 100), "User login")) {
			UserStatementManager.user_login("big", "pass", onLoginSuccess,onUserNotFound,onPasswordErr);
		}

		if(GUI.Button (new Rect (50, 140, 100, 100), "Get user info")) {
			UserStatementManager.get_user_info(UserStatementManager.id, UserStatementManager.token);
		}

		if(GUI.Button (new Rect (50, 250, 100, 100), "Set user info")) {
			UserStatementManager.set_user_info(UserStatementManager.id, UserStatementManager.token ,1,3,3,3,3);
		}
		
		if(GUI.Button (new Rect (160, 30, 100, 100), "Change repletion")) {
			UserStatementManager.change_user_repletion(UserStatementManager.id, UserStatementManager.token , 1);
		}
		
		if(GUI.Button (new Rect (160, 140, 100, 100), "Change score")) {
			UserStatementManager.change_user_score(UserStatementManager.id, UserStatementManager.token , 100);
		}
		
		if(GUI.Button (new Rect (160, 250, 100, 100), "User logout")) {
			UserStatementManager.user_logout(UserStatementManager.id, UserStatementManager.token);
		}

		if(GUI.Button (new Rect (270, 30, 100, 100), "Learn skill")) {
			int[] list = {0,1,2,3,4,5,6,7};
			SkillManager.user_learn_skill(UserStatementManager.id, UserStatementManager.token , list);
		}
		
		if (GUI.Button (new Rect (270, 140, 100, 100), "Forget skill")) {
			int[] list = {1,2,3,4,5};
			SkillManager.user_forget_skill(UserStatementManager.id, UserStatementManager.token , list);
		}

		if (GUI.Button (new Rect (270, 250, 100, 100), "Generate DElay Item")) {
			Global.addItemPref (new List<int>{3,3,2,1},new List<int>{2,-1,1,1});
			PlayerPrefs.SetInt("score",10);
		}

		if(GUI.Button (new Rect (380, 30, 100, 100), "Get items")) {
			ItemManager.user_get_items(UserStatementManager.id, UserStatementManager.token);
		}
		
		if (GUI.Button (new Rect (380, 140, 100, 100), "Alter items")) {
			List<int> list = new List<int>{0,1,2};
			List<int> num = new List<int>{2,2,1};
			ItemManager.user_change_item_amount(UserStatementManager.id, UserStatementManager.token , list,num);
		}



		if (GUI.Button (new Rect (380, 250, 100, 100), "Failure test")) {
			UserStatementManager.failureTest(UserStatementManager.id, UserStatementManager.token);
		}

		if(GUI.Button (new Rect (490, 30, 100, 100), "Get all missions")) {
			MissionManager.get_all_missions(UserStatementManager.id, UserStatementManager.token);
		}
		
		if (GUI.Button (new Rect (490, 140, 100, 100), "take mission")) {
			MissionManager.take_mission(UserStatementManager.id, UserStatementManager.token , 1);
		}
		
		if (GUI.Button (new Rect (490, 250, 100, 100), "quit mission")) {
			MissionManager.quit_mission(UserStatementManager.id, UserStatementManager.token , 1);
		}

		if (GUI.Button (new Rect (490, 360, 100, 100), "finish mission")) {
			MissionManager.finish_mission(UserStatementManager.id, UserStatementManager.token , 1);
		}

		if(GUI.Button (new Rect (600, 30, 100, 100), "Get urgent info")) {
			UrgentManager.get_urgent_info();
		}
		
		if (GUI.Button (new Rect (600, 140, 100, 100), "Add damage")) {
			UrgentManager.user_add_damage(UserStatementManager.id, UserStatementManager.token , 1);
		}

		if (GUI.Button (new Rect (600, 250, 100, 100), "Add damage")) {
			int[] skills = {1,2,3,4};
			List<int> item = new List<int>{1,2,3,4};
			List<int> item_num = new List<int>{1,1,1,1};

			UserStatementManager.give_reward(UserStatementManager.id, UserStatementManager.token ,100,skills,item,item_num);
		}
	}

	void onLoginSuccess(){
		UserStatementManager.get_user_info(UserStatementManager.id, UserStatementManager.token);
	}

	void onUserNotFound(){
		Debug.Log ("onUserNotFound");
	}

	void onPasswordErr(){
		Debug.Log ("PasswordError");
	}

	void onAlterItemSuccess(){
		ItemManager.user_get_items(UserStatementManager.id, UserStatementManager.token);
	}
}
