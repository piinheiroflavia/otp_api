namespace Teste_TOPT_Swagger.Service.IService
{
    public interface ILoginService
    { 
        bool AuthLogin(string username, string password);
        string GenerateJwtToken(string username);
    }
}
