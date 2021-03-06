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

    [DataContract]
    public class Movimento
    {
        [DataMember]
        public string idMovimenti { get; set; } = string.Empty;
        [DataMember]
        public string IBANCommittente { get; set; } = string.Empty;
        [DataMember]
        public string tipo { get; set; } = string.Empty;
        [DataMember]
        public decimal? importo { get; set; } = null;
        [DataMember]
        public string IBANBeneficiario { get; set; } = string.Empty;
        [DataMember]
        public DateTime? dataOra { get; set; } = null;

        public Movimento() {
            this.idMovimenti = string.Empty;
            this.IBANCommittente = string.Empty;
            this.tipo = string.Empty;
            this.importo = null;
            this.IBANBeneficiario = string.Empty;
            this.dataOra = null;
        }

        public Movimento(string idMovimenti, string IBANCommittente, string tipo, decimal? importo, string IBANBeneficiario, DateTime? dataOra) {
            this.idMovimenti = idMovimenti;
            this.IBANCommittente = IBANCommittente;
            this.tipo = tipo;
            this.importo = importo;
            this.IBANBeneficiario = IBANBeneficiario;
            this.dataOra = dataOra;
        }
    }
}
