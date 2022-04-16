using KeepCoding;
using UnityEngine;
using UnityEngine.UI;

public sealed class WordleTile : CacheableBehaviour, ITweenable
{
	[SerializeField]
	private Image _emptyState;

	[SerializeField]
	private Image _filledState;

	[SerializeField]
	private Color _absentColor;

	[SerializeField]
	private Color _presentColor;

	[SerializeField]
	private Color _correctColor;

	public bool HasLetter { get; private set; }

	public char Letter { get; private set; }

	private int _revealTween;

	private void Awake()
	{
		ShowLetter(false);
	}

	public void ShowLetter(bool visible)
	{
		HasLetter = visible;
		GetChild<Text>().enabled = HasLetter;
	}

	public void SetLetter(char letter)
	{
		Letter = letter;
		ShowLetter(true);
		GetChild<Text>().text = Letter.ToUpper().ToString();
	}

	public void RevealState(TileState state)
	{
		GetParent<WordleModule>().Log("Board reveal letter {0} {1}", Letter.ToUpper(), state.ToString().ToLowerInvariant());
		_revealTween = LeanTween.value(1, 0, 0.25f)
			.setEaseInSine()
			.setOnUpdate(SetRotate)
			.setOnComplete(() =>
			{
				_emptyState.enabled = false;
				_filledState.enabled = true;
				_filledState.color = GetFillColor(state);
				_revealTween = LeanTween.value(0, 1, 0.25f).setEaseOutSine().setOnUpdate(SetRotate).uniqueId;
			})
			.uniqueId;
	}

	private Color GetFillColor(TileState state)
	{
		switch (state)
		{
			case TileState.Absent:
			default:
				return _absentColor;
			case TileState.Present:
				return _presentColor;
			case TileState.Correct:
				return _correctColor;
		}
	}

	private void SetRotate(float value)
	{
		Get<RectTransform>().localScale = new Vector3(1, value, 1);
	}

	public void CancelTweens()
	{
		LeanTween.cancel(_revealTween);
	}

	public void OnDestroy()
	{
		CancelTweens();
	}
}
