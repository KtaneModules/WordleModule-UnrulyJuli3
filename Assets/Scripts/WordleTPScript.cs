using KeepCoding;
using System.Collections;
using System.Linq;
using UnityEngine;

public sealed class WordleTPScript : TPScript<WordleModule>
{
	public override IEnumerator Process(string command)
	{
		string word = command.Trim().ToLowerInvariant().Where(char.IsLetter).Join("");
		if (word.Length != WordleConstants.TileCount)
		{
			yield return SendToChatError("Word must be {0} letters long!".Form(WordleConstants.TileCount));
			yield break;
		}

		yield return null;
		yield return SubmitWord(word);
	}

	public override IEnumerator ForceSolve()
	{
		yield return SubmitWord(Module.CorrectWord);
	}

	private IEnumerator SubmitWord(string word)
	{
		if (Module.IsSolved)
			yield break;

		yield return new WaitUntil(() => Module.CurrentState == GameState.Write);

		while (GetChild<WordleBoard>().CurrentRow.Tiles.Any(t => t.HasLetter))
			GetChild<WordleBoard>().DeleteLetter();

		foreach (char letter in word)
			GetChild<WordleBoard>().AddLetter(letter);

		GetChild<WordleBoard>().SubmitWord();
	}
}
