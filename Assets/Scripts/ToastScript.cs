using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ToastScript : MonoBehaviour
{
    [SerializeField]
    private float timeout = 3.0f;
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private TMPro.TextMeshProUGUI toastTMP;


    private static ToastScript instance;

    private static float showTime;
    private static readonly LinkedList<ToastMessage> toastMessages = new LinkedList<ToastMessage>();

    public static void ShowToast(string message, float? timeout = null)
    {
        if (toastMessages.Count > 0 && toastMessages.Last.Value.message == message)
        {
            return;
        }
        toastMessages.AddLast(new ToastMessage
        {
            message = message,
            timeout = timeout ?? instance.timeout
        });

       
    }


    void Start()
    {   
        instance = this;
        GameState.SubscribeTrigger(BroadcastListener);
    }

    void Update()
    {
        if (showTime > 0.0f)
        {
            showTime -= Time.deltaTime;
            if (showTime <= 0.0f)
            {

                showTime = 0.0f;
                toastMessages.RemoveFirst();
                content.SetActive(false);
            }
        }
        else
        {
            if (toastMessages.Count > 0)
            {
                toastTMP.text = toastMessages.First.Value.message;
                showTime = toastMessages.First.Value.timeout;
                content.SetActive(true);

            }
        }


    }

    private void BroadcastListener(string type, object payload)
    {
        string[] toastedTypes = { "Battery" };
        if (toastedTypes.Contains(type))
        {
            ShowToast($"{type}: {payload.ToString() ?? ""}");
        }

        if(payload is TriggerPayload triggerPayload)
        {
            if(triggerPayload.notification != null)
            {
                ShowToast(triggerPayload.notification);
            }
        }
       
    }

    //private void KeyGotListener(string type, object payload)
    //{
    //    string[] toastedTypes = { "Key" };
    //    if (toastedTypes.Contains(type))
    //    {
    //        ShowToast($"You collected key: {payload.ToString() ?? "unknown"}");
    //        key1Image.enabled = true;

    //        switch (payload.ToString())
    //        {
    //            case "1":
    //                key1Image.enabled = true;
    //            break;
    //            case "2":
    //                key2Image.enabled = true;
    //            break;
    //        }
    //    }

    //}

    private void OnDestroy()
    {
        GameState.UnSubscribeTrigger(BroadcastListener);

    }

    private class ToastMessage
    {
        public string message;
        public float timeout;
    }
}