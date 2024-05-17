using System.Collections.Generic;
using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using BMIapp.Common;


namespace BMIapp.Common
{
    public class UserResult
    {
        public float Height { get; set; }
        public float Weight { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public double BMI { get; set; }

        public UserResult(float height, float weight, int age, string gender, double bmi)
        {
            Height = height;
            Weight = weight;
            Age = age;
            Gender = gender;
            BMI = bmi;
        }
    }
}
