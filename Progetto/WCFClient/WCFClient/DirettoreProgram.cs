using EasyConsole;
using WCFClient.Pages;

namespace WCFClient
{
    class DirettoreProgram : Program
    {
        public DirettoreProgram()
            : base("Menu Direttore", breadcrumbHeader: true)
        {
            AddPage(new MainMenuDirettore(this));

            //Clienti
            AddPage(new ClientiMenu(this));
            AddPage(new RegistraPersona(this));
            AddPage(new EliminaCliente(this));
            AddPage(new ModificaAccount(this));
            AddPage(new ListaClienti(this));

            //Filiale
            AddPage(new FilialeMenu(this));
            AddPage(new ModificaDatiFiliale(this));

            //Impiegati
            AddPage(new ImpiegatiMenu(this));
            AddPage(new ListaImpiegati(this));
            AddPage(new EliminaImpiegato(this));
            AddPage(new AggiungiImpiegato(this));
            AddPage(new ModificaImpiegato(this));

            //Profilo
            AddPage(new ProfiloMenu(this));
            AddPage(new ProfiloLogout(this));
            AddPage(new ProfiloModifica(this));
        
            //Movimenti e ContiCorrenti
            AddPage(new ContoMenu(this));
            AddPage(new CreaContoCorrente(this));
            AddPage(new VisualizzaConto(this));
            AddPage(new MovimentiMenu(this));
            AddPage(new ListaMovimenti(this));

            SetPage<MainMenuDirettore>();
        }
    }
}
