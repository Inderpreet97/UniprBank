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
        int GetPrivilegi(string username);

        [OperationContract]
        List<Persona> GetListaPersone(string tipoAccount, string idFiliale);

        [OperationContract]
        bool SospendiImpiegato(string username);

        [OperationContract]
        bool AttivaImpiegato(string username);

        [OperationContract]
        bool EliminaImpiegato(string username);

        [OperationContract]
        bool CheckUsername(string username);

        [OperationContract]
        bool RegistraPersona(Persona persona, string password);

        [OperationContract]
        bool ModificaPersona();

    }

    enum Privilegi { unknown, admin, impiegato, cliente }

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
        public DateTime dataDiNascita { get; set; }
        [DataMember]
        public string sesso { get; set; }
        [DataMember]
        public string indirizzo { get; set; }
        [DataMember]
        public int CAP { get; set; }
        [DataMember]
        public string citta { get; set; }
        [DataMember]
        public string provincia { get; set; }
        [DataMember]
        public string stato { get; set; }
        [DataMember]
        public string numeroDiTelefono { get; set; }

        public Persona() {
        }

        public Persona(string username, string privilegi, string codiceFiscale, string nome,
                            string cognome, DateTime dataDiNascita, string sesso, string indirizzo,
                            int CAP, string citta, string provincia, string stato, string numeroDiTelefono) {
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
        }
    }
}
