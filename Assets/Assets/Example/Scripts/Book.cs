﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DataClass]
public class Book : MonoBehaviour {

	public Author author;
	public string name;
	public List<Page> pages;
}
