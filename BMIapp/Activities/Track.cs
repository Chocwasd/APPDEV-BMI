using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using BMIapp.Adaptor;
using BMIapp.Common;
using DevConnect.Common;
using Firebase.Firestore;
using Java.Util;
using System;
using System.Collections.Generic;
using static AndroidX.RecyclerView.Widget.RecyclerView;

namespace BMIapp.Activities
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class Track : AppCompatActivity
    {
        ImageButton imageButtonHome, imageButtonSettings;
        List<ResultModel> resultsList = new List<ResultModel>();
        RecyclerView recyclerViewResults;
        ResultsAdapter adapter;// Assuming you have a RecyclerView Adapter named ResultAdapter

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.Track);

            imageButtonHome = FindViewById<ImageButton>(Resource.Id.imageButtonHome);
            imageButtonSettings = FindViewById<ImageButton>(Resource.Id.imageButtonSettings);
            recyclerViewResults = FindViewById<RecyclerView>(Resource.Id.recyclerViewResults);


            imageButtonSettings.Click += ImageButtonSettings_Click;
            imageButtonHome.Click += ImageButtonHome_Click;

            // Initialize and set up RecyclerView
            recyclerViewResults.SetLayoutManager(new LinearLayoutManager(this));
            adapter = new ResultsAdapter(resultsList);
            recyclerViewResults.SetAdapter(adapter);

            // Load results from Firestore
            LoadResultsFromFirestore();
        }

        private void ImageButtonHome_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(Dashboard));
            StartActivity(intent);
        }

        private void ImageButtonSettings_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SettingsPage));
            StartActivity(intent);
        }

        public override void OnBackPressed()
        {
            // Do nothing when the back button is pressed
        }

        private void LoadResultsFromFirestore()
        {
            FirebaseFirestore db = FirebaseRepository.GetFirebaseFirestore();
            string userId = FirebaseRepository.getFirebaseAuth().CurrentUser.Uid;

            CollectionReference resultsCollectionRef = db.Collection("user_results").Document(userId).Collection("results");

            resultsCollectionRef.Get().AddOnSuccessListener(new OnSuccessListener((querySnapshot) =>
            {
                if (querySnapshot.IsEmpty)
                {
                    // Handle the case when there is no data available
                    Toast.MakeText(this, "No data available yet", ToastLength.Short).Show();
                    return;
                }

                foreach (DocumentSnapshot doc in querySnapshot.Documents)
                {
                    ResultModel result = new ResultModel
                    {
                        Height = doc.Get("Height") != null ? Convert.ToDouble(doc.Get("Height")) : 0,
                        Weight = doc.Get("Weight") != null ? Convert.ToDouble(doc.Get("Weight")) : 0,
                        Age = doc.Get("Age") != null ? Convert.ToInt32(doc.Get("Age")) : 0,
                        Gender = doc.GetString("Gender"),
                        BMI = doc.Get("BMI") != null ? Convert.ToDouble(doc.Get("BMI")) : 0,
                        Date = doc.GetString("Date"),
                        BMICategory = CalculateBMICategory((double)(doc.GetDouble("BMI")))
                    };
                    resultsList.Add(result);
                }

                adapter.NotifyDataSetChanged();
            }));
        }


        private string CalculateBMICategory(double bmi)
        {
            if (bmi < 18.5)
            {
                return "Underweight";
            }
            else if (bmi >= 18.5 && bmi <= 24.9)
            {
                return "Healthy";
            }
            else if (bmi >= 25 && bmi <= 29.9)
            {
                return "Overweight";
            }
            else
            {
                return "Obese";
            }
        }
        private class OnSuccessListener : Java.Lang.Object, IOnSuccessListener
        {
            private readonly Action<QuerySnapshot> _action;

            public OnSuccessListener(Action<QuerySnapshot> action)
            {
                _action = action;
            }

            public void OnSuccess(Java.Lang.Object result)
            {
                _action.Invoke(result.JavaCast<QuerySnapshot>());
            }
        }
    }
}
