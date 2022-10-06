using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Logger
{
	public static string log;

	public static void Log(string s)
	{
		log = log + s + '\n';
	}
}

public class AutoHeight : MonoBehaviour
{
	public float LetterHeight;
	private void Update()
	{
		var rt = GetComponent<RectTransform>();
		var tm = GetComponent<TMPro.TMP_Text>();

		tm.text = Logger.log;
		var l = tm.textInfo.lineCount;
		rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, l * LetterHeight);
	}
}
