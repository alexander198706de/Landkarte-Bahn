using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Landkarte_Bahn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Haltestelle[] haltestellen;
       
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            GeoPunkt a = new GeoPunkt(23.0, 42.0);
            GeoPunkt b = a;

            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == true)
            {
                string dateiname = ofd.FileName;
                string[] zeilen = File.ReadAllLines(dateiname);
                haltestellen = new Haltestelle[zeilen.Length - 1];  // -1 weil Spaltentiteln in der obersten Zeile
                for (int i = 1; i < zeilen.Length - 1; i++)
                {
                    string[] teile = zeilen[i].Split(';');
                    double laenge = Convert.ToDouble(teile[5]);
                    double breite = Convert.ToDouble(teile[6]);
                    haltestellen[i - 1] = new Haltestelle(teile[3], laenge, breite);

                }
                // double minLänge = haltestellen.Min(x => x.Laenge);
                double minLänge = 100.0;
                double maxLänge = 0.0;
                double minBreite = 100.0;
                //double minBreite = haltestellen.Min(x => x.Breite);
                double maxBreite = 0.0;
                //double minLänge = 10.0245;
                foreach (var item in haltestellen)
                {
                    try
                    {
                        // statements that may cause an exception
                        if (item != null)
                        {


                            if (item.Laenge < minLänge)
                            {
                                minLänge = item.Laenge;
                            }
                            if (item.Laenge > maxLänge)
                            {
                                maxLänge = item.Laenge;
                            }

                            if (item.Laenge < minBreite)
                            {
                                minBreite = item.Breite;
                            }
                            if (item.Laenge > maxBreite)
                            {
                                maxBreite = item.Breite;
                            }
                        }
                    }
                    catch (Exception obj)
                    {
                        MessageBox.Show(obj.ToString());
                    }
                }
                //double minLänge = haltestellen.;
                for (int i = 0; i < haltestellen.Length; i++)
                {
                    
                    Haltestelle h = haltestellen[i];
                    if (h != null)
                    {
                        if (!(h.IstHbf))
                        {
                            Ellipse elli = new Ellipse();
                            elli.Width = 5.0;
                            elli.Height = 5.0;
                            elli.Fill = Brushes.DarkBlue;
                            elli.ToolTip = h.Ort;
                            zeichenflaeche.Children.Add(elli);
                            
                            // zeichenflaeche.ActualHeight

                            Canvas.SetLeft(elli, zeichenflaeche.ActualWidth / (maxLänge - minLänge) * (h.Laenge - minLänge));
                            Canvas.SetBottom(elli, zeichenflaeche.ActualHeight / (maxBreite - minBreite) * (h.Breite - minBreite));
    
                        }
                    }
                }

                for (int i = 0; i < haltestellen.Length; i++)
                {
                    Haltestelle h = haltestellen[i];
                    if (h != null)
                    { 
                        if (h.IstHbf)
                        {
                            Ellipse elli = new Ellipse();
                            elli.Width = 5.0;
                            elli.Height = 5.0;
                            elli.Fill = Brushes.Red;
                            elli.ToolTip = h.Ort;
                            zeichenflaeche.Children.Add(elli);

                            // zeichenflaeche.ActualHeight

                            Canvas.SetLeft(elli, zeichenflaeche.ActualWidth / (maxLänge - minLänge) * (h.Laenge - minLänge));
                            Canvas.SetBottom(elli, zeichenflaeche.ActualHeight / (maxBreite - minBreite) * (h.Breite - minBreite));
                        }
                    }
        
                }
            }
        }
    }

    class Haltestelle
    {
        string ort;
        double breite;
        double laenge;

        public Haltestelle(string ort, double laenge, double breite)
        {
            this.ort = ort;
            this.laenge = laenge;
            this.breite = breite;
            
        }

        public string Ort
        {

            get { return ort; }
        }

        public double Laenge // Property
        {
            get { return laenge; }
        }

        public double Breite // Property
        {
            get { return breite; }
        }

        public bool IstHbf
        {
            get { return ort.EndsWith("Hbf");  }
        }
    }
}
