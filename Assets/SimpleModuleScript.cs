using System.Collections.Generic;
using UnityEngine;
using KModkit;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections;
using System;
using Rnd = UnityEngine.Random;

public class SimpleModuleScript : MonoBehaviour {

	public KMAudio audio;
	public KMBombInfo info;
	public KMBombModule module;

	public KMSelectable[] highlightButtons;
	public KMHighlightable[] but1Highlights;
	public KMHighlightable[] but2Highlights;
	public KMHighlightable[] but3Highlights;
	public KMHighlightable[] but4Highlights;
	public KMHighlightable[] but5Highlights;

	static int ModuleIdCounter = 1;
	int ModuleId;

	private static readonly int[,] sequences = new int[10,4]
	{
		{1,2,3,4},
		{1,2,4,3},
		{1,3,2,4},
		{1,3,4,2},
		{4,3,2,1},
		{4,3,1,2},
		{4,2,1,3},
		{4,2,3,1},
		{3,4,2,1},
		{2,4,3,1}
	};

	private int[] butShiftOrder = new int[5];
	private int butShiftPos = 0;

	bool _isSolved = false;
	bool incorrect = false;


	void Awake() 
	{
		ModuleId = ModuleIdCounter++;

		foreach (KMSelectable button in highlightButtons)
		{
			KMSelectable pressedButton = button;
			button.OnInteract += delegate () { buttonPress(pressedButton); return false; };
		}
	}

	void Start ()
	{
		for (int i = 0; i != 5; i++)
		{
			int random = Rnd.Range (0, 10);
			butShiftOrder [i] = random;
		}
		StartCoroutine (Repeat ());
	}

	IEnumerator Repeat()
	{
		yield return new WaitForSeconds (1.5f);
		butShiftPos++;
		butShiftPos = butShiftPos % 4;
		StartCoroutine (Repeat ());
	}

	void Update()
	{
		int shapeNum1 = sequences [butShiftOrder[0], butShiftPos];
		highlightButtons [0].Highlight = but1Highlights [shapeNum1 - 1];

		int shapeNum2 = sequences [butShiftOrder[0], butShiftPos];
		highlightButtons [1].Highlight = but2Highlights [shapeNum2 - 1];

		int shapeNum3 = sequences [butShiftOrder[0], butShiftPos];
		highlightButtons [2].Highlight = but3Highlights [shapeNum3 - 1];

		int shapeNum4 = sequences [butShiftOrder[0], butShiftPos];
		highlightButtons [3].Highlight = but4Highlights [shapeNum4 - 1];

		int shapeNum5 = sequences [butShiftOrder[0], butShiftPos];
		highlightButtons [4].Highlight = but5Highlights [shapeNum5 - 1];
	}

	void buttonPress(KMSelectable pressedButton)
	{
		audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		int buttonPosition = Array.IndexOf(highlightButtons, pressedButton);

		if (_isSolved == false)
		{
			switch (buttonPosition) 
			{
			case 0:
				
				break;
			case 1:
				
				break;
			case 2:
				
				break;
			case 3:
				
				break;
			case 4:
				
				break;
			}
			if (incorrect) 
			{
				module.HandleStrike ();
				incorrect = false;
			}
		}
	}

	void Log(string message)
	{
		Debug.LogFormat("[Highlight Shift #{0}] {1}", ModuleId, message);
	}
}

