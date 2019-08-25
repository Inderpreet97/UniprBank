using EasyConsole;
using ConsoleTables;
using System.Collections.Generic;
using WCFClient.ServiceReference1;

namespace WCFClient.Pages
{
    class ListaImpiegati : Page
    {
        public ListaImpiegati(Program program) : base("Lista Impiegati", program) { }

        public override void Display()
        {
            base.Display();
            // LoggedUser.idFiliale = L'idfiliale del direttore/impiegato che sta richiedendo la lista
            List<Persona> listaImpiegati = Globals.wcfClient.GetListaPersone("impiegato", LoggedUser.idFiliale);

            var table = new ConsoleTable("Filiale", "Privilegi", "Codice Fiscale", "Nome", "Cognome", "Sesso",
                "Data di nascita", "Indirizzo", "CAP", "Città", "Provincia", "Stato", "Numero di Telefono");

            listaImpiegati.ForEach(persona => {
                // Prima di poter inserire la data di nascita nella tabella va sistemata in questo modo
                var tempDataNascita = persona.dataDiNascita.ToString().Remove(10, 9);

                table.AddRow(persona.filiale, persona.privilegi, persona.codiceFiscale, persona.nome,
                    persona.cognome, persona.sesso, tempDataNascita, persona.indirizzo, persona.CAP,
                    persona.citta, persona.provincia, persona.stato, persona.numeroDiTelefono);
            });

            table.Write();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
