using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class CreateTooltip : MonoBehaviour {

	public GameObject tooltip;
	public RectTransform anchor;

	void Start()
	{
		EventTrigger myTrigger = GetComponent<EventTrigger>();

		EventTriggerHelper.InsertFunctionIntoCallback(myTrigger,EventTriggerType.PointerEnter,show);
		EventTriggerHelper.InsertFunctionIntoCallback(myTrigger,EventTriggerType.PointerExit,hide);
	}

	void OnDisable(){
		if(Application.isPlaying && gameObject != null){
			hide(null);
		}
	}
	public void show(BaseEventData data)
	{
		tooltip.SetActive(true);
		tooltip.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = anchor.position;
		Animator tooltipAnimator = tooltip.GetComponent<Animator>();
		if(tooltipAnimator)
			tooltipAnimator.SetTrigger("Show");
	}
	public void hide(BaseEventData data)
	{
		tooltip.SetActive(false);
		Animator tooltipAnimator = tooltip.GetComponent<Animator>();
		if(tooltipAnimator)
			tooltipAnimator.SetTrigger("Hide");
	}
}
