using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortalCMS.Application.Interfaces
{
    public interface ISlugService
    {
        Task<string> GenerateUniqueSlugAsync(string baseText);
    }
}
