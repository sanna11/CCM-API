using CCM.Data.Models.DBContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CCM.Data.UOW
{
    public class UnitOfWork: IUnitOfWork
    {
        private CCBDBContext CCBContext { get; set; }

        public UnitOfWork(CCBDBContext ccbContext)
        {
            this.CCBContext = ccbContext;
        }

        public async Task Commit()
        {
            await CCBContext.SaveChangesAsync().ConfigureAwait(true);
        }
    }
}
