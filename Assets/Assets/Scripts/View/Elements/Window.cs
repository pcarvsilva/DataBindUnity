using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GraphicRaycaster))]
public class Window : MonoBehaviour {

	private Animator animator;

	public UICreationUnityEvent OnWindowShow;
	public UICreationUnityEvent OnWindowHide;

	// Use this for initialization
	void Awake() {
		animator = GetComponent<Animator>();
	}

    public void StackWindow(Window windowToCreate)
    {
        windowToCreate.Show();
        Hide();
        windowToCreate.GetComponent<Window>().OnWindowHide.AddListener(delegate {
            Show();
        });
    }

    // Update is called once per frame
    public void Show() {
        gameObject.SetActive(true);
        GetComponent<GraphicRaycaster>().enabled = true;
        animator = GetComponent<Animator>();
        if(animator != null)
            animator.SetTrigger("Show");
		OnWindowShow.Invoke();
	}

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

    private void CloseAndLeaveList()
    {
        CloseWindow();
        OnWindowHide.RemoveListener(CloseAndLeaveList);
    }



	public void Hide(){
        GetComponent<GraphicRaycaster>().enabled = false;
		if(animator != null)
            animator.SetTrigger("Hide");
        else
        {
            OnWindowHide.AddListener(CloseAndLeaveList);
        }
		OnWindowHide.Invoke();
	}

}
