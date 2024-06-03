using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCompany.Application.Models.Input
{
    public class UploadCnhImageInput
    {
        public string CnhNumber { get; set; }
        public IFormFile CnhImage { get; set; }
    }
}
