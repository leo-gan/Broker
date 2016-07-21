﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Broker.WS_Ref {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WS_Ref.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/PetitionAddUpdate", ReplyAction="http://tempuri.org/IService/PetitionAddUpdateResponse")]
        string PetitionAddUpdate(Contracts.CallRequest value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/PetitionAddUpdate", ReplyAction="http://tempuri.org/IService/PetitionAddUpdateResponse")]
        System.Threading.Tasks.Task<string> PetitionAddUpdateAsync(Contracts.CallRequest value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/AddMinute", ReplyAction="http://tempuri.org/IService/AddMinuteResponse")]
        string AddMinute(Contracts.CallRequest value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/AddMinute", ReplyAction="http://tempuri.org/IService/AddMinuteResponse")]
        System.Threading.Tasks.Task<string> AddMinuteAsync(Contracts.CallRequest value);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : Broker.WS_Ref.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<Broker.WS_Ref.IService>, Broker.WS_Ref.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string PetitionAddUpdate(Contracts.CallRequest value) {
            return base.Channel.PetitionAddUpdate(value);
        }
        
        public System.Threading.Tasks.Task<string> PetitionAddUpdateAsync(Contracts.CallRequest value) {
            return base.Channel.PetitionAddUpdateAsync(value);
        }
        
        public string AddMinute(Contracts.CallRequest value) {
            return base.Channel.AddMinute(value);
        }
        
        public System.Threading.Tasks.Task<string> AddMinuteAsync(Contracts.CallRequest value) {
            return base.Channel.AddMinuteAsync(value);
        }
    }
}