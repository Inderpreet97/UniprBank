using System;
using System.Collections.Generic;
using System.ServiceModel;
using WCFServerManager.ServiceReferenceAccountPersona;
using WCFServerManager.ServiceReferenceContoCorrente;
using WCFServerManager.ServiceReferenceFiliale;
using WCFServerManager.ServiceReferenceServiceMovimenti;

namespace WCFServerManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IService1" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IService1
    {
        // Metodi Account & Persona

        [OperationContract]
        bool Login(string username, string password);

        [OperationContract]
        string GetPrivilegi(string username);

        [OperationContract]
        List<Persona> GetListaPersone(string tipoAccount, string idFiliale);

        [OperationContract]
        bool EliminaAccount(string username);

        [OperationContract]
        Persona CheckUsername(string username); 

        [OperationContract]
        Persona GetPersona(string codiceFiscale);

        [OperationContract]
        bool AggiungiPersona(Persona persona, string password);

        [OperationContract]
        bool ModificaPersona(string username, Persona persona);

        [OperationContract]
        string GetIdFilialeByUsername(string username);


        // Metodi ContoCorrente

        [OperationContract]
        List<ContoCorrente> GetListaContoCorrente(string username);

        [OperationContract]
        ContoCorrente SelectContoCorrente(UInt64 idContoCorrente);

        [OperationContract]
        bool CheckIBAN(string IBAN);

        [OperationContract]
        bool CheckIDConto(UInt64 idContoCorrente);

        [OperationContract]
        bool AggiungiContoCorrente(string username, string idFiliale, decimal? saldo);


        // Metodi Filiale

        [OperationContract]
        Filiale GetFiliale(string username);

        [OperationContract]
        bool ModificaDatiFiliale(string idFiliale, Filiale nuovaFiliale);

        [OperationContract]
        string GetNameFiliale(string idFiliale);


        // Metodi Movimenti 

        [OperationContract]
        List<Movimento> GetListaMovimenti(UInt64 idContoCorrente);

        [OperationContract]
        bool CheckImporto(decimal importo, string IBANCommittente);

        [OperationContract]
        bool EseguiBonifico(string IBANCommittente, string IBANBeneficiario, decimal importo);

        [OperationContract]
        bool EseguiPrelievoDenaro(UInt64 idContoCorrente, decimal importo);

        [OperationContract]
        bool EseguiDeposito(UInt64 idContoCorrente, decimal importo);
    }
}
