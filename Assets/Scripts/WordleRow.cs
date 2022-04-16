using KeepCoding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class WordleRow : CacheableBehaviour
{
	[SerializeField]
	private WordleTile _tile;

	public List<WordleTile> Tiles { get; private set; }

	public bool IsFilled
	{
		get
		{
			return Tiles.All(t => t.HasLetter);
		}
	}

	public bool IsSubmitted { get; private set; }

	public string SpelledWord
	{
		get
		{
			return Tiles.Where(t => t.HasLetter).Select(t => t.Letter).Join("");
		}
	}

	private void Awake()
	{
		Tiles = new List<WordleTile>();
		for (int i = 0; i < WordleConstants.TileCount; i++)
			Tiles.Add(Instantiate(_tile, transform));
	}

	private IEnumerator RevealRoutine(Action callback)
	{
		TileState[] evaluation = WordleEvaluator.Evaluate(GetParent<WordleModule>().CorrectWord, Tiles.Select(t => t.Letter).Join(""));
		int index = 0;
		foreach (WordleTile tile in Tiles)
		{
			char letter = tile.Letter;
			tile.RevealState(evaluation[index]);
			yield return new WaitForSeconds(0.25f);
			index++;
		}

		callback.Invoke();
	}

	public void RevealLetters(Action callback)
	{
		IsSubmitted = true;
		StartCoroutine(RevealRoutine(callback));
	}
}
