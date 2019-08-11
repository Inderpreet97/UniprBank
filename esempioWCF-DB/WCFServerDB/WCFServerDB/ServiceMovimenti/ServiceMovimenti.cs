using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServerDB
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "ServiceMovimenti" nel codice e nel file di configurazione contemporaneamente.
    public class ServiceMovimenti : IServiceMovimenti
    {
        public bool CheckIBAN(string IBAN) {
            throw new NotImplementedException();
        }

        public bool CheckIDConto(int idContoCorrente) {
            throw new NotImplementedException();
        }

        public bool CheckImporto(decimal importo, string IBANCommittente) {
            throw new NotImplementedException();
        }

        public bool EseguiBonifico(string IBANCommittente, string IBANBeneficiario, decimal Importo) {
            throw new NotImplementedException();
        }

        public bool EseguiDeposito(int idContoCorrente, decimal importo) {
            throw new NotImplementedException();
        }

        public bool EseguiPrelievoDenaro(int idContoCorrente, decimal importo) {
            throw new NotImplementedException();
        }

        public List<Movimento> getListaMovimenti(int idContoCorrente) {
            throw new NotImplementedException();
        }

        public List<Movimento> GetListaMovimenti(int idContoCorrente) {
            throw new NotImplementedException();
        }
    }
}
