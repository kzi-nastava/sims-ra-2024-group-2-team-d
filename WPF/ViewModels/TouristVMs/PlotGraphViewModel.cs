using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts.Wpf;
using LiveCharts;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class PlotGraphViewModel
    {
        public SeriesCollection SeriesCollection { get; set; }

        public string XAxisTitle { get; set; } // Naslov za x-osu

        // Konstruktor koji prima Dictionary
        public PlotGraphViewModel(Dictionary<string, int> dataDictionary, string xAxisTitle)
        {
            SeriesCollection = new SeriesCollection();

            foreach (var item in dataDictionary)
            {
                SeriesCollection.Add(new ColumnSeries
                {
                    Title = item.Key,
                    Values = new ChartValues<int> { item.Value }
                });
            }

            // Postavite Labels i Formatter ako je potrebno
            Labels = dataDictionary.Keys.ToArray();
            Formatter = value => value.ToString("N0"); // N0 formatira broj bez decimala
            XAxisTitle = xAxisTitle; // Postavljanje naslova x-ose
        }

        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }
    }
}
