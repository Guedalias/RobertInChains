using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

//[ExecuteInEditMode]
public class GridLevel : EditorWindow
{
	[MenuItem("Window/Grid Level")]
	public static void ShowWindow()
	{
		GridLevel windows = EditorWindow.GetWindow(typeof(GridLevel)) as GridLevel;
		windows.CameraDummyObj = new GameObject();
	}

	static void Init()
	{
		GridLevel windows = EditorWindow.GetWindow(typeof(GridLevel)) as GridLevel;
		windows.CameraDummyObj = new GameObject();
	}
	
	public float Height = 1;
	public float Width = 1;

	public int rows = 100;
	public int columns = 100;

	public float Xoffset = 0.5f;
	public float Yoffset = 0.5f;

	GameObject CameraDummyObj;


	void OnFocus()
	{
		SceneView.onSceneGUIDelegate -= OnSceneGUI;
		SceneView.onSceneGUIDelegate += OnSceneGUI;
	}

	void OnDestroy()
	{
		SceneView.onSceneGUIDelegate -= OnSceneGUI;
		GameObject.DestroyImmediate(CameraDummyObj);
	}

	void OnGUI()
	{
	}

	void OnSceneGUI(SceneView sceneView)
	{
		if (CameraDummyObj == null)
		{
			CameraDummyObj = new GameObject();
			CameraDummyObj.name = "DummyCam_EDITOR";
		}
			

		var view = SceneView.lastActiveSceneView;

		if (view != null)
		{
			CameraDummyObj.transform.position = view.camera.transform.position;
			CameraDummyObj.transform.rotation = view.rotation;
		}
		// Do your drawing here using Handles.
		Handles.BeginGUI();

		var pos = CameraDummyObj.transform.position;

		float x = Xoffset + (float)Math.Floor(pos.x)- (rows / 2) * Width;
		float y = Yoffset + (float)Math.Floor(pos.y) - (columns / 2) * Height;
		for (int i = 0; i < columns; ++i) 
		{
			Debug.DrawLine(new Vector3(x, y + i * Height, 0f),
						   new Vector3(x + columns * Height, y + i * Height, 0f),
						   new Color(0.6f, 0.6f, 0.6f, 0.2f));
			
		} // End draw vertical lines

		for (int i = 0; i < rows; ++i)
		{
			Debug.DrawLine(new Vector3(x + i * Width, y, 0f),
						   new Vector3(x + i * Width, y + rows * Width, 0f),
						   new Color(0.6f,0.6f,0.6f,0.2f));
		} // End draw horizontal lines
		  // Do your drawing here using GUI.
		Handles.EndGUI();
	}
}

public class MyScriptGizmoDrawer
{
	[DrawGizmo(GizmoType.Selected | GizmoType.Active)]
	static void DrawGizmoForMyScript(CharacterController scr, GizmoType gizmoType)
	{
		//Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(0, 10, 0));
		//Gizmos.DrawLine(new Vector3(0, 0, 0), new Vector3(10, 0, 0));
	}
}