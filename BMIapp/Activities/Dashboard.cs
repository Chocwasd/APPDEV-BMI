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
using AlertDialog = Android.App.AlertDialog;

namespace BMIapp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class Dashboard : AppCompatActivity, IOnCompleteListener
    {

        TextView username;
        ImageButton imageBtnEnterCalculator;
        ImageButton imageButtonSettings, imageButtonLog;
        private const string PrefsName = "BMIappPreferences";
        private const string DialogShownKey = "EducationalDialogShown";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.Dashboard);

            // Check if the dialog has already been shown
            ISharedPreferences prefs = GetSharedPreferences(PrefsName, FileCreationMode.Private);
            bool dialogShown = prefs.GetBoolean(DialogShownKey, false);

            username = FindViewById<TextView>(Resource.Id.userName);
            imageBtnEnterCalculator = FindViewById<ImageButton>(Resource.Id.imageButtonCalculate);
            imageButtonSettings = FindViewById<ImageButton>(Resource.Id.imageButtonSettings);
            imageButtonLog = FindViewById<ImageButton>(Resource.Id.imageButtonLog);


            imageBtnEnterCalculator.Click += ImageBtnEnterCalculator_Click;
            imageButtonSettings.Click += ImageButtonSettings_Click;
            imageButtonLog.Click += ImageButtonLog_Click;
            RetrieveUsernameFromFirestore();

            ShowEducationalDialogIfNeeded();
        }

        private void ShowEducationalDialogIfNeeded()
        {
            // Check if the dialog has already been shown
            ISharedPreferences prefs = GetSharedPreferences(PrefsName, FileCreationMode.Private);
            bool dialogShown = prefs.GetBoolean(DialogShownKey, false);

            if (!dialogShown)
            {
                ShowEducationalPurposesDialog();
            }
        }

        private void ShowEducationalPurposesDialog()
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle("Disclaimer");
            builder.SetMessage("This app is for educational purposes only. Please consult with a healthcare provider for any health-related advice.");
            builder.SetPositiveButton("OK", (sender, e) =>
            {
                // Update the shared preferences to indicate that the dialog has been shown
                ISharedPreferences prefs = GetSharedPreferences(PrefsName, FileCreationMode.Private);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutBoolean(DialogShownKey, true);
                editor.Apply();
            });

            AlertDialog dialog = builder.Create();
            dialog.Show();
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