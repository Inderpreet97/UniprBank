using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class AggiungiImpiegato : Page
    {
        public AggiungiImpiegato(Program program) : base("Modifica Dati", program) { }

        public override void Display()
        {
            base.Display();

            string privilegio = "impiegato";

            string usernameImpiegato = null;
            bool checkUsername = true;
            bool risultato = false;

            while (checkUsername) {
                usernameImpiegato = Input.ReadString("Username: ");
                //checkUsername = WCFClient.UsernameDisponibile(usernameImpiegato)
                if (checkUsername) { Output.WriteLine("Username già presente, riprovare"); }
            }

            Persona impiegato = new Persona();

            string password = Input.ReadString("Digita password: ");
            string checkPassword = Input.ReadString("Conferma password: ");

            while(checkPassword != password) {
                Output.WriteLine("Le password non coincidono");
                password = Input.ReadString("Digita password: ");
                checkPassword = Input.ReadString("Conferma password: ");
            }
            
            impiegato.username = usernameImpiegato;
            impiegato.nome = Input.ReadString("Nome: ");
            impiegato.cognome = Input.ReadString("Cognome: ");
            impiegato.codiceFiscale = Input.ReadString("Codice fiscale: ");
            impiegato.dataDiNascita = Convert.ToDateTime(Input.ReadString("Data di nascita: "));
            impiegato.citta = Input.ReadString("Città: ");
            impiegato.provincia = Input.ReadString("Provincia: ");
            impiegato.stato = Input.ReadString("Stato");
            impiegato.CAP = Input.ReadString("CAP: ");
            impiegato.indirizzo = Input.ReadString("Indirizzo: ");
            impiegato.sesso = Input.ReadString("Sesso: ");
            impiegato.numeroDiTelefono = Input.ReadString("Numero di telefono: ");
            impiegato.privilegi = privilegio;
            impiegato.filiale = Input.ReadString("Filiale associata: ");
            //bool risultato = WCFClient.RegistraPersona(impiegato, password);

            if (risultato) { Output.WriteLine(privilegio + " registrato correttamente\n"); }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
