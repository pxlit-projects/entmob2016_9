using EuphoricElephant.Data;
using EuphoricElephant.Enumerations;
using EuphoricElephant.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EuphoricElephant.Services
{
    public static class ProfileService
    {
        private static User _user;
        private static List<Data.Action> actions;
        private static bool isCreatingNew = false;

        public async static Task<ContentDialog> CreateNewProfileDialog(User user)
        {
            isCreatingNew = true;

            List<ComboBox> comboxlist = new List<ComboBox>();
            _user = user;

            Profile newProfile = null;

            ContentDialog dialog = new ContentDialog()
            {
                Title = "Create new profile"
            };

            var grid = new Grid();

            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();
            col1.Width = new GridLength(200, GridUnitType.Pixel);
            col2.Width = new GridLength(200, GridUnitType.Pixel);
            grid.ColumnDefinitions.Add(col1);
            grid.ColumnDefinitions.Add(col2);

            actions = await GetActions();

            RowDefinition r1 = new RowDefinition();
            r1.Height = new GridLength(0, GridUnitType.Auto);
            grid.RowDefinitions.Add(r1);

            TextBox box = new TextBox { PlaceholderText = "New Profile" };

            grid.Children.Add(box);
            Grid.SetColumn(box, 0);
            Grid.SetColumnSpan(box, 2);
            Grid.SetRow(box, 0);

            for (int i = 0; i < actions.Count(); i++)
            {
                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(0, GridUnitType.Auto);
                grid.RowDefinitions.Add(r);

                TextBlock text = new TextBlock { Text = actions[i].action, Width = 150 };

                ComboBox commandBox = new ComboBox { Width = 150 };
                List<Command> commands = await GetCommands();

                foreach(var c in commands)
                {
                    commandBox.Items.Add(new ComboBoxItem { Content = c.command });
                }

                commandBox.SelectedIndex = i;

                comboxlist.Add(commandBox);

                grid.Children.Add(text);
                grid.Children.Add(commandBox);

                Grid.SetColumn(text, 0);
                Grid.SetColumn(commandBox, 1);

                Grid.SetRow(text, i+1);
                Grid.SetRow(commandBox, i+1);
            }

            dialog.Content = grid;
            dialog.PrimaryButtonText = "OK";
            dialog.SecondaryButtonText = "Cancel";

            dialog.PrimaryButtonClick += async delegate
            {
                try
                {
                    if (Validated(box.Text, comboxlist))
                    {
                        newProfile = new Profile
                        {
                            profileName = box.Text,
                            userId = user.userId,
                            pairings = await GetPairings(comboxlist)
                        };

                        var v = await JSonParseService2<Profile>.SerializeDataToJson(Constants.PROFILE_ADD_URL, newProfile, Enumerations.SerializeType.Post);

                        if (!v.Equals("1"))
                        {
                            showError(v);
                        }
                    }
                }
                catch (Exception e)
                {
                    await ErrorService.showError(e.Message);
                    
                }
            };

            return dialog;
        }

        public static async Task<List<Data.Action>> GetActions()
        {
            try
            {
                return await JSonParseService2<List<Data.Action>>.DeserializeDataFromJson(Constants.ACTION_ALL_URL, null);
            }
            catch (Exception e)
            {
                showError(e.Message);
                return null;
            }
        }

        public static async Task<List<Command>> GetCommands()
        {
            var v = await JSonParseService2<List<Command>>.DeserializeDataFromJson(Constants.COMMAND_ALL_URL, null);
            return v;
        }

        public async static Task<List<ProfileItem>> SetPairings(Profile profile)
        {
            var actions = await GetActions();
            var commands = await GetCommands();

            var pairingsString = profile.pairings.Split(';');

            var actionString = pairingsString[0].Split(',');

            List<ProfileItem> items = new List<ProfileItem>();

            for(int i = 0; i< actionString.Length; i++)
            {
                ProfileItem item = new ProfileItem
                {
                    command = commands,
                    action = actions.Where(x => x.actId == Convert.ToInt16(actionString[i])).SingleOrDefault(),
                    commandIndex = commands.IndexOf(commands.Where(x => x.commandId == Convert.ToInt16(pairingsString[1].Split(',')[i])).SingleOrDefault())
                };

                items.Add(item);
            }

            return items;
        }

        public static async Task<string> GetPairings(List<ComboBox> comboxlist)
        {
            try
            {
                if (actions == null)
                {
                    actions = await GetActions();
                }

                string pairings = string.Empty;

                for (int i = 0; i < actions.Count; i++)
                {
                    pairings += actions[i].actId;

                    if (i != actions.Count - 1)
                    {
                        pairings += ",";
                    }
                }

                pairings += ";";

                List<Command> commands = await GetCommands(); ;

                if (comboxlist == null)
                {

                    for (int i = 0; i < commands.Count; i++)
                    {
                        pairings += commands[i].commandId;

                        if (i != actions.Count - 1)
                        {
                            pairings += ",";
                        }
                    }

                    return pairings;
                }
                else
                {
                    for (int i = 0; i < comboxlist.Count; i++)
                    {
                        var v = ((ComboBoxItem)comboxlist[i].SelectedItem).Content.ToString();
                        pairings += commands.Where(x => x.command == v).SingleOrDefault().commandId;
    
                    if (i != comboxlist.Count - 1)
                        {
                            pairings += ",";
                        }
                    }
                    
                    return pairings;
                }
            }
            catch (Exception e)
            {
                showError(e.Message);
                return null;
            }

        }

        public static bool Validated(string name, List<ComboBox> comboxList)
        {
            if(name != null && name != string.Empty)
            {
                List<int> indexes = new List<int>();

                foreach (var c in comboxList)
                {
                    indexes.Add(c.SelectedIndex);
                }

                bool validated = indexes.Distinct().Count() == indexes.Count();

                if (!validated)
                {
                    showError("All commands must be unique.");
                }

                return validated;
            }else
            {
                showError("Please enter a name for the profile.");
                return false;
            }
            
        }

        private static async void showError(string error)
        {
            var result = await ErrorService.showError(error);

            if (Convert.ToUInt32(result.Id.ToString()) == 0 && isCreatingNew)
            {
                await CreateNewProfileDialog(_user);
            }
        }

        public static CommandType getCommandType(Profile profile, ActionType action)
        {
            CommandType command = CommandType.NoCommand;

            var pairingsString = profile.pairings.Split(';')[1].Split(',');

            switch (action)
            {
                case ActionType.DOWN:
                    command = (CommandType)Convert.ToInt32(pairingsString[0]);
                    break;
                case ActionType.UP:
                    command = (CommandType)Convert.ToInt32(pairingsString[1]);
                    break;
                case ActionType.LEFT:
                    command = (CommandType)Convert.ToInt32(pairingsString[2]);
                    break;
                case ActionType.RIGHT:
                    command = (CommandType)Convert.ToInt32(pairingsString[3]);
                    break;
                case ActionType.SHAKE:
                    command = (CommandType)Convert.ToInt32(pairingsString[4]);
                    break;
            }

            return command;
        }
    }
}
