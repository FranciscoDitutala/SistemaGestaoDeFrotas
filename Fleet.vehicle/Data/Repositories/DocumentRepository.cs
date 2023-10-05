using Fleet.Data;
using Fleet.Transport.Data.Entities;

namespace Fleet.Transport.Data.Repositories
{
    public class DocumentRepository:EntityRepository<Document, int>
    {
        public DocumentRepository(FleetTransportDbContext context):base(context) { 
        
        }
    }
}
