using SSPS.VO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSPS.UWP.ViewModels
{
    class SchoolClassViewModel : NotificationBase<SchoolClass>
    {
        public SchoolClassViewModel(SchoolClass schollClass) : base(schollClass)
        {
            foreach (var item in schollClass.Supplementations)
            {
                supplementation.Add(new SupplementationViewModel(item));
            }
        }

        public string Name
        {
            get { return This.Name; }
        }

        public int Year
        {
            get { return This.Year; }
        }

        ObservableCollection<SupplementationViewModel> supplementation = new ObservableCollection<SupplementationViewModel>();
        public ObservableCollection<SupplementationViewModel> Supplementation
        {
            get { return supplementation; }
            set { SetProperty(ref supplementation, value); }
        }
    }
}
