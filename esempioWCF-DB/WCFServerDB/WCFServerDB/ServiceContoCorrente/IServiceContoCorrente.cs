﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServerDB
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IServiceContoCorrente" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IServiceContoCorrente
    {
        [OperationContract]
        ContoCorrente SelectContoCorrente(int idContoCorrente);
    }

    [DataContract]
    public class ContoCorrente
    {

        [DataMember]
        public int idContoCorrente { get; set; }
        [DataMember]
        public string IBAN { get; set; }
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public string idFiliale { get; set; }

        public ContoCorrente() {
        }

        public ContoCorrente(int idContoCorrente, string IBAN, string username, decimal saldo, string idFiliale) {
            this.idContoCorrente = idContoCorrente;
            this.IBAN = IBAN;
            this.username = username;
            this.saldo = saldo;
            this.idFiliale = idFiliale;
        }
    }
}
