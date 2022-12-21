using System.Security.Cryptography;
using System.Text;


namespace JMC.Portal.Web.UI.Helpers
{
    public class SecurityProvider
    {
       
        const string SessionUserName = "UserName";
        const string SessionUserId = "UserId";
        const string SessionUserFullName = "UserFullName";

        private readonly IConfiguration _configuration;

        public SecurityProvider( IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Enums

        public enum LogInResults
        {
            UserNameNotFound = 1,
            AccountNotActive,
            IncorrectPassword,
            ApplicationNotAuthorized,
            Success
        }

        #endregion


        #region Static Methods

        public static bool CheckPassword(string password, string encodedPassword, string salt)
        {
            password = password.Trim();
            string checkPassword = EncodePassword(password, salt);
            return encodedPassword.Equals(checkPassword);
        }

        public static string EncodePassword(string pass, string salt)
        {
            byte[] src = Encoding.Unicode.GetBytes(pass);
            byte[] buffer2 = Convert.FromBase64String(salt);
            byte[] dst = new byte[buffer2.Length + src.Length];
            byte[] inArray;
            Buffer.BlockCopy(buffer2, 0, dst, 0, buffer2.Length);
            Buffer.BlockCopy(src, 0, dst, buffer2.Length, src.Length);
            var algorithm = HashAlgorithm.Create("SHA1");
            inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }

        
        public static string GeneratePassword(int length)
        {
            byte[] data = new byte[length * 2];
            char[] chArray = new char[length];
            int index = 0;
            new RNGCryptoServiceProvider().GetBytes(data);
            for (int i = 0; i < length * 2; i++)
            {
                int charIndex = data[i] % 0x57;
                if (charIndex < 10)
                {
                    // numbers
                    chArray[index] = (char)(0x30 + charIndex);
                    index++;
                }
                else if (charIndex < 0x24)
                {
                    // uppercase 
                    chArray[index] = (char)((0x41 + charIndex) - 10);
                    index++;
                }
                else if (charIndex < 0x3e)
                {
                    // lowercase
                    chArray[index] = (char)((0x61 + charIndex) - 0x24);
                    index++;
                }
                if (index >= length)
                {
                    break;
                }
            }
            return new string(chArray);
        }

        public static string GenerateSalt()
        {
            byte[] data = new byte[0x10];
            new RNGCryptoServiceProvider().GetBytes(data);
            return Convert.ToBase64String(data);
        }
       
        public void SendCustomerAccountCreatedEmail(string username, string password, string emailAddress)
        {

            string appUrl = _configuration["AppServer:ServerName"];
            emailAddress = emailAddress + ";" +_configuration["Email:ToEmailAddress"];

            string body = @"
      <span>An Atlas Tube Web Account has been activated for you with the following information:</span><br />
      <br />
      <ul>
        <li>User Name: " + username + @"</li>
        <li>Password: " + password + @"</li>
      </ul>
      <br />
      //<span>To log in, <a href='" + appUrl +  "'>click here</a> or go to <a href=' " + appUrl + "'>http://www.atlastube.com</a> and enter your User Name and Password.</span>";

            //EmailHelper email = new EmailHelper(_notificationMetadata);
            //email.Send(_notificationMetadata.Value.FromEmailAddress, emailAddress, "Quality Management", body);

        }

        #endregion
    }
}
