using Microsoft.AspNetCore.Mvc;
using RestWebScrapping.Domain.Services;

namespace RestWebScrapping.Controllers;

[ApiController]
[Route("[controller]")]
public class WebScrappingController : ControllerBase
{
    private readonly ITheWorldBankService _theWorldBankService;
    private readonly IOffshoreLeaksService _offshoreLeaksService;
    private readonly IOfacService _ofacService;

    public WebScrappingController(ITheWorldBankService theWorldBankService, IOffshoreLeaksService offshoreLeaksService, IOfacService ofacService)
    {
        _theWorldBankService = theWorldBankService;
        _offshoreLeaksService = offshoreLeaksService;
        _ofacService = ofacService;
    }

    [HttpGet("the-world-bank")]
    public IActionResult GetTheWorldBankEntities()
    {
        //ok status 200...
        return Ok(_theWorldBankService.GetEntities());
    }

    [HttpGet("offshore-leaks")]
    public IActionResult GetOffshoreLeaksEntities()
    {
        return Ok(_offshoreLeaksService.GetEntities());
    }

    [HttpGet("ofac")]
    public IActionResult GetOfacEntities()
    {
        return Ok(_ofacService.GetEntities());
    }

}