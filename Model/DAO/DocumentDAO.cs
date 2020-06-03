using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Model.EF;

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

        public async Task<int> AddAs(String part)
        {
            document document = new document();
            document.DocumentPart = part;
            var sql = WcDbContext.documents.Add(document);
            var n = await WcDbContext.SaveChangesAsync();

            return n;
        }
        
        public async Task<int> DeleteAs(int id)
        {

            var select = await WcDbContext.documents.FindAsync(id);
            
            var sql = WcDbContext.documents.Remove(select);

            var n = await WcDbContext.SaveChangesAsync();

            return n;
        }
    }
}