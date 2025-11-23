using Microsoft.Maui.Controls;

namespace BSM322_Odev2_SumeyyeCakir
{
    public partial class VkiPage : ContentPage
    {
        private bool _kiloEntryEditing = false;
        private bool _boyEntryEditing = false;

        public VkiPage()
        {
            InitializeComponent();

            // baþlangýç deðerleri (çarpým uygulanýr)
            sliderKilo.Value = 70.50 * 100;
            sliderBoy.Value = 175.80 * 100;

            entryKilo.Text = (sliderKilo.Value / 100).ToString("N2");
            entryBoy.Text = (sliderBoy.Value / 100).ToString("N2");

            HesaplaVki();
        }

        // --- kilo slider ---
        private void sliderKilo_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            double kilo = e.NewValue / 100;

            if (!_kiloEntryEditing)
                entryKilo.Text = kilo.ToString("N2");

            labelKilo.Text = $"{kilo:N2} kg";
            HesaplaVki();
        }

        // --- kilo entry ---
        private void entryKilo_TextChanged(object sender, TextChangedEventArgs e)
        {
            _kiloEntryEditing = true;

            if (double.TryParse(entryKilo.Text, out double kilo))
            {
                kilo = Math.Clamp(kilo, 0, 300);
                sliderKilo.Value = kilo * 100;
            }

            _kiloEntryEditing = false;
        }

        // --- boy slider ---
        private void sliderBoy_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            double boy = e.NewValue / 100;

            if (!_boyEntryEditing)
                entryBoy.Text = boy.ToString("N2");

            labelBoy.Text = $"{boy:N2} cm";
            HesaplaVki();
        }

        // --- boy entry ---
        private void entryBoy_TextChanged(object sender, TextChangedEventArgs e)
        {
            _boyEntryEditing = true;

            if (double.TryParse(entryBoy.Text, out double boy))
            {
                boy = Math.Clamp(boy, 0, 300);
                sliderBoy.Value = boy * 100;
            }

            _boyEntryEditing = false;
        }

        // --- vki hesaplama ---
        private void HesaplaVki()
        {
            if (!double.TryParse(entryKilo.Text, out double kilo) ||
                !double.TryParse(entryBoy.Text, out double boyCm) ||
                boyCm == 0)
            {
                labelVki.Text = "VKÝ: 0.00";
                labelDurum.Text = "Durum: Veri eksik";
                return;
            }

            double boyM = boyCm / 100.0;
            double vki = kilo / (boyM * boyM);

            labelVki.Text = $"VKÝ: {vki:N2}";

            string durum =
                vki < 16 ? "Ýleri Düzeyde Zayýf" :
                vki < 17 ? "Orta Düzeyde Zayýf" :
                vki < 18.5 ? "Hafif Düzeyde Zayýf" :
                vki < 25 ? "Normal Kilolu" :
                vki < 30 ? "Fazla Kilolu" :
                vki < 35 ? "1. Derece Obez" :
                vki < 40 ? "2. Derece Obez" :
                "3. Derece Obez (Morbid Obez)";

            labelDurum.Text = $"Durum: {durum}";
        }
    }
}
