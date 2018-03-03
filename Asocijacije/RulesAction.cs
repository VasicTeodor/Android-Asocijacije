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

namespace Asocijacije
{
    [Activity(Label = "Pravila:", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class RulesAction : Activity
    {
        private Button returnBtn;
        private TextView rulesText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            string pravilo = "Za pocetak igre igrac treba da pritisne dugme 'Nova igra', zatim prati sledeca uputstva: \n\tU polje 'Broj Timova' treba da unese najmanje " +
                "dva tima, ili vise. Jedan tim se sastoji od dva igraca, ako igraci zele da tim ima vise od dva igraca onda igrac treba da poveca broj reci. " +
                "\n\tU polje 'Broj Reci' igrac treba da unese broj reci po igracu od koliko zeli da se igra sastoji, minimalno tri reci." +
                "\n\tU polje 'Vreme Trajanja Runde', igrac unosi koliko sekundi zeli da jedna runda traje, minimalno je trideset sekundi, a maksimalno sezdeset." +
                "\n\nOpsta pravila:\n" +
                "'IGRA ASOCIJACIJA' se sastoji od tri runde, u prvoj rundi igraci treba da koriste sto vise reci da bi opisali dobijenu rec svome saigracu, u drugoj rundi" +
                "igracu je dozvoljeno da koristi samo jednu rec da opise dobijenu rec, a u trecoj rundi nisu dozvoljene reci, vec se dboijena rec objasnjava pamtomimom.\n\n" +
                "Svako pritiskanje dugmeta sledeca rec dodaje po jedan poen trenutnom timu, a na karju svake runde prikazuju se imena timova sa osvojenim bodovima." +
                "\n\nAplikaciju napravio i dizajnirao Teodor Vasic iz zabave i ucenja.";

            SetContentView(Resource.Layout.RulesView);
            // Create your application here
            FindViews();
            HandleEvents();
            rulesText.Text = pravilo;

        }

        private void FindViews()
        {
            returnBtn = FindViewById<Button>(Resource.Id.btnReturn);
            rulesText = FindViewById<TextView>(Resource.Id.textRules);
        }

        private void HandleEvents()
        {
            returnBtn.Click += ReturnBtn_Click;
        }

        private void ReturnBtn_Click(object sender, EventArgs e)
        {
            this.Finish();
        }
    }
}