using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;

public class UnityNotifications : MonoBehaviour
{
    public static string GrinningFace = "\U0001F600";
    public static string SmilingFace = "\u263A";

    private void Start()
    {
        AndroidNotificationCenter.CancelAllScheduledNotifications();
        createchannel();
        if (!PlayerPrefs.HasKey("welcome2"))
        {
            sendNotification();
            sendEmojiNotification();
            PlayerPrefs.SetInt("welcome2", 0);
        }


        retention_reminder();
        DontDestroyOnLoad(gameObject);
    }

    void createchannel()
    {
        var welcome = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(welcome);
    }

    public string[] msgs;

    void retention_reminder()
    {
        var notification = new AndroidNotification();
        notification.Title = "Its been a While";
        notification.Text = msgs[Random.Range(0, msgs.Length - 1)];
        notification.FireTime = System.DateTime.Now.AddHours(24);
        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }

    void sendNotification()
    {
        var notification = new AndroidNotification();
        notification.Title = "Welcome to CakeSmasher";
        notification.Text = "Hope You had fun !" + SmilingFace;
        notification.FireTime = System.DateTime.Now.AddSeconds(10);
        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }

    void sendEmojiNotification()
    {
        var notification = new AndroidNotification();
        notification.Title = "Emoji Test From Touseef :) ";
        notification.Text = SmilingFace + " " + GrinningFace;
        notification.FireTime = System.DateTime.Now.AddSeconds(10);
        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
}