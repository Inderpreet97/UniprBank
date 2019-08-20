using EasyConsole;
using ConsoleTables;
using System.Collections.Generic;

namespace WCFClient.Pages
{
    class ListaImpiegati : Page
    {
        public ListaImpiegati(Program program) : base("Sospendi", program) { }

        public override void Display()
        {
            base.Display();

            List<Persona> listaImpiegati = new List<Persona>();
            string idFiliale = LoggedUser.idFiliale;    //L'id filiale del direttore/impiegato che sta richiedendo la lista

            //listaClienti = WCFClient.GetListaPersone("impiegato", string idFiliale);

            var table = new ConsoleTable("Filiale", "Privilegi", "Codice Fiscale", "Nome", "Cognome", "Sesso",
                "Data di nascita", "Indirizzo", "CAP", "Città", "Provincia", "Stato", "Numero di Telefono");

            listaImpiegati.ForEach(persona => {
                // Prima di poter inserire la data di nascita nella tabella va sistemata in questo modo
                var tempDataNascita = persona.dataDiNascita.ToString().Remove(10, 9);

                table.AddRow(persona.filiale, persona.privilegi, persona.codiceFiscale, persona.nome,
                    persona.cognome, persona.sesso, tempDataNascita, persona.indirizzo, persona.CAP,
                    persona.citta, persona.provincia, persona.stato, persona.numeroDiTelefono);
            });

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
