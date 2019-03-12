using AarhusWebDevCoop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace AarhusWebDevCoop.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface
        public ActionResult Index()
        {
            return PartialView("ContactForm", new ContactForm());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }
            else
            {
                // change email info!
                MailMessage message = new MailMessage();
                message.To.Add("toEmail@gmail.com");
                message.Subject = model.Subject;
                message.From = new MailAddress(model.Email, model.Name);
                message.Body = model.Message;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("fromEmail@gmail.com", "password");

                    // send mail
                    smtp.Send(message);

                }

                // Creates message in Umbraco content section
                IContent msg = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "message");
                msg.SetValue("messageName", model.Name);
                msg.SetValue("email", model.Email);
                msg.SetValue("subject", model.Subject);
                msg.SetValue("messageContent", model.Message);
                msg.SetValue("umbracoNaviHide", true);
                //Save
                Services.ContentService.Save(msg);

                // displays mail success 
                TempData["success"] = true;
            }
            return RedirectToCurrentUmbracoPage();
        }
    }
}
