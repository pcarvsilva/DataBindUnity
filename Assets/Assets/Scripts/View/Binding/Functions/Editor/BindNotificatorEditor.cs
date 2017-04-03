using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(BindNotificator))]
public class BindNotificatorEditor : Editor
{
	private ReorderableList _list;
	private bool isEnteringData = false;
	private string formKey = "New Key Name";
	private int formType = 0;

	private void OnEnable() {
		_list = new ReorderableList ((target as BindNotificator).inspectedElements, typeof(DictionaryEntry), true, true, true, true);

		_list.drawElementCallback =  
			(Rect rect, int index, bool isActive, bool isFocused) => {
			DictionaryEntry element = (DictionaryEntry) _list.list[index];
			rect.y += 2;
			EditorGUI.LabelField(
				new Rect(rect.x, rect.y, rect.width / 2, EditorGUIUtility.singleLineHeight),
				element.ToString());
			EditorGUI.ObjectField(
				new Rect(rect.x + rect.width  / 2, rect.y, rect.width / 2, EditorGUIUtility.singleLineHeight),
				element.inspected, typeof(Object));
		};
		_list.onAddCallback += AddItem;

	}

	private void AddItem(ReorderableList list) {
		isEnteringData = !isEnteringData;
	}

	private void RenderDataEntrance()
	{
		EditorGUILayout.BeginHorizontal ();
		formKey = EditorGUILayout.TextField(formKey);
		formType = EditorGUILayout.Popup(formType,BindNotificator.getAllPossibleTypes().ToArray());
		if (GUILayout.Button ("Save")) {
		
			DictionaryEntry de = new DictionaryEntry ();
			de.key = formKey;
			de.type = BindNotificator.getAllPossibleTypes()[formType];
			_list.list.Remove (de);
			_list.list.Add (de);
			formKey = " New Key Name";
			formType = 0;
			isEnteringData = false;
		}
		EditorGUILayout.EndHorizontal();
	}

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();
		serializedObject.Update ();
		_list.DoLayoutList ();
		serializedObject.ApplyModifiedProperties ();
		if (isEnteringData) {
			RenderDataEntrance();
		}
	}
}
