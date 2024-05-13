using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

namespace BMIapp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            // Find the references to the buttons
            Button btnGetStarted = FindViewById<Button>(Resource.Id.btnGetStarted);
            Button btnSignup = FindViewById<Button>(Resource.Id.btnSignUp);

            // Attach click event handlers to the buttons

            btnGetStarted.Click += BtnGetStarted_Click;
            btnSignup.Click += BtnSignup_Click;



        }
        // Click event handler for "Sign Up" button


        private void BtnSignup_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SignUpActivity));
            StartActivity(intent);
        }



        // Click event handler for "Sign In" button

        private void BtnGetStarted_Click(object sender, EventArgs e)
        {
            Intent intent1 = new Intent(this, typeof(SignInActivity));
            StartActivity(intent1);
        }





    }
}