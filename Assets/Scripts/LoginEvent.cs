using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LoginEvent : MonoBehaviour {

	public InputField AccountInputField,PasswordInputField;
	public InputField NewAccountInputField, NewPasswordInputField,CheckPasswordInputField,NicknameInputField;

	public GameObject Account,Password,NewAccount, NewPassword,CheckPassword,Nickname;

	public GameObject mark;

	public GameObject loginZone;
	public GameObject registerZone;
	
	public GameObject messageBox;
	public GameObject networkLoading;

	public Sprite normal_field, correct_field, error_field;

	public Animator ani;

	NetworkLoadingSet newNetworkLoading;

	void onStart(){
		Global.init_all ();
	}


	public void onLoginAccountSelect(){
		//Debug.Log ("LoginAccountSelected!!");
	}

	public void onLoginAccountDeSelect(){
		//Debug.Log ("LoginAccountDeSelected!!");
	}

	//-------------------------------------------when user is in login zone----------------------------------
	public void onLoginButtonClick(){
		if (AccountInputField.text.Length == 0) {
			Account.GetComponent<Image>().sprite = error_field;
			Password.GetComponent<Image>().sprite = normal_field;
			ErrorMessageBox("請輸入帳號名稱!!");
			return;
		}

		if (!Global.CheckFormat(AccountInputField.text)) {
			Account.GetComponent<Image>().sprite = error_field;
			Password.GetComponent<Image>().sprite = normal_field;
			ErrorMessageBox("帳號格式不符！");
			return;
		}
		if (PasswordInputField.text.Length == 0) {
			Password.GetComponent<Image>().sprite = error_field;
			Account.GetComponent<Image>().sprite = normal_field;
			ErrorMessageBox("請輸入密碼!!");
			return;
		}
		if (!Global.CheckFormat(PasswordInputField.text)) {
			Password.GetComponent<Image>().sprite = error_field;
			Account.GetComponent<Image>().sprite = normal_field;
			ErrorMessageBox("密碼格式不符！");
			return;
		}
		NetworkLoading ();
		UserStatementManager.user_login(AccountInputField.text, PasswordInputField.text, onLoginSuccess, onConnectionError, onNoUser, onPswErr);
	}

	public void onRegisterButtonClick(){
		ani.SetTrigger ("trigger");
	}

	//-------------------------------------------when user is in register zone----------------------------------

	public void onCheckButtonClick(){
		if (NewAccountInputField.text.Length == 0) {
			NewAccount.GetComponent<Image>().sprite = error_field;
			NewPassword.GetComponent<Image>().sprite = normal_field;
			Nickname.GetComponent<Image>().sprite = normal_field;
			ErrorMessageBox("請輸入帳號名稱!!");
			return;
		}
		if (!Global.CheckFormat(NewAccountInputField.text)) {
			NewAccount.GetComponent<Image>().sprite = error_field;
			NewPassword.GetComponent<Image>().sprite = normal_field;
			Nickname.GetComponent<Image>().sprite = normal_field;
			ErrorMessageBox("帳號格式不符！");
			return;
		}
		if (NicknameInputField.text.Length == 0) {
			Nickname.GetComponent<Image>().sprite = error_field;
			NewPassword.GetComponent<Image>().sprite = normal_field;
			NewAccount.GetComponent<Image>().sprite = normal_field;
			ErrorMessageBox("請輸入暱稱！");
			return;
		}
		if ( Global.fixed_string_length(NicknameInputField.text) > 14) {
			Nickname.GetComponent<Image>().sprite = error_field;
			NewPassword.GetComponent<Image>().sprite = normal_field;
			NewAccount.GetComponent<Image>().sprite = normal_field;
			ErrorMessageBox("暱稱長度過長！");
			return;
		}
		if (NewPasswordInputField.text.Length == 0) {
			NewPassword.GetComponent<Image>().sprite = error_field;
			Nickname.GetComponent<Image>().sprite = normal_field;
			NewAccount.GetComponent<Image>().sprite = normal_field;
			ErrorMessageBox("請輸入密碼!!");
			return;
		}
		if (!Global.CheckFormat(NewPasswordInputField.text)) {
			NewPassword.GetComponent<Image>().sprite = error_field;
			Nickname.GetComponent<Image>().sprite = normal_field;
			NewAccount.GetComponent<Image>().sprite = normal_field;
			ErrorMessageBox("密碼格式不符！");
			return;
		}
		if (!NewPasswordInputField.text.Equals (CheckPasswordInputField.text)) {
			NewPassword.GetComponent<Image>().sprite = normal_field;
			Nickname.GetComponent<Image>().sprite = normal_field;
			NewAccount.GetComponent<Image>().sprite = normal_field;
			CheckPassword.GetComponent<Image> ().sprite = error_field;
			ErrorMessageBox ("密碼輸入不同!");
			CheckPasswordInputField.text = "";
			PasswordInputField.text = "";
			return;
		} 
		//prepare for regist the account

		NetworkLoading ();
		if (UserStatementManager.token == "")
			UserStatementManager.registration_request (onGetRequest, onConnectionError);
		else {
			onGetRequest();
		}
	}

	public void onCancelButtonClick(){
		ani.SetTrigger ("trigger");
	}


	public void onValueChange(){
		Account.GetComponent<Image>().sprite = normal_field;
		Password.GetComponent<Image>().sprite = normal_field;
		NewAccount.GetComponent<Image>().sprite = normal_field;
		NewPassword.GetComponent<Image>().sprite = normal_field;
		Nickname.GetComponent<Image>().sprite = normal_field;
	}

	public void onCheckPassChange(){
		if (!NewPasswordInputField.text.Equals (CheckPasswordInputField.text)) {
			CheckPassword.GetComponent<Image> ().sprite = error_field;
			mark.SetActive(false);
		}
		else{
			CheckPassword.GetComponent<Image> ().sprite = correct_field;
			mark.SetActive(true);
		}
	}

	//-------------------------------------------Event handler---------------------------------------------------
	//When login is success
	void onLoginSuccess(){
		UserStatementManager.get_full_info(UserStatementManager.id, UserStatementManager.token,onGetInfoDone, onConnectionError);
		newNetworkLoading.Clear ();
		NetworkLoading ("登入成功!設定用戶資訊中...");
	}

	//when input user is not found
	void onNoUser(){
		Account.GetComponent<Image>().sprite = error_field;
		Password.GetComponent<Image>().sprite = normal_field;
		ErrorMessageBox("找不到該帳號!");
		newNetworkLoading.Clear ();
	}

	void onPswErr(){
		Password.GetComponent<Image>().sprite = error_field;
		Account.GetComponent<Image>().sprite = normal_field;
		ErrorMessageBox("密碼輸入錯誤!");
		newNetworkLoading.Clear ();
	}

	//User login success, and get the info done
	void onGetInfoDone(){
		newNetworkLoading.Clear ();
		StartCoroutine(LoadingScene("_main"));
	}

	//User login success, and get the info done
	void onConnectionError(){
		ErrorMessageBox("連線失敗!");
		newNetworkLoading.Clear ();
	}


	//when user get the request successfully
	void onGetRequest(){
		newNetworkLoading.Clear ();
		UserStatementManager.registration_data(NewAccountInputField.text, NewPasswordInputField.text, NicknameInputField.text,UserStatementManager.token,onRegistSuccess, onConnectionError,onAccountExist,onNicknameExist);
		NetworkLoading("註冊新帳號中...");
	}

	//when user get a new account
	void onRegistSuccess(){

		PlayerPrefs.SetInt ("RegistFail", 1);
		newNetworkLoading.Clear ();
		UserStatementManager.get_full_info(UserStatementManager.id, UserStatementManager.token,onFirstGetInfoDone, onConnectionError);
		NetworkLoading ("註冊成功！初始化設定中...");
	}

	void onFirstGetInfoDone(){
		newNetworkLoading.Clear ();
		StartCoroutine(LoadingScene("_foxSetting"));
		
	}

	void onAccountExist(){
		NewPassword.GetComponent<Image>().sprite = normal_field;
		Nickname.GetComponent<Image>().sprite = normal_field;
		NewAccount.GetComponent<Image>().sprite = error_field;
		ErrorMessageBox("該帳號已被註冊!");
		newNetworkLoading.Clear ();
	}

	void onNicknameExist(){
		NewPassword.GetComponent<Image>().sprite = normal_field;
		Nickname.GetComponent<Image>().sprite = error_field;
		NewAccount.GetComponent<Image>().sprite = normal_field;
		ErrorMessageBox("該暱稱已被使用!");
		newNetworkLoading.Clear ();
	
	}

	//-------------------------------------------process event--------------------------------------------------
	//Handle Error message
	void ErrorMessageBox(string message){
		GameObject err_msg_box = Instantiate (messageBox) as GameObject;
		MessageBoxSetting mbs = (MessageBoxSetting)(err_msg_box.GetComponent<MessageBoxSetting> ());
		mbs.message = message;

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
