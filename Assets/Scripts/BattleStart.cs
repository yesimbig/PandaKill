using UnityEngine;
using System.Collections;

public class BattleStart : MonoBehaviour {

	public GameObject BattleManager, SkillBar,ExitButton;
	public Animator countdown;
	public GameObject Fight_fox;

	FoxSkinSetting fss;
	// Use this for initialization
	void Start () {
		BattleManager.SetActive (false);
		SkillBar.SetActive (false);
		ExitButton.SetActive (false);

		fss = Fight_fox.GetComponent<FoxSkinSetting> ();
		fss.set (UserStatementManager.eye, UserStatementManager.tail, UserStatementManager.color, UserStatementManager.wearing);
	}
	
	// Update is called once per frame
	void Update () {
		/*
		fss = Fight_fox.GetComponent<FoxSkinSetting> ();
		fss.set (UserStatementManager.eye, UserStatementManager.tail, UserStatementManager.color, UserStatementManager.wearing);
*/
		AnimatorStateInfo  info = countdown.GetCurrentAnimatorStateInfo(0);
		if(info.normalizedTime >= 1.0f)
		{
			countdown.SetTrigger("trigger");
			BattleManager.SetActive (true);
			SkillBar.SetActive (true);
			ExitButton.SetActive (true);
			gameObject.SetActive(false);
		}
	}
}
