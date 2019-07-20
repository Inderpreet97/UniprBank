﻿using EasyConsole;

namespace WCFClient.Pages
{
    class MainMenuImpiegato : MenuPage
    {
        public MainMenuImpiegato(Program program)
            : base("Menu Principale", program,
                  new Option("Registra Persona/Account", () => program.NavigateTo<RegistraPersona>()),
                  new Option("Crea ContoCorrente", () => program.NavigateTo<ListaClienti>()),
                  new Option("Modifica Persona/Account", () => program.NavigateTo<CreaContoCorrente>()),
                  new Option("Modifica ContoCorrente", () => program.NavigateTo<ModificaAccount>()),
                  new Option("Visualizza Lista Clienti", () => program.NavigateTo<ModificaContoCorrente>()),
                  new Option("Visualizza Lista Movimenti Cliente", () => program.NavigateTo<ListaMovimenti>()),
                  new Option("Effetua un Movimento", () => program.NavigateTo<EseguiMovimento>()),
                  new Option("Modifica Profilo Impiegato", () => program.NavigateTo<ProfiloMenu>())
                  )
        {
        }
    }
}
