using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using WPFMenu;

namespace WPFmenu
{
    public class Menu
    {
        public string Name { get; set; }
        public string MenuFile { get; set; }
        public List<MenuItem> MenuItems { get; set; }
        public Menu ()
        {
            Name = "New Menu";
            MenuItems = new List<MenuItem>();
        }

        public Menu ( string fileName )
        {
            int fileNameStart = fileName.LastIndexOf("\\") + 1;
            int length = fileName.LastIndexOf(".") - fileNameStart;

            Name = fileName.Substring(fileNameStart, length);
            MenuFile = fileName;
            MenuItems = PopulateFromXML();
        }

        public MenuItem GetItem(string itemName)
        {
            // Get menu item if it exists
            MenuItem item = null;
            foreach (MenuItem mi in MenuItems)
            {
                if (mi.ItemName == itemName)
                {
                    item = mi;
                }
            }
            
            return item;
        }

        public MenuItem AddItem(string name, List<string> ingredients, List<string> types, List<string> meals, string eth)
        {
            if (!MenuItems.Contains(GetItem(name)))
            {
                MenuItems.Add(new MenuItem(name, ingredients, types, meals, eth));
                // Return the newly added item
                return MenuItems[MenuItems.Count - 1];
            }    
            else
            {
                return null;
                // Update/Edit the item?
            }

        }

        public List<MenuItem> PopulateFromXML()
        {
            var menuItems = new List<MenuItem>();
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(MenuFile);
            }
            catch
            {
                // Error loading xml document
            }
            
            var itemNodes = xmlDoc.GetElementsByTagName("item");

            foreach (XmlNode itemNode in itemNodes)
            {
                // -----Unique to all menu items-----------
                string name = String.Empty;
                List<string> ings = new List<string>();
                var types = new List<MenuItem.FoodType>();
                var meals = new List<MenuItem.Meal>();
                var eth = MenuItem.Ethnicity.None;   
                // ----------------------------------------
                var childNodes = itemNode.ChildNodes;
                foreach (XmlNode childNode in childNodes)
                {
                    switch (childNode.Name.ToLower())
                    {
                        // The name of the menu item
                        case ("name"):
                            name = childNode.InnerText;
                            break;
                        // The list of ingredients
                        case ("ingredients"):
                            var ingNodes = childNode.SelectNodes("li");
                            foreach (XmlNode ingNode in ingNodes)
                            {
                                ings.Add(ingNode.InnerText);
                            }
                            break;
                        // The type of food
                        case ("types"):
                            var typeNodes = childNode.SelectNodes("li");
                            foreach (XmlNode typeNode in typeNodes)
                            {
                                MenuItem.FoodType ft;
                                Enum.TryParse(typeNode.InnerText, out ft);
                                types.Add(ft);
                            }
                            break;
                        // The meal 
                        case ("meals"):
                            var mealNodes = childNode.SelectNodes("li");
                            foreach (XmlNode mealNode in mealNodes)
                            {
                                MenuItem.Meal m;
                                Enum.TryParse(mealNode.InnerText, out m);
                                meals.Add(m);
                            }
                            break;
                        // The ethnicity of the dish
                        case ("ethnicity"):
                            Enum.TryParse(childNode.InnerText, out eth);
                            break;

                        default:
                            // Error
                            break;
                    }
                }

                menuItems.Add(new MenuItem(name, ings, types, meals, eth));
            }

            return menuItems;
        }

        public void Save( string fileName )
        { 
            var xml = new XElement("items",
                MenuItems.Select(menuItem =>
                    new XElement("item",
                        new XElement("name", menuItem.ItemName),
                        new XElement("ingredients", menuItem.Ingredients.Select(ing =>
                            new XElement("li", ing))),
                        new XElement("types", menuItem.ItemTypes.Select(type =>
                            new XElement("li", type.ToString()))),
                        new XElement("meals", menuItem.ItemMeals.Select(meal =>
                            new XElement("li", meal.ToString()))),
                        new XElement("ethnicity", menuItem.ItemEthnicity.ToString())
                        ))
               );

            xml.Save(fileName);
            //XmlDocument doc = new XmlDocument();

        }
    }
}
