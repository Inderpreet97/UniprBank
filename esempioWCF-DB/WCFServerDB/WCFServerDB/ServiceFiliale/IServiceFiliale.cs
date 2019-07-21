﻿using System;
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
        void DoWork();
    }
    
    [DataContract]
    public class Filiale
    {
        [DataMember]
        public string idFiliale { get; set; }
        [DataMember]
        public string direttore { get; set; }
        [DataMember]
        public string nome { get; set; }
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
        public Filiale() {
        }

        public Filiale(string idFiliale, string direttore, string nome, string indirizzo, int CAP, string citta, string provincia, string stato, string numeroDiTelefono) {
            this.idFiliale = idFiliale;
            this.direttore = direttore;
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