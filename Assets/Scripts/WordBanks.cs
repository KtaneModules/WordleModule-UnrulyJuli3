using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class WordBanks : MonoBehaviour
{
	[SerializeField]
	private TextAsset[] _assets;

	private static Dictionary<string, string[]> s_banks;

	public static string[] Answers
	{
		get
		{
			return s_banks["Answers"];
		}
	}

	public static string[] Guesses
	{
		get
		{
			return s_banks["Guesses"].Concat(s_banks["Answers"]).ToArray();
		}
	}

	private void Awake()
	{
		if (s_banks == null)
		{
			s_banks = new Dictionary<string, string[]>();
			foreach (TextAsset asset in _assets)
				s_banks[asset.name] = asset.text.Split(',');
		}
	}
}
