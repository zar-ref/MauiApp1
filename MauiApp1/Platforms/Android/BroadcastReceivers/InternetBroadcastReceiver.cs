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

    [BroadcastReceiver(Name = "com.myapp.whatever.InternetBroadcastReceiver", Enabled = true)]
    [IntentFilter(new[] { Intent.ActionBootCompleted })]
    //[BroadcastReceiver(Enabled = true, Permission = Manifest.Permission.ReceiveBootCompleted)]
    //[IntentFilter(new[] { Intent.ActionBootCompleted }, Priority = (int)IntentFilterPriority.HighPriority, Categories = new[] { Intent.CategoryDefault })]

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
