using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.Nfc;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using BMIapp.Activities;
using DevConnect.Common;
using Firebase.Auth;
using Google.Android.Material.Button;
using Google.Android.Material.TextField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;




namespace BMIapp
{
    [Activity(Label = "SignInActivity", Theme = "@style/AppTheme", MainLauncher = false)]
    public class SignInActivity : AppCompatActivity, IOnCompleteListener
    {
        EditText textInputLayout_email;
        EditText textInputLayout_pass;
        Button buttonSignIn;
        TextView textView_create;
        private const string PrefsName = "BMIappPreferences";
        private const string DialogShownKey = "EducationalDialogShown";
        FirebaseAuth auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.SignIn);
            auth = FirebaseRepository.getFirebaseAuth();

            textInputLayout_email = FindViewById<EditText>(Resource.Id.editTextEmailLogin);
            textInputLayout_pass = FindViewById<EditText>(Resource.Id.editTextPasswordLogin);
            buttonSignIn = FindViewById<Button>(Resource.Id.btnSignIn);
            textView_create = FindViewById<TextView>(Resource.Id.textButton);

            buttonSignIn.Click += ButtonSignIn_Click;
            textView_create.Click += TextView_create_Click;

            textInputLayout_email.TextChanged += delegate { ValidateField(textInputLayout_email); };
            textInputLayout_pass.TextChanged += delegate { ValidateField(textInputLayout_pass); };


        }

        private void ButtonSignIn_Click(object sender, EventArgs e)
        {
            string email = textInputLayout_email.Text.Trim();
            string pass = textInputLayout_pass.Text.Trim();

            if (string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(pass))
            {
                //textFieldFullname.Error = "Must not be empty.";
                textInputLayout_email.Error = "Must not be empty.";
                textInputLayout_pass.Error = "Must not be empty.";

                return;
            }

            auth.SignInWithEmailAndPassword(email, pass)
                              .AddOnCompleteListener(this, this);
        }


        private void TextView_create_Click(object sender, EventArgs e)
        {
            Intent create = new Intent(this, typeof(SignUpActivity));
            StartActivity(create);
        }



        public void SignInSucces()
        {
            ISharedPreferences prefs = GetSharedPreferences(PrefsName, FileCreationMode.Private);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutBoolean(DialogShownKey, false); // Reset the flag so it shows the dialog after login
            editor.Apply();

            // Navigate to the Dashboard
            Intent intent = new Intent(this, typeof(Dashboard));
            StartActivity(intent);
            Finish();
        }

        private void ValidateField(EditText field)
        {


            if (string.IsNullOrEmpty(field.Text))
            {
                buttonSignIn.Clickable = true;
                return;

            }

            if (field.Id == textInputLayout_email.Id)
            {
                bool isEmail = Regex.IsMatch(field.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

                if (!isEmail)
                {
                    textInputLayout_email.Error = "Must be a valid email address";
                    buttonSignIn.Clickable = true;
                    return;
                }

            }
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                Toast.MakeText(this, "Login was successful!", ToastLength.Short).Show();
                SignInSucces();
                Finish();
            }
            else
            {
                try
                {
                    throw task.Exception;
                }
                catch (FirebaseAuthWeakPasswordException e)
                {
                    Toast.MakeText(this, e.Message, ToastLength.Short).Show();
                }
                catch (FirebaseAuthInvalidCredentialsException e)
                {
                    Toast.MakeText(this, e.Message, ToastLength.Short).Show();

                }
                catch (FirebaseAuthUserCollisionException e)
                {
                    Toast.MakeText(this, e.Message, ToastLength.Short).Show();

                }
                catch (Exception e)
                {
                    Toast.MakeText(this, e.Message, ToastLength.Short).Show();

                }
            }

        }
    }
}