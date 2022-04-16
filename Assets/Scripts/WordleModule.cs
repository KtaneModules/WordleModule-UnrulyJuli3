using KeepCoding;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class WordleModule : ModuleScript
{
	private List<string> _wordList;

	private bool _isFocused;

	public string CorrectWord { get; private set; }

	public GameState CurrentState { get; set; }

	private void Start()
	{
		CurrentState = GameState.Write;

		PickWord();
		GetChild<WordleBoard>().CreateRows();

		Get<KMSelectable>().Assign(
			onInteract: () => SetFocused(true),
			onDefocus: () => SetFocused(false)
		);
	}

	private void SetFocused(bool focused)
	{
		_isFocused = focused;
		//GetChild<GraphicRaycaster>().enabled = _isFocused;
	}

	private void PickWord()
	{
		if (_wordList == null || _wordList.Count < 1)
			_wordList = new List<string>(WordBanks.Answers);

		int index = Random.Range(0, _wordList.Count);
		CorrectWord = _wordList[index];
		_wordList.RemoveAt(index);

		Log("The correct word is {0}.", CorrectWord.ToUpper());
	}

	private void Update()
	{
		if (!_isFocused)
			return;

		if (CurrentState == GameState.Write)
		{
			foreach (KeyValuePair<KeyCode, char> pair in WordleConstants.KeyboardMap)
				if (Input.GetKeyDown(pair.Key))
					GetChild<WordleBoard>().AddLetter(pair.Value);

			if (Input.GetKeyDown(KeyCode.Backspace))
				GetChild<WordleBoard>().DeleteLetter();

			if (Input.GetKeyDown(KeyCode.Return))
				GetChild<WordleBoard>().SubmitWord();
		}
	}

	public void StartCooldown()
	{
		Log("Failed to guess the word! Starting cooldown.");
		CurrentState = GameState.Cooldown;
		GetChild<GameplayPanel>().SetActive(false);
		GetChild<CooldownPanel>().SetActive(true);
		GetChild<CooldownPanel>().StartTimer(EndCooldown);
	}

	private void EndCooldown()
	{
		CurrentState = GameState.Write;
		PickWord();
		GetChild<WordleBoard>().CreateRows();
		GetChild<CooldownPanel>().SetActive(false);
		GetChild<GameplayPanel>().SetActive(true);
	}
}
