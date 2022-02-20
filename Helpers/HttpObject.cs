using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GenericApi
{
    public class HttpObject
    {
        public string Schema { get; set; }
        public string Table { get; set; }
        public Dictionary<string, string> NameValueDict { get; set; }

        public HttpObject()
        {
        }

        public (string columns, string values) GetInsertFromDict()
        {
            string columns = "(";
            string values = "(";

            foreach (var item in this.NameValueDict)
            {
                columns += item.Key + ",";
                values += $"'{item.Value}',";
            }
            
            //To remove the last ',' of both
            columns = columns.Substring(0, columns.Length - 1) + ")";
            values = values.Substring(0, values.Length - 1) + ")";

            return (columns, values);
        }

        public string GetUpdateSetsFromDict()
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

    }
}
