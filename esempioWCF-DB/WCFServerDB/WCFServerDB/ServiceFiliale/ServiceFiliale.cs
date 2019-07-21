using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServerDB
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Service1" nel codice e nel file di configurazione contemporaneamente.
    public class ServiceFiliale : IServiceFiliale
    {
        public Filiale GetFiliale(string username) {
            throw new NotImplementedException();
        }

        public bool ModificaDatiFiliale(string idFiliale, string direttore, string nome, string indirizzo, int? CAP, string citta, string provincia, string stato, string numeroDiTelefono) {
            throw new NotImplementedException();
        }
    }
}