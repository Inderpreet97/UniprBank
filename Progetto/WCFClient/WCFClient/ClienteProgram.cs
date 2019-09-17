using EasyConsole;
using WCFClient.Pages;

namespace WCFClient
{
    class ClienteProgram : Program
    {
        public ClienteProgram()
            : base("Menu Cliente", breadcrumbHeader: true)
        {

            /* : base(string title, bool breadcrumbHeader)
             * 
             * Chiamata al costruttore della classe base, cioè il costruttore della classe Program.
             *   - string title: titolo della homepage
             *   - bool breadcrumbHeader: attiva/disattiva visualizzazione della cronologia delle pagine aperte
             * 
             * La classe Program è una classe Abstract, quindi non possiamo istanzare oggetti di Program,
             * dobbiamo prima creare una classe figlia di Program.
             * 
             * La classe program contiene una Dictionary <type, Page> che contiene tutte le pagine che verrano usate
             * in questo programma. AddPage non fa altro che aggiungere una Page a questo dictionary.
             * 
             * Program contiene anche una proprietà chiamata History di tipo Stack<Page> che permette di mantenere
             * creare una cronologia delle Page aperte.
             * 
             * 
             * 
             */
            AddPage(new MainMenuCliente(this));
            AddPage(new ListaMovimenti(this));
            AddPage(new MovimentiMenu(this));
            AddPage(new Bonifico(this));
            AddPage(new Prelievo(this));
            AddPage(new Deposito(this));
            AddPage(new ProfiloMenu(this));
            AddPage(new ProfiloModifica(this));
            AddPage(new ProfiloLogout(this));
            AddPage(new ContoMenu(this));
            AddPage(new VisualizzaConto(this));

            SetPage<MainMenuCliente>();
        }
    }
}
