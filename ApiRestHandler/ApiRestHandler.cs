using MySql.Data.MySqlClient;
using System.Text.Json;
using MySqlDatabase.Handlers;
using MySqlDatabase.Helpers;
using GenericApi.Models;

namespace GenericApi.ApiRestHandler
{
    public class ApiRestHandler : IApiRestHandler
    {
        private readonly IQuerysHandler _querysHandler;

        public ApiRestHandler(IQuerysHandler querysHandler)
        {
            _querysHandler = querysHandler;
        }

        public async Task<Response> Get(string schema, string table, string? parameters = default)
        {
            try
            {
                string whereString = Helpers.ParseHelpers.ParseParametersIntoWhereString(parameters);

                string query = $"select * from {table} {whereString}";

                var resultList = await _querysHandler.GetQueryResultAsync(schema, query);

                string queryResult = JsonSerializer.Serialize(resultList);

                return Response.SuccesfulResponse(queryResult);
            }
            catch (Exception e)
            {
                return Response.UnsuccesfulResponseFromException(e);
            }

        }

        public async Task<Response> Post(HttpObject obj)
        {
            try
            {
                var columnsValues = obj.GetInsertFromDict();

                string query = $"insert into {obj.Table}{columnsValues.columns} values {columnsValues.values}";

                var affectedRows = await _querysHandler.GetNonQueryResultAsync(obj.Schema, query);

                return Response.SuccesfulResponse(affectedRows.ToString());
            }
            catch (Exception e)
            {
                return Response.UnsuccesfulResponseFromException(e);
            }
        }

        public async Task<Response> Put(HttpObject obj, string parameters)
        {
            try
            {
                string whereString = Helpers.ParseHelpers.ParseParametersIntoWhereString(parameters);

                if (string.IsNullOrEmpty(whereString))
                    throw new Exception("Updating without WHERE clause is not allowed");

                var setUpdateString = obj.GetUpdateSetsFromDict();
                string query = $"update {obj.Table} set {setUpdateString} {whereString}";

                var affectedRows = await _querysHandler.GetNonQueryResultAsync(obj.Schema, query);

                return Response.SuccesfulResponse(affectedRows.ToString());
            }
            catch (Exception e)
            {
                return Response.UnsuccesfulResponseFromException(e);
            }
        }

        public async Task<Response> Delete(string schema, string table, string parameters)
        {
            try
            {
                string whereString = Helpers.ParseHelpers.ParseParametersIntoWhereString(parameters);

                if (string.IsNullOrEmpty(whereString))
                    throw new Exception("Deleting without WHERE clause is not allowed");

                string query = $"delete from {table} {whereString}";

                var affectedRows = await _querysHandler.GetNonQueryResultAsync(schema, query);

                return Response.SuccesfulResponse(affectedRows.ToString());
            }
            catch (Exception e)
            {
                return Response.UnsuccesfulResponseFromException(e);
            }
            
        }

        public async Task<Response> DeleteWithoutWhere(string schema, string table)
        {
            try
            {
                string query = $"delete from {table}";

                var affectedRows = await _querysHandler.GetNonQueryResultAsync(schema, query);

                return Response.SuccesfulResponse(affectedRows.ToString());
            }
            catch (Exception e)
            {
                return Response.UnsuccesfulResponseFromException(e);
            }
            
        }

        public async Task<Response> ResetConnections()
        {
            try
            {
                _querysHandler.ResetConnections();
                return Response.SuccesfulResponse(string.Empty);
            }
            catch (Exception e)
            {
                return Response.UnsuccesfulResponseFromException(e);
            }
        }
    }
}
