﻿using EasyConsole;
using System;

namespace WCFClient.Pages
{
    class AggiungiImpiegato : Page
    {
        public AggiungiImpiegato(Program program) : base("Aggiugni Impiegato", program) { }

        public override void Display()
        {
            base.Display();

            Funzioni.aggiungiPersona("Impiegato");

            Input.ReadString("\nPremi [Invio] per ritornare al menu principale");
            Program.NavigateHome();
        }
    }
}
