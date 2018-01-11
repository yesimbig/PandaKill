using UnityEngine;
using System.Collections;

public class MusicHandler : MonoBehaviour {

	public GameObject globalUIMusic;
	public AudioClip bgm;

	static GameObject MyUIMusic;

	// Use this for initialization
	void Start () {
		MyUIMusic = GameObject.FindGameObjectWithTag ("GlobalUIMusic");

		if (MyUIMusic == null) {
			MyUIMusic = (GameObject)Instantiate(globalUIMusic);
		}
		if (bgm!=null && MyUIMusic.GetComponent<AudioSource> ().clip != bgm) {
			MyUIMusic.GetComponent<AudioSource> ().Stop();
			MyUIMusic.GetComponent<AudioSource> ().clip = bgm;
			MyUIMusic.GetComponent<AudioSource> ().loop = true;
			MyUIMusic.GetComponent<AudioSource> ().Play ();
		}
	}

	public void playOneClip(AudioClip a){
		MyUIMusic.GetComponent<AudioSource> ().PlayOneShot (a);
	}

	public void changeBGM(AudioClip cbgm){
		MyUIMusic.GetComponent<AudioSource> ().Stop();
		MyUIMusic.GetComponent<AudioSource> ().clip = cbgm;
		MyUIMusic.GetComponent<AudioSource> ().loop = false;
		MyUIMusic.GetComponent<AudioSource> ().Play ();

	}

	public void StopBGM(){
		MyUIMusic.GetComponent<AudioSource> ().Stop();
		MyUIMusic.GetComponent<AudioSource> ().clip = null;
	}
}
