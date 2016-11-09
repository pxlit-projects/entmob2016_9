using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App2.Pages
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
        
        var people = new List<Person> {
          new Person ("Steve", 21, "USA"),
          new Person ("Giel", 24, "Belgium"),
        };

            var personDataTemplate = new DataTemplate(() =>
            {
                var grid = new Grid();

                var nameLabel = new Label { FontAttributes = FontAttributes.Bold };
                var ageLabel = new Label();
                var locationLabel = new Label { HorizontalTextAlignment = TextAlignment.End };

                nameLabel.SetBinding(Label.TextProperty, "Name");
                ageLabel.SetBinding(Label.TextProperty, "Age");
                locationLabel.SetBinding(Label.TextProperty, "Location");

                grid.Children.Add(nameLabel);
                grid.Children.Add(ageLabel, 1, 0);
                grid.Children.Add(locationLabel, 2, 0);

                return new ViewCell { View = grid };
            });

            var listView = new ListView { ItemsSource = people, ItemTemplate = personDataTemplate };
            Content = new StackLayout
            {
                Padding = new Thickness(0, 20, 0, 0),
                Children = {
                    listView
                }
            };
        }

    }

    internal class Person
    {
        private string Name;
        private int Age;
        private string Location;

        public Person(string Name, int Age, string Location)
        {
            this.Name = Name;
            this.Age = Age;
            this.Location = Location;
        }
    }
}
