using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using DevConnect.Common;
using Firebase.Firestore;
using Java.Util;
using Javax.Security.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMIapp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class Details : AppCompatActivity
    {
        TextView textViewBMI, textViewBMIcategory;
        Button btnSaveResult;
        double bmi;
        double height, weight;
        int age;
        string gender;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.Details);

            // Retrieve data passed from previous activity
            height = Intent.GetDoubleExtra("height_value", 0);
            weight = Intent.GetDoubleExtra("weight_value", 0);
            age = Intent.GetIntExtra("age_value", 0);
            gender = Intent.GetStringExtra("gender_value");
            bmi = Intent.GetDoubleExtra("bmi_value", 0);


            Log.Debug("DetailsActivity", $"Height: {height}, Weight: {weight}, Age: {age}, Gender: {gender}, BMI: {bmi}");

            // Referencing
            textViewBMI = FindViewById<TextView>(Resource.Id.textViewBMI);
            textViewBMIcategory = FindViewById<TextView>(Resource.Id.textViewBMIcategory);
            btnSaveResult = FindViewById<Button>(Resource.Id.btnSaveResult);

            textViewBMI.Text = bmi.ToString("00.00");

            btnSaveResult.Click += BtnSaveResult_Click;
            // Call the method to categorize BMI
            Categorized();
        }

        private void BtnSaveResult_Click(object sender, EventArgs e)
        {
            // Get the current date
            DateTime currentDate = DateTime.Now;

            // Format the date as a string (excluding the time)
            string dateString = currentDate.ToString("yyyy-MM-dd");

            // Create a HashMap to store user result data
            HashMap userResultData = new HashMap();
            userResultData.Put("Height", height);
            userResultData.Put("Weight", weight);
            userResultData.Put("Age", age);
            userResultData.Put("Gender", gender);
            userResultData.Put("BMI", bmi);
            userResultData.Put("Date", dateString); // Add date to HashMap

            // Access Firestore database
            FirebaseFirestore db = FirebaseRepository.GetFirebaseFirestore();

            // Reference to the collection where user results will be stored
            CollectionReference resultsCollectionRef = db.Collection("user_results").Document(FirebaseRepository.getFirebaseAuth().CurrentUser.Uid).Collection("results");

            // Create a new document reference and set the user result data
            DocumentReference resultDocRef = resultsCollectionRef.Document();
            resultDocRef.Set(userResultData);

            // Show a toast message indicating successful saving
            Toast.MakeText(this, "Result saved successfully", ToastLength.Short).Show();
            Intent intent = new Intent(this, typeof(Track));
            StartActivity(intent);
        }

        private void Categorized()
        {


            if (bmi < 18.5)
            {
                textViewBMIcategory.Text = "Underweight";
            }
            else if (bmi >= 18.5 && bmi <= 24.9)
            {
                textViewBMIcategory.Text = "Healthy";
            }
            else if ((bmi >= 25 && bmi <= 29.9))
            {
                textViewBMIcategory.Text = "Overweight";
            }
            else
            {
                textViewBMIcategory.Text = "Obese";
            }
        }
    }
}