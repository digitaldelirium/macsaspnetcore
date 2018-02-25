
using System;
using System.IO;
using System.Linq;
using MacsASPNETCore.Models;
using MacsASPNETCore.Services;
using MacsASPNETCore.ViewModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using MimeKit;

namespace MacsASPNETCore.Controllers
{
    public class HomeController : Controller
    {
        private IEmailSender _mailService;
        private readonly IHostingEnvironment _env;
        private ReservationDbContext _reservations;
        private CustomerDbContext _customers;
        private IActivityRepository _activityRepository;
        private readonly int year = DateTime.Now.Year;
        private readonly IConfiguration _configuration;
        //private ApplicationDbContext _context;

        public HomeController(IEmailSender mailService, IHostingEnvironment env, IActivityRepository activities, CustomerDbContext customers, ReservationDbContext reservations, ApplicationDbContext context, IConfiguration configuration)
        {
            _mailService = mailService;
            _env = env;
            _activityRepository = activities;
            _customers = customers;
            _reservations = reservations;
            _configuration = configuration;
            //_context = context;
        }

        public IActionResult Index()
        {
            var slidepath = _env.WebRootPath + "/images/titleslide";
            ViewData["Title"] = "Welcome To Mac's Camping Area!";
            var titleSlide = new System.IO.DirectoryInfo(slidepath).GetFiles();
            ViewBag.ImageList = titleSlide;
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Please drop us a line or pay us a visit!";
            ViewData["Title"] = "Contact Mac's";
            ViewBag.onLoad = "getMap()";

            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            ViewData["Message"] = "Please drop us a line or pay us a visit!";
            ViewData["Title"] = "Contact Mac's";
            ViewBag.onLoad = "getMap()";
            var user = _configuration["AppSettings:SendGridUserName"];
            var pass = _configuration["AppSettings:SendGridPassword"];
            //var credential = new NetworkCredential(user, pass);

            if (ModelState.IsValid)
            {
                if (_env.IsDevelopment())
                {
                    ViewBag.MailMessage = "Dummy Mail sent, Thanks!";
                    return View();
                }

                using (var client = new SmtpClient())
                {
                    var message = new MimeMessage();
                    string name = model.FirstName + " " + model.LastName;
                    DateTime dt = DateTime.UtcNow;
                    TimeZoneInfo tz;
                    try
                    {
                        tz = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    }
                    catch (System.Exception)
                    {
                        tz = TimeZoneInfo.FindSystemTimeZoneById("posixrules");
                    }

                    var utcOffset = new DateTimeOffset(dt, TimeSpan.Zero);
                    var currentTime = utcOffset.ToOffset(tz.GetUtcOffset(utcOffset));
                    var email = _configuration["AppSettings:InfoEmailAddress"];
                    string emailFormatted = $"Contact Page Email \n from {name} <{model.EmailAddress}>\n Phone:  {model.PhoneNumber} sent at {currentTime}: \n\n {model.EmailBody}";

                    message.From.Add(new MailboxAddress(name, email));
                    message.To.Add(new MailboxAddress("Contact Page Email", email));
                    message.Subject = model.Subject;
                    message.Body = new TextPart("plain")
                    {
                        Text = emailFormatted
                    };

                    client.Connect("smtp.sendgrid.net", Convert.ToInt32(587), SecureSocketOptions.Auto);
                    client.Authenticate(user, pass);
                    client.Send(message);
                    client.Disconnect(true);
                }
                
                ModelState.Clear();
                ViewBag.MailMessage = "Mail sent, Thanks!";
            }
            return View();
        }
        public IActionResult Rates()
        {
            ViewData["Title"] = $"{year} Rates";
            return View();
        }

        public IActionResult RV()
        {
            var slidepath1 = _env.WebRootPath +  "/images/trailer1";
            var slidepath2 = _env.WebRootPath +  "/images/trailer2";
            ViewData["Title"] = "RV Rentals";
            ViewData["Message"] = "2 RV Rentals Available";
            var trailer1Slide = new System.IO.DirectoryInfo(slidepath1).GetFiles();
            var trailer2Slide = new System.IO.DirectoryInfo(slidepath2).GetFiles();
            ViewBag.ImageList = trailer1Slide;
            ViewBag.ImageList2 = trailer2Slide;
            return View();
        }

        public IActionResult Amenities()
        {
            ViewData["Title"] = "Mac's Amenities";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Activities()
        {
            var activityPics = new System.IO.DirectoryInfo(_env.WebRootPath +  "/images/activityslide").GetFiles();
            var activities = _activityRepository.GetAllActivities()
                .AsQueryable();

            ViewData["Title"] = $"{year} Activities";
            ViewData["Message"] = "See what's going on at Mac's this year";
            ViewBag.ImageList = activityPics;
            return View(activities);
        }
        [HttpPost]
        public JsonResult Activities(DateTime startDate, DateTime endDate)
        {
            ViewBag.onLoad = "pageInit();";
            ViewBag.ngApp = "ActivitiesApp";
            ViewBag.ngController = "ActivitiesController";
            var activityPics = new System.IO.DirectoryInfo(_env.WebRootPath +  "/images/activityslide").GetFiles();
            var activities = _activityRepository.GetAllActivitiesByMonth(startDate, endDate).AsEnumerable();
            ViewData["Title"] = $"{year} Activities";
            ViewData["Message"] = "See what's going on at Mac's this year";
            ViewBag.ImageList = activityPics;
            return Json(activities);
        }
    }
}
