using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class QuoteListSO : ScriptableObject {
	public List<QuoteObj> QuoteList;

}

[CreateAssetMenu]

public class QuoteObj : ScriptableObject{
	public string Quote;
	public string[] Keys;
}
