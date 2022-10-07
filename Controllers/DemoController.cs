using LoginTokenTask.Models;
using LoginTokenTask.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginTokenTask.Controllers
{
    [Route("api/[controller]")]
    //api version
    [ApiVersion("2.0")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<GadgetController> _logger;
        private readonly IGadgetService _gadgetService;

        public DemoController(IGadgetService gadgetService, ILogger<GadgetController> logger)
        {
            _gadgetService = gadgetService;
            _logger = logger;
        }

        #region
        //[HttpPost]
        //[Route("Login(Version)")]
        //public ActionResult Login(LoginUser loginUser)
        //{
        //    string result = _gadgetService.Login(loginUser);
        //    return Ok(result);

        //}
        //[Authorize]
        //[HttpGet]
        //[Route("GetAllGadgets")]
        //public ActionResult GetAlllabour()
        //{
        //    _logger.LogInformation("Gadget Information");
        //    List<Gadget> gadget = _gadgetService.GetAllGadget();
        //    return Ok(gadget);
        //}
        #endregion


        //get all gadgets for version 2
        [HttpGet]
        [Route("GetAllGadgets")]
        public ActionResult GetAllGadgets()
        {

            return new OkObjectResult("Gadgets from v2 controller");
           
        }

    }
}
