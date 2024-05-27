

using CommonService.API.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommonService.API.Controllers.V1._0;


public class ActionsController : BaseController
{
    private readonly IIdentityService _claimSeederService;
    public ActionsController(IIdentityService claimSeederService)
    {
        _claimSeederService = claimSeederService;
    }
    [AllowAnonymous]
    [HttpPost(nameof(CommonServiceClaimSeeder))]
    public async Task<IActionResult> CommonServiceClaimSeeder()
    {

        Assembly asm = Assembly.GetExecutingAssembly();

        var controlleractionlist = asm.GetTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                .Select(x => new { Controller = x.DeclaringType.Name, Action = x.Name, ReturnType = x.ReturnType.Name, Attributes = String.Join(",", x.GetCustomAttributes().Select(a => a.GetType().Name.Replace("Attribute", ""))) })
                .OrderBy(x => x.Controller).ThenBy(x => x.Action).ToList();
        Claimrequest claimrequest = new Claimrequest() { requiredIdToken = true, serviceId = 4, claim = new List<string>() };

        foreach (var controlleraction in controlleractionlist)
        {
            claimrequest.claim.Add(controlleraction.Controller.Replace("Controller", "") + "-" + controlleraction.Action);
        }

        string accessToken = HttpContext.Session.GetString("accessToken");

        var claimsresponse = await _claimSeederService.PostClaimsAsync<Claimrequest>(accessToken, claimrequest);

        return Ok(claimsresponse);
    }



}



