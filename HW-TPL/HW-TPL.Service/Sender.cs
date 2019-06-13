using HW_TPL.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HW_TPL.Service
{
    public class Sender
    {
        public bool InvoiceToOrderIngredients(List<Ingredient> ingredient)
        {
            string bodyEmail = "Заказ на достаку ингредиентов в Коктейль бар:\n";
            foreach (var nameIng in ingredient)
            {
                bodyEmail += string.Concat($" - {nameIng.IngredientName} - 10 порции\n");
            }
            Task.Run(() => SendEmail("azat.serikovich@yandex.kz", "azatmkushman@yandex.kz", bodyEmail, "Order the Ingredient"));

            return true;
        }
        
        public async void SendReportToBoss(List<Drinks> drinks)
        {
            string bodyEmail = "Отчет заказов за месяц:\n\n";
            foreach (var allDrink in drinks)
            {
                bodyEmail += string.Concat($"Коктейль {allDrink.NameDrink} - {allDrink.CountDrink} порции\n");
            }
            bodyEmail += string.Concat($"\n\nВсего продано - {drinks.Count()} коктейлей\n");
            
            await Task.Run(() => SendEmail("azat.serikovich@yandex.kz", "azatmkushman@yandex.kz", bodyEmail, "Monthly report on orders"));
        }

        public async void SendEmail(string to, string from, string boby, string subject)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from);
            mail.To.Add(new MailAddress(to));
            mail.Subject = subject;
            mail.Body = boby;

            SmtpClient client = new SmtpClient();
            client.Host = "smtp.yandex.ru";
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(from, "AzatMukushman123");
            await client.SendMailAsync(mail);
        }
    }
}
