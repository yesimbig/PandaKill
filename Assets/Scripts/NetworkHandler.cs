using UnityEngine;
using System.Collections;
using MiniJSON;

public class NetworkHandler : MonoBehaviour {
	
	public static IEnumerator PostRequest(System.Action<string> onComplete)
	{
		WWWForm form = new WWWForm();
		form.AddField ("POST_test", "1");
		form.AddField ("POST_test2", "2");

		WWW hs_post = new WWW(Global.CONN_URL + "test/post_json", form.data);
		yield return hs_post;
		if (hs_post == null || hs_post.error != null) {
			onComplete("GG:" + hs_post.error);
		}
		else
		{
			onComplete(hs_post.text);
		}
	}
	
	public static IEnumerator failureTest(string account, string psw ,System.Action<string> onComplete)
	{
		WWW hs_get = new WWW ("https://nctumeichu2016.nctucs.net/test/delay/10");
		yield return hs_get;		
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}


	public static IEnumerator users_login(string account, string psw ,System.Action<string> onComplete)
	{
		WWW hs_get = new WWW(Global.CONN_URL + "users/login?account="+account+"&password="+psw);

		yield return hs_get;		
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator user_logout(string user_id ,string token ,System.Action<string> onComplete)
	{
		WWW hs_get = new WWW(Global.CONN_URL + "user/"+ user_id + "/logout?token=" + token);
		yield return hs_get;		
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator get_info(string user_id ,string token ,System.Action<string> onComplete)
	{
		WWW hs_get = new WWW(Global.CONN_URL +"user/"+ user_id + "/get_info?token=" + token);
		yield return hs_get;		
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator get_full_info(string user_id ,string token ,System.Action<string> onComplete)
	{
		WWW hs_get = new WWW(Global.CONN_URL +"user/"+ user_id + "/get_full_info?token=" + token);
		yield return hs_get;		
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator set_info(string user_id ,string token ,int n_color,int n_eye,int n_tail,int n_wearing,int n_weapon,System.Action<string> onComplete)
	{ Debug.Log (Global.CONN_URL +"user/"+ user_id + "/set_info");


		WWWForm form = new WWWForm();
		//form.AddField ("token", token);
		form.AddField ("color", n_color.ToString());
		form.AddField ("eye", n_eye.ToString());
		form.AddField ("tail", n_tail.ToString());
		form.AddField ("wearing", n_wearing.ToString());
		form.AddField ("weapon", n_weapon.ToString());

		//WWW hs_get = new WWW(Global.CONN_URL +"user/"+ user_id + "/set_info?token="+token,form.data);

		WWW hs_get = new WWW(Global.CONN_URL + "user/" + user_id + "/set_info?token=" + token + "&color=" + n_color + "&eye=" + n_eye + "&tail=" + n_tail + "&wearing=" + n_wearing + "&weapon=" + n_weapon );

		yield return hs_get;		

		if (hs_get == null || hs_get.error != null) {
			Debug.Log (hs_get.error);
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator change_repletion(string user_id ,string token ,int value, System.Action<string> onComplete)
	{
		WWW hs_get = new WWW(Global.CONN_URL +"user/"+ user_id + "/change_repletion?token=" + token + "&value="+value);
		yield return hs_get;		
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator change_score(string user_id ,string token ,int value, System.Action<string> onComplete)
	{
		WWW hs_get = new WWW(Global.CONN_URL +"user/"+ user_id + "/change_score?token=" + token + "&value="+value);
		yield return hs_get;		
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator learn_skill(string user_id ,string token ,string list, System.Action<string> onComplete)
	{
		WWW hs_get = new WWW(Global.CONN_URL +"user/"+ user_id + "/learn_skill?token=" + token + "&list="+list);
		yield return hs_get;		
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator forget_skill(string user_id ,string token ,string list, System.Action<string> onComplete)
	{
		WWW hs_get = new WWW(Global.CONN_URL +"user/"+ user_id + "/forget_skill?token=" + token + "&list="+list);
		yield return hs_get;		
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator get_items(string user_id ,string token , System.Action<string> onComplete)
	{
		WWW hs_get = new WWW(Global.CONN_URL +"user/"+ user_id + "/get_items?token=" + token);
		yield return hs_get;
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator change_item_amount(string user_id ,string token ,string list, System.Action<string> onComplete)
	{
		System.Text.Encoding myEncoding = System.Text.Encoding.GetEncoding("utf-8");
		list = WWW.EscapeURL (list, myEncoding);
		WWW hs_get = new WWW(Global.CONN_URL +"user/"+ user_id + "/change_item_amount?token=" + token +"&with_all_item=true&pair_list=" + list);
		yield return hs_get;
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator registration_request(System.Action<string> onComplete)
	{
		WWW hs_get = new WWW(Global.CONN_URL +"users/registration_request");
		yield return hs_get;
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator registration_data(string account,string psw,string n_nickname, string token, System.Action<string> onComplete)
	{

		
		System.Text.Encoding myEncoding = System.Text.Encoding.GetEncoding("utf-8");
		n_nickname = WWW.EscapeURL (n_nickname, myEncoding);

		WWW hs_get = new WWW(Global.CONN_URL +"users/registration_data?account="+account+"&password="+psw+"&token="+token+"&nickname=" + n_nickname);

		yield return hs_get;
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator all_missions(string id,string token, System.Action<string> onComplete)
	{
		WWW hs_get = new WWW(Global.CONN_URL +"user/"+id+"/all_missions?token="+token);
		yield return hs_get;
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator take_mission(string id,string token,int mission_id, System.Action<string> onComplete)
	{
		WWW hs_get = new WWW(Global.CONN_URL +"user/"+id+"/take_mission?mission_id="+mission_id+"&token="+token);
		yield return hs_get;
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator quit_mission(string id,string token,int mission_id, System.Action<string> onComplete)
	{
		WWW hs_get = new WWW(Global.CONN_URL +"user/"+id+"/quit_mission?mission_id="+mission_id+"&token="+token);
		yield return hs_get;
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator finish_mission(string id,string token,int mission_id, System.Action<string> onComplete)
	{
		WWW hs_get = new WWW(Global.CONN_URL +"user/"+id+"/finish_mission?mission_id="+mission_id+"&token="+token);
		yield return hs_get;
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator Urgent_info(System.Action<string> onComplete)
	{
		WWW hs_get = new WWW(Global.CONN_URL +"urgent/info?urgent_with_rank=true&with_boss_info=true");
		yield return hs_get;
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator add_damage(string id,string token,int damage,System.Action<string> onComplete)
	{
		WWW hs_get = new WWW(Global.CONN_URL +"urgent/user/"+id+"/add_damage?token="+token+"&value="+damage + "&with_rank=true");
		yield return hs_get;
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator get_damage(string id,string token,System.Action<string> onComplete)	{
		WWW hs_get = new WWW(Global.CONN_URL +"urgent/user/"+id+"/get_damage?token="+token+ "&with_rank=true");
		yield return hs_get;
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator urgent_exit(string id,string token,System.Action<string> onComplete)	{
		WWW hs_get = new WWW(Global.CONN_URL +"urgent/user/"+id+"/exit?token="+token);
		yield return hs_get;
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}
	
	public static IEnumerator equip_skill(string id,string token,string list,System.Action<string> onComplete)
	{
		WWW hs_get = new WWW(Global.CONN_URL +"user/"+id+"/set_equip_skill?token="+token+"&list="+list);
		yield return hs_get;
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator give_reward(string id,string token,int score_value,string skill_list,string item_pair_list,System.Action<string> onComplete)
	{
		System.Text.Encoding myEncoding = System.Text.Encoding.GetEncoding("utf-8");
		item_pair_list = WWW.EscapeURL (item_pair_list, myEncoding);

		WWW hs_get = new WWW(Global.CONN_URL +"user/"+id+"/give_reward?token="+token+"&score_value="+score_value+"&skill_list="+skill_list+"&item_pair_list="+item_pair_list+"&item_with_all_item=true");
		yield return hs_get;
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}

	public static IEnumerator general_rank(int id,System.Action<string> onComplete)
	{
		WWW hs_get = new WWW(Global.CONN_URL +"general/rank?query_uid="+id);
		yield return hs_get;
		if (hs_get == null || hs_get.error != null) {
			onComplete("Connention failed!!");
		}
		else
		{
			onComplete(hs_get.text);
		}
	}
}
