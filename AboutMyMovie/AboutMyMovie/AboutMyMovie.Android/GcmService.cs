using Android.App;
using Android.Content;
using Android.Media;
using Android.Support.V4.App;
using Android.Util;
using Gcm.Client;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]

namespace AboutMyMovie.Droid
{
    [BroadcastReceiver(Permission = Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "@PACKAGE_NAME@" })]
    public class PushHandlerBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
    {
        public static string[] SenderIds = new string[] { "49597656424" };
    }

    [Service]
    public class GcmService : GcmServiceBase
    {
        public readonly MobileServiceClient Client = new MobileServiceClient("http://pushmaratonaxamarin.azurewebsites.net");
        public static string RegistrationId { get; private set; }
        public GcmService() : base(PushHandlerBroadcastReceiver.SenderIds) { }
        public GcmService(params string[] senderIds) : base(senderIds)
        {
        }
        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose("PushHandlerBroadcastReceiver", "GCM Registered: " + registrationId);
            RegistrationId = registrationId;
            var push = Client.GetPush();
            MainActivity.CurrentActivity.RunOnUiThread(() => Register(push, null));
        }
        public async void Register(Push push, IEnumerable<string> tags)
        {
            try
            {
                const string templateBodyGcm = "{\"data\":{\"message\":\"$(messageParam)\"}}";
                var templates = new JObject
                {
                    ["genericMessage"] = new JObject
                    {
                        {"body", templateBodyGcm}
                    }
                };
                await push.RegisterAsync(RegistrationId, templates);
                Log.Info("Push Installation Id", push.InstallationId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debugger.Break();
            }
        }
        protected override void OnMessage(Context context, Intent intent)
        {
            Log.Info("PushHandlerBroadcastReceiver", "GCM Message Received!");
            var msg = new StringBuilder();
            if (intent?.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                    msg.AppendLine(key + "=" + intent.Extras.Get(key));
            }

            //Store the message
            var prefs = GetSharedPreferences(context.PackageName, FileCreationMode.Private);
            var edit = prefs.Edit();
            edit.PutString("last_msg", msg.ToString());
            edit.Commit();

            var message = intent.Extras.GetString("message");

            if (!string.IsNullOrEmpty(message))
            {
                CreateNotification("About My Movie", message);
                return;
            }

            var msg2 = intent.Extras.GetString("msg");

            if (!string.IsNullOrEmpty(msg2))
            {
                CreateNotification("About My Movie!", msg2);
                return;
            }

            CreateNotification("Unknown message details", msg.ToString());
        }
        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Error("PushHandlerBroadcastReceiver", "Unregistered RegisterationId : " + registrationId);
        }
        protected override void OnError(Context context, string errorId)
        {
            Log.Error("PushHandlerBroadcastReceiver", "GCM Error: " + errorId);
        }
        public void CreateNotification(string title, string desc)
        {
            //Create notification
            var notificationManager = GetSystemService(NotificationService) as NotificationManager;
            //Create an intent to show ui
            var uiIntent = new Intent(this, typeof(MainActivity));
            //Use Notification Builder
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this);
            //Create the notification
            //we use the pending intent, passing our ui intent over which will get called
            //when the notification is tapped.
            var notification = builder.SetContentIntent(PendingIntent.GetActivity(this, 0, uiIntent, 0))
                .SetSmallIcon(Android.Resource.Drawable.SymActionEmail)
                .SetTicker(title)
                .SetContentTitle(title)
                .SetContentText(desc)
               //Set the notification sound
               .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
               //Auto cancel will remove the notification once the user touchesit
               .SetAutoCancel(true).Build();
            //Show the notification
            notificationManager.Notify(1, notification);
        }
    }
}