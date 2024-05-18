using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMIapp.Common
{
    public class ResultModel
    {
        public string DocumentId { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public double BMI { get; set; }
        public string Date { get; set; }
        public string BMICategory { get; set; } // New property for BMI category
    }

}