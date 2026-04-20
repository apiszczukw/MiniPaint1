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

            if(ostatniRysunek != null)
            {
                plotno.Linie.Remove(ostatniRysunek);
            }

            PointF punktAktualny = e.Touches[0];

            RysujLinie(punktAktualny);

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
    }

}
