using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Menu
{
    public class MenuItem
    {
        public enum FoodType
        {
            None,
            Misc,
            Chicken,
            Pasta,
            Rice,
            Snack,
            Dessert
        }

        public enum Ethnicity
        {
            None,
            American,
            Italian,
            Asian,
            Chinese,
            Japanese,
            Thai,
            Indian,
            Mexican,
            Greek,
            French
        }

        public enum Meal
        {
            Any,
            None,
            Breakfast,
            Lunch,
            Dinner,
            Snack,
            Dessert
        }

        public string ItemName;
        public List<string> Ingredients;
        public List<FoodType> ItemTypes;
        public List<Meal> ItemMeals;
        public Ethnicity ItemEthnicity;
        public DateTime TimeAdded;

        public MenuItem( string name, List<string> ings, List<FoodType> types, List<Meal> meals, Ethnicity ethnicity )
        {
            TimeAdded = DateTime.Now;
            ItemName = name;
            ItemEthnicity = ethnicity;
            // Initialize food types
            if (types != null)
                ItemTypes = new List<FoodType>(types);
            else
            {
                ItemTypes = new List<FoodType>();
                ItemTypes.Add(FoodType.Misc);
            }

            // Initialize ingredients
            if (ings != null)
                Ingredients = new List<string>(ings);
            else
                Ingredients = new List<string>();

            // Initialize meals
            if (meals != null)
                ItemMeals = new List<Meal>(meals);
            else
            {
                ItemMeals = new List<Meal>();
                ItemMeals.Add(Meal.None);
            }    
        }

        public MenuItem( string name, List<string> ings, List<string> types, List<string> meals, string ethnicity )
        {
            ItemName = name;
            if (!Enum.TryParse(ethnicity, out ItemEthnicity))
                ItemEthnicity = Ethnicity.None;
            if (ings != null)
                Ingredients = new List<string>(ings);
            else
                Ingredients = new List<string>();

            ItemTypes = new List<FoodType>();
            foreach (var type in types)
            {
                FoodType ft;
                if (!Enum.TryParse(type, out ft))
                    ft = FoodType.Misc;
                ItemTypes.Add(ft);
            }

            ItemMeals = new List<Meal>();
            foreach (var meal in meals)
            {
                Meal m;
                if (!Enum.TryParse(meal, out m))
                    m = Meal.None;
                ItemMeals.Add(m);
            }
        }

        public List<string> ItemMeals_Strings()
        {
            var returnList = new List<string>();
            foreach (var item in ItemMeals)
            {
                returnList.Add(item.ToString());
            }
            return returnList;
        }

        public List<string> ItemTypes_Strings()
        {
            var returnList = new List<string>();
            foreach (var item in ItemTypes)
            {
                returnList.Add(item.ToString());
            }
            return returnList;
        }

    }
}
