using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections.Generic;
using BitStrap;
using System.Collections;

[CustomEditor(typeof(ModelController))]
public class ModelControllerEditor : Editor
{
	private Dictionary<string, object> values = new Dictionary<string, object> ();

	private bool HaveChanged(FieldInfo fi, object value) {
		if (values.ContainsKey (fi.Name)) {
			object o = values [fi.Name];
			values [fi.Name] = value;
			if (o == null) {
				return o != value;
			} else {
				Debug.Log (o + " != " + value);
				return !(o.Equals (value));
			}
		} else {
			values.Add(fi.Name, value);
			return true;
		}
	}

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI();
		ModelController model = target as ModelController;

		if (model.inspectedModel != null) {
			SerializedObject so = new SerializedObject (model.inspectedModel);

			foreach (FieldInfo fi in model.inspectedModel.GetType().GetFields())
			{
				SerializedProperty sp = so.FindProperty (fi.Name);
				EditorGUILayout.PropertyField (sp);
			}

			if (GUI.changed)
			{
				so.ApplyModifiedProperties ();

				if (Application.isPlaying) {
					foreach (FieldInfo fi in model.inspectedModel.GetType().GetFields()) {
						if (HaveChanged(fi, fi.GetValue(model.inspectedModel))) {
							BindedData data = new BindedData (model.inspectedModel.GetType(), fi);

							try {
								Singleton<BindNotificator>.Instance.InvokeFor (data);
							} catch {
								Debug.LogWarning("Data not being inspected: " + data);
							}
						}
					}
				}
			}
		}
	}
}
