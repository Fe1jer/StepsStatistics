using System.Collections.ObjectModel;
using System.Windows.Forms.DataVisualization.Charting;

namespace TestTask.Helpers
{
    /// <summary>
    /// Помощник работы с MSChart
    /// </summary>
    public static class MSChartHelper
    {
        public static void MyChart(Chart chart1, ObservableCollection<Series> chartSeries, string chartTitle, string xLabel, string yLabel)
        {
            if (chart1.ChartAreas.Count < 1)
            {
                ChartArea area = new ChartArea();
                chart1.ChartAreas.Add(area);
                chart1.Invalidate();
            }

            if (chartTitle != "")
                chart1.Titles.Add(chartTitle);
            chart1.ChartAreas[0].AxisX.Title = xLabel;
            chart1.ChartAreas[0].AxisY.Title = yLabel;

            foreach (var ds in chartSeries)
                chart1.Series.Add(ds);
        }
    }
}
