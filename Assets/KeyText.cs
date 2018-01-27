using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Use this class for the special text. 
 * It holds one sentence
 * + keywords
 * + verifier that keywords are in the sentence
 * + verifier that keywords are unique
 * + verifier that chars are only letters.
 * 
 */

public class KeyText{
	public string Sentence;
	public string[] KeyWords;

	public KeyText()
	{
		Sentence = "This is a special sentence. It has text n.";
		KeyWords = new string[] {"special", "text", "It", "text"};
	}
}
