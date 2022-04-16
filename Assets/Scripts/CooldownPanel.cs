using KeepCoding;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public sealed class CooldownPanel : WordlePanel
{
	[SerializeField]
	private Text _subtitleText;

	[SerializeField]
	private Text _timerText;

	public override void SetActive(bool active)
	{
		base.SetActive(active);
		if (active)
			_subtitleText.text = "The word was <b>{0}</b>.".Form(GetParent<WordleModule>().CorrectWord.ToUpper());
	}

	public void StartTimer(Action callback)
	{
		StartCoroutine(TimerRoutine(callback));
	}

	private IEnumerator TimerRoutine(Action callback)
	{
		float start = Time.time;
		while (Time.time - start < WordleConstants.CooldownTime)
		{
			SetTimerText(Mathf.Max(0, WordleConstants.CooldownTime - (Time.time - start)));
			yield return null;
		}

		callback.Invoke();
	}

	private static string Pad(int number)
	{
		return number.ToString().PadLeft(2, '0');
	}

	private void SetTimerText(float time)
	{
		_timerText.text = "{0}:{1}".Form(Mathf.FloorToInt(time / 60f % 60f), Mathf.FloorToInt(time % 60f).ToString().PadLeft(2, '0'));
	}
}
