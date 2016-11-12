using EuphoricElephant.Custom;
using EuphoricElephant.Data;
using EuphoricElephant.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EuphoricElephant.Services
{
    public static class RegisterService
    {
        public static async void CreateRegisterDialog()
        {
            ContentDialog dialog = new ContentDialog()
            {
                Title = "Create new account"
            };


            var grid = new Grid();

            var userNameTB = new TextBox();
            var passWordTB = new TextBox();
            var repeatpassWordTb = new TextBox();
            var fnTB = new TextBox();
            var lnTB = new TextBox();

            var uNT = new TextBlock { Text = "User Name" };
            var pwT = new TextBlock { Text = "Password" };
            var rpwT = new TextBlock { Text = "Repeat Password" };
            var fnT = new TextBlock { Text = "First Name" };
            var lnT = new TextBlock { Text = "Last Name" };

            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();
            col1.Width = new GridLength(200, GridUnitType.Pixel);
            col2.Width = new GridLength(200, GridUnitType.Pixel);
            grid.ColumnDefinitions.Add(col1);
            grid.ColumnDefinitions.Add(col2);
            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();
            RowDefinition row4 = new RowDefinition();
            RowDefinition row5 = new RowDefinition();
            row1.Height = new GridLength(0, GridUnitType.Auto);
            row2.Height = new GridLength(0, GridUnitType.Auto);
            row3.Height = new GridLength(0, GridUnitType.Auto);
            row4.Height = new GridLength(0, GridUnitType.Auto);
            row5.Height = new GridLength(0, GridUnitType.Auto);
            grid.RowDefinitions.Add(row1);
            grid.RowDefinitions.Add(row2);
            grid.RowDefinitions.Add(row3);
            grid.RowDefinitions.Add(row4);
            grid.RowDefinitions.Add(row5);

            grid.Children.Add(uNT);
            grid.Children.Add(pwT);
            grid.Children.Add(rpwT);
            grid.Children.Add(fnT);
            grid.Children.Add(lnT);

            grid.Children.Add(userNameTB);
            grid.Children.Add(passWordTB);
            grid.Children.Add(repeatpassWordTb);
            grid.Children.Add(fnTB);
            grid.Children.Add(lnTB);

            Grid.SetColumn(uNT, 0);
            Grid.SetColumn(userNameTB, 1);
            Grid.SetRow(uNT, 0);
            Grid.SetRow(userNameTB, 0);

            Grid.SetColumn(lnT, 0);
            Grid.SetColumn(lnTB, 1);
            Grid.SetRow(lnT, 1);
            Grid.SetRow(lnTB, 1);

            Grid.SetColumn(fnT, 0);
            Grid.SetColumn(fnTB, 1);
            Grid.SetRow(fnT, 2);
            Grid.SetRow(fnTB, 2);

            Grid.SetColumn(pwT, 0);
            Grid.SetColumn(passWordTB, 1);
            Grid.SetRow(pwT, 3);
            Grid.SetRow(passWordTB, 3);

            Grid.SetColumn(rpwT, 0);
            Grid.SetColumn(repeatpassWordTb, 1);
            Grid.SetRow(rpwT, 4);
            Grid.SetRow(repeatpassWordTb, 4);


            dialog.Content = grid;

            dialog.PrimaryButtonText = "OK";

            dialog.SecondaryButtonText = "Cancel";

            dialog.PrimaryButtonClick += async delegate
            {
                if (userNameTB.Text != "" && passWordTB.Text != "" && repeatpassWordTb.Text != "" && fnTB.Text != "" && lnTB.Text != "")
                {
                    if (passWordTB.Text.Equals(repeatpassWordTb.Text))
                    {
                        User tempUser = await Services.JSonParseService2<User>.DeserializeDataFromJson("user", userNameTB.Text);

                        if (tempUser.userName == null || tempUser.userName == string.Empty)
                        {
                            try
                            {
                                User newUser = new User
                                {
                                    userName = userNameTB.Text,
                                    lastName = lnTB.Text,
                                    firstName = fnTB.Text,
                                    password = CustomPasswordIncriptor.sha256_hash(passWordTB.Text, userNameTB.Text),
                                    defaultProfileId = 0
                                };
                                bool succes = await Services.JSonParseService2<User>.SerializeDataToJson("user", newUser, SerializeType.Post);

                                if (succes)
                                {
                                    var u = await Services.JSonParseService2<User>.DeserializeDataFromJson("user", newUser.userName);
                                   

                                    Profile newProfile = new Profile()
                                    {
                                        profileName = "Default Profile",
                                        userId = u.userId,
                                        pairings = "[(1,2,3,4,5),(1,2,3,4,5)]" //TODO dynamic
                                    };

                                    succes = await Services.JSonParseService2<Profile>.SerializeDataToJson("profile", newProfile, SerializeType.Post);

                                    if (succes)
                                    {
                                        await Services.JSonParseService2<User>.DeserializeDataFromJson("user/profile", newUser.userName);
                                    }
                                }


                            }
                            catch (Exception e)
                            {
                                showError(e.Message);
                            }
                        }
                        else
                        {
                            showError("User already exists.");

                        }
                    }
                    else
                    {
                        showError("Passwords do not match.");
                    }
                }
                else
                {
                    showError("Please fill in all fields.");
                }
            };

            // Show Dialog
            await dialog.ShowAsync();
        }

        private static async void showError(string error)
        {
            var result = await ErrorService.showError(error);

            if (Convert.ToUInt32(result.Id.ToString()) == 0)
            {
                CreateRegisterDialog();
            }
        }
    }
}
