using FanaticFirefly.Data;
using FanaticFirefly.Helpers;
using FanaticFirefly.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanaticFirefly.ViewModels
{
    public class ProfileViewModel : BaseModel
    {
        private Profile selectedProfile = null;
        public Profile SelectedProfile
        {
            get { return selectedProfile; }
            set { SetProperty(ref selectedProfile, value); }
        }

        private string action1 = string.Empty;
        public string Action1
        {
            get { return action1; }
            set { SetProperty(ref action1, value); }
        }

        private string action2 = string.Empty;
        public string Action2
        {
            get { return action2; }
            set { SetProperty(ref action2, value); }
        }

        private string action3 = string.Empty;
        public string Action3
        {
            get { return action3; }
            set { SetProperty(ref action3, value); }
        }

        private string action4 = string.Empty;
        public string Action4
        {
            get { return action4; }
            set { SetProperty(ref action4, value); }
        }

        private string action5 = string.Empty;
        public string Action5
        {
            get { return action5; }
            set { SetProperty(ref action5, value); }
        }

        private string command1 = string.Empty;
        public string Command1
        {
            get { return command1; }
            set { SetProperty(ref command1, value); }
        }

        private string command2 = string.Empty;
        public string Command2
        {
            get { return command2; }
            set { SetProperty(ref command2, value); }
        }

        private string command3 = string.Empty;
        public string Command3
        {
            get { return command3; }
            set { SetProperty(ref command3, value); }
        }

        private string command4 = string.Empty;
        public string Command4
        {
            get { return command4; }
            set { SetProperty(ref command4, value); }
        }

        private string command5 = string.Empty;
        public string Command5
        {
            get { return command5; }
            set { SetProperty(ref command5, value); }
        }

        private string profileName = string.Empty;
        public string ProfileName
        {
            get { return profileName; }
            set { SetProperty(ref profileName, value); }
        }

        public ProfileViewModel()
        {
            Init();
        }

        private void Init()
        {
            if (ApplicationSettings.Contains("SelectedProfile"))
            {
                SelectedProfile = (Profile)ApplicationSettings.GetItem("SelectedProfile");

                if(SelectedProfile != null)
                {
                    var aList = SelectedProfile.pairings.Split(';')[0].Split(',');

                    Action1 = aList[0];
                    Action2 = aList[1];
                    Action3 = aList[2];
                    Action4 = aList[3];
                    Action5 = aList[4];

                    var cList = SelectedProfile.pairings.Split(';')[1].Split(',');

                    Command1 = cList[0];
                    Command2 = cList[1];
                    Command3 = cList[2];
                    Command4 = cList[3];
                    Command5 = cList[4];

                    ProfileName = SelectedProfile.profileName;
                }
                else
                {
                    ErrorService.ShowError(Enumerations.ViewType.ProfileView);
                }
            }
            else
            {
                ErrorService.ShowError(Enumerations.ViewType.ProfileView);
            }
        }
    }
}
