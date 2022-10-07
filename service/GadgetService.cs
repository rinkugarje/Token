using LoginTokenTask.Models;
using LoginTokenTask.Repository;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace LoginTokenTask.service
{
    public class GadgetService:IGadgetService
    {
        private readonly IGadgetRepository _gadgetRepository;
        private readonly IConfiguration _configuration;
        public GadgetService(IGadgetRepository gadgetRepository, IConfiguration configuration)
        {
            _gadgetRepository = gadgetRepository;
            _configuration = configuration;
        }

        public string Login(LoginUser loginUser)
        {
            bool result = _gadgetRepository.Login(loginUser);
            if (result)
            {
                string token = CreateToken(loginUser);


               // var refreshToken = GenerateRefreshToken();
               // SetRefreshToken(refreshToken);
                return token;
            }
            else
            {
                return "Unsuccessfull login";
            }
        }



        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken()
            {

                //convert:used to covert into another base data type
                //RandomNumberGenerator:implementation of all cryptography random number
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires=DateTime.Now.AddHours(24),
                Created=DateTime.Now
            };
            return refreshToken;
        }

        #region
        //private void SetRefreshToken(RefreshToken newrefreshToken)
        //{
        //    var cookieOptions = new CookieOptions()
        //    {
        //        HttpOnly = true,
        //        Expires = newrefreshToken.Expires

        //    };
        //    Response.Cookies.Append("refreshToken", newrefreshToken.Token, cookieOptions);
        //    loginUser.RefreshToken = newrefreshToken.Token;
        //    loginUser.TokenCreted = newrefreshToken.Created;
        //    loginUser.TokenExpires = newrefreshToken.Expires;
        //}
        #endregion


        #region Crete Token
        //Create token
        public string CreateToken(LoginUser loginUser)
        {
            //claims: store info
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginUser.UserName),

            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = GenerateRefreshToken();
            string securityToken = JsonConvert.SerializeObject(new { Token = jwt, refreshToken1=refreshToken.Token });
            return securityToken;
        }
        #endregion


        //get All gadgets
        public List<Gadget> GetAllGadget()
        {
            return _gadgetRepository.getAllGadget();
        }

    }
}
