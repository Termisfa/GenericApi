using MySql.Data.MySqlClient;
using System.Text.Json;
using MySqlDatabase.Handlers;

namespace GenericApi.ApiRestHandler
{
    public class ApiRestHandler : IApiRestHandler
    {
        private readonly IQuerysHandler _querysHandler;

        public ApiRestHandler(IQuerysHandler querysHandler)
        {
            _querysHandler = querysHandler;
        }

        public async Task<string> Get(string schema, string table, string? parameters = default)
        {
            string whereString = Helpers.ParseHelpers.ParseParametersIntoWhereString(parameters);

            string query = $"select * from {table} {whereString}";

            var resultList = await _querysHandler.GetQueryResultAsync(schema, query);

            string result = JsonSerializer.Serialize(resultList);

            return result;
        }

        public async Task<int> Post(HttpObject obj)
        {
            try
            {
                var columnsValues = obj.GetInsertFromDict();

                string query = $"insert into {obj.Table}{columnsValues.columns} values {columnsValues.values}";

                var affectedRows = await _querysHandler.GetNonQueryResultAsync(obj.Schema, query);

                return affectedRows;
            }
            catch (MySqlException e)
            {
                if (e.Number == 1062) //Duplicate entry
                    return -1;
                else
                    throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<int> Put(HttpObject obj, string parameters)
        {
            try
            {
                string whereString = Helpers.ParseHelpers.ParseParametersIntoWhereString(parameters);

                if (string.IsNullOrEmpty(whereString))
                    throw new Exception("Updating without WHERE clause is not allowed");

                var setUpdateString = obj.GetUpdateSetsFromDict();
                string query = $"update {obj.Table} set {setUpdateString} {whereString}";

                var affectedRows = await _querysHandler.GetNonQueryResultAsync(obj.Schema, query);

                return affectedRows;
            }
            catch (MySqlException e)
            {
                if (e.Number == 1062) //Duplicate entry
                    return -1;
                else
                    throw;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<int> Delete(string schema, string table, string parameters)
        {
            string whereString = Helpers.ParseHelpers.ParseParametersIntoWhereString(parameters);

            if (string.IsNullOrEmpty(whereString))
                    throw new Exception("Deleting without WHERE clause is not allowed");

            string query = $"delete from {table} {whereString}";

            var affectedRows = await _querysHandler.GetNonQueryResultAsync(schema, query);

            return affectedRows;
        }

        public async Task<int> DeleteWithoutWhere(string schema, string table)
        {
            string query = $"delete from {table}";

            var affectedRows = await _querysHandler.GetNonQueryResultAsync(schema, query);

            return affectedRows;
        }

        public void DeleteConnectionStrings()
        {
            _querysHandler.DeleteConnectionStrings();
        }
    }
}
