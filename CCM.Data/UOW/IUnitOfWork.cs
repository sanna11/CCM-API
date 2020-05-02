using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CCM.Data.UOW
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
