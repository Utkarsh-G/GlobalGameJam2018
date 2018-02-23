using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

/*
 *  This class can switch between panels, 
 * and pass along data accordingly. 
 * 
 * It also loads the game initially. 
 * 
 */

public class SceneManager : MonoBehaviour {
	public TextChecker myTexter;
	public QuoteLoader quoteLoader;
	public QuoteMaker quoteMaker;

	public GameObject QuoteLoaderPanel;
	public GameObject TextCheckerPanel;
	public GameObject MenuPanel;
	public GameObject QuoteMakerPanel;

	//public GameObject Background1;
	public GameObject Background2;

	public InputField QuoteLoaderInput;

	private TouchScreenKeyboard keyboard;
	bool isKeyboardOpen;

	public QuoteListSO qList;
	KeyText keyText;

	// Use this for initialization
	void Start () {
		GoToMenu ();
		isKeyboardOpen = false;
	}

	void OnGUI() {
		/* 
		if(QuoteLoaderInput.isFocused && !isKeyboardOpen){
			Debug.Log("Got focus now.");
			OpenKeyboard();
			isKeyboardOpen = true;
		}
		if(!QuoteLoaderInput.isFocused && isKeyboardOpen){
			Debug.Log("Lost focus");
			isKeyboardOpen = false;
		}
		*/
	}

	public void GoToMenu()
	{
		QuoteLoaderPanel.SetActive (false);
		TextCheckerPanel.SetActive (false);
		QuoteMakerPanel.SetActive (false);
		MenuPanel.SetActive (true);

		Background2.SetActive(false);
	}

	public void StartMaker(){
		QuoteLoaderPanel.SetActive (false);
		TextCheckerPanel.SetActive (false);
		QuoteMakerPanel.SetActive (true);
		MenuPanel.SetActive (false);

		Background2.SetActive(false);

		quoteMaker.MakerInit ();
	}

	public void FromMakerToLoader(){
		keyText = quoteMaker.SubmitQuote ();
		if (keyText != null) {
			QuoteLoaderPanel.SetActive (true);
			TextCheckerPanel.SetActive (false);
			QuoteMakerPanel.SetActive (false);
			MenuPanel.SetActive (false);

			Background2.SetActive(false);

			quoteLoader.initQuoteLoader (keyText);
		
		}
	}

	public void StartLoader()
	{
		QuoteLoaderPanel.SetActive (true);
		TextCheckerPanel.SetActive (false);
		QuoteMakerPanel.SetActive (false);
		MenuPanel.SetActive (false);

		Background2.SetActive(false);

		int index = Random.Range(0,17);

		QuoteObj myQuote = qList.QuoteList [index];

		keyText = new KeyText (myQuote.Quote, myQuote.Keys, myQuote.Banned);
		quoteLoader.initQuoteLoader (keyText);

	}

	public void FromLoaderToChecker(){

		string userText = quoteLoader.SubmitUserText ();

		if (userText != "") {
			QuoteLoaderPanel.SetActive (false);
			QuoteMakerPanel.SetActive (false);
			TextCheckerPanel.SetActive (true);
			MenuPanel.SetActive (false);

			Background2.SetActive(true);

			myTexter.initTexter (keyText, userText);
		}

	}

	public void OpenKeyboard()
	{
		//Debug.Log("Trying to open keyboard");
		keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
	}

}
