using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

/*
 * The peer of Texter.cs, this class
 * manages the intake of user text
 * for panel 1. It also can switch between panels.
 * 
 */

public class SceneManager : MonoBehaviour {
	public Texter myTexter;
	public GameObject Panel1;
	public GameObject Panel2;

	public Text QuoteText;
	public InputField UserText;
	public Text Warning1;

	public QuoteListSO qList;
	KeyText keyText;

	string UserInput;
	KeyText Quote;

	// Use this for initialization
	void Start () {

		this.initScene ();
	}

	public void initScene()
	{
		Panel1.SetActive (true);
		Panel2.SetActive (false);

		keyText = new KeyText (qList.QuoteList[0].Quote, qList.QuoteList[0].Keys);
		UserText.text = "";

		QuoteText.text = keyText.Sentence;
	}

	public void SubmitUserText(){
		Warning1.text = "";

		if (!SanitizeInput (UserText.text)) {
			Warning1.text = "Invalid characters. \nAllowed input: A-Z a-z . ? ' ,";
			return;
		}

		SwitchToCheckMode ();

	}

	bool SanitizeInput(string text){ //TODO: prohibit use of keywords
		Regex rgx = new Regex (@"^[a-zA-Z\s\.\?',]+$");
		bool isClean = rgx.IsMatch (text);
		return isClean;
	}

	void SwitchToCheckMode(){
		Panel1.SetActive (false);
		Panel2.SetActive (true);
		myTexter.initTexter (keyText, UserText.text);
	}

}
