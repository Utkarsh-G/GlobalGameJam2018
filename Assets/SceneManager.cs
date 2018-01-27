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
	public Text BannedText;

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

		QuoteObj myQuote = qList.QuoteList [1];

		keyText = new KeyText (myQuote.Quote, myQuote.Keys, myQuote.Banned);
		UserText.text = "";

		QuoteText.text = keyText.Sentence;
		string BanList = "Ban List: ";
		foreach (string word in keyText.KeyWords) {
			BanList += "\n" + word;
		}
		foreach (string word in keyText.BannedWords) {
			BanList += "\n" + word;
		}
		BannedText.text = BanList;

	}

	public void SubmitUserText(){
		Warning1.text = "";

		if (!SanitizeInput (UserText.text)) {
			Warning1.text = "Invalid input. Avoid banned words. \nAllowed input: A-Z a-z . ? ' ,";
			return;
		}

		SwitchToCheckMode ();

	}

	bool SanitizeInput(string text){ //Got a non-reproducible error here :( :( Some kind of "object not set" in line 74
		Regex rgx = new Regex (@"^[a-zA-Z\s\.\?',]+$");
		bool isClean = rgx.IsMatch (text);
		//Debug.Log ("Show text: " + text + " \nShow key:" + keyText.KeyWords [0]);
		foreach (string key in keyText.KeyWords) {
			bool hasKey = Regex.IsMatch (text, key);
			isClean = isClean && !hasKey;
		}

		foreach (string key in keyText.BannedWords) {
			bool hasKey = Regex.IsMatch (text, key);
			isClean = isClean && !hasKey;
		}

		return isClean;
	}

	void SwitchToCheckMode(){
		Panel1.SetActive (false);
		Panel2.SetActive (true);
		myTexter.initTexter (keyText, UserText.text);
	}

}
