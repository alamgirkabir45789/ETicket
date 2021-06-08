using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicket.ViewModels
{
    public class EditImageViewModel:UploadImageViewModel
    {
        public int Id { get; set; }
        public string ExistingImage { get; set; }
    }
}
