using FirstWebAPI.DepenConJectClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DependecyInjectionController : ControllerBase
    {

        private readonly DepenConJectInterface _IDepenConJectInterface;
        private readonly ILogger<DependecyInjectionController> _ILogger;


        //Tity DePenInject
        //public DependecyInjectionController()
        //{
        //    _IDepenConJectInterface = new DepenConJectClass2();
        //}
        public DependecyInjectionController(DepenConJectInterface IDepenConJectInterface, ILogger<DependecyInjectionController> iLogger)
        {
            _IDepenConJectInterface = IDepenConJectInterface;
            _ILogger = iLogger;
        }



        [HttpGet]
        public ActionResult Index()
        {
            _ILogger.LogTrace("LogTrace Logger");
            _ILogger.LogDebug("LogDebug Logger");
            _ILogger.LogInformation("LogInformation Logger");
            _ILogger.LogWarning("LogWarning Logger");
            _ILogger.LogError("LogError Logger");
            _ILogger.LogCritical("LogCritical Logger");
            _IDepenConJectInterface.log("Start Depen");
            return Ok();
        }
    }
}
