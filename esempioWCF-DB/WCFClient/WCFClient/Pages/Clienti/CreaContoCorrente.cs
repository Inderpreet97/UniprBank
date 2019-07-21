using EasyConsole;

namespace WCFClient.Pages
{
    class CreaContoCorrente : Page
    {
        public CreaContoCorrente(Program program) : base("Modifica Persona/Account", program) { }
        
        public int generaIdContoCorrente() {
            int idLastConto = 0;
            //idLastConto = WCFClient.getLastIdContoCorrente();
            return idLastConto+1;
        }

        public string generaNuovoIBAN(string idFiliale) {
            string iban = "Iban";
            return iban;
        }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Hello from Page 1Ai");

            string username = Funzioni.digitaUsername();

            ContoCorrente contoCorrente = new ContoCorrente();
            contoCorrente.idContoCorrente = generaIdContoCorrente();
            contoCorrente.saldo = 0;
            contoCorrente.idFiliale = "";
            contoCorrente.IBAN = generaNuovoIBAN(contoCorrente.idFiliale);
            //idFiliale = WCFClient.getIdFiliale(LoggedUser.username);

            //bool WCFClient.aggiungiContoCorrente(contoCorrente);
            bool risultato = false;

            if (risultato) { Output.WriteLine("Conto corrente aggiunto"); } else { Output.WriteLine("Errore"); }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
