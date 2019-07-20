[OperationContract]
bool ModificaProdotto(int idProdotto ,int? nuovoIdProdotto, string nome, string tipologia, decimal? prezzo, int? quantita);
/* Funzionamento ModificaProdotto:
    * 
    *      CHIAMATA 1: bool risultato = wcfClient.ModificaProdotto( id del prodotto da aggiornare ,null, "Un nome", "Una tipologia", null, null);
    *              In questo caso avendo messo null nei campi nuovoIdProdotto, prezzo e quantita questi campi NON verrano modificati.
    *              Mentre i campi nome e tipologia VERRANO modificati.
    *              
    *      CHIAMATA 2: bool risultato = wcfClient.ModificaProdotto(id del prodotto da aggiornare ,null, string.Empty, "Una tipologia", null, null);
    *              In questo caso avendo messo null nei campi nuovoIdProdotto, prezzo e quantita questi campi NON verrano modificati.
    *              Anche il campo nome NON verrà modificato perchè presente string.Empty.
    *              Mentre il tipologia VERRA' modificato.
    *              
    *      CHIAMATA 3: bool risultato = wcfClient.ModificaProdotto(id del prodotto da aggiornare , Nuovo ID, string.Empty, string.Empty, Nuovo Prezzo, null);
    *              In questo caso avendo messo null solo nel campo quantita questo campo NON verrà modificato.
    *              Anche i campi nome e tipologia NON verranno modificati perchè presente string.Empty.
    *              Mentre verrà aggiornato l'id ed il prezzo del prodotto. id = Nuovo ID e prezzo = Nuovo Prezzo.
    *              
    *  CAMPO OBBLIGATORIO: idProdotto -> Per sapere l'id del prodotto da modificare.
    *  
    *      Perciò inserire null nei campi tipo int e decimal (nuovoIdProdotto, prezzo, quantita) per lasciarli inalterati 
    *      e inserire string.Empty nei campi di tipo string (nome, tipologia) per lasciarli inalterati.
    *      
    *      FARE ATTENZIONE ALL'ORDINE CON CUI VENGONO INSERITI I CAMPI. (int, string, string, decimal, int)
*/
    