using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServerDB
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IServiceAccountPersona" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IServiceAccountPersona
    {
        [OperationContract]
        bool Login(string username, string password);

        [OperationContract]
        string GetPrivilegi(string username);

        [OperationContract]
        List<Persona> GetListaPersone(string tipoAccount, string idFiliale);

        [OperationContract]
        bool EliminaAccount(string username);

        [OperationContract]
        Persona CheckUsername(string username); // Restituisce i dati dell'account e i dati anagrafici 
                                                // della persona che possiede l'account
        [OperationContract]
        Persona GetPersona(string codiceFiscale); // Restituisce solo i dati anagrafici di una persona

        [OperationContract]
        bool AggiungiPersona(Persona persona, string password);

        [OperationContract]
        bool ModificaPersona(string username, Persona persona);

        [OperationContract]
        string GetIdFilialeByUsername(string username);

    }

    [DataContract]
    public class Persona
    {
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string privilegi { get; set; }
        [DataMember]
        public string codiceFiscale { get; set; }
        [DataMember]
        public string nome { get; set; }
        [DataMember]
        public string cognome { get; set; }
        [DataMember]
        public DateTime? dataDiNascita { get; set; }
        [DataMember]
        public string sesso { get; set; }
        [DataMember]
        public string indirizzo { get; set; }
        [DataMember]
        public int? CAP { get; set; }
        [DataMember]
        public string citta { get; set; }
        [DataMember]
        public string provincia { get; set; }
        [DataMember]
        public string stato { get; set; }
        [DataMember]
        public string numeroDiTelefono { get; set; }
        [DataMember]
        public string filiale { get; set; }

        public Persona() {
            this.username = string.Empty;
            this.privilegi = string.Empty;
            this.codiceFiscale = string.Empty;
            this.nome = string.Empty;
            this.cognome = string.Empty;
            this.dataDiNascita = null;
            this.sesso = string.Empty;
            this.indirizzo = string.Empty;
            this.CAP = null;
            this.citta = string.Empty;
            this.provincia = string.Empty;
            this.stato = string.Empty;
            this.numeroDiTelefono = string.Empty;
            this.filiale = string.Empty;
        }

        public Persona(string username, string privilegi, string codiceFiscale, string nome,
                            string cognome, DateTime? dataDiNascita, string sesso, string indirizzo,
                            int? CAP, string citta, string provincia, string stato, string numeroDiTelefono, string filiale) {
            this.username = username;
            this.privilegi = privilegi;
            this.codiceFiscale = codiceFiscale;
            this.nome = nome;
            this.cognome = cognome;
            this.dataDiNascita = dataDiNascita;
            this.sesso = sesso;
            this.indirizzo = indirizzo;
            this.CAP = CAP;
            this.citta = citta;
            this.provincia = provincia;
            this.stato = stato;
            this.numeroDiTelefono = numeroDiTelefono;
            this.filiale = filiale;
        }

    }
}
