﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TEST_APP.ServiceReferenceMovimenti {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Movimento", Namespace="http://schemas.datacontract.org/2004/07/WCFServerDB")]
    [System.SerializableAttribute()]
    public partial class Movimento : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IBANBeneficiarioField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IBANCommittenteField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> dataOraField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string idMovimentiField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> importoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string tipoField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IBANBeneficiario {
            get {
                return this.IBANBeneficiarioField;
            }
            set {
                if ((object.ReferenceEquals(this.IBANBeneficiarioField, value) != true)) {
                    this.IBANBeneficiarioField = value;
                    this.RaisePropertyChanged("IBANBeneficiario");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IBANCommittente {
            get {
                return this.IBANCommittenteField;
            }
            set {
                if ((object.ReferenceEquals(this.IBANCommittenteField, value) != true)) {
                    this.IBANCommittenteField = value;
                    this.RaisePropertyChanged("IBANCommittente");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> dataOra {
            get {
                return this.dataOraField;
            }
            set {
                if ((this.dataOraField.Equals(value) != true)) {
                    this.dataOraField = value;
                    this.RaisePropertyChanged("dataOra");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string idMovimenti {
            get {
                return this.idMovimentiField;
            }
            set {
                if ((object.ReferenceEquals(this.idMovimentiField, value) != true)) {
                    this.idMovimentiField = value;
                    this.RaisePropertyChanged("idMovimenti");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> importo {
            get {
                return this.importoField;
            }
            set {
                if ((this.importoField.Equals(value) != true)) {
                    this.importoField = value;
                    this.RaisePropertyChanged("importo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string tipo {
            get {
                return this.tipoField;
            }
            set {
                if ((object.ReferenceEquals(this.tipoField, value) != true)) {
                    this.tipoField = value;
                    this.RaisePropertyChanged("tipo");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReferenceMovimenti.IServiceMovimenti")]
    public interface IServiceMovimenti {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceMovimenti/GetListaMovimenti", ReplyAction="http://tempuri.org/IServiceMovimenti/GetListaMovimentiResponse")]
        TEST_APP.ServiceReferenceMovimenti.Movimento[] GetListaMovimenti(ulong idContoCorrente);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceMovimenti/GetListaMovimenti", ReplyAction="http://tempuri.org/IServiceMovimenti/GetListaMovimentiResponse")]
        System.Threading.Tasks.Task<TEST_APP.ServiceReferenceMovimenti.Movimento[]> GetListaMovimentiAsync(ulong idContoCorrente);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceMovimenti/CheckImporto", ReplyAction="http://tempuri.org/IServiceMovimenti/CheckImportoResponse")]
        bool CheckImporto(decimal importo, string IBANCommittente);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceMovimenti/CheckImporto", ReplyAction="http://tempuri.org/IServiceMovimenti/CheckImportoResponse")]
        System.Threading.Tasks.Task<bool> CheckImportoAsync(decimal importo, string IBANCommittente);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceMovimenti/EseguiBonifico", ReplyAction="http://tempuri.org/IServiceMovimenti/EseguiBonificoResponse")]
        bool EseguiBonifico(string IBANCommittente, string IBANBeneficiario, decimal importo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceMovimenti/EseguiBonifico", ReplyAction="http://tempuri.org/IServiceMovimenti/EseguiBonificoResponse")]
        System.Threading.Tasks.Task<bool> EseguiBonificoAsync(string IBANCommittente, string IBANBeneficiario, decimal importo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceMovimenti/EseguiPrelievoDenaro", ReplyAction="http://tempuri.org/IServiceMovimenti/EseguiPrelievoDenaroResponse")]
        bool EseguiPrelievoDenaro(ulong idContoCorrente, decimal importo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceMovimenti/EseguiPrelievoDenaro", ReplyAction="http://tempuri.org/IServiceMovimenti/EseguiPrelievoDenaroResponse")]
        System.Threading.Tasks.Task<bool> EseguiPrelievoDenaroAsync(ulong idContoCorrente, decimal importo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceMovimenti/EseguiDeposito", ReplyAction="http://tempuri.org/IServiceMovimenti/EseguiDepositoResponse")]
        bool EseguiDeposito(ulong idContoCorrente, decimal importo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceMovimenti/EseguiDeposito", ReplyAction="http://tempuri.org/IServiceMovimenti/EseguiDepositoResponse")]
        System.Threading.Tasks.Task<bool> EseguiDepositoAsync(ulong idContoCorrente, decimal importo);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceMovimentiChannel : TEST_APP.ServiceReferenceMovimenti.IServiceMovimenti, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceMovimentiClient : System.ServiceModel.ClientBase<TEST_APP.ServiceReferenceMovimenti.IServiceMovimenti>, TEST_APP.ServiceReferenceMovimenti.IServiceMovimenti {
        
        public ServiceMovimentiClient() {
        }
        
        public ServiceMovimentiClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceMovimentiClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceMovimentiClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceMovimentiClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public TEST_APP.ServiceReferenceMovimenti.Movimento[] GetListaMovimenti(ulong idContoCorrente) {
            return base.Channel.GetListaMovimenti(idContoCorrente);
        }
        
        public System.Threading.Tasks.Task<TEST_APP.ServiceReferenceMovimenti.Movimento[]> GetListaMovimentiAsync(ulong idContoCorrente) {
            return base.Channel.GetListaMovimentiAsync(idContoCorrente);
        }
        
        public bool CheckImporto(decimal importo, string IBANCommittente) {
            return base.Channel.CheckImporto(importo, IBANCommittente);
        }
        
        public System.Threading.Tasks.Task<bool> CheckImportoAsync(decimal importo, string IBANCommittente) {
            return base.Channel.CheckImportoAsync(importo, IBANCommittente);
        }
        
        public bool EseguiBonifico(string IBANCommittente, string IBANBeneficiario, decimal importo) {
            return base.Channel.EseguiBonifico(IBANCommittente, IBANBeneficiario, importo);
        }
        
        public System.Threading.Tasks.Task<bool> EseguiBonificoAsync(string IBANCommittente, string IBANBeneficiario, decimal importo) {
            return base.Channel.EseguiBonificoAsync(IBANCommittente, IBANBeneficiario, importo);
        }
        
        public bool EseguiPrelievoDenaro(ulong idContoCorrente, decimal importo) {
            return base.Channel.EseguiPrelievoDenaro(idContoCorrente, importo);
        }
        
        public System.Threading.Tasks.Task<bool> EseguiPrelievoDenaroAsync(ulong idContoCorrente, decimal importo) {
            return base.Channel.EseguiPrelievoDenaroAsync(idContoCorrente, importo);
        }
        
        public bool EseguiDeposito(ulong idContoCorrente, decimal importo) {
            return base.Channel.EseguiDeposito(idContoCorrente, importo);
        }
        
        public System.Threading.Tasks.Task<bool> EseguiDepositoAsync(ulong idContoCorrente, decimal importo) {
            return base.Channel.EseguiDepositoAsync(idContoCorrente, importo);
        }
    }
}
