using KeepCoding;
using UnityEngine;

public abstract class WordlePanel : CacheableBehaviour, ITweenable
{
	private int _fadeTween;

	public virtual void SetActive(bool active)
	{
		CancelTweens();
		_fadeTween = LeanTween.value(active ? 0 : 1, active ? 1 : 0, 0.5f)
			.setOnUpdate(v => Get<CanvasGroup>().alpha = v)
			.uniqueId;
	}

	public void CancelTweens()
	{
		LeanTween.cancel(_fadeTween);
	}

	public void OnDestroy()
	{
		CancelTweens();
	}
}
