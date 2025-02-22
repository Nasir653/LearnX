namespace LearnX_Server
{
  public interface ITokenService
  {
    string CreateToken(string userId, string email, string username);
    Guid VerifyTokenAndGetId(string token);

  }
}
