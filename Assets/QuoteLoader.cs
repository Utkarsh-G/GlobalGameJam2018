using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;


/* 
 * The peer of TextChecker, this class
 * loads the quote (from database or custom-made)
 * and asks the user for appropriate hint input.
 * 
 */

public class QuoteLoader : MonoBehaviour {
	//public SceneManager myManager;
	
	public Text QuoteText;
	public InputField UserText;
	public Text Warning1;
	public Text BannedText;
	KeyText keyText;

	public void initQuoteLoader(KeyText inkeyText){

		keyText = inkeyText;
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

	public string SubmitUserText(){
		Warning1.text = "";

		if (!SanitizeInput (UserText.text)) {
			Warning1.text = "Invalid input. Avoid banned words. \nAllowed input: A-Z a-z . ? ' ,";
			return "";
		}

		//myManager.FromLoaderToChecker (UserText.text);
		return UserText.text;

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
}
