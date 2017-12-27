using System;
using System.Net;
using MacsASPNETCore.Services;
using MacsASPNETCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace MacsASPNETCore.Controllers.Api
{
    [Route("api/contact")]
    public class ContactController : Controller
    {
        private readonly IEmailSender _mailService;
        private readonly IConfiguration _configuration;

        public ContactController(IEmailSender service, IConfiguration configuration)
        {
            _mailService = service;
            _configuration = configuration;
        }
        [HttpPost("email")]
        public JsonResult SendMessage(ContactViewModel model)
        {
            var email = _configuration["AppSettings:SiteEmailAddress"];
            if (ModelState.IsValid)
            {
                var message = new MimeMessage();
                string name = model.FirstName + " " + model.LastName;
                DateTime dt = DateTime.UtcNow;
                TimeZoneInfo tz;
                try
                {
                  tz  = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                }
                catch (System.Exception)
                {
                    
                    tz = TimeZoneInfo.FindSystemTimeZoneById("posixrules");
                } 
                var utcOffset = new DateTimeOffset(dt, TimeSpan.Zero);
                var currentTime = utcOffset.ToOffset(tz.GetUtcOffset(utcOffset));
                string emailFormatted = $"Contact Page Email from {name} sent at {currentTime}: \n\n {model.EmailBody}";

                message.From.Add(new MailboxAddress(name, email));
                message.To.Add(new MailboxAddress("Contact Page Email", email));
                message.Subject = model.Subject;
                message.Body = new TextPart("plain")
                {
                    Text = emailFormatted
                };

                try
                {
                    _mailService.SendMail(message);

                    Response.StatusCode = (int) HttpStatusCode.OK;
                    return Json(true);
                }
                catch (Exception ex)
                {
                    string errMsg =
                        $"We were unable to send your message. <br /> Please contact the webmaster with this message response: {ex.Message}";
                    Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    return Json(new {Message = errMsg});
                }
            }
            else
            {
                Redirect("/Home/Contact");
                return Json(new {Message = "Contact form fields missing, Please fill in missing form fields!"});
            }
        }
    }
}