using Fleet.Transport.Data.Entities;
using System.Collections.ObjectModel;

namespace Fleet.Transport.Data.Repositories
{
    public class OrgaoRepository
    {
        public ObservableCollection<Orgao>  Orgaos { get; set; }= new ObservableCollection<Orgao>();
       
        public OrgaoRepository() { 
           Orgaos.Add(new Orgao { Id=1,Acronym="Dsics", Name="Direção de sistemas infornancionais comunicação sefura"});
            Orgaos.Add(new Orgao { Id = 2, Acronym = "Org1", Name = "Organização 1" });
            Orgaos.Add(new Orgao { Id = 3, Acronym = "Org2", Name = "Organização 2" });
            Orgaos.Add(new Orgao { Id = 4, Acronym = "Org3", Name = "Organização 3" });
            Orgaos.Add(new Orgao { Id = 5, Acronym = "Org4", Name = "Organização 4" });
            Orgaos.Add(new Orgao { Id = 6, Acronym = "Org5", Name = "Organização 5" });
            Orgaos.Add(new Orgao { Id = 7, Acronym = "Org6", Name = "Organização 6" });
            Orgaos.Add(new Orgao { Id = 8, Acronym = "Org7", Name = "Organização 7" });
            Orgaos.Add(new Orgao { Id = 9, Acronym = "Org8", Name = "Organização 8" });
            Orgaos.Add(new Orgao { Id = 10, Acronym = "Org9", Name = "Organização 9" });
            Orgaos.Add(new Orgao { Id = 11, Acronym = "Org10", Name = "Organização 10" });
            Orgaos.Add(new Orgao { Id = 12, Acronym = "Org11", Name = "Organização 11" });
            Orgaos.Add(new Orgao { Id = 13, Acronym = "Org12", Name = "Organização 12" });
            Orgaos.Add(new Orgao { Id = 14, Acronym = "Org13", Name = "Organização 13" });
            Orgaos.Add(new Orgao { Id = 15, Acronym = "Org14", Name = "Organização 14" });
            Orgaos.Add(new Orgao { Id = 16, Acronym = "Org15", Name = "Organização 15" });
            Orgaos.Add(new Orgao { Id = 17, Acronym = "Org16", Name = "Organização 16" });
            Orgaos.Add(new Orgao { Id = 18, Acronym = "Org17", Name = "Organização 17" });
            Orgaos.Add(new Orgao { Id = 19, Acronym = "Org18", Name = "Organização 18" });
            Orgaos.Add(new Orgao { Id = 20, Acronym = "Org19", Name = "Organização 19" });
        }

        public Orgao Find(int id )
        {
            foreach ( Orgao o in Orgaos ) {
                if ( o.Id == id )
                    return o;
            }
            return null;
        }
    }
}
