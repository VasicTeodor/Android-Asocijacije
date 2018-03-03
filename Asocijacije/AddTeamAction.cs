using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Asocijacije.Data;

namespace Asocijacije
{
    [Activity(Label = "Unesite imena timova", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class AddTeamAction : Activity
    {
        private EditText teamName;
        private TextView teamLeft;
        private Button addTeam;
        private Button next;
        private Button cancel;
        private static int teamCounter = 1;
        private int brTimova = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.TeamName);

            brTimova = AsocijacijeData.teamNu;

            FindViews();
            HandleEvents();

            teamLeft.Text = "Preostali broj timova za unos: " + brTimova;
        }

        private void FindViews()
        {
            next = FindViewById<Button>(Resource.Id.btnNext2);
            cancel = FindViewById<Button>(Resource.Id.btnCancel2);
            addTeam = FindViewById<Button>(Resource.Id.btnAddTeam);
            teamName = FindViewById<EditText>(Resource.Id.teamNameIn);
            teamLeft = FindViewById<TextView>(Resource.Id.teamsLeft);
        }

        private void HandleEvents()
        {
            next.Click += Next_Click;
            cancel.Click += Cancel_Click;
            addTeam.Click += AddTeam_Click;
        }

        private void AddTeam_Click(object sender, EventArgs e)
        {
            if (AsocijacijeData._timovi.Count <= AsocijacijeData.teamNu)
            {
                string help = "";

                try
                {
                    help = teamName.Text;
                }
                catch (Exception) { };

                help.Trim();

                if (help.Equals(""))
                {
                    var dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("Greska");
                    dialog.SetMessage(string.Format("Morate uneti ime za tim!"));
                    dialog.Show();
                }
                else
                {
                    AsocijacijeData._timovi.Add(new Model.Team(teamCounter, help));
                    teamName.Text = "";
                    teamCounter++;
                    brTimova--;

                    teamLeft.Text = "Preostali broj timova za unos: " + brTimova;
                }
            }
            
            if(AsocijacijeData._timovi.Count == AsocijacijeData.teamNu)
            {
                var intent = new Intent(this, typeof(AddWordsAction));
                StartActivity(intent);

                this.Finish();
            }
    }

        private void Cancel_Click(object sender, EventArgs e)
        {
            if(AsocijacijeData._timovi.Count != 0)
            {
                AsocijacijeData._timovi.Clear();
            }

            if(teamCounter != 1)
            {
                teamCounter = 1;
                brTimova = 0;
            }

            this.Finish();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (AsocijacijeData._timovi.Count != 0)
            {
                AsocijacijeData._timovi.Clear();
            }

            if (teamCounter != 1)
            {
                teamCounter = 1;
                brTimova = 0;
            }

            AsocijacijeData.teamNu = 0;
            AsocijacijeData.timeSpan = 0;
            AsocijacijeData.wordNu = 0;

            var intent = new Intent(this, typeof(NewGame));
            StartActivity(intent);

            this.Finish();
        }
    }
}