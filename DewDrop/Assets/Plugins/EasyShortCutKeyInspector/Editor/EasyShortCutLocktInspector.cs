﻿using UnityEngine;
using UnityEditor;

public class EasyShortCutLockInspector : MonoBehaviour {
	
	[MenuItem("Edit/Lock2 %`")]
	public static void Lock ()
	{
		ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
		ActiveEditorTracker.sharedTracker.ForceRebuild ();
	}
	
	[MenuItem("Edit/Lock %`", true)]
	public static bool Valid ()
	{
		return ActiveEditorTracker.sharedTracker.activeEditors.Length != 0;
	}
}
