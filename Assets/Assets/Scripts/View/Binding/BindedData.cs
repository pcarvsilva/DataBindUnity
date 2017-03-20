using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public class BindedData {

	public Type type;
	public FieldInfo field;

	public BindedData(Type _type,FieldInfo _field)
	{
		type = _type;
		field = _field;
	}
}
