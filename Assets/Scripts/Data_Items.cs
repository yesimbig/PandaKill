using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Data_Items : MonoBehaviour {
	public class Items{
		public int type;				//道具種類	0任務道具	1獎勵道具	2武器	3消耗品
		public string name;			//道具名稱
		public string description;		//道具描述

		public Items(int t,string n,string d){
			type = t;
			name = n;
			description = d;
		}
	}

	public static List<Items> Items_list;

	public static void setItems(){
		Items_list = new List<Items>();
		/*									type	name			description
		/* 0 */Items_list.Add ( new Items(	3,		"積分加成藥水",	"只能使用一次，使用後下次在一般戰鬥獲得積分變成1.2倍"));
		/* 1 */Items_list.Add ( new Items(	3,		"體力回復劑",		"只能使用一次，使用後可以將飽食度增加一點"));

		/* 2 */Items_list.Add ( new Items(	0,		"狐狸尾巴",		"象徵是狐狸的信物"));
		/* 3 */Items_list.Add ( new Items(	0,		"熊貓糞便",		"這不過是手下敗將而已，好臭"));
		/* 4 */Items_list.Add ( new Items(	1,		"威秀娃娃",		"一起跟我奮鬥梅竹賽的夥伴，夜烤出示可以抽獎"));

		/* 5 */Items_list.Add ( new Items(	0,		"全家圖示",		"象徵從全家搜刮到了食物"));
		/* 6 */Items_list.Add ( new Items(	0,		"奶廚圖示",		"象徵從奶廚搜刮到了食物"));
		/* 7 */Items_list.Add ( new Items(	0,		"麥當勞圖示",		"象徵從麥當勞搜刮到了食物"));
		/* 8 */Items_list.Add ( new Items(	0,		"小七圖示",		"象徵從小七搜刮到了食物"));
		/* 9 */Items_list.Add ( new Items(	0,		"萊爾富圖示",		"象徵從萊爾富搜刮到了食物"));
		/*10 */Items_list.Add ( new Items(	0,		"一餐圖示",		"象徵從一餐搜刮到了食物"));
		/*11 */Items_list.Add ( new Items(	0,		"二餐圖示",		"象徵從二餐搜刮到了食物"));
		/*12 */Items_list.Add ( new Items(	0,		"逐風圖示",		"象徵從(很閃的)逐風廣場搜刮到了食物"));

		/*13 */Items_list.Add ( new Items(	1,		"小熊貓的屍體",	"獎勵你虐殺小熊貓不眨眼"));

		/*14 */Items_list.Add ( new Items(	0,		"查詢能力",		"為了修練自己，從浩然圖書館學習到了查詢能力"));
		/*15 */Items_list.Add ( new Items(	0,		"程式邏輯",		"為了修練自己，在工三學到了寫程式的能力，聽說工具人都要會修電腦，呵呵"));
		/*16 */Items_list.Add ( new Items(	0,		"電路頭腦",		"為了修練自己，在工四把自己的頭腦變成電路了QQ"));
		/*17 */Items_list.Add ( new Items(	0,		"機械修理",		"為了修練自己，在工五學習到了機械修理的能力啦"));
		/*18 */Items_list.Add ( new Items(	0,		"生物知識",		"為了修練自己，在工六學習到了生物相關的知識"));
		/*19 */Items_list.Add ( new Items(	0,		"藝文賞析",		"為了修練自己，在藝文空間解放了文青的自我"));
		/*20 */Items_list.Add ( new Items(	0,		"運動體魄",		"為了修練自己，在游泳館看著別人健身游泳，自己也忍不住激發出了運動魂"));
		/*21 */Items_list.Add ( new Items(	0,		"文書處理",		"為了修練自己，在計中得到了文書處理的能力"));

		/*22 */Items_list.Add ( new Items(	0,		"沙茶滷味",		"真好吃!真好吃!!"));
		/*23 */Items_list.Add ( new Items(	0,		"絕代雙Q奶茶",	"真好吃!真好吃!!"));
		/*24 */Items_list.Add ( new Items(	0,		"湯麵",			"真好吃!真好吃!!"));
		/*25 */Items_list.Add ( new Items(	0,		"薯條",			"真好吃!真好吃!!"));
		/*26 */Items_list.Add ( new Items(	0,		"麵包",			"真好吃!真好吃!!"));
		/*27 */Items_list.Add ( new Items(	0,		"咖哩飯",		"真好吃!真好吃!!"));
		/*28 */Items_list.Add ( new Items(	0,		"叉燒飯",		"真好吃!真好吃!!"));
		/*29 */Items_list.Add ( new Items(	0,		"泡麵",			"真好吃!真好吃!!"));
		/*30 */Items_list.Add ( new Items(	0,		"韭菜水餃",		"真好吃!真好吃!!"));
		/*31 */Items_list.Add ( new Items(	0,		"乾麵",			"真好吃!真好吃!!"));
		/*32 */Items_list.Add ( new Items(	0,		"大熱美",		"真好吃!真好吃!!"));
		/*33 */Items_list.Add ( new Items(	0,		"韓式辣味鍋貼",	"真好吃!真好吃!!"));
		/*34 */Items_list.Add ( new Items(	0,		"烏龍奶",		"真好吃!真好吃!!"));
		/*35 */Items_list.Add ( new Items(	0,		"雞腿飯",		"真好吃!真好吃!!"));
		/*36 */Items_list.Add ( new Items(	0,		"排骨飯",		"真好吃!真好吃!!"));
		/*37 */Items_list.Add ( new Items(	0,		"滷味",			"真好吃!真好吃!!"));
		/*38 */Items_list.Add ( new Items(	0,		"夯番薯",		"真好吃!真好吃!!"));
		/*39 */Items_list.Add ( new Items(	0,		"青醬義大利麵",	"真好吃!真好吃!!"));
		/*40 */Items_list.Add ( new Items(	0,		"小木屋鬆餅",		"真好吃!真好吃!!"));

		/*41 */Items_list.Add ( new Items(	1,		"宋少卿海報",		"恭喜你答對了所有的梅竹名人演講題目!這個海報就送給你ㄅ"));

		/*42 */Items_list.Add ( new Items(	0,		"密碼學",		"讀得懂密碼學，讀不懂女人的心阿"));
		/*43 */Items_list.Add ( new Items(	0,		"電磁學",		"雖然讀了電磁學，還是不懂怎麼跟女孩來電"));
		/*44 */Items_list.Add ( new Items(	0,		"熱傳學",		"將自己的熱情與魅力傳到世界各地~吧!"));
		/*45 */Items_list.Add ( new Items(	0,		"分子生物學",		"世界上最難懂的生物就是女人了，這本有沒有教阿?"));
		/*46 */Items_list.Add ( new Items(	0,		"運輸規劃",		"這本是...工具人指南嗎?"));
		/*47 */Items_list.Add ( new Items(	0,		"離散數學",		"嗚嗚嗚我跟她只能永遠離散了...(觸景傷情崩潰中)"));
		/*48 */Items_list.Add ( new Items(	0,		"微積分",		"危機危機，公雞公雞(好啦不好笑)"));
		/*49 */Items_list.Add ( new Items(	0,		"物理",			"這是一個蘋果撞到頭的故事"));


		/*50 */Items_list.Add ( new Items(	0,		"強化裝備的碎片1","參加梅竹電競活動所得到的強化裝備碎片，蒐集四片就能完成囉"));
		/*51 */Items_list.Add ( new Items(	0,		"強化裝備的碎片2","參加梅竹電競活動所得到的強化裝備碎片，蒐集四片就能完成囉"));
		/*52 */Items_list.Add ( new Items(	0,		"強化裝備的碎片3","參加梅竹電競活動所得到的強化裝備碎片，蒐集四片就能完成囉"));
		/*53 */Items_list.Add ( new Items(	0,		"強化裝備的碎片4","參加梅竹電競活動所得到的強化裝備碎片，蒐集四片就能完成囉"));
		/*54 */Items_list.Add ( new Items(	1,		"終極強化裝備",	"參加梅竹電競活動，得到終極強化裝備!攻擊力變成原來的兩倍!!"));

		/*55 */Items_list.Add ( new Items(	2,		"大眼睛",		"晶瑩剔透、大大的眼睛"));
		/*56 */Items_list.Add ( new Items(	2,		"點點眼",		"小巧可愛的眼睛"));
		/*57 */Items_list.Add ( new Items(	2,		"墨鏡",			"誰不會迷上拉風帥氣的墨鏡呢?"));
		/*58 */Items_list.Add ( new Items(	2,		"霸氣眼",		"銳利的眼神似乎能夠看穿一切事物"));

		/*59 */Items_list.Add ( new Items(	2,		"狐狸尾巴",		"象徵活潑活力的狐狸尾巴"));
		/*60 */Items_list.Add ( new Items(	2,		"火焰尾巴",		"最新的潮流尾巴造型"));
		/*61 */Items_list.Add ( new Items(	2,		"九尾",			"深不可測、傳說中的九尾"));
		/*62 */Items_list.Add ( new Items(	2,		"松鼠尾巴",		"超萌的松鼠尾巴"));

		/*63 */Items_list.Add ( new Items(	2,		"紅領巾",		"熱情奔放的紅領巾"));
		/*64 */Items_list.Add ( new Items(	2,		"紅圍巾",		"冬季保暖的紅圍巾"));
		/*65 */Items_list.Add ( new Items(	2,		"紅吊帶褲",		"可愛俏皮的紅吊帶褲"));
		/*66 */Items_list.Add ( new Items(	2,		"紅蝴蝶結",		"萌萌的紅蝴蝶結"));

		/*67 */Items_list.Add ( new Items(	2,		"黃領巾",		"熱情奔放的黃領巾"));
		/*68 */Items_list.Add ( new Items(	2,		"黃圍巾",		"冬季保暖的黃圍巾"));
		/*69 */Items_list.Add ( new Items(	2,		"黃吊帶褲",		"可愛俏皮的黃吊帶褲"));
		/*70 */Items_list.Add ( new Items(	2,		"黃蝴蝶結",		"萌萌的黃蝴蝶結"));

		/*71 */Items_list.Add ( new Items(	2,		"綠領巾",		"熱情奔放的綠領巾"));
		/*72 */Items_list.Add ( new Items(	2,		"綠圍巾",		"冬季保暖的綠圍巾"));
		/*73 */Items_list.Add ( new Items(	2,		"綠吊帶褲",		"可愛俏皮的綠吊帶褲"));
		/*74 */Items_list.Add ( new Items(	2,		"綠蝴蝶結",		"萌萌的綠蝴蝶結"));

		/*75 */Items_list.Add ( new Items(	2,		"藍領巾",		"熱情奔放的藍領巾"));
		/*76 */Items_list.Add ( new Items(	2,		"藍圍巾",		"冬季保暖的藍圍巾"));
		/*77 */Items_list.Add ( new Items(	2,		"藍吊帶褲",		"可愛俏皮的藍吊帶褲"));
		/*78 */Items_list.Add ( new Items(	2,		"藍蝴蝶結",		"萌萌的藍蝴蝶結"));

		/*79 */Items_list.Add ( new Items(	1,		"開幕邀請函",	"各位~梅竹開幕時間就在2/17!大家記得要去參加喔!"));
		/*80 */Items_list.Add ( new Items(	2,		"隨機造型",		"可以隨機拿到眼睛、尾巴、服裝造型之一"));

		Global.ITEM_NUM = Items_list.Count;
	}


}
