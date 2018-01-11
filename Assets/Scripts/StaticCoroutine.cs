using UnityEngine;
using System;
using System.Collections;

public class StaticCoroutine : MonoBehaviour {
	
	private static StaticCoroutine mInstance = null;
	private static Action IsDone,ConnErr,IsErr1,IsErr2;
	public static int errLog = 0;

	static float timeleft = 0f;
	private static StaticCoroutine instance
	{
		get
		{
			if (mInstance == null)
			{
				//mInstance = GameObject.FindObjectOfType(typeof(StaticCoroutine)) as StaticCoroutine;
				
				if (mInstance == null)
				{
					mInstance = new GameObject("StaticCoroutine").AddComponent<StaticCoroutine>();
				}
				else{
					Debug.Log ("GG");
				}
			}
			return mInstance;
		}
	}
	
	void Awake()
	{
		if (mInstance == null)
		{
			mInstance = this as StaticCoroutine;
		}
	}
	
	IEnumerator Perform(IEnumerator coroutine)
	{
		yield return StartCoroutine(coroutine);
		while (errLog == -1)yield return null;
		Die();
	}
	
	/// <summary>
	/// Place your lovely static IEnumerator in here and witness magic!
	/// </summary>
	/// <param name="coroutine">Static IEnumerator</param>
	public static void DoCoroutine(IEnumerator coroutine,Action Done=null,Action connect_error=null,Action Err1=null,Action Err2=null)
	{

		IsDone = Done;
		ConnErr = connect_error;
		IsErr1 = Err1;
		IsErr2 = Err2;
		timeleft = 0f;
		errLog = -1;
		instance.StartCoroutine(instance.Perform(coroutine)); //this will launch the coroutine on our instance
	}
	
	void Die()
	{
		//Debug.Log (errLog);
		mInstance = null;
		Destroy(gameObject);
		if (IsDone != null && errLog == 0)
			IsDone ();
		else if (IsErr1 != null && errLog == 1) {

			IsErr1 ();
		}
		else if(IsErr2!=null && errLog == 2) IsErr2 ();
		else if(ConnErr!=null) ConnErr();
	
	}


	void Update(){

		timeleft += Time.deltaTime;
		if (timeleft > Global.timeout) {
			Debug.Log ("Connection timeout!" + mInstance.name);
			StopCoroutine("StaticCoroutine");

			mInstance = null;
			Destroy (gameObject);
			if (ConnErr != null)
				ConnErr ();
		}


	}

	void OnApplicationQuit()
	{
		mInstance = null;
	}
}