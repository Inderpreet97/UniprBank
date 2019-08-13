CREATE TABLE Persona(
    codiceFiscale VARCHAR(16) NOT NULL,
    nome varchar(255) NOT NULL,
    cognome varchar(255) NOT NULL,
    dataDiNascita date NOT NULL,
    sesso varchar(25) NOT NULL,
    indirizzo varchar(255) NOT NULL,
    CAP NUMERIC(5) NOT NULL,
    citta varchar(255) NOT NULL,
    provincia varchar(2) NOT NULL,
    stato varchar(255) NOT NULL,
    numeroDiTelefono varchar(20) NOT NULL,
    CONSTRAINT PK_persona PRIMARY KEY (codiceFiscale)
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
    CONSTRAINT PK_Filiale PRIMARY KEY (idFiliale)
);

CREATE TABLE Account (
    username varchar(255) NOT NULL COLLATE SQL_Latin1_General_CP1_CS_AS,
    password varchar(255) NOT NULL COLLATE SQL_Latin1_General_CP1_CS_AS,
    privilegi varchar(255) NOT NULL,
    codiceFiscale VARCHAR(16) NOT NULL,
    filiale VARCHAR(10) NOT NULL,
    CONSTRAINT PK_utente PRIMARY KEY (username),
    CONSTRAINT FK_codiceFiscale FOREIGN KEY (codiceFiscale) REFERENCES Persona(codiceFiscale) ON UPDATE CASCADE,
    CONSTRAINT FK_filiale FOREIGN KEY (filiale) REFERENCES Filiale(idFiliale) ON UPDATE CASCADE
);

-- usare questo codice SOLO SE le colonne password e username non sono già CASESENSITIVE
ALTER TABLE Account
ALTER COLUMN username varchar(255)
COLLATE SQL_Latin1_General_CP1_CS_AS

ALTER TABLE Account
ALTER COLUMN password varchar(255)
COLLATE SQL_Latin1_General_CP1_CS_AS
-- =====================================================================================

CREATE TABLE ContoCorrente(
    idContoCorrente NUMERIC(10) IDENTITY(1000000000,1) NOT NULL,
    IBAN VARCHAR(20) DEFAULT NULL,
    username VARCHAR(255) NOT NULL,
    saldo DECIMAL(12,2),
    idFiliale varchar(10) not null,
    CONSTRAINT PK_IdConto PRIMARY KEY (IdContoCorrente),
    CONSTRAINT FK_username FOREIGN KEY (username) REFERENCES Account(username)  ON UPDATE CASCADE,
    CONSTRAINT AK_IBAN UNIQUE(IBAN)
);

CREATE TABLE Movimenti(
    idMovimenti NUMERIC(12) IDENTITY(100000000000,1) NOT NULL,
    IBANCommittente varchar(20) NOT NULL,
    tipo VARCHAR(255) NOT NULL,
    importo DECIMAL(12,2) NOT NULL,
    IBANBeneficiario varchar(20),
    dataOra DATETIME NOT NULL,
    CONSTRAINT PK_IdMovimenti PRIMARY KEY (IdMovimenti),
    CONSTRAINT FK_Committente FOREIGN KEY (IBANCommittente) REFERENCES ContoCorrente(IBAN)  ON UPDATE CASCADE
    -- CONSTRAINT FK_Beneficiario FOREIGN KEY (IBANBeneficiario) REFERENCES ContoCorrente(IBAN); -- SQL non permette di aver ON UPDATE CASCADE su tutte e due le Foreign Key
    -- FK_Beneficiario BISOGNA TOGLIERLO PER FAR FUNZIONARE IL TRIGGER
);
-- ##################################################################################
-- ################ AGGIUNGERE I TRIGGER DOPO AVER CREATO LE TABELLE ################
-- ##################################################################################
CREATE TRIGGER UpdateIdFiliale
ON Account
AFTER UPDATE
AS IF UPDATE(filiale)
SET NOCOUNT ON; -- NOCOUNT ON: non restituisce il numero di righe modificate
   UPDATE ContoCorrente
   SET idFiliale = (SELECT filiale FROM INSERTED)
   WHERE username = (SELECT username FROM INSERTED)

-- IL TRIGGER PER AGGIORNARE L'IBAN SE VIENE AGGIORNATO QUALCHE ATTRIBUTO DEL CONTO CORRENTE
-- E CREARE AUTOMATICAMENTE IL NUOVO IBAN DOPO UN INSERT

CREATE TRIGGER UpdateContoCorrente
ON ContoCorrente
after INSERT, UPDATE
AS
BEGIN
  SET NOCOUNT ON; -- NOCOUNT ON: non restituisce il numero di righe modificate
	Update ContoCorrente
	SET idFiliale = (SELECT filiale FROM Account WHERE username = (SELECT username FROM INSERTED))
	WHERE idContoCorrente = (SELECT idContoCorrente FROM INSERTED);

	UPDATE ContoCorrente
	SET IBAN = (SELECT filiale FROM Account WHERE username = (SELECT username FROM INSERTED)) + (SELECT CAST(idContoCorrente AS VARCHAR) FROM INSERTED)
	WHERE idContoCorrente = (SELECT idContoCorrente FROM INSERTED);
END

-- QUESTO TRIGGER FUNZIONA PERFETTAMENTE
CREATE TRIGGER UpdateIBANBeneficiario
ON ContoCorrente
AFTER UPDATE
AS IF UPDATE(IBAN)
SET NOCOUNT ON; -- NOCOUNT ON: non restituisce il numero di righe modificate
   UPDATE Movimenti
   SET IBANBeneficiario = (SELECT IBAN FROM INSERTED)
   WHERE IBANBeneficiario = (SELECT IBAN FROM DELETED)

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

-- ##################################################################################
-- ##################### Query per inserire alcuni dati di prova ####################
-- ##################################################################################

INSERT INTO Persona VALUES ('INDI28091997','Inderpreet','Singh','28-09-1997','maschio','Via Torricelli,10',43036,'Fidenza','PR','Italia',3279199829);
INSERT INTO Persona VALUES ('ADIN12121997','Adin','Piergaru','12-12-1997','maschio','Via Torricelli,10',43036,'Fidenza','PR','Italia',3329199829);
INSERT INTO Persona VALUES ('BEPPE15031998','Giuseppe','Urbano','15-03-1998','maschio','Via Torricelli,10',43036,'Fidenza','PR','Italia',3279188829);
INSERT INTO Persona VALUES ('LUCA28071999','Luca','Inzaghi','28-07-1999','maschio','Via Torricelli,10',43036,'Fidenza','PR','Italia',3449199829);

INSERT INTO Filiale VALUES ('PR12FID001','UniPR Bank Fidenza','Via Gramsci,71',43036,'Fidenza','PR','Italia',0524123564);
INSERT INTO Filiale VALUES ('CR12CRM001','UniPR Bank Cremona','Via Pascoli,1',45015,'Cremona','CR','Italia',0524123333);

INSERT INTO Account VALUES ('indi97','indi123','cliente','INDI28091997','PR12FID001');
INSERT INTO Account VALUES ('indi97Dir','indi123','admin','INDI28091997','PR12FID001');
INSERT INTO Account VALUES ('adin97Imp','adin123','impiegato','ADIN12121997','CR12CRM001');
INSERT INTO Account VALUES ('beppe98Dir','beppe123','admin','BEPPE15031998','CR12CRM001');
INSERT INTO Account VALUES ('luca99','luca123','cliente','LUCA28071999','CR12CRM001');
INSERT INTO Account VALUES ('beppe98','beppe123','cliente','BEPPE15031998','PR12FID001');

INSERT INTO ContoCorrente VALUES ('1','indi97',1000.00,'12FID001');
INSERT INTO ContoCorrente VALUES ('2','luca99',1000.00,'12CRM001');
INSERT INTO ContoCorrente VALUES ('3','beppe98',1000.00,'12FID001');
INSERT INTO ContoCorrente VALUES ('4','beppe98',1000.00,'PR12FID001');
INSERT INTO ContoCorrente VALUES ('5','luca99',1000.00,'CR12CRM001');
INSERT INTO ContoCorrente VALUES ('6','indi97',1000.00,'PR12FID001');

--INSERT INTO Movimenti VALUES ();
--INSERT INTO Movimenti VALUES ();
--INSERT INTO Movimenti VALUES ();
--INSERT INTO Movimenti VALUES ();
--INSERT INTO Movimenti VALUES ();
