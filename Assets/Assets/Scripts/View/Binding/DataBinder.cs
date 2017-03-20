using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

[Serializable]
public class DataBinder {
	public BindDataUnityEvent onAttributeChange;
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
		onAttributeChange = new BindDataUnityEvent ();
	}

	public void RetrieveAttributeFrom(GameObject obj) {
		Component c = obj.GetComponent(type);
		if (c == null)
			throw new MissingComponentException(type.ToString());

		FieldInfo fi = inspectedTypes [0].field;
		object o = fi.GetValue (c);
		for (int i = 1; i < inspectedTypes.Count; ++i) {
			fi = inspectedTypes [i].field;
			o = fi.GetValue(o);
		}

		this.onAttributeChange.Invoke (o);
	}
}
