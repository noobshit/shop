using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Web.ViewModels
{
    public class ErrorViewModel
    {
        public bool ShowErrorCode { get => ErrorCode.HasValue; }
        public int? ErrorCode { get; set; }
    }
}
