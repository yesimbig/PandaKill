using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UItestE : MonoBehaviour {

	void OnGUI() {
		GUI.skin.button.fontSize =30;
		if(GUI.Button (new Rect (200, 130, 200, 200), "Full repletion")) {
			int r = UserStatementManager.repletion;
			UserStatementManager.change_user_repletion(UserStatementManager.id, UserStatementManager.token , 4-r);
		}
		
		if(GUI.Button (new Rect (420, 130, 200, 200), "Zero score")) {
			int c = UserStatementManager.score;
			UserStatementManager.change_user_score(UserStatementManager.id, UserStatementManager.token , -c);
		}

		if (GUI.Button (new Rect (640, 130, 200, 200), "Clear items")) {
			List<int> list = new List<int>();
			List<int> num = new List<int>();
			for(int i=0;i<UserStatementManager.item_list.Count;i++){
				list.Add (UserStatementManager.item_list[i]);
				num.Add (-UserStatementManager.item_num_list[i]);
			}
			ItemManager.user_change_item_amount(UserStatementManager.id, UserStatementManager.token , list,num);
		}

		if(GUI.Button (new Rect (860, 130, 200, 200), "Logout")) {
			UserStatementManager.user_logout(UserStatementManager.id, UserStatementManager.token);
		}

		if(GUI.Button (new Rect (200, 340, 200, 200), "Learn skill")) {
			int[] list = {0,1,2,3,4,5,6,7};
			SkillManager.user_learn_skill(UserStatementManager.id, UserStatementManager.token , list);
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
}
