namespace GenericApi.Helpers
{
    public static class ParseHelpers
    {
        public static string ParseParametersIntoWhereString(string parameters)
        {
            try
            {
                string result = string.Empty;

                if (!string.IsNullOrEmpty(parameters))
                {
                    result += "where ";

                    string[] parts = parameters.Substring(1, parameters.Length - 2).Trim().Split("|$|");

                    foreach (string keyValuePair in parts)
                    {
                        string[] parameterSplitted = keyValuePair.Trim().Split("$$$", 3);

                        result += $"{parameterSplitted[0]} {parameterSplitted[1]} {parameterSplitted[2]} and ";
                    }

                    result = result.Substring(0, result.Length - 5); //To remove the last ' and '
                }

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
