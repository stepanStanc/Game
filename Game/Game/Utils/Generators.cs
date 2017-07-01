using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game
{
    /// <summary>
    /// Třída s generováním otázek a barev
    /// </summary>
    public class Generators
    {    
        public List<int> Primes;
        public customColor lastClr = new customColor("White", "#FFFFFF"); //pomocná proměná s poslední použitou barvou   

        public Generators()
        {
            Primes = GeneratePrimes(112);
        }
        /// <summary>
        /// Náhodná otázkas
        /// </summary>
        /// <returns></returns>
        public question loadRndQ()
        {
            question q = new question("Question text", true, Color.Red,Color.Black);
            Random rnd = new Random();

            int swch = rnd.Next(1, 4);
            switch (swch)
            {
                case 1:
                    q = generateColorQ();
                    break;
                case 2:
                    q = generateEquation();
                    break;
               case 3:
                    q = generateNumberQ();
                    break;
            }

            return q;
        }

        /// <summary>
        /// genorování otázky na barvu textu, pozadí
        /// </summary>
        public question generateColorQ()
        {
            question q;
            customColor colorTxt = clr1();
            customColor colorBg = clr2(colorTxt);
            
            Random rnd = new Random();
            int num = rnd.Next(0, 55);
            int num2 = rnd.Next(0, 55);
            if(num % 2 == 0)
            {
                if(num2 % 2 == 0)
                {
                    //qe.Add(new question("Question text",bool, "text color", "background color"));
                     q = new question("Is the text " + colorTxt.Name + " ?", true, colorTxt.Color, colorBg.Color);
                }
                else
                {
                     q = new question("Is the text " + colorTxt.Name + " ?", false, colorBg.Color, clr2(colorBg).Color);        
                }
                
            }
            else
            {
                if(num2 % 2 == 0)
                {
                    q = new question("Is the background " + colorBg.Name + " ?", true, colorTxt.Color, colorBg.Color);        
                }
                else
                {
                    q = new question("Is the background " + colorBg.Name + " ?",false, clr2(colorTxt).Color, colorTxt.Color);
                }
            }
            
            return q;           
         }
          /// <summary>
          /// Vygeneruje prvočísla
          /// </summary>
          /// <param name="n">nejvyšší číslo</param>
          /// <returns></returns>
          public List<int> GeneratePrimes(int n) {
              var r = from i in Enumerable.Range(2, n - 1).AsParallel()
                      where Enumerable.Range(2, (int)Math.Sqrt(i)).All(j => i % j != 0)
                      select i;
              return r.ToList();
          }
          /// <summary>
          /// Náhodá otázka na číslo pokud je prvočíslo, jestli jsou čísla lichá sudá
          /// </summary>
          /// <returns></returns>
          public question generateNumberQ()
          {
              question q = new question("Question text", true, Color.Red,Color.Black);
              customColor colorTxt = clr1();
              customColor colorBg = clr2(colorTxt);
            
              Random rnd = new Random();
              int num = rnd.Next(0,100);
              int num2 = rnd.Next(0,100);
              int smallNum = rnd.Next(2,12);
              int smallNum2 = rnd.Next(2,12);
              int prime = Primes[rnd.Next(0,Primes.Count)];
        
              int swch = rnd.Next(1, 3);
              switch (swch)
              {
                  case 1:
                      if(num % 2 == 0)
                      {
                          q = new question("Is " + prime + " prime ?",true, colorTxt.Color, colorBg.Color);   
                      }
                      else
                      {                     
                          q = new question("Is " + smallNum*smallNum2 + " prime ?",false, colorTxt.Color, colorBg.Color);
                      }
                      
                      break;
                  case 2:
                      if(smallNum % 2 == 0)
                      {
                          q = new question("Is " + num + " even and " + num2 + " odd ?",((num % 2 == 0)&&(num2 % 2 != 0)), colorTxt.Color, colorBg.Color);
                      }
                      else
                      {                     
                          q = new question("Is " + num + " odd and " + num2 + " even ?",((num2 % 2 == 0)&&(num % 2 != 0)), colorTxt.Color, colorBg.Color);
                      }
                      
                      break;
              }

              return q;
          }

         
          /// <summary>
          /// generovaní otázky s píkladem
          /// </summary>
         public question generateEquation()
         {
            question q;
            customColor colorTxt = clr1();
            customColor colorBg = clr2(colorTxt);
            string qTxt = "";
            double True = 0;

            Random rnd = new Random();
            int num = rnd.Next(2, 50);
            int num2 = rnd.Next(2, 50);
            int smallNum = rnd.Next(1, 8);
            int smallNum2 = rnd.Next(1, 8);           
            int caseSwitch = rnd.Next(1, 5);
            int pom = rnd.Next(0,55);
            switch (caseSwitch)
            {
                case 1:
                  True = num + num2;
                  qTxt = num + " + " + num2;
                  break;
                case 2:
                  True = num - num2;
                  qTxt = num + " - " + num2;
                  break;
                case 3:
                  True = smallNum * smallNum2;
                  qTxt = smallNum + " * " + smallNum2;
                  break;
                case 4:
                  True = (double)((double)num / (double)smallNum);
                  //formáty v závorkách určují s jakým formátem přilehlý kód pracuje          
                  True = (double) ( (int) (True * 1000.0) ) / 1000.0 ;
                  qTxt = num + " ÷ " + smallNum;
                  break;
            }
            //vytvoří špatnou odpověď
            double False = True + rnd.Next(-5,5);       
            while(False == True)
            {
                False = True + rnd.Next(-5,5);    
            }

            if(pom % 3 == 0)
            {
                q = new question(qTxt + " = " + True,true,colorTxt.Color,colorBg.Color);
            }
            else
            {
                q = new question(qTxt + " = " + False,false,colorTxt.Color,colorBg.Color);
            }

            return q;

          }         

        //první náhodná barva - pokud se ihned neopakuje
        public customColor clr1()
        {
            customColor clr = getRandoClr();
            while (clr.Name == lastClr.Name)
            {
                clr = getRandoClr();
            }
            lastClr = clr;
            return clr;
        }
        //druhá barva pokud se neshoduje s druhou barvou - nepodobá se (viz. výpočet kontarastního poměru)
        public customColor clr2(customColor clr1)
        {
            customColor clr = getRandoClr(); ;
            int txtDecValue = Convert.ToInt32(clr1.ColorHex.Remove(0, 1), 16);
            bool done = true;
            while (done)
            {
              int bgDecValue = Convert.ToInt32(clr.ColorHex.Remove(0, 1), 16);
              //výpočet kontrastního poměru - vychází v desetiných číslech 1(bílá/černá) max kontrast 0 žádný kontrast(2 stejné barvy)
              //nasobky vytváří poměr barev - R:30% G:59% B:11% - převod na jasnost ve stuních šedi
              double colorDiff = (299 * clr.Color.R + 578 * clr.Color.G + 114 * clr.Color.B) / 1000 - (299 * clr1.Color.R + 578 * clr1.Color.G + 114 * clr1.Color.B) / 1000;

              //doporučený kontrastní poměr pro četní 0.5
              if(colorDiff > 0.35 || colorDiff < -0.35)
              {
                done = false;
              }
              else
              {
                clr = getRandoClr();
              }
            }

            return clr;
        }
        //vybere náhodnou barvu - předdefinované tímto způsobem aby bylo zaručeno použití Material design barev
        public customColor getRandoClr()
        {           
            List<customColor> clr = new List<customColor>();
            clr.Add(new customColor("Red", "#F44336"));
            clr.Add(new customColor("Red", "#C62828"));
            clr.Add(new customColor("Red", "#D50000"));
            clr.Add(new customColor("Pink", "#E91E63"));
            clr.Add(new customColor("Dark Pink", "#880E4F"));
            clr.Add(new customColor("Purple", "#9C27B0"));
            clr.Add(new customColor("Purple", "#4A148C"));
            clr.Add(new customColor("Dark Blue", "#1A237E"));
            clr.Add(new customColor("Blue", "#1565C0"));
            clr.Add(new customColor("Light Blue", "#4FC3F7"));
            clr.Add(new customColor("Cyan", "#00BCD4"));
            clr.Add(new customColor("Teal", "#009688"));
            clr.Add(new customColor("Green", "#2E7D32"));
            clr.Add(new customColor("Light Green", "#8BC34A"));
            clr.Add(new customColor("Lime", "#CDDC39"));
            clr.Add(new customColor("Bright Green", "#76FF03"));
            clr.Add(new customColor("Amber", "#FFC107"));
            clr.Add(new customColor("Yellow", "#FFEB3B"));
            clr.Add(new customColor("Orange", "#FF9800"));
            clr.Add(new customColor("Black", "#000000"));
            clr.Add(new customColor("White", "#FFFFFF"));
            clr.Add(new customColor("Brown", "#3E2723"));
            clr.Add(new customColor("Light orange", "#FFB74D"));
            clr.Add(new customColor("Azure", "#18FFFF"));
            clr.Add(new customColor("Deep Purple", "#4527A0"));
            clr.Add(new customColor("Pink", "#E91E63"));
            clr.Add(new customColor("Blue", "#2196F3"));
            clr.Add(new customColor("Indigo", "#3F51B5"));
            clr.Add(new customColor("Orange", "#E65100"));
            clr.Add(new customColor("Grey", "#607D8B"));
            clr.Add(new customColor("Bright Green", "#00E676"));
            clr.Add(new customColor("Teal", "#1DE9B6"));
            clr.Add(new customColor("Cyan", "#29B6F6"));
            int c = clr.Count;
            Random rnd = new Random();
            int r = rnd.Next(0, c);
            customColor color = clr[r];
          
            return color;
        }
    }
}
