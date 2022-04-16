using KeepCoding;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class WordleBoard : CacheableBehaviour
{
	[SerializeField]
	private WordleRow _row;

	public List<WordleRow> Rows { get; private set; }

	public WordleRow CurrentRow
	{
		get
		{
			return Rows.First(r => !r.IsSubmitted);
		}
	}

	private void DestroyRows()
	{
		if (Rows != null)
			foreach (WordleRow row in Rows)
				Destroy(row.gameObject);
	}

	public void CreateRows()
	{
		GetParent<WordleModule>().Log("Board reset");

		DestroyRows();
		Rows = new List<WordleRow>();
		for (int i = 0; i < WordleConstants.RowCount; i++)
			Rows.Add(Instantiate(_row, transform));
	}

	public void AddLetter(char letter)
	{
		WordleTile tile = CurrentRow.Tiles.FirstOrDefault(t => !t.HasLetter);
		if (tile != null)
			tile.SetLetter(letter);
	}

	public void DeleteLetter()
	{
		WordleTile tile = CurrentRow.Tiles.LastOrDefault(t => t.HasLetter);
		if (tile != null)
			tile.ShowLetter(false);

		GetParent<WordleModule>().GetChild<ErrorBanner>().Hide();
	}

	public void SubmitWord()
	{
		if (!CurrentRow.IsFilled)
			return;

		if (!WordBanks.Guesses.Contains(CurrentRow.SpelledWord))
		{
			GetParent<WordleModule>().GetChild<ErrorBanner>().Show("<b>{0}</b> is not a valid word!".Form(CurrentRow.SpelledWord.ToUpper()));
			return;
		}

		GetParent<WordleModule>().Log("Board reveal start");
		GetParent<WordleModule>().CurrentState = GameState.Reveal;
		CurrentRow.RevealLetters(RevealComplete);
	}

	private void RevealComplete()
	{
		if (Rows.Last(r => r.IsSubmitted).SpelledWord == GetParent<WordleModule>().CorrectWord)
		{
			GetParent<WordleModule>().CurrentState = GameState.None;
			GetParent<WordleModule>().Solve("Correct word submitted! Module solved.");
			return;
		}

		if (Rows.All(r => r.IsSubmitted))
			GetParent<WordleModule>().StartCooldown();

		else
			GetParent<WordleModule>().CurrentState = GameState.Write;
	}
}
