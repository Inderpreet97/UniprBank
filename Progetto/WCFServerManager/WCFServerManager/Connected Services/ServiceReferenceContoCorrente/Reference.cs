﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCFServerManager.ServiceReferenceContoCorrente {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ContoCorrente", Namespace="http://schemas.datacontract.org/2004/07/WCFServerDB")]
    [System.SerializableAttribute()]
    public partial class ContoCorrente : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IBANField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<ulong> idContoCorrenteField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string idFilialeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<decimal> saldoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string usernameField;
        
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
        public string IBAN {
            get {
                return this.IBANField;
            }
            set {
                if ((object.ReferenceEquals(this.IBANField, value) != true)) {
                    this.IBANField = value;
                    this.RaisePropertyChanged("IBAN");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<ulong> idContoCorrente {
            get {
                return this.idContoCorrenteField;
            }
            set {
                if ((this.idContoCorrenteField.Equals(value) != true)) {
                    this.idContoCorrenteField = value;
                    this.RaisePropertyChanged("idContoCorrente");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string idFiliale {
            get {
                return this.idFilialeField;
            }
            set {
                if ((object.ReferenceEquals(this.idFilialeField, value) != true)) {
                    this.idFilialeField = value;
                    this.RaisePropertyChanged("idFiliale");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<decimal> saldo {
            get {
                return this.saldoField;
            }
            set {
                if ((this.saldoField.Equals(value) != true)) {
                    this.saldoField = value;
                    this.RaisePropertyChanged("saldo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string username {
            get {
                return this.usernameField;
            }
            set {
                if ((object.ReferenceEquals(this.usernameField, value) != true)) {
                    this.usernameField = value;
                    this.RaisePropertyChanged("username");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReferenceContoCorrente.IServiceContoCorrente")]
    public interface IServiceContoCorrente {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContoCorrente/GetListaContoCorrente", ReplyAction="http://tempuri.org/IServiceContoCorrente/GetListaContoCorrenteResponse")]
        WCFServerManager.ServiceReferenceContoCorrente.ContoCorrente[] GetListaContoCorrente(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContoCorrente/GetListaContoCorrente", ReplyAction="http://tempuri.org/IServiceContoCorrente/GetListaContoCorrenteResponse")]
        System.Threading.Tasks.Task<WCFServerManager.ServiceReferenceContoCorrente.ContoCorrente[]> GetListaContoCorrenteAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContoCorrente/SelectContoCorrente", ReplyAction="http://tempuri.org/IServiceContoCorrente/SelectContoCorrenteResponse")]
        WCFServerManager.ServiceReferenceContoCorrente.ContoCorrente SelectContoCorrente(ulong idContoCorrente);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContoCorrente/SelectContoCorrente", ReplyAction="http://tempuri.org/IServiceContoCorrente/SelectContoCorrenteResponse")]
        System.Threading.Tasks.Task<WCFServerManager.ServiceReferenceContoCorrente.ContoCorrente> SelectContoCorrenteAsync(ulong idContoCorrente);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContoCorrente/CheckIBAN", ReplyAction="http://tempuri.org/IServiceContoCorrente/CheckIBANResponse")]
        bool CheckIBAN(string IBAN);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContoCorrente/CheckIBAN", ReplyAction="http://tempuri.org/IServiceContoCorrente/CheckIBANResponse")]
        System.Threading.Tasks.Task<bool> CheckIBANAsync(string IBAN);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContoCorrente/CheckIDConto", ReplyAction="http://tempuri.org/IServiceContoCorrente/CheckIDContoResponse")]
        bool CheckIDConto(ulong idContoCorrente);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContoCorrente/CheckIDConto", ReplyAction="http://tempuri.org/IServiceContoCorrente/CheckIDContoResponse")]
        System.Threading.Tasks.Task<bool> CheckIDContoAsync(ulong idContoCorrente);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContoCorrente/AggiungiContoCorrente", ReplyAction="http://tempuri.org/IServiceContoCorrente/AggiungiContoCorrenteResponse")]
        bool AggiungiContoCorrente(string username, string idFiliale, System.Nullable<decimal> saldo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContoCorrente/AggiungiContoCorrente", ReplyAction="http://tempuri.org/IServiceContoCorrente/AggiungiContoCorrenteResponse")]
        System.Threading.Tasks.Task<bool> AggiungiContoCorrenteAsync(string username, string idFiliale, System.Nullable<decimal> saldo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContoCorrente/GetIBANByIdContoCorrente", ReplyAction="http://tempuri.org/IServiceContoCorrente/GetIBANByIdContoCorrenteResponse")]
        string GetIBANByIdContoCorrente(ulong idContoCorrente);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContoCorrente/GetIBANByIdContoCorrente", ReplyAction="http://tempuri.org/IServiceContoCorrente/GetIBANByIdContoCorrenteResponse")]
        System.Threading.Tasks.Task<string> GetIBANByIdContoCorrenteAsync(ulong idContoCorrente);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceContoCorrenteChannel : WCFServerManager.ServiceReferenceContoCorrente.IServiceContoCorrente, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceContoCorrenteClient : System.ServiceModel.ClientBase<WCFServerManager.ServiceReferenceContoCorrente.IServiceContoCorrente>, WCFServerManager.ServiceReferenceContoCorrente.IServiceContoCorrente {
        
        public ServiceContoCorrenteClient() {
        }
        
        public ServiceContoCorrenteClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceContoCorrenteClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceContoCorrenteClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceContoCorrenteClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public WCFServerManager.ServiceReferenceContoCorrente.ContoCorrente[] GetListaContoCorrente(string username) {
            return base.Channel.GetListaContoCorrente(username);
        }
        
        public System.Threading.Tasks.Task<WCFServerManager.ServiceReferenceContoCorrente.ContoCorrente[]> GetListaContoCorrenteAsync(string username) {
            return base.Channel.GetListaContoCorrenteAsync(username);
        }
        
        public WCFServerManager.ServiceReferenceContoCorrente.ContoCorrente SelectContoCorrente(ulong idContoCorrente) {
            return base.Channel.SelectContoCorrente(idContoCorrente);
        }
        
        public System.Threading.Tasks.Task<WCFServerManager.ServiceReferenceContoCorrente.ContoCorrente> SelectContoCorrenteAsync(ulong idContoCorrente) {
            return base.Channel.SelectContoCorrenteAsync(idContoCorrente);
        }
        
        public bool CheckIBAN(string IBAN) {
            return base.Channel.CheckIBAN(IBAN);
        }
        
        public System.Threading.Tasks.Task<bool> CheckIBANAsync(string IBAN) {
            return base.Channel.CheckIBANAsync(IBAN);
        }
        
        public bool CheckIDConto(ulong idContoCorrente) {
            return base.Channel.CheckIDConto(idContoCorrente);
        }
        
        public System.Threading.Tasks.Task<bool> CheckIDContoAsync(ulong idContoCorrente) {
            return base.Channel.CheckIDContoAsync(idContoCorrente);
        }
        
        public bool AggiungiContoCorrente(string username, string idFiliale, System.Nullable<decimal> saldo) {
            return base.Channel.AggiungiContoCorrente(username, idFiliale, saldo);
        }
        
        public System.Threading.Tasks.Task<bool> AggiungiContoCorrenteAsync(string username, string idFiliale, System.Nullable<decimal> saldo) {
            return base.Channel.AggiungiContoCorrenteAsync(username, idFiliale, saldo);
        }
        
        public string GetIBANByIdContoCorrente(ulong idContoCorrente) {
            return base.Channel.GetIBANByIdContoCorrente(idContoCorrente);
        }
        
        public System.Threading.Tasks.Task<string> GetIBANByIdContoCorrenteAsync(ulong idContoCorrente) {
            return base.Channel.GetIBANByIdContoCorrenteAsync(idContoCorrente);
        }
    }
}