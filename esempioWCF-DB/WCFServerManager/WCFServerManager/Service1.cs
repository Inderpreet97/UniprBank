using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFServerManager.ServiceReference1;

namespace WCFServerManager
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Service1" nel codice e nel file di configurazione contemporaneamente.
    public class Service1 : IService1
    {
        private ServiceReference1.Service1Client wcfDBClient = new ServiceReference1.Service1Client();

        //Operazioni su conto corrente
        public double? GetSaldo(int idContoCorrente){
            ContoCorrente myConto = new ContoCorrente();
            myConto = wcfDBClient.SelectContoCorrente(idContoCorrente);

            if (myConto != null) { return myConto.Saldo; }
            return null;
        }
        //Operazioni di Login
        public bool Login(string username, string password){
            return wcfDBClient.Login(username, password);
        }
        public string GetPrivilegi(string username){
            return wcfDBClient.GetPrivilegi(username);
        }
    }
}
