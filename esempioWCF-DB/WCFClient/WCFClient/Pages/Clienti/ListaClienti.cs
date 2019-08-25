using EasyConsole;
using ConsoleTables;
using System.Collections.Generic;
using WCFClient.ServiceReference1;

namespace WCFClient.Pages
{
    class ListaClienti : Page
    {
        public ListaClienti(Program program) : base("Crea ContoCorrente", program) { }

        public void getListaClienti() {

            //LoggedUser.idFiliale = L'id filiale del direttore/impiegato che sta richiedendo la lista dei clienti
            List<Persona> listaClienti = Globals.wcfClient.GetListaPersone("cliente", LoggedUser.idFiliale);

            var table = new ConsoleTable("Filiale", "Privilegi", "Codice Fiscale", "Nome", "Cognome", "Sesso",
                "Data di nascita", "Indirizzo", "CAP", "Città", "Provincia", "Stato", "Numero di Telefono");

            listaClienti.ForEach(persona => {
                // Prima di poter inserire la data di nascita nella tabella va sistemata in questo modo
                var tempDataNascita = persona.dataDiNascita.ToString().Remove(10, 9);

                table.AddRow(persona.filiale, persona.privilegi, persona.codiceFiscale, persona.nome,
                    persona.cognome, persona.sesso, tempDataNascita, persona.indirizzo, persona.CAP,
                    persona.citta, persona.provincia, persona.stato, persona.numeroDiTelefono);
            });

            table.Write();

        }

        public override void Display()
        {
            base.Display();
            Output.WriteLine("Lista clienti");

            this.getListaClienti();

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
