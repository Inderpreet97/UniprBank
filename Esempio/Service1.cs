public bool ModificaProdotto(int idProdotto, int? nuovoIdProdotto, string nome, string tipologia, decimal? prezzo, int? quantita)
{
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();

        // Start a local transaction.
        SqlTransaction transaction = connection.BeginTransaction();

        SqlCommand command = connection.CreateCommand();

        // Must assign both transaction object and connection
        // to Command object for a pending local transaction
        command.Connection = connection;
        command.Transaction = transaction;

        try
        {
            command.CommandText = "UPDATE Prodotti SET ";

            if (nuovoIdProdotto.HasValue)
            {
                command.CommandText += " id_prodotti = " + nuovoIdProdotto;
                if(nome != string.Empty || tipologia != string.Empty || prezzo.HasValue || quantita.HasValue)
                {
                    command.CommandText += ",";
                }
            }

            if (nome != string.Empty)
            {
                command.CommandText += " nome_prodotti = '" + nome + "'";
                if (tipologia != string.Empty || prezzo.HasValue || quantita.HasValue)
                {
                    command.CommandText += ",";
                }
            }

            if (tipologia != string.Empty)
            {
                command.CommandText += " tipologia_prodotti = '" + tipologia + "'";
                if (prezzo.HasValue || quantita.HasValue)
                {
                    command.CommandText += ",";
                }
            }

            if (prezzo.HasValue)
            {
                command.CommandText += " prezzo_prodotti = " + prezzo;
                if (quantita.HasValue)
                {
                    command.CommandText += ",";
                }
            }

            if (quantita.HasValue)
            {
                command.CommandText += " quantita_prodotti = " + quantita;
            }


            command.CommandText += " WHERE id_prodotti = " + idProdotto;

            int result = command.ExecuteNonQuery();

            // Attempt to commit the transaction.
            transaction.Commit();

            if (result > 0) return true;
            else return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
            Console.WriteLine("  Message: {0}", ex.Message);

            // Attempt to roll back the transaction.
            try
            {
                transaction.Rollback();
            }
            catch (Exception ex2)
            {
                // This catch block will handle any errors that may have occurred
                // on the server that would cause the rollback to fail, such as
                // a closed connection.
                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                Console.WriteLine("  Message: {0}", ex2.Message);
            }
            return false;
        }
    }
}