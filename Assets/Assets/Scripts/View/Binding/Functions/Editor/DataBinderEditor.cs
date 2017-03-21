using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEditorInternal;

[CustomPropertyDrawer(typeof(DataBinder),true)]
public class DataBinderEditor : PropertyDrawer{
	

	private ReorderableList _list;
	public List<string> possibleTypes;
	private DataBinder binder;
	public bool fold;

	private void addCallback(object _field)
	{
		if(_list.count > 0) {
			FieldInfo field = _field as FieldInfo;
			Type lastType = (_list.list[_list.list.Count- 1] as BindedData).field.FieldType;
			_list.list.Add(new BindedData(lastType,field));
		}else{
			FieldInfo field = _field as FieldInfo;
			_list.list.Add(new BindedData(binder.type,field));
		}
	}

	private List<string> getAllPossibleTypes()
	{
		List<string> list = new List<string>();
		foreach(Type t in typeof(DataBinder).Assembly.GetTypes())
		{
			if (t.IsDefined(typeof(DataClass),true)) 
				list.Add(t.ToString());

		}
		return list;
	}

	public List<Type> getPossibleTypesValue()
	{
		List<Type> list = new List<Type>();
		foreach(Type t in typeof(DataBinder).Assembly.GetTypes())
		{
			if (t.IsDefined(typeof(DataClass),true)) 
				list.Add(t);
			
		}
		return list;
	}

	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
	{
		float result = 25.0f;
		if (_list != null)
		{
			result += _list.GetHeight();
		}
		return result;
	}

	private string parseElementsName()
	{
		if(binder.type == null) return "None";
		string value = binder.type.Name;
		foreach(BindedData data in binder.inspectedTypes)
		{
			value+= ">" + data.field.Name.ToString();
		}
		return value;
	}

	private void OnEnable() {
		_list = new ReorderableList(binder.inspectedTypes, typeof(BindedData), false, true, true, true);

		_list.drawElementCallback =  
			(Rect rect, int index, bool isActive, bool isFocused) => {	
			BindedData data = _list.list[index] as BindedData;

			if (data == null) {
				Debug.Log("data is null");
			}

			if (data.field == null) {
				Debug.Log("data field is null");
			}

			EditorGUI.SelectableLabel(
				new Rect(rect.x + 60, rect.y, rect.width - 60 - 30, EditorGUIUtility.singleLineHeight),
				data.field.Name.ToString());
		};
		_list.onAddDropdownCallback = (Rect buttonRect, ReorderableList l) => {  
			var menu = new GenericMenu();
			if(_list.list.Count > 0)
			{
				BindedData data = _list.list[l.list.Count -1] as BindedData;
				var guids = data.field.FieldType.GetFields();
				foreach (FieldInfo guid in guids) {
					menu.AddItem(new GUIContent(guid.Name),false,addCallback,guid);
				}
				menu.ShowAsContext();
			}
			else{
				var guids = binder.type.GetFields();
				foreach (FieldInfo guid in guids) {
					menu.AddItem(new GUIContent(guid.Name),false,addCallback,guid);
				}
				menu.ShowAsContext();
			}
		};
		_list.onRemoveCallback = (ReorderableList l) => {  
			l.list.RemoveAt(l.index);
		};
	}

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
		EditorGUI.BeginProperty (position, label, property);

		binder = fieldInfo.GetValue(property.serializedObject.targetObject) as DataBinder;
		if(_list == null)
		{
			OnEnable();
		}

		_list.DoList(new Rect(position.x,position.y,position.width,position.height));

		int index = 0;
		possibleTypes = getAllPossibleTypes();
		if (binder.type != null)
			index = possibleTypes.IndexOf(binder.type.ToString());
		if (index == -1) { 
			index = 0;
			CleanList ();
		}
			
		int typeIndex = EditorGUI.Popup(new Rect (position.x +35, position.y +2, 90, 20), index, possibleTypes.ToArray());
		if (typeIndex != index)
			CleanList ();
		
		binder.type = getPossibleTypesValue()[typeIndex];

		EditorGUI.EndProperty();

		/*
		EditorGUI.BeginProperty (position, label, property);
		binder = fieldInfo.GetValue(property.serializedObject.targetObject) as DataBinder;
		if(_list == null)
		{
			OnEnable();
		}
		int indentLevel = EditorGUI.indentLevel;
		int index =0;
		possibleTypes = getAllPossibleTypes();
		if(binder.type != null)
			index = possibleTypes.IndexOf(binder.type.ToString());
		if(index == -1) 
			index =0;
		int typeIndex = EditorGUI.Popup(new Rect (position.x +35, position.y +2, 90, 20),index,possibleTypes.ToArray());
		binder.type = getPossibleTypesValue()[typeIndex];
		EditorGUI.indentLevel = 0;
		_list.DoList(new Rect(position.x,position.y,position.width,position.height - 30 ));
		EditorGUI.indentLevel = indentLevel;

		EditorGUI.EndProperty();
		*/
	}

	private void CleanList() {
		_list.list.Clear ();
	}
}
