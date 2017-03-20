using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

[Serializable]
public class DataBinder {

	//private static Dictionary<FieldInfo,List<DataBinder>> binds ; 
	public List<BindedData> inspectedTypes;

	[SerializeField]
	private string _type;

	public Type type {
		get {
			if (_type == null)
				return null;
			return Type.GetType (_type);
		}

		set {
			_type = value.ToString ();
		}
	}

	public DataBinder() {
		inspectedTypes = new List<BindedData> ();
	}

	/*
	public static void NotifyField()
	{

	}

	public void Bind()
	{
		foreach(BindedData data in inspectedTypes)
		{
			if(binds[data.field] != null)
				binds[data.field].Remove(this);
		}
		
	}
	*/
}
