using LoginTokenTask.Models;
using LoginTokenTask.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Net;

namespace LoginTokenTask.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class GadgetController : ControllerBase
    {
        private readonly ILogger<GadgetController> _logger;
        private readonly IGadgetService _gadgetService;
       // private readonly IDataProtector _protector;


        //dependency injection
        public GadgetController(IGadgetService gadgetService, ILogger<GadgetController> logger)
        {
            _gadgetService = gadgetService;
            _logger = logger;
            //_protector = dataProtector.CreateProtector("");
            //_protector = dataProtector;
        }

        //[HttpPost]
        //[Route("refreshToken")]
        //public async Task<ActionResult<string>> RefreshToken()
        //{
        //    var refreshToken = Request.Cookies["refreshToken"];
        //    if(!LoginUser.Refresh)
        //}


        #region Login
        [HttpPost]
        [Route("Login")]     //routing
        public ActionResult Login(LoginUser loginUser)
        {
            //login 
            string result = _gadgetService.Login(loginUser);
            return Ok(result);

        }


        #endregion

        #region Get All Gadgets
        [Authorize]
        [HttpGet]
        [Route("GetAllGadgets")]
        public ActionResult GetAlllabour()
        {
            //file logger
            _logger.LogInformation("Gadget Information");


            //list of gadgets
            List<Gadget> gadget = _gadgetService.GetAllGadget();

            //gadget.ForEach(a =>
            //{
            //    a.GadgetId = _protector.Protect(a.GadgetId.ToString());
            //});
            return Ok(gadget);
        }

        #endregion

    }
}
