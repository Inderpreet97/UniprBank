﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFServerDB
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IServiceMovimenti" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IServiceMovimenti
    {
        [OperationContract]
        void DoWork();
    }

    [DataContract]
    public class Movimenti
    {
        [DataMember]
        public int idMovimenti { get; set; }
        [DataMember]
        public string IBANCommittente { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public decimal importo { get; set; }
        [DataMember]
        public string IBANBeneficiario { get; set; }
        [DataMember]
        public DateTime dataOra { get; set; }
    }
}