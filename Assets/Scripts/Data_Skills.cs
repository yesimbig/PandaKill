using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Data_Skills : MonoBehaviour {

	public class Skills{
	
		public int type;			//技能形式(0直傷、1輔助、2附加效果追加傷害、3附加效果、4蓄力技)
		public int weapon;			//技能所適用的武器
		public string name;		//技能名稱
		public string description;	//技能描述
		public float cd;			//技能冷卻時間
		public float delay;  		//技能延遲時間
		public int damage;			//直傷技能的傷害
		public float sustain;		//輔助技能的持續時間
		public int prepose;		//前置技能的ID
		public float other;		//其他資訊

		public Skills(int t,int w,string n,string d,float de,float c,int dd,float s,int p,float o){
			type = t;
			weapon = w;
			name = n;
			delay = de;
			description = d;
			cd = c;
			damage = dd;
			sustain = s;
			prepose = p;
			other = o;
		}
	}
	
	public static List<Skills> Skills_list;
	
	public static void setSkills(){
		
		Skills_list = new List<Skills>();
				//--------------------------type weapon	name  		  description											delay	cd		damage	sustain	prepose	other
		/* 0 */	Skills_list.Add ( new Skills(0,	 0,		"普通攻擊",	  "熊貓出現了!快打啊!!!",									0f,		0.1f,	5,		0f,		0,		0f));
		/* 1 */ Skills_list.Add ( new Skills(0,	 0,		"重擊",		  "狐狸集中自己的力量使出重擊",								0.37f,	2f,		80,		0f,		0,		0f));
		/* 2 */ Skills_list.Add ( new Skills(2,	 0,		"致命攻擊",	  "狐狸找出敵人弱點使出攻擊，若目標有異常狀態則得兩倍傷害",	0.46f,	4f,		100,	0f,		0,		0f));
		/* 3 */ Skills_list.Add ( new Skills(3,	 0,		"詛咒",		  "狐狸詛咒目標，使目標陷入混亂5秒，每秒造成持續傷害",		0.5f,	5f,		60,		3f,		0,		0f));
		/* 4 */ Skills_list.Add ( new Skills(4,	 0,		"終極一擊",	  "狐狸蓄氣並且造成連續大量傷害",							0.1f,	5f,		100,	0f,		0,		8f));
		/* 5 */ Skills_list.Add ( new Skills(1,	 0,		"精神集中1","開啟15秒間，攻擊有20%機率造成爆擊",						0.3f,	30f,	0,		15f,	0,		0.20f));
		/* 6 */ Skills_list.Add ( new Skills(1,	 0,		"精神集中2","開啟15秒間，攻擊有33%機率造成爆擊",						0.3f,	20f,	0,		15f,	5,		0.33f));
		/* 7 */ Skills_list.Add ( new Skills(1,	 0,		"精神集中3","開啟15秒間，攻擊有50%機率造成爆擊",						0.3f,	20f,	0,		15f,	6,		0.50f));
		/* 8 */ Skills_list.Add ( new Skills(1,	 0,		"精神集中升級","開啟15秒間，攻擊有50%機率造成爆擊",						0.3f,	20f,	0,		15f,	6,		0.50f));
		Global.SKILL_NUM = Skills_list.Count;
	}

}
