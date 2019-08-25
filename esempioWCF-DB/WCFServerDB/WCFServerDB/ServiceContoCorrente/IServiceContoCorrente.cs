using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WCFServerDB
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IServiceContoCorrente" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IServiceContoCorrente
    {
        [OperationContract]
        List<ContoCorrente> GetListaContoCorrente(string username);

        [OperationContract]
        ContoCorrente SelectContoCorrente(int idContoCorrente);

        [OperationContract]
        bool CheckIBAN(string IBAN); //true se l'iban esiste, false in caso contrario

        [OperationContract]
        bool CheckIDConto(int idContoCorrente); // controlla se esiste un conto Corrente con quell'ID

        [OperationContract]
        bool AggiungiContoCorrente(string username, string idFiliale, decimal? saldo);
    }

    [DataContract]
    public class ContoCorrente
    {

        [DataMember]
        public int? idContoCorrente { get; set; } = null;
        [DataMember]
        public string IBAN { get; set; } = string.Empty;
        [DataMember]
        public string username { get; set; } = string.Empty;
        [DataMember]
        public decimal? saldo { get; set; } = null;
        [DataMember]
        public string idFiliale { get; set; } = string.Empty;

        public ContoCorrente() {
            this.idContoCorrente = null;
            this.IBAN = string.Empty;
            this.username = string.Empty;
            this.saldo = null;
            this.idFiliale = string.Empty;
        }

        public ContoCorrente(int? idContoCorrente, string IBAN, string username, decimal? saldo, string idFiliale) {
            this.idContoCorrente = idContoCorrente;
            this.IBAN = IBAN;
            this.username = username;
            this.saldo = saldo;
            this.idFiliale = idFiliale;
        }
    }
}
