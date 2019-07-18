﻿using EasyConsole;

namespace WCFClient.Pages
{
    class ProfiloLogout : Page
    {
        public ProfiloLogout(Program program) : base("Logout", program) { }

        public override void Display()
        {
            base.Display();
            string scelta = Input.ReadString("Sei sicuro di voler uscire? (si/no) : ");

            if(scelta.ToUpper() == "NO")
            {
                Program.NavigateHome();
            }
        }
    }
}
