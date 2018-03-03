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
using Asocijacije.Model;

namespace Asocijacije.Data
{
    public class AsocijacijeData
    {
        public static List<Team> _timovi = new List<Team>();
        public static List<string> _reci = new List<string>();
        public static List<string> _pomocna = new List<string>();

        public static int teamNu = 0;
        public static int wordNu = 0;
        public static int timeSpan = 0;
    }
}