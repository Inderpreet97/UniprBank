using EasyConsole;

namespace WCFClient.Pages {
    class ProfiloModifica : Page {

        //Questa classe permette la modifica solo del proprio profilo (LoggedUser)

        public ProfiloModifica(Program program) : base("Logout", program) { }

        public override void Display() {
            base.Display();

            Funzioni.modificaPersona(LoggedUser.username);

            // ========================
            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
