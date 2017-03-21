using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class CreateWindow : MonoBehaviour {

	public GameObject windowToCreate;

	void Start()
	{
		GetComponent<Button>().onClick.AddListener(OnClick);
	}

	public void OnClick()
	{
		windowToCreate.SetActive(true);
		transform.root.gameObject.SetActive(false);
		windowToCreate.GetComponent<Window>().OnWindowHide.AddListener(delegate {
			transform.root.gameObject.SetActive(true);
		});
	}
	
}
