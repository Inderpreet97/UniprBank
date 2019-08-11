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
        List<Movimento> GetListaMovimenti(int idContoCorrente);

        [OperationContract]
        bool CheckIBAN(string IBAN); //true se l'iban esiste, false in caso contrario

        [OperationContract]
        bool CheckImporto(decimal importo, string IBANCommittente); // controlla se il saldo ricopre l'importo del bonifico

        [OperationContract]
        bool CheckIDConto(int idContoCorrente); // controlla se esiste un conto Corrente con quell'ID
        
        [OperationContract]
	    bool EseguiBonifico(string IBANCommittente, string IBANBeneficiario, decimal Importo); // true (bonifico avvenuto) false (caso contrario)

        [OperationContract]
        bool EseguiPrelievoDenaro(int idContoCorrente, decimal importo); // true (avvenuto), false (non avvenuto)

        [OperationContract]
	    bool EseguiDeposito(int idContoCorrente, decimal importo);  // true (avvenuto), false (non avvenuto)


    }

    [DataContract]
    public class Movimento
    {
        

        [DataMember]
        public int? idMovimenti { get; set; }
        [DataMember]
        public string IBANCommittente { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public decimal? importo { get; set; }
        [DataMember]
        public string IBANBeneficiario { get; set; }
        [DataMember]
        public DateTime? dataOra { get; set; }

        public Movimento() {
            this.idMovimenti = null;
            this.IBANCommittente = string.Empty;
            this.tipo = string.Empty;
            this.importo = null;
            this.IBANBeneficiario = string.Empty;
            this.dataOra = null;
        }

        public Movimento(int? idMovimenti, string IBANCommittente, string tipo, decimal? importo, string IBANBeneficiario, DateTime? dataOra) {
            this.idMovimenti = idMovimenti;
            this.IBANCommittente = IBANCommittente;
            this.tipo = tipo;
            this.importo = importo;
            this.IBANBeneficiario = IBANBeneficiario;
            this.dataOra = dataOra;
        }
    }
}
