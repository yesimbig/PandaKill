using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FoxAction : MonoBehaviour {

	public class FoxActionList{
		public int type;
		public int action;
		public float delay;
		public string dialog;

		public FoxActionList(int t,int a,float f,string d){
			type = t;
			action = a;
			delay = f;
			dialog = d;
		}
	
	}

	public Text LbubbleText,SbubbleText;
	public GameObject large_bubble,small_bubble;

	public GameObject TheFox;
	public Animator Fox_ani;

	public bool RandomMove;

	const int STATE_NORMAL = 0;
	const int STATE_EAT = 1;
	const int STATE_SLEEP = 2;

	const int TYPE_THINKING = 0;
	const int TYPE_TALKING = 1;

	const int ACTION_IDLE = -1;
	const int ACTION_ONHIT = 0;
	const int ACTION_JUMP = 1;
	const int ACTION_NOD = 2;
	const int ACTION_SAYHI = 3;
	const int ACTION_WALK = 4;
	const int ACTION_SHOWLUO = 99;

	const int EAT_IDLE = -1;
	const int EAT_ONHIT = 0;

	const int SLEEP_IDLE = -1;
	const int SLEEP_ONHIT = 0;

	float walk_delta_time = 20f;
	float time = 0f;

	List<FoxActionList> fox_action_list = new List<FoxActionList>();
	List<FoxActionList> fox_eat_list = new List<FoxActionList>();
	List<FoxActionList> fox_sleep_list = new List<FoxActionList>();
	bool is_action,is_walk;

	// Use this for initialization
	void Start () {
		is_walk = false;
		is_action = false;
		time = 0f;

		/*											type			action			delay		dialog			*/
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_SAYHI,	3.3f, 		"哈囉~"+UserStatementManager.nickname+"好啊~~真是個美好的一天!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_JUMP,	2.6f, 		"梅竹成功!交大必勝!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_THINKING,	ACTION_NOD,		2.0f, 		"卓大又高又帥怎麼沒女朋友呢?" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_ONHIT,	2.6f, 		"我是隻可愛的狐狸~" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_THINKING,	ACTION_IDLE,	3f, 		"聽說清大每六十秒，就有一分鐘過去..." ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_JUMP,	2.6f, 		"一年一度的梅竹賽就要開打了耶~我好興奮!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_JUMP,	2.6f, 		"TAIWAN NO.1!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_THINKING,	ACTION_NOD,		2.3f, 		"咦~是正妹嗎?" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_SAYHI,	3f, 		"你知道竹狐是代表交大的吉祥物嗎?" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_IDLE,	3f, 		"你知道該死的熊貓就是代表清大的吉祥物嗎?" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_THINKING,	ACTION_IDLE,	2.6f, 		"熊貓必須死!!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_THINKING,	ACTION_JUMP,	2.3f, 		"不要再點我了RRRRRR" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_NOD,		2.6f, 		"只要認真賺積分，就能領取很好的獎勵喔!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_SHOWLUO,	2.6f, 		"偷偷跟你說喔!我最欣賞亞洲空幹王惹!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_JUMP,	2.6f, 		"來嘛來嘛!我們來玩~" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_IDLE,	2.6f, 		"我...才沒有喜歡你呢!哼!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_SAYHI,	3f, 		"你好阿~旅行者" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_THINKING,	ACTION_WALK,	4.5f, 		"正在思考天地萬物間的運轉..." ));
		fox_action_list.Add ( new FoxActionList (	TYPE_THINKING,	ACTION_WALK,	4.5f, 		"熊貓翻了身，就是死掉的熊貓!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_NOD,		2.3f, 		"ˊ(.____.)ˋ" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_JUMP,	2.3f, 		"這就是Rock and Roll!!!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_ONHIT,	2.3f, 		"我的心中只有妳~" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_THINKING,	ACTION_ONHIT,	2.3f, 		UserStatementManager.nickname+"!!!不要再看我了辣" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_JUMP,	2.3f, 		"你在大聲什麼辣!!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_SAYHI,	3f, 		"HELLO!"+UserStatementManager.nickname+"你好嗎?" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_JUMP,	2.3f, 		"買布雷一修死" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_JUMP,	2.3f, 		"為了末日槌!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_THINKING,	ACTION_NOD,		2.6f, 		"算什麼男人~算什麼男人~" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_NOD,		2.6f, 		"梅竹活動棒棒棒~" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_ONHIT,	2.5f, 		"討厭~不要這麼用力地盯著我看啦QQ!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_JUMP,	2.3f, 		"你再點我我要生氣氣囉!!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_NOD,		3.0f, 		"哪天在路上碰到熊貓，我一定要把牠揍出大大的黑眼圈!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_IDLE,	3.0f, 		"聽說梅竹賽不叫竹梅賽，是丟銅板決定的喔!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_ONHIT,	2.3f, 		"嘻嘻嘻，你搔得我好癢喔" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_THINKING,	ACTION_WALK,	4.5f, 		"漫步在通往成功的道路上..." ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_ONHIT,	2.3f, 		"哎呦討厭啦~" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_SAYHI,	3f, 		"每天都有任務可以去解喔~有機會能拿到新造型呢!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_NOD,		2.6f, 		"梅竹賽只有兩種可能: 交大贏或清大輸!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_JUMP,	2.3f, 		"該死的熊貓!你以為你是保育類就殺不了你嗎?" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_IDLE,	2.3f, 		"注意上面的時鐘!請務必結伴討伐大熊貓!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_JUMP,	2.3f, 		"交大！屌！！\n清小！鳥！！" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_ONHIT,	2.3f, 		"清兵入棺！\n清賤口香糖！！" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_ONHIT,	2.3f, 		"取締清華！\n交通順暢！" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_SAYHI,	3f, 		"你在看我嗎?你可以再靠近一點~" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_JUMP,	2.3f, 		"該死的熊貓!沒竹子你是不是就活不下去了?" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_NOD,		2.6f, 		"聽說一般戰鬥有機率能獲取新的造型喔~" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_SAYHI,	2.6f, 		"財神到!登等登等登!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_NOD,		2.6f, 		"這是一隻熊貓，我搓了一下就變成死掉的熊貓!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_JUMP,	2.3f, 		"嚇嚇哈兮！快使用雙節棍!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_NOD,		2.6f, 		"哇a麻吉底對?底加底加!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_IDLE,	2.3f, 		"你去排梅竹票了嗎?" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_THINKING,	ACTION_IDLE,	2.3f, 		"我才不會說我最喜歡"+UserStatementManager.nickname+"了呢!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_WALK,	4.5f, 		"看我走路姿勢多優雅~" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_ONHIT,	3f, 		"哈哈!2月26號下午一點梅竹賽就要在清大田徑場開幕啦!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_ONHIT,	3f, 		"2/26,19:30，羽球正式賽@清大校友體育館" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_NOD,		3f, 		"2/27,28號早上九點，在清大蒙民偉樓有橋藝正式賽喔!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_NOD,		3f, 		"2/27,9:30，棋藝正式賽會在交大活中二樓PK~" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_SAYHI,	3f, 		"2/27,13:00在綜合球館有網球正式賽!" ));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_SAYHI,	3f, 		"2/27,18:00，交大體育館將進行女籃男籃的正式決鬥!"));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_NOD,		2.6f, 		"2/28,22:30，梅竹閉幕@清大體育館~"));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_JUMP,	3f, 		"女排男排的正式決鬥將在2/28晚上18:00於清大體育館進行!"));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_NOD,		3f, 		"2/28,13:00@交大棒球場，壘球表演賽~"));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_IDLE,	3f, 		"2/27,10:00@交大綜合球館，女網表演賽~"));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_NOD,		3f, 		"2/27,9:30@交大活中四樓，圍棋表演賽~"));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_ONHIT,	3f, 		"2/26,14:40@清大田徑場，田徑表演賽~"));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_ONHIT,	3f, 		"2/26,16:30@交大體育館，劍道表演賽~"));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_ONHIT,	3f, 		"桌球正式賽在2/26下午五點半，清大體育館進行喔喔喔"));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_JUMP,	2.3f, 		"一起為交大加油吧！加油！"));
		fox_action_list.Add ( new FoxActionList (	TYPE_THINKING,	ACTION_WALK,	4.5f, 		"沒參加過梅竹賽也看過梅竹賽走路吧?"));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_IDLE,	3f, 		"嘻嘻嘻，梅竹開幕遊行就在2/17喔，一起嗆翻清大吧"));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_ONHIT,	3f, 		"2/17晚上～梅竹夜烤，來跟朋友們體驗一下梅竹氣氛吧"));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_JUMP,	3f, 		"什麼？宋少卿要來交大？就在2/23名人演講!"));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_JUMP,	3f, 		"最熱血的梅竹電競賽就在2/24晚上，浩然廣場進行！"));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_NOD,		2.6f, 		"玩遊戲時不仿把聲音打開吧，音樂很好聽喔!"));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_IDLE,	2.6f, 		"每天過了十二點，飽食度就會自動補滿喔XD"));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_ONHIT,	2.3f, 		"哇啦拉拉，我是隻口愛的狐狸~"));
		fox_action_list.Add ( new FoxActionList (	TYPE_TALKING,	ACTION_IDLE,	2.3f, 		"你再點我我就要不高興了辣QQ"));

		
		//----------------------------------------------------------

		fox_eat_list.Add ( new FoxActionList (	TYPE_TALKING,	EAT_ONHIT,	1f,	"好吃!" ));
		fox_eat_list.Add ( new FoxActionList (	TYPE_TALKING,	EAT_ONHIT,	1f,	"齁甲!" ));
		fox_eat_list.Add ( new FoxActionList (	TYPE_TALKING,	EAT_ONHIT,	1f,	"潮好吃der!" ));
		fox_eat_list.Add ( new FoxActionList (	TYPE_TALKING,	EAT_ONHIT,	1f,	"好美味阿!" ));
		fox_eat_list.Add ( new FoxActionList (	TYPE_TALKING,	EAT_ONHIT,	1f,	"你有事嗎?" ));
		fox_eat_list.Add ( new FoxActionList (	TYPE_TALKING,	EAT_ONHIT,	1f,	"讚!" ));
		fox_eat_list.Add ( new FoxActionList (	TYPE_TALKING,	EAT_ONHIT,	1f,	"歐伊系" ));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "不給你吃"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "再點我阿"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "好吃好吃"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "爽!"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "真的好好吃!"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "齁加!齁加!"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "再來一碗!"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "DERRRRRR!"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "阿姆阿姆阿姆阿姆"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "哈哈哈"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "XD"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "哇哈哈哈哈...咳咳咳"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, UserStatementManager.nickname+"你要吃嗎?才不給你~"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "真是豐盛的麵~"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "我怎麼吃都不會胖~羨慕我吧"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "看我吃麵樣子多豪氣~"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "宋!"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "咳咳讓我專心吃麵!"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "你害我噎到了辣QQ"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "XDDD"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "嘴巴張開開~火車過山洞囉~好吃~"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "一直吃一直吃"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, UserStatementManager.nickname+"!你一直看我吃都不會餓嗎?"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "卓大好帥!!"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "這食物...有毒!!!"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "吃飯還是要挺梅竹!!交大必勝!!"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "阿哈哈哈...哈哈哈(拍手)"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "這麵條會發光!!!"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "哈哈哈哈"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "我要把你們通通吃掉"));
		fox_eat_list.Add (new FoxActionList (	TYPE_TALKING, 	EAT_ONHIT, 	1f, "阿哇呾喀呾啦!"));


		//----------------------------------------------------------------

		fox_sleep_list.Add ( new FoxActionList (TYPE_THINKING,	SLEEP_ONHIT,	2.3f,	"賣剎!" ));
		fox_sleep_list.Add ( new FoxActionList (TYPE_THINKING,	SLEEP_ONHIT,	2.3f,	"我要睡覺辣!" ));
		fox_sleep_list.Add ( new FoxActionList (TYPE_THINKING,	SLEEP_ONHIT,	2.3f,	"都幾點了?" ));
		fox_sleep_list.Add ( new FoxActionList (TYPE_THINKING,	SLEEP_ONHIT,	3f,		"我就是要睡..." ));
		fox_sleep_list.Add ( new FoxActionList (TYPE_THINKING,	SLEEP_ONHIT,	2.3f,	"很累耶..." ));
		fox_sleep_list.Add ( new FoxActionList (TYPE_THINKING,	SLEEP_ONHIT,	2.3f,	"...." ));
		fox_sleep_list.Add ( new FoxActionList (TYPE_THINKING,	SLEEP_ONHIT,	2.3f,	"....." ));
		fox_sleep_list.Add ( new FoxActionList (TYPE_THINKING,	SLEEP_ONHIT,	2.3f,	"再點我我要生氣氣囉!" ));
		fox_sleep_list.Add ( new FoxActionList (TYPE_THINKING,	SLEEP_ONHIT,	2.3f,	"這麼晚了你怎麼還不睡啦QQ" ));
		fox_sleep_list.Add ( new FoxActionList (TYPE_THINKING,	SLEEP_ONHIT,	2.3f,	"夢到"+UserStatementManager.nickname+"了!" ));
		fox_sleep_list.Add ( new FoxActionList (TYPE_THINKING,	SLEEP_ONHIT,	2.3f,	"嗯嗯...Hebe好正..." ));
		fox_sleep_list.Add ( new FoxActionList (TYPE_THINKING,	SLEEP_ONHIT,	2.3f,	"從前從前，有一個魔法師..." ));
		fox_sleep_list.Add ( new FoxActionList (TYPE_THINKING,	SLEEP_ONHIT,	2.3f,	"...他到了一座森林..." ));
		fox_sleep_list.Add ( new FoxActionList (TYPE_THINKING,	SLEEP_ONHIT,	2.3f,	"...森林裡住著個長髮公主..." ));
		fox_sleep_list.Add ( new FoxActionList (TYPE_THINKING,	SLEEP_ONHIT,	2.3f,	"...突破重重困難終於見到了公主..." ));
		fox_sleep_list.Add ( new FoxActionList (TYPE_THINKING,	SLEEP_ONHIT,	2.3f,	"...不料公主卻已經先被騎士救走了..." ));
		fox_sleep_list.Add ( new FoxActionList (TYPE_THINKING,	SLEEP_ONHIT,	2.3f,	"...嗚嗚嗚我還是回去當魔法師好了" ));
	}

	public void on_hit(){
		int x = Random.Range (1, 4);
		Debug.Log (x);
		if (x == 1) {
			Fox_ani.SetInteger ("move", ACTION_JUMP);
			Fox_ani.SetTrigger ("on_hit");
		} else {
			Fox_ani.SetInteger ("move", ACTION_ONHIT);
			Fox_ani.SetTrigger ("on_hit");
		}
	}


	public void on_action(int state){
		if (!is_action) {
			time = 0f;
			int x;

			if(state == STATE_NORMAL){
				x = Random.Range (0, fox_action_list.Count);
				Debug.Log (x);
				if(!is_walk){//走路時就不做動作
					Fox_ani.SetInteger ("move", fox_action_list [x].action);
					Fox_ani.SetTrigger ("on_hit");
				}
				is_action = true;

				if (fox_action_list [x].type == TYPE_TALKING) {
					LbubbleText.text = fox_action_list [x].dialog;
					StartCoroutine (delay (large_bubble, fox_action_list [x].delay));
				}
				else if (fox_action_list [x].type == TYPE_THINKING) {
					SbubbleText.text = fox_action_list [x].dialog;
					StartCoroutine (delay (small_bubble, fox_action_list [x].delay));
				}
			}
			else if (state == STATE_EAT){
				x = Random.Range (0, fox_eat_list.Count);
				Debug.Log (x);
				if(!is_walk){//走路時就不做動作
					Fox_ani.SetInteger ("move", fox_eat_list [x].action);
					Fox_ani.SetTrigger ("on_hit");
				}
				is_action = true;

				if (fox_eat_list [x].type == TYPE_TALKING) {
					LbubbleText.text = fox_eat_list [x].dialog;
					StartCoroutine (delay (large_bubble, fox_eat_list [x].delay));
				}
				else if (fox_eat_list [x].type == TYPE_THINKING) {
					SbubbleText.text = fox_eat_list [x].dialog;
					StartCoroutine (delay (small_bubble, fox_eat_list [x].delay));
				}
			}
			else if (state == STATE_SLEEP){
				x = Random.Range (0, fox_sleep_list.Count);
				Debug.Log (x);
				if(!is_walk){//走路時就不做動作
					Fox_ani.SetInteger ("move", fox_sleep_list [x].action);
					Fox_ani.SetTrigger ("on_hit");
				}
				is_action = true;
				
				if (fox_sleep_list [x].type == TYPE_TALKING) {
					LbubbleText.text = fox_sleep_list [x].dialog;
					StartCoroutine (delay (large_bubble, fox_sleep_list [x].delay));
				}
				else if (fox_sleep_list [x].type == TYPE_THINKING) {
					SbubbleText.text = fox_sleep_list [x].dialog;
					StartCoroutine (delay (small_bubble, fox_sleep_list [x].delay));
				}
			}
		}
	}

	IEnumerator fox_move(int action){
		Fox_ani.SetInteger ("move", action);
		Fox_ani.SetTrigger ("on_hit");
		is_walk = true;

		AnimatorStateInfo  info = Fox_ani.GetCurrentAnimatorStateInfo(0);
		while (info.normalizedTime <= 1.0f) {
			yield return null;
			info = Fox_ani.GetCurrentAnimatorStateInfo(0);
		}
		is_walk = false;
		yield return null;

		//TheFox.transform.localScale = new Vector3 (-TheFox.transform.localScale.x, TheFox.transform.localScale.y,TheFox.transform.localScale.z);
	}

	IEnumerator delay(GameObject g,float f){
		g.SetActive (false);
		g.SetActive (true);
		yield return new WaitForSeconds (f);
		is_action = false;
		g.SetActive (false);
	}


	// Update is called once per frame
	void Update () {

		if(!RandomMove) return;
		time += Time.deltaTime;

		if (time >= walk_delta_time) {
			time = 0f;
			int x = Random.Range (0,4);
			Debug.Log (x);
			if(x == 0)	StartCoroutine (fox_move (ACTION_WALK));
			else if(x==1)StartCoroutine (fox_move (ACTION_SAYHI));
		}
	}
}
