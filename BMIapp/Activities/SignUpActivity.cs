using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Firebase;
using Firebase.Auth;
using AndroidX.AppCompat.App;
using Android.Widget;
using Firebase.Firestore;
using System;
using Google.Android.Material.Snackbar;
using Android.Gms.Tasks;
using Java.Util;
using DevConnect.Common;

namespace BMIapp
{
    [Activity(Label = "SignUpActivity", Theme = "@style/AppTheme", MainLauncher = false)]
    public class SignUpActivity : AppCompatActivity, View.IOnClickListener, IOnCompleteListener
    {
        EditText editTextFullname, editTextEmail, editTextPassword, editConfirmPassword;
        Button btnSignUp;
        FirebaseFirestore db;
        FirebaseAuth auth;
        TextView textViewSignIn, textButtonLogIn;
        LinearLayout register;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.SignUp);

            editTextFullname = FindViewById<EditText>(Resource.Id.editTextFullName);
            editTextEmail = FindViewById<EditText>(Resource.Id.editTextEmail);
            editTextPassword = FindViewById<EditText>(Resource.Id.editTextPassword);
            editConfirmPassword = FindViewById<EditText>(Resource.Id.editTextConfirmPassword);
            btnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            textViewSignIn = FindViewById<TextView>(Resource.Id.textViewSignIn);
            register = FindViewById<LinearLayout>(Resource.Id.signupregister);
            textButtonLogIn = FindViewById<TextView>(Resource.Id.textButton);

            auth = FirebaseRepository.getFirebaseAuth();
            db = FirebaseRepository.GetFirebaseFirestore();

            btnSignUp.SetOnClickListener(this);
            textViewSignIn.SetOnClickListener(this);

            textButtonLogIn.Click += TextButtonLogIn_Click;
        }

        private void TextButtonLogIn_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(SignInActivity));
        }

        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.textViewSignIn)
            {
                StartActivity(typeof(SignInActivity));
            }
            else if (v.Id == Resource.Id.btnSignUp)
            {
                string email = editTextEmail.Text;
                string password = editTextPassword.Text;
                string confirmPassword = editConfirmPassword.Text;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
                {
                    ShowSnackbar("Please enter your email/password");
                }
                else if (confirmPassword != password)
                {
                    ShowSnackbar("Confirm Password doesn't match with password");
                }
                else if (password.Length < 6)
                {
                    ShowSnackbar("Please make sure your password is at least 6 characters long");
                }
                else if (!(email.Contains("@mail.com") || email.Contains("@gmail.com") || email.Contains("@yahoo.com")))
                {
                    ShowSnackbar("Please make sure your email is valid");
                }
                else
                {
                    RegisterFirestore(email, password);
                }
            }
        }

        private void RegisterFirestore(string email, string password)
        {
            auth.CreateUserWithEmailAndPassword(email, password).AddOnCompleteListener(this, this);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                HashMap map = new HashMap();
                map.Put("fullname", editTextFullname.Text);
                map.Put("email", editTextEmail.Text);
                map.Put("password", editConfirmPassword.Text);

                DocumentReference docRef = db.Collection("Users").Document(auth.CurrentUser.Uid);
                docRef.Set(map);

                ShowSnackbar("Registered Successfully");

            }
            else
            {
                Exception exception = task.Exception;
                string message = exception?.Message ?? "Register failed";
                ShowSnackbar(message);
            }
        }

        private void ShowSnackbar(string message)
        {
            Snackbar snackbar = Snackbar.Make(register, message, Snackbar.LengthShort);
            snackbar.Show();
        }
    }
}