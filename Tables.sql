CREATE TABLE Persona(
    codiceFiscale VARCHAR(16) NOT NULL,
    nome varchar(255) NOT NULL,
    cognome varchar(255) NOT NULL,
    dataNascita date NOT NULL,
    sesso varchar(25) NOT NULL,
    indirizzo varchar(255) NOT NULL,
    CAP NUMERIC(5) NOT NULL,
    citta varchar(255) NOT NULL,
    provincia varchar(2) NOT NULL,
    stato varchar(255) NOT NULL,
    numTelefono varchar(20) NOT NULL,
    CONSTRAINT PK_persona PRIMARY KEY (codiceFiscale)
);


CREATE TABLE Account (
    username varchar(255) NOT NULL,
    password varchar(255) NOT NULL,
    privilegi varchar(255) NOT NULL,
    codiceFiscale VARCHAR(16) NOT NULL,
    filiale VARCHAR(10) NOT NULL,
    CONSTRAINT PK_utente PRIMARY KEY (username),
    CONSTRAINT FK_codiceFiscale FOREIGN KEY (codiceFiscale) REFERENCES Persona(codiceFiscale),
    CONSTRAINT FK_filiale FOREIGN KEY (filiale) REFERENCES Filiale(idFiliale)
);

CREATE TABLE Filiale(
    idFiliale varchar(10) NOT NULL,
    nome varchar(255),
    indirizzo varchar(255) NOT NULL,
    CAP NUMERIC(5) NOT NULL,
    citta varchar(255) NOT NULL,
    provincia varchar(2) NOT NULL,
    stato varchar(255) NOT NULL,
    numTelefono varchar(20) NOT NULL,
    direttore VARCHAR(255) NOT NULL,
    CONSTRAINT PK_Filiale PRIMARY KEY (idFiliale),
    CONSTRAINT FK_direttore FOREIGN KEY (direttore) REFERENCES Account(username)
);

CREATE TABLE ContoCorrente(
    idContoCorrente NUMERIC(10) NOT NULL,
    IBAN VARCHAR(20) DEFAULT NULL,
    username VARCHAR(255) NOT NULL,
    saldo DECIMAL(12,2),
    idFiliale varchar(10) not null,
    CONSTRAINT PK_IdConto PRIMARY KEY (IdContoCorrente),
    CONSTRAINT FK_username FOREIGN KEY (username) REFERENCES Account(username)  ON UPDATE CASCADE,
    CONSTRAINT FK_idFiliale FOREIGN KEY (idFiliale) REFERENCES Filiale(idFiliale) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT AK_IBAN UNIQUE(IBAN)
);
-- AGGIUNGERE IL TRIGGER PER AGGIORNARE L'IBAN SE VIENE AGGIORNATO QUALCHE ATTRIBUTO DEL CONTO CORRENTE
-- QUESTO TRIGGER DA DEI PROBLEMI PERCHE' IN SQL SERVER NON ESISTE il NEW
create trigger IBAN_ContoCorrente
on ContoCorrente
after INSERT
for each row
set new.IBAN = new.idFiliale + new.idContoCorrente;

CREATE TABLE Movimenti(
    idMovimenti NUMERIC(12) NOT NULL,
    IBANCommittente varchar(20) NOT NULL,
    tipo VARCHAR(255) NOT NULL,
    importo DECIMAL(12,2) NOT NULL,
    IBANBeneficiario varchar(20),
    dataOra DATETIME NOT NULL,
    CONSTRAINT PK_IdMovimenti PRIMARY KEY (IdMovimenti),
    CONSTRAINT FK_Committente FOREIGN KEY (IBANCommittente) REFERENCES ContoCorrente(IBAN)  ON UPDATE CASCADE,
    -- CONSTRAINT FK_Beneficiario FOREIGN KEY (IBANBeneficiario) REFERENCES ContoCorrente(IBAN); -- SQL non permette di aver ON UPDATE CASCADE su tutte e due le Foreign Key
    -- FK_Beneficiario BISOGNA TOGLIERLO PER FAR FUNZIONARE IL TRIGGER
);
-- QUESTO TRIGGER FUNZIONA PERFETTAMENTE
CREATE TRIGGER UpdateIBANBeneficiario
ON ContoCorrente
after UPDATE
AS IF UPDATE(IBAN)
BEGIN
SET NOCOUNT ON; -- NOCOUNT ON: non restituisce il numero di righe modificate
   UPDATE Movimenti
   SET IBANBeneficiario = (SELECT IBAN FROM INSERTED)
   WHERE IBANBeneficiario = (SELECT IBAN FROM DELETED)
END
-- INSERTED e DELETED sono due tabelle create in automatico dal TRIGGER.
-- INSERTED contiene i dati dopo l'update
-- DELETED contiene i dati che sono stati modficati
-- ES.  idMovimento: 123456789123, IBANCommittente: xx52, tipo: "bonifico",  importo: 500€, IBANBeneficiario: xx23
--      idMovimento: 123456789124, IBANCommittente: xx23, tipo: "bonifico",  importo: 500€, IBANBeneficiario: xx52
-- SU CONTOCORRENTE viene fatto l'update sul conto con IBAN: xx52 e viene modificato in IBAN: xx55
-- In tutti i movimenti in cui l'IBANCommittente era uguale a xx52 viene aggiornato in xx55 automaticamente per via della politica
-- di UPDATE CASCADE.
-- Mentre in tutti i movimenti in cui è presente come IBANBeneficiario viene aggiornato grazie al Trigger.
-- In DELETED ci sarà l'IBAN precedente alla modifica, quindi IBAN = xx52
-- In INSERTED ci sarà l'IBAN dopo la modifica, quindi IBAN = xx55
