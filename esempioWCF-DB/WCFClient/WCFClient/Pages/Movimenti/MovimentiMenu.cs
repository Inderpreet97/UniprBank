using EasyConsole;

namespace WCFClient.Pages
{

    static class FunzioniMovimento {
        public static bool checkIBAN(string IBAN) {
            bool risultato = false;
            //risultato = WCFClient.checkIBAN(IBAN);
            return risultato;
        }
        public static bool checkImportoDisponibile(decimal importo, string IBANCommittente) {
            bool risultato = false;
            //risultato = WCFClient.checkImportoDisponibile(importo, idContoCorrente);
            return risultato;
        }
        public static bool checkIDConto(int idContoCorrente) {
            //bool risultato = WCFClient.CheckIdContoCorrente(int idContoCorrente);
            return false;
        }
    }

    class MovimentiMenu : MenuPage
    {
        public MovimentiMenu(Program program)
            : base("Movimenti", program,
                    new Option("Bonifico", () => program.NavigateTo<Bonifico>()),
                    new Option("Prelievo", () => program.NavigateTo<Prelievo>()),
                    new Option("Deposito", () => program.NavigateTo<Deposito>()))
        {
        }
    }
}
