using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Repository.Context;
using Projeto.Entities;
using Projeto.Repository.Generics;
using System.Data.Entity; //entityframework



namespace Projeto.Repository.Persistence
{
   public class ClienteAdquirenteRepository : GenericRepository<ClienteAdquirente>
    {
       


        //método que retorne a quantidade de Taxas
        //de um determinado Adquirente..
        public ClienteAdquirente Adquirente(string adquirente)
        {
            using (DataContext ctx = new DataContext())
            {
                return ctx.ClienteAdquirente.Where(t => t.Adquirente == adquirente).First();
            }
        }


        public decimal Credito(string bandeira, int idClienteAdquirente)
        {
            using (DataContext ctx = new DataContext())
            {
                return ctx.Taxa.Where(t => t.Bandeira == bandeira && t.IdClienteAdquirente == idClienteAdquirente).Select(t => t.Credito).Single();



            }
        }


        public decimal Debito(string bandeira, int idClienteAdquirente)
        {
            using (DataContext ctx = new DataContext())
            {
                return ctx.Taxa.Where(t => t.Bandeira == bandeira && t.IdClienteAdquirente == idClienteAdquirente).Select(t => t.Debito).Single();



            }
        }

        public ClienteAdquirente Adquirente(string adquirente, object cEP)
        {
            throw new NotImplementedException();
        }
    }
}
