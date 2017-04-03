using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class BindNotificator : MonoBehaviour {

	private Dictionary<BindedData,DataChangeUnityEvent> listeners = new Dictionary<BindedData, DataChangeUnityEvent>(new BindedDataComparer());

	public void Register(DataBinder binder,UnityAction rebind){
		foreach(BindedData data in binder.inspectedTypes){
			if(listeners.ContainsKey(data))
				listeners[data].AddListener(rebind);
			else{
				DataChangeUnityEvent myEvent = new DataChangeUnityEvent ();
				myEvent.AddListener (rebind);
				listeners.Add(data,myEvent);
			}
		}
	}

	public void InvokeFor(BindedData data)
	{
		try{
			listeners [data].Invoke ();
		}catch{
			throw new UnityException ("Data not found for: " + data.ToString());
		}
	}

	/// <summary>
	/// Gets all possible types.
	/// </summary>
	/// <returns>All possible types with data class.</returns>
	public static List<string> getAllPossibleTypes()
	{
		List<string> list = new List<string>();
		foreach(Type t in typeof(DataBinder).Assembly.GetTypes())
		{
			if (t.IsDefined(typeof(DataClass),true)) 
				list.Add(t.ToString());

		}
		return list;
	}

	/// <summary>
	/// The inspected elements.
	/// </summary>
	[HideInInspector]
	public List<DictionaryEntry> inspectedElements = new List<DictionaryEntry>();
}

[System.Serializable]
public class DictionaryEntry  {
	public string key = "";
	public string type;
	[HideInInspector]
	public UnityEngine.Object inspected;

	public override string ToString ()
	{
		return string.Format ( key + " (" + type + ")");
	}

	public override bool Equals (object other)
	{
		if(other is DictionaryEntry) 
			return key.Equals((other as DictionaryEntry).key);
		return false;
	}
}