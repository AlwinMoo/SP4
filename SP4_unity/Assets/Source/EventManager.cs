using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{

    //private Dictionary<string, UnityEvent> eventDictionary;
    private Dictionary<string, Dictionary<string, UnityEvent>> UserDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (UserDictionary == null)
        {
            UserDictionary = new Dictionary<string, Dictionary<string, UnityEvent>>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener, string userTag)
    {
        UnityEvent thisEvent = null;
        Dictionary<string, UnityEvent> thisDictionary = new Dictionary<string, UnityEvent>();
        if (!instance.UserDictionary.TryGetValue(userTag, out thisDictionary))
        {
            if (userTag != null)
            {
                thisDictionary = new Dictionary<string, UnityEvent>();
                instance.UserDictionary.Add(userTag, thisDictionary);
            }
        }

        if (thisDictionary != null && thisDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            thisDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener, string userTag)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        Dictionary<string, UnityEvent> thisDictionary;
        if (!instance.UserDictionary.TryGetValue(userTag, out thisDictionary))
            return;

        if (thisDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, string userTag)
    {
        UnityEvent thisEvent = null;
        Dictionary<string, UnityEvent> thisDictionary;

        if (!instance.UserDictionary.TryGetValue(userTag, out thisDictionary))
            return;

        if (thisDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}