﻿using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using RS.Shared;
using Microsoft.AspNetCore.SignalR.Client;
using Android.Content;

namespace RS.Mobile.Droid
{
    [Activity(Label = "RS.Mobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            RegisterReceiver(new BackgroundTasks(), new IntentFilter("com.rs.mobile"));

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnResume()
        {
            if (RsClient.HubConnection == null || RsClient.HubConnection.State != HubConnectionState.Connected)
            {
                RsClient.BuildOrReBuildHub();
                RsClient.StartAsync();
            }

            base.OnResume();
        }
    }
}