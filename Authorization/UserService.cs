using GenericApi.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySqlDatabase.Handlers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GenericApi.Authorization
{
    public class UserService : IUserService
    {
        private string SECRET_MYSQL_PASSW_KEY;
        private readonly IQuerysHandler _querysHandler;

        public UserService(IQuerysHandler querysHandler)
        {
            SECRET_MYSQL_PASSW_KEY = AppSettingsManager.GetSecretMySqlPasswDecodeKey();
            _querysHandler = querysHandler;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            //var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            User user = GetUserFromCredentials(model.Username, model.Password, model.Schema);

            // return null if user not found
            if (user == default)
                return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        private User GetUserFromCredentials(string username, string password, string schema)
        {
            string queryUserId = $"select id from user where username = '{username}' and password = encode('{password}', '{SECRET_MYSQL_PASSW_KEY}')";

            List<List<string>> resultQuery = _querysHandler.GetQueryResult(Constants.MASTER_SCHEMA, queryUserId);

            if(resultQuery.Count == 0 || resultQuery.First().Count == 0)
            {
                return default;
            }

            int userId = int.Parse(resultQuery.First().First());

            string queryAuth = $"select * from schemas_users where SchemaName = '{schema}' and UserId = {userId}";

            resultQuery = _querysHandler.GetQueryResult(Constants.MASTER_SCHEMA, queryAuth);

            if (resultQuery.Count == 0 || resultQuery.First().Count == 0)
            {
                return default;
            }

            User user = new()
            {
                Id = userId,
                Schema = schema
            };

            return user;
        }

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettingsManager.GetSecretAuthKey());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim("schema", user.Schema) }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
