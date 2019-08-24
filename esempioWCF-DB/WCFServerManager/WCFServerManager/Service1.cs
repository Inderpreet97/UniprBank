using System.Collections.Generic;
using System.Linq;
using WCFServerManager.ServiceReferenceAccountPersona;
using WCFServerManager.ServiceReferenceContoCorrente;
using WCFServerManager.ServiceReferenceFiliale;
using WCFServerManager.ServiceReferenceServiceMovimenti;

namespace WCFServerManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Service1" nel codice e nel file di configurazione contemporaneamente.
    public class Service1 : IService1
    {
        private ServiceAccountPersonaClient serviceAccountPersonaClient = new ServiceAccountPersonaClient();
        private ServiceContoCorrenteClient serviceContoCorrenteClient = new ServiceContoCorrenteClient();
        private ServiceFilialeClient serviceFilialeClient = new ServiceFilialeClient();
        private ServiceMovimentiClient serviceMovimentiClient = new ServiceMovimentiClient();

        public bool AggiungiContoCorrente(string username, string idFiliale, decimal? saldo) {
            return serviceContoCorrenteClient.AggiungiContoCorrente(username, idFiliale, saldo);
        }

        public bool AggiungiPersona(Persona persona, string password) {
            return serviceAccountPersonaClient.AggiungiPersona(persona, password);
        }

        public bool CheckIBAN(string IBAN) {
            return serviceContoCorrenteClient.CheckIBAN(IBAN);
        }

        public bool CheckIDConto(int idContoCorrente) {
            return serviceContoCorrenteClient.CheckIDConto(idContoCorrente);
        }

        public bool CheckImporto(decimal importo, string IBANCommittente) {
            return serviceMovimentiClient.CheckImporto(importo, IBANCommittente);
        }

        public Persona CheckUsername(string username) {
            return serviceAccountPersonaClient.CheckUsername(username);
        }

        public bool EliminaAccount(string username) {
            return serviceAccountPersonaClient.EliminaAccount(username);
        }

        public bool EseguiBonifico(string IBANCommittente, string IBANBeneficiario, decimal importo) {
            return serviceMovimentiClient.EseguiBonifico(IBANCommittente, IBANBeneficiario, importo);
        }

        public bool EseguiDeposito(string IBANCommittente, decimal importo) {
            return serviceMovimentiClient.EseguiDeposito(IBANCommittente, importo);
        }

        public bool EseguiPrelievoDenaro(string IBANCommittente, decimal importo) {
            return serviceMovimentiClient.EseguiPrelievoDenaro(IBANCommittente, importo);
        }

        public Filiale GetFiliale(string username) {
            return serviceFilialeClient.GetFiliale(username);
        }

        public string GetIdFilialeByUsername(string username) {
            return serviceAccountPersonaClient.GetIdFilialeByUsername(username);
        }

        public List<ContoCorrente> GetListaContoCorrente(string username) {
            return serviceContoCorrenteClient.GetListaContoCorrente(username).ToList();
        }

        public List<Movimento> GetListaMovimenti(int idContoCorrente) {
            return serviceMovimentiClient.GetListaMovimenti(idContoCorrente).ToList();

        }

        public List<Persona> GetListaPersone(string tipoAccount, string idFiliale) {
            return serviceAccountPersonaClient.GetListaPersone(tipoAccount, idFiliale).ToList();
        }

        public string GetNameFiliale(string idFiliale) {
            return serviceFilialeClient.GetNameFiliale(idFiliale);

        }

        public Persona GetPersona(string codiceFiscale) {
            return serviceAccountPersonaClient.GetPersona(codiceFiscale);
        }

        public string GetPrivilegi(string username) {
            return serviceAccountPersonaClient.GetPrivilegi(username);

        }

        public bool Login(string username, string password) {
            return serviceAccountPersonaClient.Login(username, password);

        }

        public bool ModificaDatiFiliale(string idFiliale, Filiale nuovaFiliale) {
            return serviceFilialeClient.ModificaDatiFiliale(idFiliale, nuovaFiliale);

        }

        public bool ModificaPersona(string username, Persona persona) {
            return serviceAccountPersonaClient.ModificaPersona(username, persona);

        }

        public ContoCorrente SelectContoCorrente(int idContoCorrente) {
            return serviceContoCorrenteClient.SelectContoCorrente(idContoCorrente);

        }
    }
}
