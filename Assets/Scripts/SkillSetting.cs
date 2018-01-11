using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillSetting : MonoBehaviour {

	public GameObject Icons;
	public Image icon;
	Sprite[] skills;
	//public Sprite[] used;
	public GameObject used,shine;
	public Text skill_name;
	int myid;
	bool is_equip;


	void Update(){
		if (allSkillSetting.now_id == myid) {
			shine.SetActive (true);
			skill_name.color = new Color32(255,155,155,255);
		} else {
			shine.SetActive (false);
			skill_name.color = Color.white;
		}
	}

	public void setSkill(int id,bool u){
		Icons icons = Icons.GetComponent<Icons> ();
		skills = icons.skills;

		if(id < skills.Length)
			icon.sprite = skills[id];
		this.transform.localPosition = new Vector2(0,0);
		this.transform.localScale = Vector3.one;            
		
		RectTransform rt = this.GetComponent<RectTransform> ();
		rt.anchoredPosition = new Vector2(0,0);
		myid = id;
		is_equip = u;
		used.SetActive (u);
		skill_name.text = Data_Skills.Skills_list [id].name;

		//init
		if (myid == UserStatementManager.skill_id_list [0])
			onClick ();
	}

	public void changeUsed(bool u){
		is_equip = u;
		used.SetActive (u);
	}

	public void onClick(){
		Debug.Log (myid);
		allSkillSetting.now_id = myid;
		allSkillSetting.go = gameObject;
		allSkillSetting.is_equip = is_equip;
	}
}
