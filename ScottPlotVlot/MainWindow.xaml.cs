using System.Timers;
using System.Windows;

namespace ScottPlotVlot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ParameterSourceService _parameterSourceService = new();
        private readonly System.Timers.Timer _signalTimer = new() { Interval = 10, Enabled = true };
        private readonly IDictionary<string, PlotAwareValueList> _parameterValues = new Dictionary<string, PlotAwareValueList>();
        private readonly IDictionary<string, double[]> _signalBuffers = new Dictionary<string, double[]>();
        private readonly string[] _parameterNames = [];
        private readonly IDictionary<string, ScottPlot.Plottables.Signal> _signals = new Dictionary<string, ScottPlot.Plottables.Signal>();
        private readonly int _maxValues = 18000;

        public MainWindow()
        {
            InitializeComponent();
            _parameterNames = ["Temp1", "Temp2", "Temp3"];
            foreach (string parameterName in _parameterNames)
            {
                _parameterValues[parameterName] = new();
                _signalBuffers[parameterName] = new double[_maxValues];
                _signals[parameterName] = WpfPlot1.Plot.Add.Signal(_signalBuffers[parameterName]);
                _signals[parameterName].MaxRenderIndex = 0;
            }
  
            WpfPlot1.Plot.Axes.SetLimitsX(1800 - 1, 0);
            WpfPlot1.Plot.Axes.SetLimitsY(-3, 3);
            WpfPlot1.Refresh();

            _signalTimer.Elapsed += (s, e) => AddNewData();
        }

        public void AddNewData()
        {
            var newValues = _parameterSourceService.GetParameters(_parameterNames);
            foreach (var item in newValues)
            {
                _parameterValues[item.Key].Add(item.Value);
                _parameterValues[item.Key].CopyLastNInto(_maxValues, _signalBuffers[item.Key]);
                _signals[item.Key].MaxRenderIndex = Math.Min(_parameterValues[item.Key].Count() - 1, _maxValues);
            }
            WpfPlot1.Refresh();
        }
    }
}