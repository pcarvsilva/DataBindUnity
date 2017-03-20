using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventTriggerHelper : MonoBehaviour {

	public delegate void Del(BaseEventData data);

	public static void InsertFunctionIntoCallback(EventTrigger trigger,EventTriggerType type,Del func){

		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = type;
		entry.callback = new EventTrigger.TriggerEvent();
		UnityEngine.Events.UnityAction<BaseEventData> call = new UnityEngine.Events.UnityAction<BaseEventData>(func);
		entry.callback.AddListener(call);
		trigger.triggers.Add(entry);
	}
	
}
