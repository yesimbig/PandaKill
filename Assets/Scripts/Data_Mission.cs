using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Data_Mission : MonoBehaviour {

	public class Missions{
		public int server_id;
		public string name;			//mission名稱
		public string description,finish;		//mission描述
		public List<string> assign, message;  //接取任務時的劇情、任務訊息
		public int state;	//任務進度
		public int score;
		public int[] skills;
		public int[] item;

		public Missions(int s,string n,string d,string f,List<string> a,List<string> m){
			server_id = s;
			name = n;
			description = d;
			finish = f;
			assign = a;
			message = m;
			state = 0;
		}
	}
	
	public static List<Missions> Missions_list;
	
	public static void setMissions(){
		Missions_list = new List<Missions>();

		//--------------------- set mission 緊急任務：尋找同伴--------------------------------------------------
		List<string> a1 = new List<string>(){
			"哈囉朋友~最近過得怎樣啊?",
			"你知道一年一度的梅竹開幕遊行已經開始了嗎^ ^",
			"是的!沒錯就在2/17!!今天!!",
			"快點揪我的同伴們一起去踏青遊行~拜託嘛",
			"首先，遊行的時候請先去女二逐風廣場找布偶，使用左下方的相機掃布偶的圖示吧!"};
		List<string> s1 = new List<string> (){
			"遊行的時候請先去女二逐風廣場找布偶，使用左下方的相機掃布偶的圖示",
			"找到狐狸的信物了，接著去工五尋找其它夥伴吧!",
			"好噁心喔，居然是熊貓的大便!快去科三尋找最後一個夥伴吧~~"
		};
		Missions_list.Add ( new Missions(	1,"緊急任務：尋找同伴","梅竹開幕遊行進行中，快來活動中尋找竹狐的同伴們一起玩樂吧，享受開幕遊行的熱鬧，一起加入遊行大隊伍，嗆翻清大吧！","找到了與我一起奮鬥梅竹賽的夥伴了，今年一定要電翻清大阿！",a1,s1));
		Missions_list[0].score = 6000;
		Missions_list [0].skills = new int[]{8};
		Missions_list [0].item = new int[]{1,4};


		//---------------------set mission 緊急任務：怒烤青蛙---------------------------------------------------
		List<string> a2 = new List<string> (){
			"下午參加了遊行活動好疲累喔",
			"現在餓到頭昏眼花了呢!有沒有什麼好料的可以填飽肚子呢~~",
			"阿!!對了!!",
			"今天有梅竹夜烤大會呢",
			"聽說有好吃好玩好看的活動!!我絕對不要錯過了",
			"我要快點到體育館前集合了",
			"去看看有什麼好料的食物可以嘗嘗囉~~"};
		List<string> s2 = new List<string> (){
			"使用左下方的相機掃夜烤活動的圖示，來選擇出今晚夜烤正確的食材吧",
			"這些就是我正在吃的食物阿!!你做得非常好!!"};
		
		Missions_list.Add ( new Missions(	2,"緊急任務：怒烤青蛙","梅竹夜烤活動進行中，快來參加活動，一起享受美食、欣賞表演，一起為出戰梅竹賽的選手們鼓舞加油吧！！","選手們都吃得飽飽的了，接下來的訓練以及比賽，絕對是完全沒有問題的！",a2,s2));
		Missions_list[1].score = 3000;
		Missions_list [1].skills = new int[]{3};
		Missions_list [1].item = new int[]{80};


		//---------------------set mission 搜刮食物---------------------------------------------------
		List<string> a3 = new List<string> (){
			"好的一天開始，我也要來覓食了",
			"交通大學裡面有不錯的店面可以參考參考",
			"等等就先去校內的各餐廳先看看他們有甚麼好吃的吧~~~"
		};
		List<string> s3 = new List<string> (){
			"請到十三舍全家、奶廚、麥當勞、萊爾富、小七、一餐、二餐、女二尋找食物吧!(0/3)",
			"請到十三舍全家、奶廚、麥當勞、萊爾富、小七、一餐、二餐、女二尋找食物吧!(1/3)",
			"請到十三舍全家、奶廚、麥當勞、萊爾富、小七、一餐、二餐、女二尋找食物吧!(2/3)"
		};
		
		Missions_list.Add ( new Missions(	3,"搜刮食物","經過了一天，人類似乎都不會注意到我…\n"+
		                                 "對於昨天夜烤還意猶未盡…香氣四溢的烤肉，在烤肉架上滋滋作響、烙上美麗的烙紋，最後再配上令人暢快的啤酒，啊…一大享受…\n"+
		                                 "想著想著肚子有點餓了…人家都說吃飽才能做大事！來去找找交大校園內有什麼好吃的食物！聽說學校裡有琳瑯滿目的餐點可以選擇耶！",
		                                 "在一個校園裡就可以滿足很多種需求耶…都不必像以前還要到處尋找食物！(擦嘴)",a3,s3));
		Missions_list[2].score = 4000;
		Missions_list [2].skills = new int[]{};
		Missions_list [2].item = new int[]{0};

		//---------------------set mission 捕捉出生小熊貓---------------------------------------------------
		List<string> a4 = new List<string> (){
			"今天本來是個可以好好休息的一天",
			"聽到消息說，咱們學校跟對面的交界處有許多小熊貓要出生了",
			"接下來我必須要當起英雄",
			"去處理掉那邊正在繁殖的怪物們了!"};
		List<string> s4 = new List<string> (){
			"前往兩校的交界處尋找標誌，許許多多小熊貓正在吵鬧，快前去處理",
			"處理掉所有繁殖的熊貓了，終於先解決掉一群小怪獸們了~"
		};
		
		Missions_list.Add ( new Missions(	4,"捕捉出生小熊貓","今天看來不是一個和平的一天阿，有許多對面的野獸正在生長，必須趁著他們還沒茁壯之前，要迅速處理掉他們以防萬一阿!!","順利處理掉手下敗將的小熊貓們，這下子校園可以安心了",a4,s4));
		Missions_list[3].score = 4000;
		Missions_list [3].skills = new int[]{};
		Missions_list [3].item = new int[]{0,13};


		//---------------------set mission 潛心修練 蒐集東西---------------------------------------------------
		List<string> a5 = new List<string> (){
			"假日不可以偷懶，還是要繼續增強自己的實力",
			"趁著這個周末好好鍛鍊一下自己的身體",
			"那麼接下來的比賽就可以不用擔心了!",
			"有聽狐狸長老說，各個系館都有不同的知識能夠自我修煉",
			"那麼就聽從狐狸長老的建議，去修練自己的身心吧!!"};
		List<string> s5 = new List<string> (){
			"前往浩然、工三、工四、工五、工六、藝文空間、游泳館、計中修練身心吧!(0/5)",
			"前往浩然、工三、工四、工五、工六、藝文空間、游泳館、計中修練身心吧!(1/5)",
			"前往浩然、工三、工四、工五、工六、藝文空間、游泳館、計中修練身心吧!(2/5)",
			"前往浩然、工三、工四、工五、工六、藝文空間、游泳館、計中修練身心吧!(3/5)",
			"前往浩然、工三、工四、工五、工六、藝文空間、游泳館、計中修練身心吧!(4/5)",
			"接著前往活中去展現自己的訓練成果吧!(5/5)",
			"完成修練了!現在我的實力已經足以應付梅竹賽的任何比賽了!!"};
		
		Missions_list.Add ( new Missions(	5,"潛心修練 蒐集東西","周末就是個要好好安排的，看我趁這個時間好好訓練自己，讓自己有強健的體魄可以上陣梅竹賽!!","現在我的實力可是非常厲害，2/26~2/28等著看我秀翻全場吧!!",a5,s5));

		Missions_list[4].score = 5000;
		Missions_list [4].skills = new int[]{8};
		Missions_list [4].item = new int[]{1};

		//---------------------set mission 吃好吃滿---------------------------------------------------
		List<string> a6 = new List<string> (){
			"在校園裡待一段時間了，也交到一些狐狸朋友，雖然牠們都待得比我久，不過好像都還是遵循古法，從來沒有品嘗過美味得人類食物",
			"現在我要去各個餐廳裡面尋找真正好吃的食物了",
			"帶著我一起去吧~~~"};
		List<string> s6 = new List<string> (){
			"請去學校一餐、二餐及女二的商家或便利商店餵我吧!(0/10)",
			"請去學校一餐、二餐及女二的商家或便利商店餵我吧!(1/10)",
			"請去學校一餐、二餐及女二的商家或便利商店餵我吧!(2/10)",
			"請去學校一餐、二餐及女二的商家或便利商店餵我吧!(3/10)",
			"請去學校一餐、二餐及女二的商家或便利商店餵我吧!(4/10)",
			"請去學校一餐、二餐及女二的商家或便利商店餵我吧!(5/10)",
			"請去學校一餐、二餐及女二的商家或便利商店餵我吧!(6/10)",
			"請去學校一餐、二餐及女二的商家或便利商店餵我吧!(7/10)",
			"請去學校一餐、二餐及女二的商家或便利商店餵我吧!(8/10)",
			"請去學校一餐、二餐及女二的商家或便利商店餵我吧!(9/10)"
		};
		
		Missions_list.Add ( new Missions(	6,"吃好吃滿","長老說最近因為熊貓偷偷入侵村莊作亂，導致許多辛苦照顧的作物都泡湯了，就讓我來幫助他們吧！去店家找一些食物，並且拿去給他們！	聽長老說，村民中有人擁有一股神祕的力量，是不是蒐集越多越好呢？他會不會教我蓋世武功？讓我成為打敗熊貓大魔王的大英雄呢？","謝謝你，我正好在煩惱呢！…真好吃！",a6,s6));
		Missions_list[5].score = 5000;
		Missions_list [5].skills = new int[]{4};
		Missions_list [5].item = new int[]{0,1,80};


		//---------------------set mission 緊急任務：回答問題---------------------------------------------------
		List<string> a7 = new List<string> (){
			"今天晚上有個大咖要來我們學校演講欸，想去聽~",
			"你知道關於今天的演講的事情嗎?",
			"快點來做做有獎徵答來拿獎勵吧!!"
			};
		List<string> s7 = new List<string> (){
			"快點去浩然的國際會議廳參加名人演講回答問題吧!"
		};
		
		Missions_list.Add ( new Missions(	7,"緊急任務：回答問題","在浩然圖書館b1國際會議廳，正有名人受邀來交通大學演講，快來參予這次的演講，吸收新知，開拓視野吧~~~","全部都回答正確了，你有認真聽演講喔，非常棒呢~~",a7,s7));

		Missions_list[6].score = 6000;
		Missions_list [6].skills = new int[]{8};
		Missions_list [6].item = new int[]{1,41};

		//---------------------set mission 蒐集資料---------------------------------------------------
		List<string> a8 = new List<string> (){
			"學海無涯，我想進修更多的課程",
			"新的一個學期就是要好好吸收新的知識",
			"前往每個系館吸收他們新的學問",
			"充實自己的內在!!"};
		List<string> s8 = new List<string> (){
			"前往工三、工四、工五、工六、綜一、科三、科二、科一學習課程吧!(0/4)",
			"前往工三、工四、工五、工六、綜一、科三、科二、科一學習課程吧!(1/4)",
			"前往工三、工四、工五、工六、綜一、科三、科二、科一學習課程吧!(2/4)",
			"前往工三、工四、工五、工六、綜一、科三、科二、科一學習課程吧!(3/4)",
			"都學到了很多的學問了，這下子我腦袋可變得非常聰明了呢~"};
		
		Missions_list.Add ( new Missions(	8,"蒐集資料","前往每個系館蒐集他們的必修資料，來讓竹狐更聰明，充實過的竹狐，相信戰鬥力會滿滿的，輕鬆解決掉我們的死對頭","學習到好多專業課程唷，這下子我的實力一下子升到最高點了，看來要扳倒熊貓只是個小事情而已阿~~~",a8,s8));

		Missions_list[7].score = 4000;
		Missions_list [7].skills = new int[]{};
		Missions_list [7].item = new int[]{0};

		//---------------------set mission 緊急任務：肥仔熊貓最終戰---------------------------------------------------
		List<string> a9 = new List<string> (){
			"第一波的梅竹賽已經開打啦，快到浩然前廣場集合集氣吧",
			"梅竹電競率先開打，快加入加油的行列",
			"一起為交大吶喊吧~~~",
			"奪下重要的首勝!!"};
		List<string> s9 = new List<string> (){
			"蒐集贊助商的標誌，來為交大一方集氣加油!(0/4)",
			"蒐集贊助商的標誌，來為交大一方集氣加油!(1/4)",
			"蒐集贊助商的標誌，來為交大一方集氣加油!(2/4)",
			"蒐集贊助商的標誌，來為交大一方集氣加油!(3/4)",
			"蒐集到所有廠商的加持，目前竹狐的所有攻擊技能傷害兩倍阿!!!快快把握這難得的機會"};
		
		Missions_list.Add ( new Missions(	9,"緊急任務：肥仔熊貓最終戰","丙申梅竹電競賽已經開打，快快召集身邊所有的室友、好友、男友、女友、基友們一起來浩然前廣場提升士氣吧，每有一個人參加，交大選手們就會更用心打敗對手們!!!來掃得現場的贊助廠商，可以讓竹狐更具有威力唷","終於集完了大家的支持，未來的梅竹賽肯定也能勢如破竹的!!",a9,s9));
		Missions_list[8].score = 4000;
		Missions_list [8].skills = new int[]{};
		Missions_list [8].item = new int[]{1};

		Global.MISSION_NUM = Missions_list.Count;

		setStates ();
	}

	public static void setStates(){
		//--------mission1 states-------------------------
		//set state
		if (!ItemManager.haveItemId (2) && !ItemManager.haveItemId (3) && !ItemManager.haveItemId (4)) {
			Data_Mission.Missions_list [0].state = 0;
		} else if (ItemManager.haveItemId (2) && !ItemManager.haveItemId (3) && !ItemManager.haveItemId (4)) {
			Data_Mission.Missions_list [0].state = 1;
		}else if (ItemManager.haveItemId (3) && !ItemManager.haveItemId (4)) {
			Data_Mission.Missions_list [0].state = 2;
		}
		//------------------------------------------------

		Data_Mission.Missions_list [1].state = 0;

		//------mission3 states---------------------------
		Data_Mission.Missions_list [2].state = 0;
		
		for (int i=5; i<=12; i++) {
			if(ItemManager.haveItemId(i)) Data_Mission.Missions_list [2].state++;
			if (Data_Mission.Missions_list [2].state == 2)
				break;
		}
		//-----------------------------------------------

		Data_Mission.Missions_list [3].state = 0;

		//-----mission5 states--------------------------
		Data_Mission.Missions_list [4].state = 0;
		for (int i=14; i<=21; i++) {
			if(ItemManager.haveItemId(i)) Data_Mission.Missions_list [4].state++;
			if(Data_Mission.Missions_list [4].state>=5)break;
		}
		//----------------------------------------------

		//-----mission6 states--------------------------
		Data_Mission.Missions_list [5].state = 0;
		for (int i=22; i<=40; i++) {
			if(ItemManager.haveItemId(i)) Data_Mission.Missions_list [5].state++;
			if(Data_Mission.Missions_list [5].state>9)break;
		}
		//-----------------------------------------------

		//---------mission8 states--------------------------
		Data_Mission.Missions_list [7].state = 0;
		for (int i=42; i<=49; i++) {
			if(ItemManager.haveItemId(i)) Data_Mission.Missions_list [7].state++;
			if(Data_Mission.Missions_list [7].state>3)break;
		}
		//--------------------------------------------------

		//---------mission9 states-------------------------
		Data_Mission.Missions_list [8].state = PlayerPrefs.GetInt("ESport");

	}

}
