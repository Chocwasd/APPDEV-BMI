using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using DevConnect;
using System.Linq;
using System.Text;
using Xamarin.KotlinX.Coroutines.Intrinsics;
using Java.Lang;
using Xamarin.Grpc.OkHttp.Internal.Framed;
using BMIapp.Activities;

namespace BMIapp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class Dashboard : AppCompatActivity, IOnCompleteListener
    {

        TextView username;
        ImageButton imageBtnEnterCalculator;
        ImageButton imageButtonSettings, imageButtonLog;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.Dashboard);

            username = FindViewById<TextView>(Resource.Id.userName);
            imageBtnEnterCalculator = FindViewById<ImageButton>(Resource.Id.imageButtonCalculate);
            imageButtonSettings = FindViewById<ImageButton>(Resource.Id.imageButtonSettings);
            imageButtonLog = FindViewById<ImageButton>(Resource.Id.imageButtonLog);


            imageBtnEnterCalculator.Click += ImageBtnEnterCalculator_Click;
            imageButtonSettings.Click += ImageButtonSettings_Click;
            imageButtonLog.Click += ImageButtonLog_Click;
            RetrieveUsernameFromFirestore();


        }

        private void ImageButtonLog_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Track));
            StartActivity(intent);
        }

        private void ImageButtonSettings_Click(object sender, EventArgs e)
        {
            // Get the username from the TextView
            string usernameValue = username.Text;

            // Start the next activity (Calculator) and pass the username as an extra
            Intent intent = new Intent(this, typeof(SettingsPage));
            intent.PutExtra("USERNAME_EXTRA", usernameValue);
            StartActivity(intent);
            Finish();
        }

        private void RetrieveUsernameFromFirestore()
        {
            Firebase.Auth.FirebaseAuth auth = DevConnect.Common.FirebaseRepository.getFirebaseAuth();
            Firebase.Auth.FirebaseUser currentUser = auth.CurrentUser;

            if (currentUser != null)
            {
                FirebaseFirestore db = DevConnect.Common.FirebaseRepository.GetFirebaseFirestore();
                DocumentReference docRef = db.Collection("Users").Document(currentUser.Uid);
                docRef.Get().AddOnCompleteListener(this);
            }
            else
            {
                // No user is signed in
                // Redirect to login activity or handle accordingly
            }
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                DocumentSnapshot snapshot = (task.Result as DocumentSnapshot);
                if (snapshot.Exists())
                {
                    string userUsername = snapshot.GetString("fullname");
                    RunOnUiThread(() =>
                    {
                        username.Text = userUsername;
                    });
                }
                else
                {
                    // Document does not exist
                }
            }
            else
            {
                // Handle failure
            }
        }

        private void ImageBtnEnterCalculator_Click(object sender, EventArgs e)
        {
            // Get the username from the TextView
            string usernameValue = username.Text;

            // Start the next activity (Calculator) and pass the username as an extra
            Intent intent = new Intent(this, typeof(Calculator));
            intent.PutExtra("USERNAME_EXTRA", usernameValue);
            StartActivity(intent);
            
        }

        public override void OnBackPressed()
        {
            // Do nothing when the back button is pressed
        }
    }
}