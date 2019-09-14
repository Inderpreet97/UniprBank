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
    username varchar(255) NOT NULL,
    password varchar(255) NOT NULL,
    privilegi varchar(255) NOT NULL,
    codiceFiscale VARCHAR(16) NOT NULL,
    filiale VARCHAR(10) NOT NULL,
    CONSTRAINT PK_utente PRIMARY KEY (username),
    CONSTRAINT FK_codiceFiscale FOREIGN KEY (codiceFiscale) REFERENCES Persona(codiceFiscale) ON UPDATE CASCADE,
    CONSTRAINT FK_filiale FOREIGN KEY (filiale) REFERENCES Filiale(idFiliale) ON UPDATE CASCADE
);

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
);
-- ##################################################################################
-- ################ AGGIUNGERE I TRIGGER DOPO AVER CREATO LE TABELLE ################
-- ################        INSERIRE I TRIGGERE UNO ALLA VOLTA        ################
-- ##################################################################################

-- Questo trigger è stato creato perchè dalla tabella Filiale(idFiliale, nome...) dipendono Account e ContoCorrente. L'obiettivo è quello di aggiornare
-- l'idFiliale nelle due tabelle dipendenti qualora venisse modificato l'id nella tabella Filiale. Ma SQL Server non permette di avere due Foreign Key con
-- politica di update CASCADE contemporaneamente. Perciò abbiamo rimosso la FK da ContoCorrente e lasciato la FK con ON UPDATE CASCADE su Account.

-- Quando viene fatto un update su filiale, cascade fa l'update su account. Il trigger su account fa aggiornare la filiale anche in conto corrente
CREATE TRIGGER UpdateIdFiliale
ON Account
AFTER UPDATE
AS IF UPDATE(filiale) -- se l'update riguarda la colonna filiale nella tabella account
SET NOCOUNT ON; -- NOCOUNT ON: non restituisce il numero di righe modificate
   UPDATE ContoCorrente
   SET idFiliale = (SELECT filiale FROM INSERTED)
   WHERE username = (SELECT username FROM INSERTED)


-- ##################################################################################
-- ##################################################################################

-- Essendo l'IBAN di un conto composto da idFiliale + idContoCorrente, nel momento in cui una filiale
-- modifica il proprio idFiliale dobbiamo ricalcolare l'IBAN e aggiornarlo nei conti correnti dipendenti
-- dalla filiale modificata.

-- Questo trigger serve anche per calcolare l'IBAN di un conto corrente quando viene inserito per la prima volta.

CREATE TRIGGER UpdateContoCorrente
ON ContoCorrente
after INSERT, UPDATE
AS
BEGIN
  SET NOCOUNT ON; -- NOCOUNT ON: non restituisce il numero di righe modificate

  -- VENGONO ESEGUITE 2 QUERY:

  -- QUERY 1:
        -- Questa query permette di avere la sicurezza che l'idFiliale sia quello corretto, e che la filiale sia esistente (perchè abbiamo tolto la FK)
        -- Aggiorno l'idFIliale prendendolo dall'account
	Update ContoCorrente
	SET idFiliale = (SELECT filiale FROM Account WHERE username = (SELECT username FROM INSERTED))
	WHERE idContoCorrente = (SELECT idContoCorrente FROM INSERTED);

  -- QUERY 2:
        -- Questa query 'calcola' l'IBAN concatenando idFiliale + idContoCorrente (dopo conversione in VARCHAR)
	UPDATE ContoCorrente
	SET IBAN = (SELECT filiale FROM Account WHERE username = (SELECT username FROM INSERTED)) + (SELECT CAST(idContoCorrente AS VARCHAR) FROM INSERTED)
	WHERE idContoCorrente = (SELECT idContoCorrente FROM INSERTED);
END

-- ##################################################################################
-- ##################################################################################

-- Queto Trigger serve per simulare una Foreign Key. La tabella Movimenti dovrebbe avere IBANCommiettente e IBANBeneficiario come attributi che dipendono
-- dalla stessa tabelle ContoCorrente e questo non è possibile. Quindi teniamo la FK con UPDATE CASCADE su IBANCommittente e su IBANBeneficiario la simuliamo.

-- Perciò se viene aggiornato l'IBAN di un Conto Corrente automaticamente aggiorno l'IBAN nell'attributo IBANBeneficiario nella tabella Movimenti.
-- L'attributo IBANCommittente viene aggioranto grazie alla politica di UPDATE CASCADE
CREATE TRIGGER UpdateIBANBeneficiario
ON ContoCorrente
AFTER UPDATE
AS IF UPDATE(IBAN) -- se l'update riguarda la colonna IBAN nella tabella ContoCorrente
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

INSERT INTO Persona VALUES ('SNGNRP97P28Z222J','Inderpreet','Singh','28-09-1997','maschio','Via Torricelli,10',43036,'Fidenza','PR','Italia','3278929199');
INSERT INTO Persona VALUES ('RBNGPP98C15B034L','Giuseppe','Urbano','15-03-1998','maschio','Via Saffi, 1',43036,'Fidenza','PR','Italia','3280504123');

INSERT INTO Filiale VALUES ('PR12FID001','UniPR Bank Fidenza','Via Gramsci,71',43036,'Fidenza','PR','Italia','0524123564');
INSERT INTO Filiale VALUES ('CR12CRM001','UniPR Bank Cremona','Via Pascoli,1',45015,'Cremona','CR','Italia','0524123333');

-- FILIALI USATE PER TEST
INSERT INTO Filiale VALUES ('PR00TST000','UniPR Bank Test1','Via Dordone, 12',43036,'Fidenza','PR','Italia','0524000001');
INSERT INTO Filiale VALUES ('PR00TST001','UniPR Bank Test2','Via Gramsci, 12',43100,'Parma','PR','Italia','0524000000');

INSERT INTO Account VALUES ('indi97Dir','indi123','admin','SNGNRP97P28Z222J','PR12FID001');
INSERT INTO Account VALUES ('indi97Imp','indi123','impiegato','SNGNRP97P28Z222J','PR12FID001');
INSERT INTO Account VALUES ('indi97','indi123','cliente','SNGNRP97P28Z222J','PR12FID001');
INSERT INTO Account VALUES ('beppe97Dir','beppe123','admin','RBNGPP98C15B034L','CR12CRM001');
INSERT INTO Account VALUES ('beppe97Imp','beppe123','impiegato','RBNGPP98C15B034L','CR12CRM001');
INSERT INTO Account VALUES ('beppe97','beppe123','cliente','RBNGPP98C15B034L','CR12CRM001');


INSERT INTO ContoCorrente VALUES ('1','indi97',1000.00,'PR12FID001');
INSERT INTO ContoCorrente VALUES ('2','beppe97',1000.00,'CR12CRM001');
INSERT INTO ContoCorrente VALUES ('3','beppe97',1000.00,'CR12CRM001');

-- GUARDARE IBAN NEL DB, NON USARE '1', '2', '3' -> sono IBAN temporanei
INSERT INTO Movimenti VALUES (@IBANCommittente, @tipo, @importo, @IBANBeneficiario, @dataOra)
