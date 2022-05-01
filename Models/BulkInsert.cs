namespace GenericApi.Models
{
    public class BulkInsert
    {
        public List<string> ColumnNames { get; set; }

        public List<List<string>> Rows { get; set; }

        public string GetInsertQuery(string table)
        {
            try
            {
                string columns = "(" + string.Join(',', this.ColumnNames) + ")";
                string values = string.Empty;

                foreach (List<string> row in Rows)
                {
                    values += "\n('" + string.Join("','", row) + "'),";
                }
                values = values[..(values.Length - 1)];
                //string values = "('" + string.Join("','", this.NameValueDict.Values) + "')";

                string query = $"insert into {table}{columns} values {values}";

                return query;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
