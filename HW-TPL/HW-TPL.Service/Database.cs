using HW_TPL.DataAccess;
using HW_TPL.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HW_TPL.Service
{
    public class Database
    {
        private DrinkContext context;
        private Sender sender;
        public Database()
        {
            context = new DrinkContext();
            sender = new Sender();
        }

        ~Database()
        {
            context.Dispose();
        }
        public async void Write(AllDrinks drink)
        {
            var ingredient = new Ingredient();
            var tmpD = drink.Drinks[0];

            var searchDublicateIngredient = context.Ingredients.Where(dupl => dupl.IngredientName == tmpD.IngredientDrink).FirstOrDefault();
            if (searchDublicateIngredient != null)
            {
                searchDublicateIngredient.IngredientCount-=tmpD.CountDrink;
                context.Drinksess.AddRange(drink.Drinks);
            }
            else
            {
                context.Drinksess.AddRange(drink.Drinks);
                ingredient.IngredientName = tmpD.IngredientDrink;
                context.Ingredients.Add(ingredient);
            }
            await context.SaveChangesAsync();
            await Task.Run(() => CheckIngredient());
        }

        public async void CheckIngredient()
        {
            var minIngredient = 3;
            var maxRefilIngredient = 10;
            var endedIngr = await context.Ingredients.Where(ingr => ingr.IngredientCount <= minIngredient).ToListAsync();
            if (endedIngr.Count != 0)
            {
                if (sender.InvoiceToOrderIngredients(endedIngr))
                {
                    foreach (var refilIngr in endedIngr)
                    {
                        refilIngr.IngredientCount += maxRefilIngredient;
                        await context.SaveChangesAsync();
                    }
                }
            }

            //context.Dispose();
        }


        public async void ReportOrders()
        {
            DateTime dateToSendReport = new DateTime(2019, 07, 01);
            DateTime dateToSendReportEarlier = dateToSendReport.AddMonths(-1);
            if(DateTime.Now>=dateToSendReport && DateTime.Now <= dateToSendReport.AddDays(1))
            {
                var ordersDrinkToMonth = context.Drinksess
                    .Where(drink => drink.DateOrderDrink > dateToSendReportEarlier && drink.DateOrderDrink < dateToSendReport).ToList();
                await Task.Run(() => sender.SendReportToBoss(ordersDrinkToMonth));
                dateToSendReport.AddMonths(1);
            }
        }

    }
}
