using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Asocijacije.Data;

namespace Asocijacije
{
    [Activity(Label = "Nova Igra", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class NewGame : Activity
    {
        private Button next;
        private Button cancel;
        private EditText teamNum;
        private EditText wordNum;
        private EditText timeNum;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.NewGameView);
            // Create your application here
            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            next = FindViewById<Button>(Resource.Id.btnNext);
            cancel = FindViewById<Button>(Resource.Id.btnCancel);
            teamNum = FindViewById<EditText>(Resource.Id.teamNumIn);
            wordNum = FindViewById<EditText>(Resource.Id.numNumIn);
            timeNum = FindViewById<EditText>(Resource.Id.timeIn);
        }

        private void HandleEvents()
        {
            next.Click += Next_Click;
            cancel.Click += Cancel_Click;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Finish();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            int help = 0;
            Int32.TryParse(teamNum.Text, out help);
            AsocijacijeData.teamNu = help;
            Int32.TryParse(wordNum.Text, out help);
            AsocijacijeData.wordNu = help;
            Int32.TryParse(timeNum.Text, out help);
            AsocijacijeData.timeSpan = help;

            if ((AsocijacijeData.teamNu != 0) && (AsocijacijeData.wordNu != 0) && (AsocijacijeData.timeSpan != 0))
            {
                if (AsocijacijeData.timeSpan < 30 || AsocijacijeData.timeSpan > 60)
                {
                    var dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("Greska");
                    dialog.SetMessage(string.Format("Vreme mora biti u zadatom intervalu!"));
                    dialog.Show();
                }
                else if (AsocijacijeData.teamNu < 2)
                {
                    var dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("Greska");
                    dialog.SetMessage(string.Format("Mora biti najmanje 2 tima!"));
                    dialog.Show();
                }
                else if (AsocijacijeData.wordNu < 3)
                {
                    var dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("Greska");
                    dialog.SetMessage(string.Format("Broj reci mora biti najmanje 3!"));
                    dialog.Show();
                }
                else
                {
                    var intent = new Intent(this, typeof(AddTeamAction));
                    StartActivity(intent);

                    this.Finish();
                }
            }
            else
            {
                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Greska");
                dialog.SetMessage(string.Format("Morate uneti trazene podatke!"));
                dialog.Show();
            }
        }
    }
}