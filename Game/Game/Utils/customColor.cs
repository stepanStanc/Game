using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game
{
    /// <summary>
    /// Vlastní formát barvy pro účely této aplikace
    /// </summary>
    public class customColor
    {
        private string _ColorHex;
        private string _Name;

        public customColor(string name, string colorHex)
        {
          _ColorHex = colorHex;
          _Name = name;
        }

        public string ColorHex { get { return _ColorHex; } set { _ColorHex = value; } }
        public Color Color { get { return Color.FromHex(_ColorHex); } set {  } } //xamarin forms má vlastní formáty barev - na které je tato funkce převádí z hexadecimálního kódu
        public string Name { get { return _Name; } set { _Name = value; } }
  }
}
