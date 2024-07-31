using OtpNet;
using Teste_TOPT_Swagger.Models;
namespace Teste_TOPT_Swagger.Service
{
    public class TwoFactorAuthService
    {
        private readonly string secretKey;
        private readonly ConfigJwt _configJwt;

        public TwoFactorAuthService()
        {
            secretKey = Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(20));
        }

        public string GenerateTotp()
        {
            var key = Base32Encoding.ToBytes(secretKey);
            var totp = new Totp(key);
            //gera um o código de acordo com a chave secreta
            return totp.ComputeTotp();
        }

        public bool ValidateTotp(string totpCode)
        {
            var key = Base32Encoding.ToBytes(secretKey);
            var totp = new Totp(key);
            // verifica o código gerado em computeTotp
            return totp.VerifyTotp(totpCode, out _, new VerificationWindow(2, 2));
        }

        public string GetSecretKey()
        {
            return secretKey;
        }
    }
}
