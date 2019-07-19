using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFServerManager.ServiceReference1;


namespace WCFServerManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IService1" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IService1
    {

        //Operazioni su conto corrente
        [OperationContract]
        double? GetSaldo(int idContoCorrente);

        //Operazioni di login
        [OperationContract]
        bool Login(string username, string password);

        [OperationContract]
        int GetPrivilegi(string username);
    }
}
