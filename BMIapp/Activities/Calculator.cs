using Android.App;
using Android.Content;
using Android.Hardware.Lights;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using BMIapp.Activities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMIapp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class Calculator : AppCompatActivity
    {
        //Declaring Variables
        EditText editTextHeight, editTextWeight, editTextAge;
        TextView textViewBMIResult;
        ImageButton imageButtonBack, ibPlusWeight, ibPlusAge, ibMinusAge, ibMinusWeight;
        Button buttonCalculate;
        RadioGroup radioGroupGender;




        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.bmicalculator);

            string fullName = Intent.GetStringExtra("FULL_NAME");

            // Initialize EditText fields 
            editTextHeight = FindViewById<EditText>(Resource.Id.editTextHeight);
            editTextWeight = FindViewById<EditText>(Resource.Id.editTextWeight);
            editTextAge = FindViewById<EditText>(Resource.Id.editTextAge);

            // Initialize ImageButtons
            imageButtonBack = FindViewById<ImageButton>(Resource.Id.imageButtonBack);
            ibPlusWeight = FindViewById<ImageButton>(Resource.Id.ibPlusWeight);
            ibPlusAge = FindViewById<ImageButton>(Resource.Id.ibPlusAge);
            ibMinusAge = FindViewById<ImageButton>(Resource.Id.ibMinusAge);
            ibMinusWeight = FindViewById<ImageButton>(Resource.Id.ibMinusWeight);

            // Initialize Buttons
            buttonCalculate = FindViewById<Button>(Resource.Id.buttonCalculate);

            // Initialize RadioButtons
            radioGroupGender = FindViewById<RadioGroup>(Resource.Id.radioGroupGender);




            //Event Handling
            imageButtonBack.Click += ImageButtonBack_Click;
            ibPlusAge.Click += IbPlusAge_Click;
            ibMinusAge.Click += IbMinusAge_Click;
            ibPlusWeight.Click += IbPlusWeight_Click;
            ibMinusWeight.Click += IbMinusWeight_Click;
            buttonCalculate.Click += ButtonCalculate_Click;
        }

        private void ButtonCalculate_Click(object sender, EventArgs e)
        {
             if (string.IsNullOrEmpty(editTextAge.Text) || string.IsNullOrEmpty(editTextHeight.Text) || string.IsNullOrEmpty(editTextWeight.Text) || radioGroupGender.CheckedRadioButtonId == -1)
    {
                Toast.MakeText(this, "Please fill in all fields", ToastLength.Short).Show();
                return;
            }

            // Get height, weight, and age values from EditText fields
            double height = Convert.ToDouble(editTextHeight.Text);
            double weight = Convert.ToDouble(editTextWeight.Text);
            int age = Convert.ToInt32(editTextAge.Text);

            // Get selected gender from radio group
            string gender = GetSelectedGender();



            Intent intent = new Intent(this, typeof(ResultActivity));
            intent.PutExtra("height_value", height);
            intent.PutExtra("weight_value", weight);
            intent.PutExtra("age_value", age);
            intent.PutExtra("gender_value", gender);
            StartActivity(intent);

        }


        private void IbMinusWeight_Click(object sender, EventArgs e)
        {
            // Dicrement the age by 1 when the "Minus" button is clicked
            int currentWeight = int.Parse(editTextWeight.Text);
            editTextWeight.Text = (currentWeight - 1).ToString();
        }

        private void IbPlusWeight_Click(object sender, EventArgs e)
        {
            // Increment the Weight by 1 when the "Plus" button is clicked
            int currentWeight = int.Parse(editTextWeight.Text);
            editTextWeight.Text = (currentWeight + 1).ToString();
        }

        private void IbMinusAge_Click(object sender, EventArgs e)
        {
            // Dicrement the age by 1 when the "Minuse" button is clicked
            int currentAge = int.Parse(editTextAge.Text);
            editTextAge.Text = (currentAge - 1).ToString();
        }

        private void IbPlusAge_Click(object sender, EventArgs e)
        {
            // Increment the age by 1 when the "Plus" button is clicked
            int currentAge = int.Parse(editTextAge.Text);
            editTextAge.Text = (currentAge + 1).ToString();


        }

        private string GetSelectedGender()
        {
            // Get the selected radio button ID from the radio group
            int selectedRadioButtonId = radioGroupGender.CheckedRadioButtonId;

            // Find the radio button by ID
            RadioButton selectedRadioButton = FindViewById<RadioButton>(selectedRadioButtonId);

            // Get the tag of the selected radio button (i.e., the selected gender)
            string selectedGender = selectedRadioButton.Tag.ToString();

            return selectedGender;
        }




        private void ImageButtonBack_Click(object sender, EventArgs e)
        {

            // Back to Dashboard
            Intent intent = new Intent(this, typeof(Dashboard));
            StartActivity(intent);
        }
    }
}

