using MBBrickwork.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace MBBrickwork.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;

        // Inject IWebHostEnvironment to access wwwroot
        public HomeController(IWebHostEnvironment env)
        {
            _env = env;
        }

        // --- Action Methods ---
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }

        // --- Mail Sending Method ---
        public bool MailSender(string name, string email, string query)
        {
            try
            {
                // Build path to the template in wwwroot/assets
                string templatePath = Path.Combine(
                    _env.WebRootPath, // points to wwwroot
                    "assets",
                    "ContactUsEmailTemplate.html"
                );

                // Check if the template exists
                if (!System.IO.File.Exists(templatePath))
                    throw new FileNotFoundException("Email template not found", templatePath);

                // Read template
                string body = System.IO.File.ReadAllText(templatePath);

                // Replace placeholders with actual values
                body = body.Replace("{Name}", name)
                           .Replace("{Email}", email)
                           .Replace("{Query}", query);

                // Prepare the email
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("tradereddie30@gmail.com"),
                    Subject = "You've been contacted by a new client",
                    Body = body,
                    IsBodyHtml = true
                };
                mail.To.Add(email);

                // Configure SMTP client (Gmail)
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(
                        "tradereddie30@gmail.com",
                        "zrsh wbsf lrki usbo" // Gmail app password
                    ),
                    EnableSsl = true
                };

                // Send the email
                smtp.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                // Optional: log ex.Message for debugging
                return false;
            }
        }

        // --- Error Handling ---
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
