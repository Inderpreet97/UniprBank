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
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReferenceMovimenti.IServiceMovimenti")]
    public interface IServiceMovimenti {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceMovimenti/DoWork", ReplyAction="http://tempuri.org/IServiceMovimenti/DoWorkResponse")]
        void DoWork();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceMovimenti/DoWork", ReplyAction="http://tempuri.org/IServiceMovimenti/DoWorkResponse")]
        System.Threading.Tasks.Task DoWorkAsync();
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
        
        public void DoWork() {
            base.Channel.DoWork();
        }
        
        public System.Threading.Tasks.Task DoWorkAsync() {
            return base.Channel.DoWorkAsync();
        }
    }
}
