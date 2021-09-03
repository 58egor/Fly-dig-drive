// NULLcode Studio © 2016
// null-code.ru

#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BezierCurves))]

public class BezierCurvesEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		BezierCurves e = (BezierCurves)target;

		GUILayout.Space(15);
		if(GUILayout.Button("Add New"))
		{
			e.AddPoint();
		}
		GUILayout.Space(5);
		if(GUILayout.Button("Destroy Last"))
		{
			e.DestroyLast();
		}
		GUILayout.Space(5);
		if(GUILayout.Button("Clear All"))
		{
			e.ClearAll();
		}
	}
}
#endif
