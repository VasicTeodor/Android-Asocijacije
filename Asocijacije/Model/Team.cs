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

namespace Asocijacije.Model
{
    public class Team
    {
        private int _id;
        private string _name;
        private int _points;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public int Points { get => _points; set => _points = value; }

        public Team(int idNum,string na)
        {
            Id = idNum;
            Name = na;
            Points = 0;
        }
    }
}