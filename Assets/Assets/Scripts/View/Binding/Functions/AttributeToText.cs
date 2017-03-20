using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Text))]
public class AttributeToText : MonoBehaviour{

	private Text text;
	public DataBinder binder = new DataBinder();

	void Start()
	{
		text = GetComponent<Text>();
	}

	public void OnAttributeChange (object changed)
	{
		text.text = changed.ToString();
	}
}