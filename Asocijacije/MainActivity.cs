using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Views;

namespace Asocijacije
{
    [Activity(Label = "Asocijacije", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        private Button newGame;
        private Button rules;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            FindViews();
            HandleEvents();
        }

        private void FindViews()
        {
            newGame = FindViewById<Button>(Resource.Id.btnNewGame);
            rules = FindViewById<Button>(Resource.Id.btnrules);
        }

        private void HandleEvents()
        {
            newGame.Click += NewGame_Click;
            rules.Click += Rules_Click;
        }

        private void Rules_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(RulesAction));
            StartActivity(intent);
        }

        private void NewGame_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(NewGame));
            StartActivity(intent);
        }
    }
}

