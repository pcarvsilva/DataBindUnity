using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
}
