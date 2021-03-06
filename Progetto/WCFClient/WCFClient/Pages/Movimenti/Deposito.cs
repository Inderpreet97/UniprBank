﻿using EasyConsole;
using System;
using System.Collections.Generic;
using WCFClient.ServiceReference1;

namespace WCFClient.Pages
{
    class Deposito : Page
    {

        public Deposito(Program program) : base("Deposito", program) { }

        public override void Display()
        {
            base.Display();

            var scelta = 1;

            do {
                try {

                    //LISTA CONTI CORRENTI
                    List<ContoCorrente> listaContiUser = Funzioni.getListaContiByPrivilege();

                    //SCELTA CONTO CORRENTE
                    UInt64 idContoCorrente = Funzioni.scegliIdContoCorrente(listaContiUser);
                    string IBANCommittente = Globals.wcfClient.GetIBANByIdContoCorrente(idContoCorrente);

                    decimal importo = Convert.ToDecimal(Input.ReadString("Importo da caricare: "));
                    if (importo > 0) {

                        if (Globals.wcfClient.EseguiDeposito(idContoCorrente, importo)) {
                            Output.WriteLine("Deposito di denaro effettuato con successo");
                        } else {
                            Output.WriteLine("Deposito non effettuato");
                        }
                    } else {
                        Output.WriteLine("Deposito non effettuato, la cifra non è valida");
                    }
                }
                catch (FormatException ex) {
                    Output.WriteLine(ConsoleColor.Red, ex.Message);
                    scelta = Input.ReadInt("Vuoi annullare l'operazione?\n1) Si\n2) No\nScelta: ", 1, 2);
                }

            } while (scelta != 1);

            Input.ReadString("\nPremi [Invio] per ritornare al menu principale");
            Program.NavigateHome();
        }
    }
}
