using Android;
using Android.Widget;
using global::Android.App;
using global::Android.Content;
using global::Android.Net;
using MauiApp1.Platforms.Android.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Platforms.Android.BroadcastReceivers
{

    //https://stackoverflow.com/questions/44383983/how-to-programmatically-enable-auto-start-and-floating-window-permissions
    [BroadcastReceiver(Name = "com.myapp.whatever.InternetBroadcastReceiver", Enabled = true, DirectBootAware = true, Exported = true)]

    public class InternetBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            //if (intent.action != connectivitymanager.connectivityaction)
            //    return;
            //intent startserviceintent = new intent(context, typeof(backgroundservice));
            //startserviceintent.setaction("start");
            //context.startservice(startserviceintent);


            Toast.MakeText(context, "Rebooted", ToastLength.Long).Show();
            if (Intent.ActionBootCompleted == intent.Action)
            {
                Intent activityIntent = new Intent(context, typeof(MainActivity));
                activityIntent.AddFlags(ActivityFlags.NewTask);
                context.StartActivity(activityIntent);


            }

        }

    }
}
