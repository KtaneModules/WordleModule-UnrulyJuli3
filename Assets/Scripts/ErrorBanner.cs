using KeepCoding;
using UnityEngine;
using UnityEngine.UI;

public class ErrorBanner : CacheableBehaviour, ITweenable
{
	private bool _isVisible;

	private int _fadeTween;

	private int _delayedCloseTween;

	public void Show(string text)
	{
		if (_isVisible)
			return;

		_isVisible = true;
		GetChild<Text>().text = text;
		CancelTweens();
		_fadeTween = LeanTween.value(0, 1, 0.2f)
			.setOnUpdate(v => Get<CanvasGroup>().alpha = v)
			.uniqueId;

		_delayedCloseTween = LeanTween.delayedCall(3, Hide).uniqueId;
	}

	public void Hide()
	{
		if (!_isVisible)
			return;

		_isVisible = false;
		CancelTweens();
		_fadeTween = LeanTween.value(1, 0, 0.2f)
			.setOnUpdate(v => Get<CanvasGroup>().alpha = v)
			.uniqueId;
	}

	public void CancelTweens()
	{
		LeanTween.cancel(_fadeTween);
		LeanTween.cancel(_delayedCloseTween);
	}

	public void OnDestroy()
	{
		CancelTweens();
	}
}
