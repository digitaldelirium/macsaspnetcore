
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MacsASPNETCore.Models;
using MacsASPNETCore.Services;
using MacsASPNETCore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

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

            var slidepath = "wwwroot/images/titleslide";
            ViewData["Title"] = "Welcome To Mac's Camping Area!";
            var titleSlide = new System.IO.DirectoryInfo(slidepath).GetFiles();
            var images = new List<String>();


            foreach (var item in titleSlide)
            {
                images.Add(item.FullName.Split('/').Last());
            }
            ViewBag.ImageList = images;
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

            if (ModelState.IsValid)
            {
                if (_env.IsDevelopment())
                {
                    ViewBag.MailMessage = "Dummy Mail sent, Thanks!";
                    return View();
                }

                Task<SendGrid.Response> response = SendContactMessage(model);
                response.Wait();

                ViewBag.MailMessage = "Message Sent!";

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
            var slidepath1 = _env.WebRootPath + "/images/lakeview";
            var slidepath2 = _env.WebRootPath + "/images/lilypad";
            ViewData["Title"] = "RV Rentals";
            ViewData["Message"] = "2 RV Rentals Available";
            var trailer1Slide = new System.IO.DirectoryInfo(slidepath1).GetFiles();
            var lakeviewPics = new List<String>();
            foreach (var item in trailer1Slide)
            {
                lakeviewPics.Add(item.FullName.Split('/').Last());
            }

            var trailer2Slide = new System.IO.DirectoryInfo(slidepath2).GetFiles();
            var lilypadPics = new List<String>();

            foreach (var item in trailer2Slide)
            {
                lilypadPics.Add(item.FullName.Split('/').Last());
            }
            ViewBag.ImageList = lakeviewPics;
            ViewBag.ImageList2 = lilypadPics;
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
            var activityPics = new System.IO.DirectoryInfo(_env.WebRootPath + "/images/activityslide").GetFiles();
            var activities = _activityRepository.GetAllActivities().AsQueryable();

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
            var activityPics = new System.IO.DirectoryInfo(_env.WebRootPath + "/images/activityslide").GetFiles();
            var activities = _activityRepository.GetAllActivitiesByMonth(startDate, endDate).AsEnumerable();
            ViewData["Title"] = $"{year} Activities";
            ViewData["Message"] = "See what's going on at Mac's this year";
            ViewBag.ImageList = activityPics;
            return Json(activities);
        }

        public IActionResult CampMap()
        {
            ViewData["Title"] = "Map of Mac's Camping Area";
            ViewData["Message"] = "Get a lay of the land at Mac's!";
            return View();
        }

        public async Task<SendGrid.Response> SendContactMessage(ContactViewModel model)
        {
            var senderName = $"{model.FirstName} {model.LastName}";
            var senderEmail = new EmailAddress(model.EmailAddress, senderName);
            var recipientEmail = new EmailAddress(_configuration.GetSection("AppSettings:InfoEmailAddress").Value, "Contact Form Email");
            var emailSubject = model.Subject;
            var emailBody = model.EmailBody;
            var apikey = _configuration.GetSection("AppSettings").GetSection("SendGridAPIKey").Value;
            var client = new SendGridClient(apikey);
            var msg = MailHelper.CreateSingleEmail(senderEmail, recipientEmail, emailSubject, emailBody, null);
            var response = await client.SendEmailAsync(msg);
            return response;
        }
    }
}
