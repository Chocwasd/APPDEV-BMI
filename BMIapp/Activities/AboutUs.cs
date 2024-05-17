using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMIapp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class AboutUs : AppCompatActivity
    {

        ImageButton btnBack;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.aboutUs);

            btnBack = FindViewById<ImageButton>(Resource.Id.imageButtonBack);

            btnBack.Click += BtnBack_Click;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SettingsPage));
            StartActivity(intent);
        }

        public override void OnBackPressed()
        {
            // Do nothing when the back button is pressed
        }

    }
}