using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class tutorial : MonoBehaviour {

	public GameObject[] circles;
	public GameObject[] frames;
	public GameObject black,networkLoading;
	public GameObject[] bubbles;
	public GameObject timeText,urgent_info_box;
	public Button touch;

	public Text FoxSay;

	public FoxSkinSetting fss;
	public FoxAction fa;
	int now_state;

	NetworkLoadingSet newNetworkLoading;

	// Use this for initialization
	void Start () {
		now_state = 0;

		FoxSay.text = "哈囉!"+UserStatementManager.nickname+ "，請先按照步驟指示完成教學喔!";

		int eye = UserStatementManager.eye;
		int tail = UserStatementManager.tail;
		int color = UserStatementManager.color;
		int wearing = UserStatementManager.wearing;
		if (eye < 1 || eye > 4)
			eye = 1;
		if (tail < 1 || tail > 4)
			tail = 1;
		if(color < 1 || color > 4)
			color = 1;
		if(wearing < 1 ||wearing > 4)
			wearing = 1;
		fss.set (eye, tail, color, wearing);

		setVisible (circles, -1);
		setVisible (frames, -1);
		setVisible (bubbles, -1);
		black.SetActive (false);
		timeText.SetActive (true);
		urgent_info_box.SetActive (false);
		touch.interactable = false;
		StartCoroutine (delaystart (2f));
	}


	public void onClick(){
		now_state++;
		if (now_state >= 1 && now_state<=8) {
			setVisible (circles, now_state-1);
			setVisible (frames, now_state-1);
			black.SetActive (true);		
		}

		if (now_state == 9) {
			setVisible (frames, 8);
			timeText.SetActive (false);
			urgent_info_box.SetActive (true);
		}
		if (now_state == 10) {
			setVisible (circles, -1);
			setVisible (frames, -1);
			black.SetActive (false);	
			setVisible (bubbles, 1);

			StartCoroutine (delaygo (2.3f));
		}

	}

	void setVisible(GameObject[] g,int s){
		for (int i = 0; i < g.Length; i++) {
			if (i == s)
				g [i].SetActive (true);
			else g [i].SetActive (false);
		}	
	}

	IEnumerator delaystart(float delay){
		setVisible (bubbles, 0);
		fa.on_hit ();
		yield return new WaitForSeconds (delay);
		onClick ();
		touch.interactable = true;
	}

	IEnumerator delaygo(float delay){
		yield return new WaitForSeconds (delay);
		StartCoroutine(LoadingScene("_main"));
	}

	//Handle network loading
	void NetworkLoading(string message = null){
		newNetworkLoading = (Instantiate (networkLoading) as GameObject).GetComponent<NetworkLoadingSet>();

		if (message != null)
			newNetworkLoading.message = message;
	}

	//Handle loading next scene
	IEnumerator LoadingScene(string scene){
		NetworkLoading ("載入中...請稍候...");
		AsyncOperation asyncf = Application.LoadLevelAsync (scene);
		if (asyncf == null)
			Debug.Log ("GG");
		while (!asyncf.isDone) {
			newNetworkLoading.setMessage("載入中...請稍候..."+((int)(asyncf.progress*100)).ToString ()+"%");
			yield return null;
		}
		newNetworkLoading.Clear ();
	}
}
