using System;
using Microsoft.Maui.Controls;

namespace BSM322_Odev2_SumeyyeCakir
{
    public partial class KrediHesaplamaPage : ContentPage
    {
        public KrediHesaplamaPage()
        {
            InitializeComponent();
        }

        private void sliderVade_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            labelVade.Text = $"{(int)e.NewValue} Ay";
        }

        private void btnHesapla_Clicked(object sender, EventArgs e)
        {
            // giriþ kontrol
            if (pickerKrediTuru.SelectedIndex == -1)
            {
                DisplayAlert("Hata", "Lütfen kredi türünü seçin.", "OK");
                return;
            }

            if (!double.TryParse(entryTutar.Text, out double krediTutari) ||
                !double.TryParse(entryFaiz.Text, out double faizOrani))
            {
                DisplayAlert("Hata", "Geçerli sayýlar giriniz.", "OK");
                return;
            }

            int vade = (int)sliderVade.Value;
            string krediTuru = pickerKrediTuru.SelectedItem.ToString();

            // vergi oranlarý
            double bsmv = 0;
            double kkdf = 0;

            switch (krediTuru)
            {
                case "Ýhtiyaç Kredisi":
                    bsmv = 10;
                    kkdf = 15;
                    break;
                case "Konut Kredisi":
                    bsmv = 0;
                    kkdf = 0;
                    break;
                case "Taþýt Kredisi":
                    bsmv = 5;
                    kkdf = 15;
                    break;
                case "Ticari Kredi":
                    bsmv = 5;
                    kkdf = 0;
                    break;
            }

            // brüt faiz
            double brutFaiz =
                (faizOrani + (faizOrani * bsmv / 100.0) + (faizOrani * kkdf / 100.0)) / 100.0;

            // aylýk faiz
            double aylikFaiz = brutFaiz;

            double aylikTaksit;
            double toplamOdeme;
            double faizYuku;

            // faiz sýfýrsa
            if (aylikFaiz == 0)
            {
                aylikTaksit = krediTutari / vade;
                toplamOdeme = krediTutari;
                faizYuku = 0;
            }
            else
            {
                // aylýk taksit formülü
                double ust = Math.Pow(1 + aylikFaiz, vade);
                aylikTaksit = (ust * aylikFaiz) / (ust - 1) * krediTutari;
                toplamOdeme = aylikTaksit * vade;
                faizYuku = toplamOdeme - krediTutari;
            }

            // sonuçlar
            labelAylikTaksit.Text = $"Aylýk Taksit: {aylikTaksit:N2} TL";
            labelToplamOdeme.Text = $"Toplam Ödeme: {toplamOdeme:N2} TL";
            labelFaizYuku.Text = $"Toplam Faiz: {faizYuku:N2} TL";
        }
    }
}
