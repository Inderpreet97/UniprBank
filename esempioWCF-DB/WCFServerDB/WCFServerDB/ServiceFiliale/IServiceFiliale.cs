using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServerDB
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IServiceFiliale" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IServiceFiliale
    {
        [OperationContract]
        Filiale GetFiliale(string username);

        [OperationContract]
        bool ModificaDatiFiliale(string idFiliale, Filiale nuovaFiliale);

        [OperationContract]
        string GetNameFiliale(string idFiliale); //restituisce il nome della filiale che ha quel determinato IDFiliale
    }
    
    [DataContract]
    public class Filiale
    {
        [DataMember]
        public string idFiliale { get; set; } = string.Empty;
        [DataMember]
        public string nome { get; set; } = string.Empty;
        [DataMember]
        public string indirizzo { get; set; } = string.Empty;
        [DataMember]
        public int? CAP { get; set; } = null;
        [DataMember]
        public string citta { get; set; } = string.Empty;
        [DataMember]
        public string provincia { get; set; } = string.Empty;
        [DataMember]
        public string stato { get; set; } = string.Empty;
        [DataMember]
        public string numeroDiTelefono { get; set; } = string.Empty;
        public Filiale() {
            this.idFiliale = string.Empty;
            this.nome = string.Empty;
            this.indirizzo = string.Empty;
            this.CAP = null;
            this.citta = string.Empty;
            this.provincia = string.Empty;
            this.stato = string.Empty;
            this.numeroDiTelefono = string.Empty;
        }

        public Filiale(string idFiliale, string nome, string indirizzo, int? CAP, string citta, string provincia, string stato, string numeroDiTelefono) {
            this.idFiliale = idFiliale;
            this.nome = nome;
            this.indirizzo = indirizzo;
            this.CAP = CAP;
            this.citta = citta;
            this.provincia = provincia;
            this.stato = stato;
            this.numeroDiTelefono = numeroDiTelefono;
        }
    }   
        

}
