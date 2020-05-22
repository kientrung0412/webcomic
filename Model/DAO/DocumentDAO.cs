using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Model.EF;
using up_down.Models;

namespace Model.DAO
{
    public class DocumentDAO
    {
        public WCDbContext WcDbContext;

        public DocumentDAO()
        {
            WcDbContext = new WCDbContext();
        }

        public async Task<List<document>> ListAs()
        {
            var list = await WcDbContext.documents.ToListAsync();
            return list;
        }
    }
}