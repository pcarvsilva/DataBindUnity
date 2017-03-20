using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

[Serializable]
public class DataBinder : UnityEngine.Object {

	private static Dictionary<FieldInfo,List<DataBinder>> binds ; 
	public List<BindedData> inspectedTypes = new List<BindedData>();
	public Type type;

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

}
