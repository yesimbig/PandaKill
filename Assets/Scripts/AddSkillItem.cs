using UnityEngine;
using System.Collections;

public class AddSkillItem : MonoBehaviour {

	public GameObject skillItemButton;
	public bool is_Urgent = false;

	GameObject item;			
	BattleSkillItem bsi;
	UrgentSkillItem usi;
	// Use this for initialization
	void Start () {

		if (!is_Urgent)
			for (int i = 0; i < UserStatementManager.equip_skill_id_list.Length && i < 4; i++) {
				Debug.Log (UserStatementManager.equip_skill_id_list [i]);
				item = Instantiate (skillItemButton) as GameObject;			
				item.transform.SetParent (this.transform);				
				bsi = (BattleSkillItem)(item.GetComponent<BattleSkillItem> ());			
				bsi.setId (UserStatementManager.equip_skill_id_list [i]); 
			}
		else {
			for (int i = 0; i < UserStatementManager.equip_skill_id_list.Length && i < 4; i++) {
				Debug.Log (UserStatementManager.equip_skill_id_list [i]);
				item = Instantiate (skillItemButton) as GameObject;			
				item.transform.SetParent (this.transform);				
				usi = (UrgentSkillItem)(item.GetComponent<UrgentSkillItem> ());			
				usi.setId (UserStatementManager.equip_skill_id_list [i]); 
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
