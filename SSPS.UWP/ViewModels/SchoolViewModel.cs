using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSPS.UWP.ViewModels
{
    class SchoolViewModel : NotificationBase
    {
        private Models.School school;

        public SchoolViewModel(string name)
        {
            school = new Models.School(name);
            selectedIndex = -1;
            foreach (var @class in school.Classes)
            {
                var cvm = new SchoolClassViewModel(@class);
                schoolClasses.Add(cvm);
            }
        }

        ObservableCollection<SchoolClassViewModel> schoolClasses = new ObservableCollection<SchoolClassViewModel>();
        public ObservableCollection<SchoolClassViewModel> SchoolClasses
        {
            get { return schoolClasses; }
            set { SetProperty(ref schoolClasses, value); }
        }

        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                if (SetProperty(ref selectedIndex, value))
                { RaisePropertyChanged(nameof(SelectedSchoolClass)); }
            }
        }

        public string Name
        {
            get { return school.Name; }
        }

        public SchoolClassViewModel SelectedSchoolClass
        {
            get { return selectedIndex >= 0 ? schoolClasses[selectedIndex] : null; }
        }
    }
}
