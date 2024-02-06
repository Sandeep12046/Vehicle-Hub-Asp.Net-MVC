using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using VehicleDetails.Models.RequiredModels.ViewModels;

namespace VehicleDetails.Helpers
{
    public class SendMail
    {

        public static void sendMails(UserQueryModel query)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("sanjusandeep12046@gmail.com", "lgotlmqgefomscda"),
                    EnableSsl = true,

                };
                MailMessage mailMessage = new MailMessage
                {

                    From = new MailAddress("sanjusandeep12046@gmail.com"),
                    Subject = "Thank You for Your Inquiry -  Vehicle Hub ",
                    Body = $"Dear {query.FullName},\r\n\r\n " +
                    "Thank you for reaching out to Project Vehicle Hub! We appreciate your interest in our services. Your inquiry is important to us, and we are excited to assist you in finding the perfect new or used vehicle.\r\n\r\n" +
                    "Here is a summary of the information you provided:\r\n\r\n " +
                    $"Full Name: {query.FullName},\r\nMobile Number: {query.MobileNumber}\r\nEmail ID: {query.EmailID}\r\nMessage: {query.Message} " +
                    "Our team will review your inquiry and get back to you as soon as possible. In the meantime, feel free to explore our website for a preview of the exciting vehicles we have available.\r\n\r\n" +
                    $"If you have any immediate questions or require urgent assistance, please don't hesitate to contact us at 9876543210 or reply to this email VehicleHubInfo@vehhub.com.\r\n\r\n" +
                    "Thank you once again for considering Project Vehicle Hub. We look forward to assisting you in your search for the perfect vehicle!\r\n\r\nBest regards,\r\n\r\n" +
                    "Customer Service Team\r\nVehicle Hub\r\n" +
                    "980225544554",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add("sanjusandeep12046@gmail.com");
                smtpClient.Send(mailMessage);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static void TestDriveEmail(UserModel userData)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("sanjusandeep12046@gmail.com", "lgotlmqgefomscda"),
                    EnableSsl = true,
                };
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("sanjusandeep12046@gmail.com"),
                    Subject = "Thank You for Contacting Us!",
                    Body = $"Dear {userData.UserName},\r\n\r\n" +
                   $"We're delighted to offer {userData.UserName} an exclusive home test drive experience! \r\n\r\n" +
                   $"Our representative will be bringing the latest models directly to {userData.UserName}'s doorstep at {userData.Address},\r\n\r\n " +
                   $"and they can be reached at {userData.PhoneNumber} to schedule this personalized test drive.\r\n",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add("sanjusandeep12046@gmail.com");
                smtpClient.Send(mailMessage);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void ContactUsMail()
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("sanjusandeep12046@gmail.com", "lgotlmqgefomscda"),
                    EnableSsl = true,
                };
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("sanjusandeep12046@gmail.com"),
                    Subject = "Thank You for Contacting Us!",
                    Body = "Dear[Customer's Name],\r\n\r\n       " +
                    "Thank you for reaching out to us! We appreciate your interest in our products and services." +
                    "A member of our Sales team will be in touch with you shortly to discuss your requirements and guide you " +
                    "through the process of finding the right vehicle and pricing that best suits your needs.\r\n\r\n        " +
                    "We look forward to assisting you in making an informed decision and ensuring a smooth and enjoyable experience with our team.\r\n\r\n" +
                    "If you have any immediate questions or concerns, feel free to reach out to us at[Your Contact Information].\r\n\r\n" +
                    "Best regards,\r\n\r\n" +
                    "Vehicle Hub\r\n" +
                    "9874554214155",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add("sanjusandeep12046@gmail.com");
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void MailSend()
        {

            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("sanjusandeep12046@gmail.com", "lgotlmqgefomscda"),
                    EnableSsl = true,

                };
                MailMessage mailMessage = new MailMessage
                {

                    From = new MailAddress("sanjusandeep12046@gmail.com"),
                    Subject = "Thank You for Your Inquiry - Vehicle Hub Name Customer Service Will Reach Out Soon",
                    Body = "Thank you for reaching out to Vehicle Hub regarding your interest in selling vehicles on our platform. " +
                    "We appreciate your inquiry, and our dedicated customer service team is eager to assist you.\r\n\r\n" +
                    "A representative from our customer service department will be contacting you shortly to discuss your questions and provide you with the information you need. " +
                    "We understand that your time is valuable, and we want to ensure that we address all your queries comprehensively.\r\n\r\nIn the meantime, " +
                    "if there's anything specific you'd like to share or if you have additional details you'd like us to be aware of, please feel free to reply to this email.\r\n\r\n" +
                    "We look forward to the opportunity to assist you and facilitate your experience on Vehicle Hub.\r\n\r\n" +
                    "Thank you for considering Vehicle Hub as your platform for selling vehicles.\r\n\r\nBest regards,\r\n\r\n" +
                    "Customer Service Team\r\nVehicle Hub\r\n" +
                    "980225544554",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add("sanjusandeep12046@gmail.com");
                smtpClient.Send(mailMessage);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}