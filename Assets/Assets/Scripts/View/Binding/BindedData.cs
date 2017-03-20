using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

[Serializable]
public class BindedData {

	[SerializeField]
	private string _type;

	[SerializeField]
	private string _field;

	public Type type {
		get {
			Debug.Log ("TYPE: " + _type);
			return Type.GetType (_type);
		}

		set {
			_type = value.ToString ();
		}
	}

	public FieldInfo field {
		get {
			return type.GetField (_field);
		}

		set {
			_field = value.ToString ();
		}
	}

	public BindedData(Type type, FieldInfo field)
	{
		_type = type.ToString ();
		_field = field.Name.ToString ();
	}
}
