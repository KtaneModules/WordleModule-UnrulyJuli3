using System.Collections.Generic;
using UnityEngine;

public static class WordleConstants
{
	public const int TileCount = 5;

	public const int RowCount = 6;

	public const float CooldownTime = 30;

	public static Dictionary<KeyCode, char> KeyboardMap { get; private set; }

	static WordleConstants()
	{
		KeyboardMap = new Dictionary<KeyCode, char>
		{
			{ KeyCode.A, 'a' },
			{ KeyCode.B, 'b' },
			{ KeyCode.C, 'c' },
			{ KeyCode.D, 'd' },
			{ KeyCode.E, 'e' },
			{ KeyCode.F, 'f' },
			{ KeyCode.G, 'g' },
			{ KeyCode.H, 'h' },
			{ KeyCode.I, 'i' },
			{ KeyCode.J, 'j' },
			{ KeyCode.K, 'k' },
			{ KeyCode.L, 'l' },
			{ KeyCode.M, 'm' },
			{ KeyCode.N, 'n' },
			{ KeyCode.O, 'o' },
			{ KeyCode.P, 'p' },
			{ KeyCode.Q, 'q' },
			{ KeyCode.R, 'r' },
			{ KeyCode.S, 's' },
			{ KeyCode.T, 't' },
			{ KeyCode.U, 'u' },
			{ KeyCode.V, 'v' },
			{ KeyCode.W, 'w' },
			{ KeyCode.X, 'x' },
			{ KeyCode.Y, 'y' },
			{ KeyCode.Z, 'z' }
		};
	}
}
