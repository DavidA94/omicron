﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Job_App_Data.OmicronService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="OmicronService.IOmicronService")]
    public interface IOmicronService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOmicronService/GetData", ReplyAction="http://tempuri.org/IOmicronService/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOmicronService/GetData", ReplyAction="http://tempuri.org/IOmicronService/GetDataResponse")]
        System.Threading.Tasks.Task<string> GetDataAsync(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOmicronService/ValidUser", ReplyAction="http://tempuri.org/IOmicronService/ValidUserResponse")]
        WebService.UserType ValidUser(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOmicronService/ValidUser", ReplyAction="http://tempuri.org/IOmicronService/ValidUserResponse")]
        System.Threading.Tasks.Task<WebService.UserType> ValidUserAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOmicronService/GetUserData", ReplyAction="http://tempuri.org/IOmicronService/GetUserDataResponse")]
        WebService.AppDataContract[] GetUserData(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IOmicronService/GetUserData", ReplyAction="http://tempuri.org/IOmicronService/GetUserDataResponse")]
        System.Threading.Tasks.Task<WebService.AppDataContract[]> GetUserDataAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IOmicronServiceChannel : Job_App_Data.OmicronService.IOmicronService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class OmicronServiceClient : System.ServiceModel.ClientBase<Job_App_Data.OmicronService.IOmicronService>, Job_App_Data.OmicronService.IOmicronService {
        
        public OmicronServiceClient() {
        }
        
        public OmicronServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public OmicronServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OmicronServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public OmicronServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public System.Threading.Tasks.Task<string> GetDataAsync(int value) {
            return base.Channel.GetDataAsync(value);
        }
        
        public WebService.UserType ValidUser(string username, string password) {
            return base.Channel.ValidUser(username, password);
        }
        
        public System.Threading.Tasks.Task<WebService.UserType> ValidUserAsync(string username, string password) {
            return base.Channel.ValidUserAsync(username, password);
        }
        
        public WebService.AppDataContract[] GetUserData(int id) {
            return base.Channel.GetUserData(id);
        }
        
        public System.Threading.Tasks.Task<WebService.AppDataContract[]> GetUserDataAsync(int id) {
            return base.Channel.GetUserDataAsync(id);
        }
    }
}
