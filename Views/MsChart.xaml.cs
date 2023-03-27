using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;
using TestTask.Helpers;

namespace TestTask.Views
{
    /// <summary>
    /// Логика взаимодействия для MsChart.xaml
    /// </summary>
    public partial class MsChart : UserControl
    {
        public MsChart()
        {
            InitializeComponent();
            SeriesCollection = new ObservableCollection<Series>();
        }

        public static DependencyProperty XValueTypeProperty = DependencyProperty.Register("XValueType", typeof(string),
            typeof(MsChart), new FrameworkPropertyMetadata("Double", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string XValueType
        {
            get { return (string)GetValue(XValueTypeProperty); }
            set { SetValue(XValueTypeProperty, value); }
        }

        public static DependencyProperty XLabelProperty = DependencyProperty.Register("XLabel", typeof(string),
            typeof(MsChart), new FrameworkPropertyMetadata("X Axis", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string XLabel
        {
            get { return (string)GetValue(XLabelProperty); }
            set { SetValue(XLabelProperty, value); }
        }

        public static DependencyProperty YLabelProperty = DependencyProperty.Register("YLabel", typeof(string),
            typeof(MsChart), new FrameworkPropertyMetadata("Y Axis", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string YLabel
        {
            get { return (string)GetValue(YLabelProperty); }
            set { SetValue(YLabelProperty, value); }
        }

        public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string),
            typeof(MsChart), new FrameworkPropertyMetadata("My Title", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty SeriesCollectionProperty = DependencyProperty.Register("SeriesCollection",
                typeof(ObservableCollection<Series>), typeof(MsChart), new FrameworkPropertyMetadata(null,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSeriesChanged));

        public ObservableCollection<Series> SeriesCollection
        {
            get { return (ObservableCollection<Series>)GetValue(SeriesCollectionProperty); }
            set { SetValue(SeriesCollectionProperty, value); }
        }

        private static void OnSeriesChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var ms = sender as MsChart;
            var sc = e.NewValue as ObservableCollection<Series>;
            if (sc != null)
                sc.CollectionChanged += ms.sc_CollectionChanged;
        }

        private void sc_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (SeriesCollection != null)
            {
                CheckCount = 0;
                if (SeriesCollection.Count > 0)
                    CheckCount = SeriesCollection.Count;
            }
        }

        private static DependencyProperty CheckCountProperty = DependencyProperty.Register("CheckCount", typeof(int),
            typeof(MsChart), new FrameworkPropertyMetadata(0, StartChart));

        private int CheckCount
        {
            get { return (int)GetValue(CheckCountProperty); }
            set { SetValue(CheckCountProperty, value); }
        }

        private static void StartChart(object sender, DependencyPropertyChangedEventArgs e)
        {
            var ms = sender as MsChart;
            if (ms.CheckCount > 0)
            {
                ms.myChart.Visible = true;
                ms.myChart.Series.Clear();
                ms.myChart.Titles.Clear();
                ms.myChart.Legends.Clear();
                ms.myChart.ChartAreas.Clear();

                MSChartHelper.MyChart(ms.myChart, ms.SeriesCollection, ms.Title, ms.XLabel, ms.YLabel);

                ms.myChart.DataBind();
            }
            else
                ms.myChart.Visible = false;
        }
    }
}
