using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Asocijacije.Data;
using Asocijacije.Model;

namespace Asocijacije
{
    [Activity(Label = "IGRA ASOCIJACIJA", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class GameAction : Activity
    {
        private TextView timeGone;
        private TextView teamName;
        private TextView wordNow;
        private Button startNext;
        private Button btnQuit;
        private int count = 0;
        private int timNaRedu = 0;
        private int rec = 0;
        private int runda = 1;
        private bool firstClick = false;
        private bool GameEnd = false;
        private bool end = false;
        private bool startTimer = false;
        Timer timer;
        MediaPlayer _player;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            count = AsocijacijeData.timeSpan;

            _player = MediaPlayer.Create(this, Resource.Raw.plucky);

            // Create your application here
            SetContentView(Resource.Layout.GameView);

            FindViews();

            teamName.Text = AsocijacijeData._timovi[timNaRedu].Name;
            startNext.Text = "Start";
            firstClick = true;

            HandleEvents();

        }

        private void FindViews()
        {
            timeGone = FindViewById<TextView>(Resource.Id.timeOn);
            teamName = FindViewById<TextView>(Resource.Id.teamOnNow);
            wordNow = FindViewById<TextView>(Resource.Id.wordOn);
            startNext = FindViewById<Button>(Resource.Id.btnSlRec);
            btnQuit = FindViewById<Button>(Resource.Id.btnQuit);
        }

        protected override void OnResume()
        {
            timer = null;
            base.OnResume();
            timer = new Timer();
            timer.Interval = 1000; // 1 - sekunda
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (startTimer)
            {
                if (count > 0)
                {
                    RunOnUiThread(() =>
                    {
                        count--;
                        timeGone.Text = count.ToString();
                    });
                }
                else
                {
                    RunOnUiThread(() =>
                    {
                        timer.Stop();
                        firstClick = true;
                        startTimer = false;
                        count = AsocijacijeData.timeSpan; //Resetujemo brojac
                        string pomocna = wordNow.Text;
                        timeGone.Text = count.ToString();
                        wordNow.Text = "";
                        startNext.Text = "Start";

                        if (timNaRedu == AsocijacijeData.teamNu - 1)
                        {
                            timNaRedu = 0;
                        }
                        else
                        {
                            timNaRedu++;
                        }

                        teamName.Text = AsocijacijeData._timovi[timNaRedu].Name;

                        if (AsocijacijeData._pomocna.Contains(pomocna))
                        {
                            int idx = AsocijacijeData._pomocna.IndexOf(pomocna);
                            AsocijacijeData._reci.Add(AsocijacijeData._pomocna[idx]);
                            AsocijacijeData._pomocna.Remove(pomocna);
                        }

                        _player = MediaPlayer.Create(this, Resource.Raw.chime);
                        _player.Start();

                        var dialog = new AlertDialog.Builder(this);
                        dialog.SetTitle("Vreme je isteklo");
                        dialog.SetMessage(string.Format("Na redu je tim {0}, a preostali broj reci je: {1}!", AsocijacijeData._timovi[timNaRedu].Name, AsocijacijeData._reci.Count));
                        dialog.Show();

                        timeGone.Text = "" + count;
                    });
                }
            }
        }

        private void HandleEvents()
        {
            startNext.Click += StartNext_Click;
            btnQuit.Click += BtnQuit_Click;
        }

        private void BtnQuit_Click(object sender, EventArgs e)
        {
            RunOnUiThread(() =>
            {
                timer.Stop();
                firstClick = true;
                startTimer = false;
                count = AsocijacijeData.timeSpan; //Resetujemo brojac
                string pomocna = wordNow.Text;
                timeGone.Text = count.ToString();
                wordNow.Text = "";
                startNext.Text = "Start";

                if (timNaRedu == AsocijacijeData.teamNu - 1)
                {
                    timNaRedu = 0;
                }
                else
                {
                    timNaRedu++;
                }

                teamName.Text = AsocijacijeData._timovi[timNaRedu].Name;

                if (AsocijacijeData._pomocna.Contains(pomocna))
                {
                    int idx = AsocijacijeData._pomocna.IndexOf(pomocna);
                    AsocijacijeData._reci.Add(AsocijacijeData._pomocna[idx]);
                    AsocijacijeData._pomocna.Remove(pomocna);
                }

                timeGone.Text = "" + count;
            });
        }

        private void StartNext_Click(object sender, EventArgs e)
        {
            RunOnUiThread(() =>
            {
                if (firstClick)
                {
                    timer.Start();
                    startNext.Text = "Sledeca rec";
                    startTimer = true;
                    firstClick = false;
                }
                else if(!firstClick && !end)
                {
                    AsocijacijeData._timovi[timNaRedu].Points += 1;
                }

                if (AsocijacijeData._reci.Count == 0 && !end)
                {
                    timer.Stop();
                    startTimer = false;
                    wordNow.Text = "";
                    
                    count = AsocijacijeData.timeSpan;
                    timeGone.Text = count.ToString();

                    string rezulatat = "";

                    foreach (Team t in AsocijacijeData._timovi)
                    {
                        rezulatat += "\n" + t.Name + ":\t" + t.Points;
                    }

                    foreach (string rec in AsocijacijeData._pomocna)
                    {
                        AsocijacijeData._reci.Add(rec);
                    }

                    AsocijacijeData._pomocna.Clear();

                    startNext.Text = "Start";
                    firstClick = true;

                    if (timNaRedu == AsocijacijeData.teamNu - 1)
                    {
                        timNaRedu = 0;
                    }
                    else
                    {
                        timNaRedu++;
                    }

                    if (runda == 3)
                    {
                        _player = MediaPlayer.Create(this, Resource.Raw.applauses);
                        _player.Start();

                        var dialog = new AlertDialog.Builder(this);
                        dialog.SetTitle("Igra je zavrsena");
                        dialog.SetMessage(string.Format("Rezultati su: {0}", rezulatat));
                        dialog.Show();

                        AsocijacijeData.teamNu = 0;
                        AsocijacijeData.timeSpan = 0;
                        AsocijacijeData.wordNu = 0;

                        AsocijacijeData._timovi.Clear();
                        AsocijacijeData._reci.Clear();

                        end = true;
                        firstClick = false;
                        
                        startNext.Text = "Zavrsi igru";
                    }
                    else
                    {
                        _player = MediaPlayer.Create(this, Resource.Raw.plucky);
                        _player.Start();

                        teamName.Text = AsocijacijeData._timovi[timNaRedu].Name;

                        var dialog = new AlertDialog.Builder(this);
                        dialog.SetTitle("Runda " + runda + " je zavrsena");
                        dialog.SetMessage(string.Format("Rezultati su: {0} \n Tim: {1} je sledeci na redu.", rezulatat, AsocijacijeData._timovi[timNaRedu].Name));
                        dialog.Show();

                        runda++;
                    }
                }
                else
                {
                    Random rand = new Random();
                    rec = rand.Next(0, AsocijacijeData._reci.Count);

                    try
                    {
                        wordNow.Text = AsocijacijeData._reci[rec];
                        AsocijacijeData._pomocna.Add(AsocijacijeData._reci[rec]);
                        AsocijacijeData._reci.RemoveAt(rec);
                    }
                    catch (Exception) { };
                }

                if (GameEnd)
                {
                    _player.Stop();
                    GameEnd = false;
                    end = false;
                    this.Finish();
                }

                if (end)
                {
                    GameEnd = true;
                }
            });
        }
    }
}