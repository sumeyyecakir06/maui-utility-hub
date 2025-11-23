using Microsoft.Maui.Controls;
using Color = Microsoft.Maui.Graphics.Color;

namespace BSM322_Odev2_SumeyyeCakir
{
    public partial class RenkSeciciPage : ContentPage
    {
        private readonly Color baslangicArkaPlan = Colors.White;

        public RenkSeciciPage()
        {
            InitializeComponent();
            // açılış arka planı
            this.BackgroundColor = baslangicArkaPlan;
        }

        private void Renk_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            // rgb değerleri
            int r = (int)sliderRed.Value;
            int g = (int)sliderGreen.Value;
            int b = (int)sliderBlue.Value;

            labelRed.Text = $"R: {r}";
            labelGreen.Text = $"G: {g}";
            labelBlue.Text = $"B: {b}";

            // hex dönüşümü
            string hex = $"#{r:X2}{g:X2}{b:X2}";
            labelHex.Text = hex;

            // arka plan güncelleme
            this.BackgroundColor = Color.FromRgb(r, g, b);
        }

        private async void btnKopyala_Clicked(object sender, EventArgs e)
        {
            // hex kopyalama
            await Clipboard.SetTextAsync(labelHex.Text);
            await DisplayAlert("Kopyalandı", $"{labelHex.Text} panoya kopyalandı.", "OK");
        }

        private void btnRastgele_Clicked(object sender, EventArgs e)
        {
            // rastgele renk
            Random rnd = new Random();
            sliderRed.Value = rnd.Next(0, 256);
            sliderGreen.Value = rnd.Next(0, 256);
            sliderBlue.Value = rnd.Next(0, 256);
        }
    }
}
