namespace GenericApi.Helpers
{
    public static class ParseHelpers
    {
        public static Dictionary<string, string> ParametersToDict(string parameters)
        {
            Dictionary<string, string> result = new();
            if (!string.IsNullOrEmpty(parameters))
            {
                string[] parts = parameters.Substring(1, parameters.Length - 2).Trim().Split('|');

                foreach (string keyValuePair in parts)
                {
                    string[] keyValueSeparated = keyValuePair.Trim().Split('=');
                    result.Add(keyValueSeparated[0].Trim(), keyValueSeparated[1].Trim());
                }
            }

            return result;
        }

        public static string ParseParametersIntoWhereString(string parameters)
        {
            string result = string.Empty;

            Dictionary<string, string> parametersDict = ParametersToDict(parameters);

            if (parametersDict.Count > 0)
            {
                result += "where ";

                foreach (var keyValuePair in parametersDict)
                {
                    if (keyValuePair.Value[0] != '%')
                        result += $"{keyValuePair.Key} = '{keyValuePair.Value}' and ";
                    else
                        result += $"{keyValuePair.Key} = {keyValuePair.Value.Substring(1)} and ";

                }

                result = result.Substring(0, result.Length - 5); //To remove the last ' and '
            }

            return result;
        }

    }
}
