using UnityEngine;
using UnityEditor;

public class FormWindow : EditorWindow
{
	public object _o;
	public delegate void CallbackDelegate();
	CallbackDelegate _del;

	public static void Init(object obj, CallbackDelegate del)
	{
		FormWindow window = EditorWindow.CreateInstance<FormWindow>();
		window._o = obj;
		window._del = del;
		window.Show();
	}


	void OnGUI()
	{
		Editor e = Editor.CreateEditor ((UnityEngine.Object)_o);
		e.OnInspectorGUI ();
	
		if (GUILayout.Button("Save", GUILayout.Width(200)))
		{
			_del();
			Close();
		}
	}
}