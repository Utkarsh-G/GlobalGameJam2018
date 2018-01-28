using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class QuoteMaker : MonoBehaviour {
	public InputField QuoteInput;
	public InputField KeyInput;
	public InputField BanInput;

	public Text WarningText;

	public KeyText keyTextToForm;

	public void MakerInit(){
		QuoteInput.text = "";
		KeyInput.text = "";
		BanInput.text = "";
	}

	public KeyText SubmitQuote(){
		if (CheckInputAndWarn ()) {
			return keyTextToForm;
		}

		return null;
	}

	bool CheckInputAndWarn(){

		bool isQuoteClean = IsTextClean (QuoteInput.text, @"^[a-zA-Z\s\.\?',]+$");
		bool isKeyListClean = IsTextClean (KeyInput.text, @"^[a-zA-Z\s,]+$");
		bool isBanListClean = IsTextClean (BanInput.text, @"^[a-zA-Z\s,]+$");



		string pattern2 = @"[\s\.\?',]";
		string[] WordsInQuote = Regex.Split (QuoteInput.text, pattern2);

		string pattern = @"[\s,]";
		string[] ListOfKeys = Regex.Split (KeyInput.text, pattern);

		string[] ListOfBans = Regex.Split (BanInput.text, pattern);

		bool isKeyCountOK = (ListOfKeys.Length < 6) && (ListOfKeys.Length > 0);
		bool areKeysInQuote = IsKeyListCorrect (ListOfKeys, WordsInQuote);

		bool isBanCountOK = (ListOfBans.Length < 6) && (ListOfBans.Length > 0);

		if (!isQuoteClean) {
			WarningText.text = "Quote has invalid characters.\nAllowed: A-Z a-z . ? ' ,";
			return false;
		}

		if (!isKeyListClean) {
			WarningText.text = "Key list has invalid characters.\nAllowed: A-Z a-z ,";
			return false;
		}

		if (!isBanListClean) {
			WarningText.text = "Ban list has invalid characters.\nAllowed: A-Z a-z ,";
			return false;
		}

		if (!isKeyCountOK) {
			WarningText.text = "Need 1-5 keys. Separate by space or ,";
			return false;
		}

		if (!isBanCountOK) {
			WarningText.text = "Need 1-5 bans. Separate by space or ,";
			return false;
		}

		if (!areKeysInQuote) {
			WarningText.text = "One or more keys not found in quote.";
			return false;
		}

		keyTextToForm = new KeyText (QuoteInput.text, ListOfKeys, ListOfBans);

		return true;		
	}

	bool IsKeyListCorrect(string[] ListOfKeys, string[] WordsInQuote) //upto 5, no ' or ? allowed, must be present in the quote
	{ 
		bool allMatch = true;
		foreach (string key in ListOfKeys) {
			bool isMatch = false;
			foreach (string word in WordsInQuote) {
				if (key.ToLower () == word.ToLower ()) {
					isMatch = true;
				}
			}
			allMatch = allMatch && isMatch;
		}
			
		return allMatch;
	}


	bool IsTextClean(string text, string pattern){ 
		Regex rgx = new Regex (pattern);
		bool isClean = rgx.IsMatch (text);

		return isClean;
	}
}
