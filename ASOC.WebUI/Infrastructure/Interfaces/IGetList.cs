using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ASOC.WebUI.Infrastructure.Interfaces
{
    public interface IGetList
    {
        SelectList GetSomeSelectList();
    }
}
