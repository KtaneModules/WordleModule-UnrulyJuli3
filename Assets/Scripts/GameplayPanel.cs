using UnityEngine;

public sealed class GameplayPanel : WordlePanel
{
	public override void SetActive(bool active)
	{
		base.SetActive(active);
		Get<CanvasGroup>().interactable = active;
		Get<CanvasGroup>().blocksRaycasts = active;
	}
}
