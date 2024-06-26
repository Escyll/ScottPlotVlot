namespace ScottPlotVlot
{
    internal class PlotAwareValueList
    {
        private readonly List<double> _values = [];
        public void CopyLastNInto(int N, double[] destination)
        {
            Array.Copy(_values.ToArray(), destination, Math.Min(N, _values.Count));
        }

        public void Add(double value)
        {
            _values.Insert(0, value);
        }

        public int Count()
        {
            return _values.Count;
        }
    }
}
