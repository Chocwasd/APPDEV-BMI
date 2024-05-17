using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

namespace BMIapp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class ResultActivity : AppCompatActivity
    {
        ProgressBar progressBar;
        TextView textViewProgress, editTextResult;
        Button btnDetails;
        double bmi;
        double height, weight;
        int age;
        string gender;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.resultpage);

            // Retrieve data passed from Calculator activity
            height = Intent.GetDoubleExtra("height_value", 0);
            weight = Intent.GetDoubleExtra("weight_value", 0);
            age = Intent.GetIntExtra("age_value", 0);
            gender = Intent.GetStringExtra("gender_value");

            // Validate inputs
            if (height <= 0 || weight <= 0)
            {
                // Handle invalid input
                Toast.MakeText(this, "Invalid height or weight", ToastLength.Short).Show();
                Finish(); // Close the activity
                return;
            }

            // Calculate BMI
            bmi = CalculateBMI(height, weight);

            // Referencing
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar);
            textViewProgress = FindViewById<TextView>(Resource.Id.textViewProgress);
            editTextResult = FindViewById<TextView>(Resource.Id.editTextResult);
            btnDetails = FindViewById<Button>(Resource.Id.btnDetails);

            // Update progress bar
            UpdateProgress();

            // Event handling
            btnDetails.Click += BtnDetails_Click;
        }

        private void BtnDetails_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Details));
            intent.PutExtra("height_value", height);
            intent.PutExtra("weight_value", weight);
            intent.PutExtra("age_value", age);
            intent.PutExtra("gender_value", gender);
            intent.PutExtra("bmi_value", bmi); // Pass BMI value to Details activity
            StartActivity(intent);
        }

        private double CalculateBMI(double height, double weight)
        {
            // Convert height from cm to meters
            double heightInMeters = height / 100;

            // Calculate BMI using the formula: BMI = weight (kg) / (height (m))^2
            double bmi = weight / (heightInMeters * heightInMeters);
            return bmi;

        }

        private void UpdateProgress()
        {
            // Determine BMI category
            if (bmi < 18.5)
            {
                progressBar.SetProgress(25, true);
                editTextResult.Text = "You have Underweight body weight";
            }
            else if (bmi >= 18.5 && bmi <= 24.9)
            {
                progressBar.SetProgress(50, true);
                editTextResult.Text = "You have Healthy body weight";
            }
            else if (bmi >= 25 && bmi <= 29.9)
            {
                progressBar.SetProgress(75, true);
                editTextResult.Text = "You have Overweight body weight";
            }
            else
            {
                progressBar.SetProgress(100, true); // Max progress for obese category  
                editTextResult.Text = "You have Obese body weight";
            }

            // Display BMI value
            textViewProgress.Text = bmi.ToString("0.00");
        }
    }
}
