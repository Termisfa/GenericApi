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
            string query = string.Empty;

            try
            {
                string whereString = Helpers.ParseHelpers.ParseParametersIntoWhereString(parameters);

                query = $"select * from {table} {whereString}";

                var resultList = await _querysHandler.GetQueryResultAsync(schema, query);

                string queryResult = JsonSerializer.Serialize(resultList);

                return Response.SuccesfulResponse(queryResult, query);
            }
            catch (Exception e)
            {
                return Response.UnsuccesfulResponseFromException(e, query);
            }
        }

        public async Task<Response> Post(string schema, string table, HttpObject obj)
        {
            string query = string.Empty;

            try
            {
                var columnsValues = obj.GetInsertFromDict();

                query = $"insert into {table}{columnsValues.columns} values {columnsValues.values}";

                var affectedRows = await _querysHandler.GetNonQueryResultAsync(schema, query);

                return Response.SuccesfulResponse(affectedRows.ToString(), query);
            }
            catch (Exception e)
            {
                return Response.UnsuccesfulResponseFromException(e, query);
            }
        }

        public async Task<Response> Put(string schema, string table, HttpObject obj, string parameters)
        {
            string query = string.Empty;

            try
            {
                string whereString = Helpers.ParseHelpers.ParseParametersIntoWhereString(parameters);

                if (string.IsNullOrEmpty(whereString))
                    throw new Exception("Updating without WHERE clause is not allowed");

                var setUpdateString = obj.GetUpdateSetsFromDict();
                query = $"update {table} set {setUpdateString} {whereString}";

                var affectedRows = await _querysHandler.GetNonQueryResultAsync(schema, query);

                return Response.SuccesfulResponse(affectedRows.ToString(), query);
            }
            catch (Exception e)
            {
                return Response.UnsuccesfulResponseFromException(e, query);
            }
        }

        public async Task<Response> Delete(string schema, string table, string parameters)
        {
            string query = string.Empty;

            try
            {
                string whereString = Helpers.ParseHelpers.ParseParametersIntoWhereString(parameters);

                if (string.IsNullOrEmpty(whereString))
                    throw new Exception("Deleting without WHERE clause is not allowed");

                query = $"delete from {table} {whereString}";

                var affectedRows = await _querysHandler.GetNonQueryResultAsync(schema, query);

                return Response.SuccesfulResponse(affectedRows.ToString(), query);
            }
            catch (Exception e)
            {
                return Response.UnsuccesfulResponseFromException(e, query);
            }
            
        }

        public async Task<Response> DeleteWithoutWhere(string schema, string table)
        {
            string query = string.Empty;

            try
            {
                query = $"delete from {table}";

                var affectedRows = await _querysHandler.GetNonQueryResultAsync(schema, query);

                return Response.SuccesfulResponse(affectedRows.ToString(), query);
            }
            catch (Exception e)
            {
                return Response.UnsuccesfulResponseFromException(e, query);
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
