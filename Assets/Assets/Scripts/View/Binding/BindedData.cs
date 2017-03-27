using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using System.Collections.Generic;

[Serializable]
public class BindedData {

	[SerializeField]
	private string _type;

	[SerializeField]
	private string _field;

	public Type type {
		get {
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

	public override string ToString ()
	{
		return string.Format ("[BindedData: type={0}, field={1}]", type, field);
	}

	public BindedData(Type type, FieldInfo field)
	{
		_type = type.ToString ();
		_field = field.Name.ToString ();
	}
}

public class BindedDataComparer : IEqualityComparer<BindedData>
{
	public bool Equals (BindedData x, BindedData y)
	{
		return x.ToString().Equals(y.ToString());
	}

	public int GetHashCode(BindedData x)
	{
		return x.ToString().GetHashCode();
	}
}