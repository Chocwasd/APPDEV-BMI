using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using BMIapp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMIapp.Adaptor
{
    public class ResultsAdapter : RecyclerView.Adapter
    {
        private List<ResultModel> results;

        public ResultsAdapter(List<ResultModel> results)
        {
            this.results = results;
        }

        public override int ItemCount => results.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ResultViewHolder vh = holder as ResultViewHolder;

            vh.TextViewHeight.Text = "Height: " + results[position].Height + " cm";
            vh.TextViewWeight.Text = "Weight: " + results[position].Weight + " kg";
            vh.TextViewAge.Text = "Age: " + results[position].Age;
            vh.TextViewBMI.Text = "BMI: " + results[position].BMI.ToString("0.00");
            vh.TextViewDate.Text = "Date: " + results[position].Date;
            vh.TextViewCategory.Text = "BMI Category: " + results[position].BMICategory; // Set BMI category TextView
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.item_result, parent, false);
            ResultViewHolder vh = new ResultViewHolder(itemView);
            return vh;
        }
    }

    public class ResultViewHolder : RecyclerView.ViewHolder
    {
        public TextView TextViewHeight { get; private set; }
        public TextView TextViewWeight { get; private set; }
        public TextView TextViewAge { get; private set; }

        public TextView TextViewBMI { get; private set; }
        public TextView TextViewDate { get; private set; }
        public TextView TextViewCategory { get; private set; }

        public ResultViewHolder(View itemView) : base(itemView)
        {
            TextViewHeight = itemView.FindViewById<TextView>(Resource.Id.textViewHeight);
            TextViewWeight = itemView.FindViewById<TextView>(Resource.Id.textViewWeight);
            TextViewBMI = itemView.FindViewById<TextView>(Resource.Id.textViewBMI);
            TextViewAge = itemView.FindViewById<TextView>(Resource.Id.textViewAgee);
            TextViewDate = itemView.FindViewById<TextView>(Resource.Id.textViewDatee);
            TextViewCategory = itemView.FindViewById<TextView>(Resource.Id.textViewCategory);
        }
    }
}