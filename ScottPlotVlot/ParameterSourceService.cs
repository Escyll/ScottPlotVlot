namespace ScottPlotVlot
{
    internal class ParameterSourceService
    {
        private int _t;
        public IDictionary<string, double> GetParameters(string[] parameterNames)
        {
            var parameters = parameterNames.Select(parameter => new { key = parameter, value = GetParameter(parameter) }).ToDictionary(t => t.key, t => t.value);
            _t++;
            return parameters;
        }

        public double GetParameter(string parameterName)
        {
            return parameterName switch
            {
                "Temp1" => Math.Sin(_t/100.0),
                "Temp2" => Math.Cos((_t + 30) / 100.0) + 1.2,
                "Temp3" => 0.5*Math.Cos((_t + 30) / 100.0) + 0.4,
                _ => throw new ArgumentException($"ParameterName {parameterName} not supported."),
            };
        }
    }
}
