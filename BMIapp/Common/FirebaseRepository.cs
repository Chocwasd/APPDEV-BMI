using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using Firebase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Firestore;

namespace DevConnect.Common
{
    public class FirebaseRepository
    {
        static FirebaseApp app;
        public static FirebaseAuth getFirebaseAuth()
        {
            //app instance
            app = FirebaseApp.InitializeApp(Application.Context);

            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                   .SetProjectId("bmitracker-56a30")
                   .SetApplicationId("1:701263452552:android:ed11915476e836efc2c23d")
                   .SetApiKey("AIzaSyBH2YMteTF3yKG_JD1NVzsvk-QNJTEIu7U")
                   .SetStorageBucket("bmitracker-56a30.appspot.com")
                   .Build();

                app = FirebaseApp.InitializeApp(Application.Context, options);

            }

            return FirebaseAuth.GetInstance(app);

        }
        public static FirebaseFirestore GetFirebaseFirestore()
        {
            //app instance
            app = FirebaseApp.InitializeApp(Application.Context);

            if (app == null)
            {
                var options = new FirebaseOptions.Builder()
                   .SetProjectId("bmitracker-56a30")
                   .SetApplicationId("1:701263452552:android:ed11915476e836efc2c23d")
                   .SetApiKey("AIzaSyBH2YMteTF3yKG_JD1NVzsvk-QNJTEIu7U")
                   .SetStorageBucket("bmitracker-56a30.appspot.com")
                   .Build();

                app = FirebaseApp.InitializeApp(Application.Context, options);

            }

            return FirebaseFirestore.GetInstance(app);
        }
    }
}