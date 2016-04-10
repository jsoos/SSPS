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

        public string DateRange
        {
            get
            {
                if (To == From)
                    return From.ToString("dd/MM");
                if (To.Year == From.Year)
                    return From.ToString("dd/MM") + " - " + To.ToString("dd/MM");
                return From.ToString("dd/MM/yyyy") + " - " + To.ToString("dd/MM/yyyy");
            }
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
