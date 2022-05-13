﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using MauiApp1.Platforms.Android.Services;

namespace MauiApp1;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density, Exported = true)]
public class MainActivity : MauiAppCompatActivity
{
    

}