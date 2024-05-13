using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
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
        float height, weight;
        int age;
        string gender;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.Details);

            // Retrieve data passed from Calculator activity
            height = Intent.GetFloatExtra("height_value", 0);
            weight = Intent.GetFloatExtra("weight_value", 0);
            age = Intent.GetIntExtra("age_value", 0);
            gender = Intent.GetStringExtra("gender_value");
            bmi = Intent.GetDoubleExtra("bmi_value", 0); // Corrected to GetDoubleExtra

            Log.Debug("DetailsActivity", $"Height: {height}, Weight: {weight}, Age: {age}, Gender: {gender}, BMI: {bmi}");

            // Referencing
            textViewBMI = FindViewById<TextView>(Resource.Id.textViewBMI);
            textViewBMIcategory = FindViewById<TextView>(Resource.Id.textViewBMIcategory);
            btnSaveResult = FindViewById<Button>(Resource.Id.btnSaveResult);

            textViewBMI.Text = bmi.ToString("00.00");

            // Call the method to categorize BMI
            Categorized();
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