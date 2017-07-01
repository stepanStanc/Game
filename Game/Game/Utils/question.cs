using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game
{
    /// <summary>
    /// Třída s otázkou
    /// </summary>
    public class question
    {
        private string _Quetsion;
        private bool _Correct;
        private Color _ColorTxt;
        private Color _ColorBg;

        public question(string q,bool c, Color cTxt, Color cBg)
        {
            _Quetsion = q;
            _Correct = c;
            _ColorTxt = cTxt;
            _ColorBg = cBg;
        }

        public string Question { get { return _Quetsion; } set { _Quetsion = value; } }
        public bool Correct { get { return _Correct; } set { _Correct = value; } }
        public Color ColorText { get { return _ColorTxt; } set { _ColorTxt = value; } }
        public Color ColorBackground { get { return _ColorBg; } set { _ColorBg = value; } }      
    }
}
