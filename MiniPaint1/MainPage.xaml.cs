
using Microsoft.Maui.Controls.Shapes;
using MiniPaint1.Klasy;

namespace MiniPaint1
{
    public partial class MainPage : ContentPage
    {
        Plotno plotno;

        PointF punktStartowy;

        Color pedzel = Colors.Black;

        bool czyRysuje = false;

        Linia? ostatniRysunek;


        public MainPage()
        {
            InitializeComponent();

            plotno = new();
            poleRysowania.Drawable = plotno;
        }

        private void poleRysowania_StartInteraction(object sender, TouchEventArgs e)
        {
            czyRysuje = true;
            punktStartowy = e.Touches[0];
        }

        private void poleRysowania_DragInteraction(object sender, TouchEventArgs e)
        {
            if (!czyRysuje) return;

            PointF punktAktualny = e.Touches[0];

            if (dowolnaRb.IsChecked)
            {
                RysujLinie(punktAktualny);
                punktStartowy = punktAktualny;
            }
            else
            {
                if(ostatniRysunek != null)
                {
                    plotno.Linie.Remove(ostatniRysunek);
                }
                RysujLinie(punktAktualny);
            }

            poleRysowania.Invalidate();
        }

        private void RysujLinie(PointF punktAktualny)
        {
            var segment = new Linia()
            {
                Start = punktStartowy,
                Koniec = punktAktualny,
                Kolor = pedzel
            };

            plotno.Linie.Add(segment);
            ostatniRysunek = segment;
        }

        private void poleRysowania_EndInteraction(object sender, TouchEventArgs e)
        {
            czyRysuje = false;
            ostatniRysunek = null;
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            WyczyscZaznaczenie();


            if(sender is Rectangle kolor)
            {
                kolor.RadiusX = 3;
                kolor.RadiusY = 3;
                kolor.WidthRequest = 18;
                kolor.HeightRequest = 18;

                if(kolor.Fill is SolidColorBrush solid)
                {
                    pedzel = solid.Color;
                }
            }
        }

        private void WyczyscZaznaczenie()
        {
            foreach (var kolor in KoloryFL.Children)
            {
                if(kolor is Rectangle kwadrat)
                {
                    kwadrat.RadiusX = 0;
                    kwadrat.RadiusY = 0;
                    kwadrat.WidthRequest = 15;
                    kwadrat.HeightRequest = 15;
                }
            }
        }
    }

}
