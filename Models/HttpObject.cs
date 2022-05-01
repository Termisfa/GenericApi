using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GenericApi
{
    public class HttpObject
    {
        public Dictionary<string, string> NameValueDict { get; set; }

        public HttpObject()
        {
        }

        public string GetQueryFromDict(string table)
        {
            try
            {
                string columns = "(" + string.Join(',', this.NameValueDict.Keys) + ")";
                string values = "('" + string.Join("','", this.NameValueDict.Values) + "')";

                string query = $"insert into {table}{columns} values {values}";

                return query;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string GetUpdateSetsFromDict()
        {
            try
            {
                string result = string.Empty;

                foreach (var item in this.NameValueDict)
                {
                    result += $"{item.Key} = '{item.Value}', ";
                }

                //To remove the last ', '
                result = result.Substring(0, result.Length - 2);

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
