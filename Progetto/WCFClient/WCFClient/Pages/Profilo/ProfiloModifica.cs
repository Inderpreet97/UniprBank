﻿using EasyConsole;

namespace WCFClient.Pages {
    class ProfiloModifica : Page {

        //Questa classe permette la modifica solo del proprio profilo (LoggedUser)

        public ProfiloModifica(Program program) : base("Modifica profilo", program) { }

        public override void Display() {
            base.Display();

            Funzioni.modificaPersona(LoggedUser.username);

            // ========================
            Input.ReadString("\nPremi [Invio] per ritornare al menu principale");
            Program.NavigateHome();
        }
    }
}
