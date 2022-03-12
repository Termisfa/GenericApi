namespace GenericApi.Authorization
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Token = token;
        }
    }
}
