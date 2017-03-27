using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Text))]
public class AttributeToText : MonoBehaviour{

	private Text text;
	public DataBinder binder = new DataBinder();

	public GameObject inspected;
	private GameObject _lastInspected;

	void Start()
	{
		text = GetComponent<Text>();
		binder.Start ();
		binder.onAttributeChange.AddListener (OnAttributeChange);
	}

	void Update()
	{
		if (_lastInspected == null && inspected != null)
		{
			_lastInspected = inspected;
			binder.RetrieveAttributeFrom(inspected);
			return;
		}

		if (_lastInspected != inspected) {
			_lastInspected = inspected;
			binder.RetrieveAttributeFrom(inspected);
		}
	}

	public void OnAttributeChange (object changed)
	{
		text.text = changed.ToString();
	}
}