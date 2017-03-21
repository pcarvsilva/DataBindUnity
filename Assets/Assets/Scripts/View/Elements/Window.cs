using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Window : MonoBehaviour {

	private Animator animator;

	public UICreationUnityEvent OnWindowShow;
	public UICreationUnityEvent OnWindowHide;

	// Use this for initialization
	void Awake() {
		animator = GetComponent<Animator>();
	}


	// Update is called once per frame
	void OnEnable() {
		if(gameObject.activeInHierarchy)
		{
			animator.SetTrigger("Show");
			OnWindowShow.Invoke();
		}
	}

	void OnDisable(){
		if(Application.isPlaying && gameObject != null){
			animator.SetTrigger("Hide");
			OnWindowHide.Invoke();
		}
	}
}
