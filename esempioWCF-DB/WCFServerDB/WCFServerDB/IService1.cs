using System.Runtime.Serialization;
using System.ServiceModel;

namespace WCFServerDB {
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IService1" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IService1 {

        [OperationContract]
        ContoCorrente SelectContoCorrente(int idContoCorrente);

        [OperationContract]
        bool Login(string username, string password);

        [OperationContract]
        int GetPrivilegi(string username);
    }

    enum Privilegi { unknown, admin, impiegato, cliente }

    [DataContract]
    public class ContoCorrente {
        [DataMember]
        public string Nome { get; set; }
        [DataMember]
        public string Cognome { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public double Saldo { get; set; }

        public ContoCorrente(string nome, string cognome, int id, double saldo) {
            this.Nome = nome;
            this.Cognome = cognome;
            this.Id = id;
            this.Saldo = saldo;
        }

        [OperationContract]
        public double getSaldo() { return Saldo; }
    }
}
