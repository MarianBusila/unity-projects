using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Colorable : MonoBehaviour {

    private Material _material;

    // the colors used when highlighting or touching
    public Color highlightedColor = Color.green;
    public Color touchedColor = Color.blue;

    // the originial color of the material which will be restored if the object is not highlighted
    private Color _originalColor;

    // for tracking the state
    private bool _isHightlighted;
    private bool _isTouched;

	// Use this for initialization
	void Start () {
        _material = GetComponent<Renderer>().material;
        _originalColor = _material.color;

        gameObject.AddListener(EventTriggerType.PointerEnter, () => SetIsHighlighted(true));
        gameObject.AddListener(EventTriggerType.PointerExit, () => SetIsHighlighted(false));
        gameObject.AddListener(EventTriggerType.PointerDown, () => SetIsTouched(true));
        gameObject.AddListener(EventTriggerType.PointerUp, () => SetIsTouched(false));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetIsHighlighted(bool value)
    {
        _isHightlighted = value;
        UpdateColor();
    }

    public void SetIsTouched(bool value)
    {
        _isTouched = value;
        UpdateColor();
    }

    private void UpdateColor()
    {
        if(_isTouched)
        {
            _material.color = touchedColor;
        }
        else if(_isHightlighted)
        {
            _material.color = highlightedColor;
        }
        else
        {
            _material.color = _originalColor;
        }
    }

}

public static class EventExtensions
{
    public static void AddListener(this GameObject gameObject, EventTriggerType eventTriggerType, UnityAction action)
    {
        // get the   EventTrigger component
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>() ?? gameObject.AddComponent<EventTrigger>();

        // check to see if the entry already exists
        EventTrigger.Entry entry = eventTrigger.triggers.Find(e => e.eventID == eventTriggerType);

        if(entry == null)
        {
            // if it does not exist, create it and add it
            entry = new EventTrigger.Entry { eventID = eventTriggerType };
            eventTrigger.triggers.Add(entry);
        }

        // add the callback listener
        entry.callback.AddListener(_ => action());
    }
}
