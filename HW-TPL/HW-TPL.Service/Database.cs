using HW_TPL.DataAccess;
using HW_TPL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_TPL.Service
{
    public class Database
    {
        //public async void Write(AllDrinks drink)
        //{
        //    bool coincidence = false;

        //    using (var context = new DrinkContext())
        //    {
        //        var tmp = context.Drinksess.ToArray();

        //        if (context.Drinksess != null)
        //        {
        //            for (int i = 0; i < tmp.Count(); i++)
        //            {
        //                if (tmp[i].IdDrink == drink.Drinks[0].IdDrink)
        //                {
        //                    tmp[i].CountOrdering++;
        //                    //var allIngredients = tmp.Where(ingr => ingr.IngredientDrink == tmp[i].IngredientDrink).ToList();
        //                    //foreach(var similarIngr in allIngredients)
        //                    //{
        //                    //    similarIngr.CountIngredient--;
        //                    //}

        //                    tmp[i].CountIngredient--;
        //                    coincidence = true;
        //                    break;
        //                }
        //                else if ((tmp.Count() - 1) == i && !coincidence)
        //                {
        //                    context.Drinksess.AddRange(drink.Drinks);
        //                }
        //            }
        //        }
        //        else
        //            context.Drinksess.AddRange(drink.Drinks);
        //        await context.SaveChangesAsync();
        //    }
        //}

        public async void Write(AllDrinks drink)
        {
            using (var context = new DrinkContext())
            {
                var ingredient = new Ingredient();
                context.AllDrinksess.Add(drink);
                if (context.Drinksess != null)
                {
                    var tmp = context.Drinksess.ToList();
                    var tmpDrink = drink.Drinks[0];
                    
                    var searchDublicateIngredient = context.Ingredients.Where(dupl => dupl.IngredientName == tmpDrink.IngredientDrink).FirstOrDefault();
                    if (searchDublicateIngredient != null)
                    {
                        searchDublicateIngredient.IngredientCount--;
                    }
                    else
                    {
                        context.Drinksess.AddRange(drink.Drinks);
                        ingredient.IngredientName = drink.Drinks[0].IngredientDrink;
                        context.Ingredients.Add(ingredient);
                    }
                }
                else
                {
                    ingredient.IngredientName = drink.Drinks[0].IngredientDrink;
                    context.Ingredients.Add(ingredient);
                    context.Drinksess.AddRange(drink.Drinks);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
