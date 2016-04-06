using SSPS.VO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSPS.UWP.ViewModels
{
    class SupplementationViewModel : NotificationBase<Supplementation>
    {
        public SupplementationViewModel(Supplementation supplementation) : base(supplementation) { }

        public DateTime From
        {
            get { return This.From; }
            set { SetProperty(This.From, value, () => This.From = value); }
        }

        public DateTime To
        {
            get { return This.To; }
            set { SetProperty(This.To, value, () => This.To = value); }
        }

        public DateTime Updated
        {
            get { return This.Updated; }
            set { SetProperty(This.Updated, value, () => This.Updated = value); }
        }

        public string Message
        {
            get { return This.Message; }
            set { SetProperty(This.Message, value, () => This.Message = value); }
        }
    }
}
