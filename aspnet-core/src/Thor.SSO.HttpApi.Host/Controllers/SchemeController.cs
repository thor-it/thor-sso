using Aguacongas.AspNetCore.Authentication;
using Aguacongas.AspNetCore.Authentication.EntityFramework;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Thor.SSO.Controllers
{
    public class AuthenticationViewModel
    {
        [Required]
        public string Scheme { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }

        public string HandlerType { get; set; }

        public PathString CallbackPath { get; internal set; }
    }


    public class SchemeController : AbpController
    {
        private readonly PersistentDynamicManager<SchemeDefinition> _manager;
        private readonly SignInManager<Volo.Abp.Identity.IdentityUser> _signInManager;

        public SchemeController(PersistentDynamicManager<SchemeDefinition> manager, SignInManager<Volo.Abp.Identity.IdentityUser> signInManager)
        {
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            _signInManager = signInManager;
        }

        // Creates a new scheme
        [HttpPost("Create/{type}")]
        public async Task<IActionResult> Create(AuthenticationViewModel model)
        {
            OAuthOptions oAuthOptions;
            if (model.HandlerType == "GoogleHandler")
            {
                oAuthOptions = new GoogleOptions();
            }
            else if (model.HandlerType == "JwtBearerHandler") {
                var handlerTypeJwt = _manager.ManagedHandlerType.First(t => t.Name == model.HandlerType);
                await _manager.AddAsync(new SchemeDefinition
                {
                    Scheme = model.Scheme,
                    DisplayName = model.DisplayName,
                    HandlerType = handlerTypeJwt,
                    Options = new JwtBearerOptions()
                    {
                        Audience = "Us"
                    }
                });
                return new RedirectToActionResult("List", "Scheme", null);
            }
            else
            {
                oAuthOptions = new OAuthOptions();
            }

            oAuthOptions.ClientId = model.ClientId;
            oAuthOptions.ClientSecret = model.ClientSecret;
            oAuthOptions.CallbackPath = "/signin-" + model.Scheme;

            var handlerType = _manager.ManagedHandlerType.First(t => t.Name == model.HandlerType);
            await _manager.AddAsync(new SchemeDefinition
            {
                Scheme = model.Scheme,
                DisplayName = model.DisplayName,
                HandlerType = handlerType,
                Options = oAuthOptions
            });
            return new RedirectToActionResult("List", "Scheme", null);
        }

        // Returns a scheme details view to update id
        [HttpGet("getforcreate/{scheme}")]
        public async Task<object> GetForCreate(string scheme)
        {
            AuthenticationViewModel model;
            var definition = await _manager.FindBySchemeAsync(scheme);
            if (definition == null)
            {
                return new AuthenticationViewModel
                {
                    Scheme = "Bearer",
                    DisplayName = "Google external IDP",
                    HandlerType = "OpenID Handler",
                    CallbackPath = "/loginredirect",
                    ClientId = "boe",
                    ClientSecret = "secret"
                };
            }
            else
            {
                model = new AuthenticationViewModel
                {
                    Scheme = definition.Scheme,
                    DisplayName = definition.DisplayName,
                    HandlerType = definition.HandlerType.Name
                };

                var oAuthOptions = definition.Options as OAuthOptions; // GoogleOptions is OAuthOptions
                model.ClientId = oAuthOptions.ClientId;
                model.ClientSecret = oAuthOptions.ClientSecret;
            }

            return model;
        }

        // Updates a scheme
        [HttpPost("Update/{scheme}")]
        public async Task Update(AuthenticationViewModel model)
        {
            var definition = await _manager.FindBySchemeAsync(model.Scheme);
            if (definition == null)
            {
                return;
            }

            if (definition.Options is OAuthOptions oAuthOptions) // GoogleOptions is OAuthOptions
            {
                oAuthOptions.ClientId = model.ClientId;
                oAuthOptions.ClientSecret = model.ClientSecret;
            }

            definition.DisplayName = model.DisplayName;

            await _manager.UpdateAsync(definition);
        }

        // Deletes a scheme
        [HttpPost("Delete/{scheme}")]
        public async Task<IActionResult> Delete(string scheme)
        {
            var definition = await _manager.FindBySchemeAsync(scheme);
            if (definition == null)
            {
                return NotFound();
            }

            await _manager.RemoveAsync(scheme);
            return RedirectToAction("List");
        }

        // Lists all schemes we can manage
        [HttpGet("list")]
        public async Task<object> List()
        {
            var schemes = await _signInManager.GetExternalAuthenticationSchemesAsync();

            var managedSchemes = schemes.Where(s => _manager.ManagedHandlerType.Any(h => s.HandlerType == h))
                .Select(s => s.Name);

            var definitions = managedSchemes.Select(name => _manager.FindBySchemeAsync(name).GetAwaiter().GetResult());
            return definitions.Select(definition => new AuthenticationViewModel
            {
                Scheme = definition.Scheme,
                DisplayName = definition.DisplayName,
                CallbackPath = definition.Options is RemoteAuthenticationOptions remote ? remote.CallbackPath : null
            }).ToList();
        }
    }
}
