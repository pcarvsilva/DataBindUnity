using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DataClass]
public class Author : MonoBehaviour {

	public string Name;

	[HasMany(typeof(Book))]
	public List<Book> Books;

	public Book bestSellerBook;
}
