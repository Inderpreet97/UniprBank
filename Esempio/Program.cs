Console.Write("\t\nInserire il ID del prodotto da modificare: ");
string idProdottoModificareString = Console.ReadLine();
int idProdottoModificare;
                                
int.TryParse(idProdottoModificareString, out idProdottoModificare);

Console.Write("\t\nInserire il Nuovo ID del prodotto ( premi invio per non modificare): ");
string NuovoIDProdottoModificareString = Console.ReadLine();
int? NuovoIDProdottoModificare = null;
int temp;

if (!string.IsNullOrWhiteSpace(NuovoIDProdottoModificareString))
{
    int.TryParse(NuovoIDProdottoModificareString, out temp);
    NuovoIDProdottoModificare = temp;
}

string NomeProdottoModificare = string.Empty;
Console.Write("\t\nInserire il Nuovo NOME del prodotto ( premi invio per non modificare): ");
string tempNomeProdottoModificare = Console.ReadLine();

if (!string.IsNullOrWhiteSpace(tempNomeProdottoModificare))
{
    NomeProdottoModificare = tempNomeProdottoModificare;
}

string TipoProdottoModificare = string.Empty;
Console.Write("\t\nInserire la Nuova TIPOLOGIA del prodotto ( premi invio per non modificare): ");
string tempTipoProdottoModificare = Console.ReadLine();

if (!string.IsNullOrWhiteSpace(tempTipoProdottoModificare))
{
    TipoProdottoModificare = tempTipoProdottoModificare;
}

Console.Write("\t\nInserire il Nuovo PREZZO del prodotto ( premi invio per non modificare): ");
string prezzoProdottoModificareString = Console.ReadLine();
decimal? prezzoProdottoModificare = null;
decimal deciamlTemp;

if (!string.IsNullOrWhiteSpace(prezzoProdottoModificareString))
{
    decimal.TryParse(prezzoProdottoModificareString, out deciamlTemp);
    prezzoProdottoModificare = deciamlTemp;
}

Console.Write("\t\nInserire la Nuova QUANTITA' del prodotto ( premi invio per non modificare): ");
string quantitaProdottoModificareString = Console.ReadLine();
int? quantitaProdottoModificare = null;
                                
if (!string.IsNullOrWhiteSpace(quantitaProdottoModificareString))
{
    Int32.TryParse(quantitaProdottoModificareString, out temp);
    quantitaProdottoModificare = temp;
}

risulato = wcfClient.ModificaProdotto(idProdottoModificare, NuovoIDProdottoModificare, NomeProdottoModificare, TipoProdottoModificare, prezzoProdottoModificare, quantitaProdottoModificare);
if (risulato) Console.WriteLine("FATTO");
else Console.WriteLine("ERRORE");
