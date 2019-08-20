using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class CreaContoCorrente : Page
    {
        public CreaContoCorrente(Program program) : base("Modifica Persona/Account", program) { }
        
        //Provvisorio -- AUTOINCREMENT
        public int generaIdContoCorrente() {
            int idLastConto = 0;
            //idLastConto = WCFClient.getLastIdContoCorrente();
            return idLastConto+1;
        }

        //Provvisiorio -- TRIGGER
        public string generaNuovoIBAN(string idFiliale, int? idContoCorrente) {
            string iban = idFiliale + Convert.ToString(idContoCorrente);
            return iban;
        }

        public override void Display()
        {
            base.Display();

            string username = Funzioni.digitaNuovoUsername();

            ContoCorrente contoCorrente = new ContoCorrente();
            //contoCorrente.idContoCorrente = generaIdContoCorrente();
            contoCorrente.saldo = 0;
            contoCorrente.idFiliale = LoggedUser.idFiliale;
            //contoCorrente.IBAN = generaNuovoIBAN(contoCorrente.idFiliale, contoCorrente.idContoCorrente);
            
            //bool WCFClient.aggiungiContoCorrente(contoCorrente);
            bool risultato = false;

            if (risultato) { Output.WriteLine("Conto corrente aggiunto"); } else { Output.WriteLine("Errore"); }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
