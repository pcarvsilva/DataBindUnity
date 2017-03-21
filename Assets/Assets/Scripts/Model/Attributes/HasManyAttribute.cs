using UnityEngine;
using System.Collections;
using System;

[AttributeUsage(AttributeTargets.Field)]
public class HasManyAttribute : Attribute {

	public Type type;
	public HasManyAttribute(Type _type)
	{
		type = type;
	}
}
