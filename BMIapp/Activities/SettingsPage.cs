﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Xamarin.Essentials.Platform;
using Intent = Android.Content.Intent;

namespace BMIapp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class SettingsPage : AppCompatActivity
    {

        ImageButton btnAboutus;
        ImageButton btnRateUs;
        ImageButton btnSignOut, btnHome;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.SettingsApp);


            btnAboutus = FindViewById<ImageButton>(Resource.Id.imageButtonAboutUs);
            btnRateUs = FindViewById<ImageButton>(Resource.Id.imageButtonRateUs);
            btnSignOut = FindViewById<ImageButton>(Resource.Id.imageButtonSignOut);
            btnHome = FindViewById<ImageButton>(Resource.Id.imageButtonHome);

            btnSignOut.Click += BtnSignOut_Click;
            btnRateUs.Click += BtnRateUs_Click;
            btnAboutus.Click += BtnAboutus_Click;
            btnHome.Click += BtnHome_Click;
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Dashboard));
            StartActivity(intent);
        }

        private void BtnAboutus_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AboutUs));
            StartActivity(intent);
        }

        private void BtnRateUs_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void BtnSignOut_Click(object sender, EventArgs e)
        {
            FirebaseAuth.Instance.SignOut();

            // Redirect to the login activity and clear the back stack
            Android.Content.Intent intent = new Android.Content.Intent(this, typeof(SignInActivity));
            intent.AddFlags(Android.Content.ActivityFlags.ClearTask | Android.Content.ActivityFlags.NewTask);
            StartActivity(intent);
            Finish();

            // Close the current activity to prevent going back to it after signing out
        }

        public override void OnBackPressed()
        {
            // Do nothing when the back button is pressed
        }
    }
}