using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameState
{
    public static bool isFpv { get; set; }
    //public static float flashCharge { get; set; }
    public static Dictionary<string, bool> collectedKeys { get; } = new Dictionary<string, bool>();

    private static float _effectsVolume = 1.0f;

    public static float effectsVolume
    {
        get => _effectsVolume;
        set
        {
            if (_effectsVolume != value)
            {
                _effectsVolume = value;
                NotifySubscribers(nameof(effectsVolume));
            }
        }
    }

    private static float _ambientVolume = 1.0f;

    public static float ambientVolume
    {
        get => _ambientVolume;
        set
        {
            if (_ambientVolume != value)
            {
                _ambientVolume = value;
                NotifySubscribers(nameof(ambientVolume));
            }
        }
    }

    private static float _musicVolume = 1.0f;

    public static float musicVolume
    {
        get => _musicVolume;
        set
        {
            if (_musicVolume != value)
            {
                _musicVolume = value;
                NotifySubscribers(nameof(musicVolume));
            }
        }
    }

    private static float _sensitivityX = 0.5f;
    public static float sensitivityX
    {
        get => _sensitivityX;
        set
        {
            if (_sensitivityX != value)
            {
                _sensitivityX = value;
                NotifySubscribers(nameof(sensitivityX));
            }
        }
    }

    private static float _sensitivityY = 0.5f;
    public static float sensitivityY
    {
        get => _sensitivityY;
        set
        {
            if (_sensitivityY != value)
            {
                _sensitivityY = value;
                NotifySubscribers(nameof(sensitivityY));
            }
        }
    }


    public static float _score = 0;
    public static float score
    {
        get => _score;
        set
        {
            if (_score != value)
            {
                _score = value;
                NotifySubscribers(nameof(score));
            }
        }
    }

        #region Difficulty

    private static GameDifficulty _difficulty = GameDifficulty.Normal;
    public static GameDifficulty difficulty
    {
        get => _difficulty;
        set
        {
            if (_difficulty != value)
            {
                _difficulty = value;
                NotifySubscribers(nameof(_difficulty));
            }
        }
    }
    public enum GameDifficulty
    {
        Easy, Normal,Hard

    }

    #endregion


    private static bool _isMuted = false;

    public static bool isMuted
    {
        get => _isMuted;
        set
        {
            if (_isMuted != value)
            {
                _isMuted = value;
                NotifySubscribers(nameof(isMuted));
            }
        }
    }


    private readonly static Dictionary<string, List<Action>> subscribers = new Dictionary<string, List<Action>>();

    private static void NotifySubscribers(String propertyName)
    {
        if (subscribers.ContainsKey(propertyName))
        {
            foreach (var action in subscribers[propertyName])
            {
                action();
            }
        }
    }



    public static void Subscribe(string propertyName, Action action)
    {
        if (!subscribers.ContainsKey(propertyName))
        {
            subscribers[propertyName] = new List<Action>();
        }
        subscribers[propertyName].Add(action);
    }

    public static void Subscribe(Action action, params string[] propertyNames)
    {
        if (propertyNames.Length == 0)
        {
            throw new ArgumentException(
                                        $"{nameof(propertyNames)} must have at least 1 value");
        }

        foreach (var propertyName in propertyNames)
        {
            Subscribe(propertyName, action);
        }

    }

    public static void UnSubscribe(string propertyName, Action action)
    {
        if (subscribers.ContainsKey(propertyName))
        {
            subscribers[propertyName].Remove(action);

        }
    }

    public static void UnSubscribe(Action action, params string[] propertyNames)
    {
        if (propertyNames.Length == 0)
        {
            throw new ArgumentException(
                                        $"{nameof(propertyNames)} must have at least 1 value");
        }

        foreach (var propertyName in propertyNames)
        {
            UnSubscribe(propertyName, action);
        }

    }

  

    #region Game Events

    private const string broadcastKey = "Broadcast";

    public static void TriggerEvent(string type, object payload = null)
    {
        if (eventListeners.ContainsKey(type))
        {
            foreach (var eventListener in eventListeners[type])
            {
                eventListener(type, payload);
            }
        }
        if (eventListeners.ContainsKey(broadcastKey))
        {
            foreach (var eventListener in eventListeners[broadcastKey])
            {
                eventListener(type, payload);
            }
        }
    }
    private static Dictionary<string, List<Action<string, object>>> eventListeners = new();
    public static void SubscribeTrigger(Action<string, object> action, params string[] types)
    {

        if (types.Length == 0)
        {
            types = new string[1] { broadcastKey };
        }
        foreach (var type in types)
        {
            if (!eventListeners.ContainsKey(type))
            {
                eventListeners[type] = new List<Action<string, object>>();
            }
            eventListeners[type].Add(action);
        }
    }

    public static void UnSubscribeTrigger(Action<string, object> action, params string[] types)
    {

        if (types.Length == 0)
        {
            types = new string[1] { broadcastKey };
        }
        foreach (var type in types)
        {
            if (eventListeners.ContainsKey(type))
            {
                eventListeners[type].Remove(action);
                if (eventListeners[type].Count == 0)
                {
                    eventListeners.Remove(type);
                }
            }
        }

        #endregion

    }
}