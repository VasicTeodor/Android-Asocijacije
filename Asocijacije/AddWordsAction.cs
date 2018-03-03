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
    [Activity(Label = "Unesite reci", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class AddWordsAction : Activity
    {
        private EditText word;
        private Button addWord;
        private Button next;
        private Button cancel;
        private TextView wordLeft;
        private static int wordCounter = 1;
        private int brojReci = 0;
        private int reciUneto = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AddWords);

            brojReci = AsocijacijeData.wordNu * 2 * AsocijacijeData.teamNu;

            FindViews();
            HandleEvents();

            wordLeft.Text = "Reci uneto: " + reciUneto +", preostali broj reci za unos: " + brojReci;
        }

        private void FindViews()
        {
            next = FindViewById<Button>(Resource.Id.btnNext3);
            cancel = FindViewById<Button>(Resource.Id.btnCancel3);
            addWord = FindViewById<Button>(Resource.Id.btnAddWord);
            word = FindViewById<EditText>(Resource.Id.addWordIn);
            wordLeft = FindViewById<TextView>(Resource.Id.wordsLeft);
        }

        private void HandleEvents()
        {
            next.Click += Next_Click;
            cancel.Click += Cancel_Click;
            addWord.Click += AddWord_Click;
        }

        private void AddWord_Click(object sender, EventArgs e)
        {
            int wordNum = AsocijacijeData.wordNu;
            int teamNum = AsocijacijeData.teamNu;
            int brReci = 2 * teamNum * wordNum;

            if (AsocijacijeData._reci.Count <= brReci)
            {
                string help = "";

                try
                {
                    help = word.Text;
                }
                catch (Exception) { };

                help.Trim();
                if (help.Equals(""))
                {
                    var dialog = new AlertDialog.Builder(this);
                    dialog.SetTitle("Greska");
                    dialog.SetMessage(string.Format("Morate uneti rec!"));
                    dialog.Show();
                }
                else
                {
                    AsocijacijeData._reci.Add(help);
                    word.Text = "";

                    brojReci--;
                    wordCounter++;

                    if (reciUneto < AsocijacijeData.wordNu - 1)
                    {
                        reciUneto++;
                    }
                    else
                    {
                        reciUneto = 0;
                    }

                    wordLeft.Text = "Reci uneto: " + reciUneto + ", preostali broj reci za unos: " + brojReci;
                }
            }
            
            if(AsocijacijeData._reci.Count == brReci)
            {
                var intent = new Intent(this, typeof(GameAction));
                StartActivity(intent);

                this.Finish();
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            if(AsocijacijeData._reci.Count != 0)
            {
                AsocijacijeData._reci.Clear();
            }

            if(wordCounter != 1)
            {
                wordCounter = 1;
                brojReci = 0;
            }

            this.Finish();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if(AsocijacijeData._reci.Count != 0)
            {
                AsocijacijeData._reci.Clear();
            }

            if(wordCounter != 1)
            {
                wordCounter = 1;
                brojReci = 0;
            }

            if(AsocijacijeData._timovi.Count != 0)
            {
                AsocijacijeData._timovi.Clear();
            }
            
            var intent = new Intent(this, typeof(AddTeamAction));
            StartActivity(intent);

            this.Finish();
        }
    }
}