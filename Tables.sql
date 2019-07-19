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
    CONSTRAINT PK_utente PRIMARY KEY (username),
    CONSTRAINT FK_codiceFiscale FOREIGN KEY (codiceFiscale) REFERENCES Persona(codiceFiscale)
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
    IBAN VARCHAR(20) NOT NULL,
    username VARCHAR(255) NOT NULL,
    saldo DECIMAL(12,2),
    idFiliale varchar(10) not null,
    CONSTRAINT PK_IdConto PRIMARY KEY (IdContoCorrente),
    CONSTRAINT FK_username FOREIGN KEY (username) REFERENCES Account(username)  ON UPDATE CASCADE,
    CONSTRAINT FK_idFiliale FOREIGN KEY (idFiliale) REFERENCES Filiale(idFiliale) ON UPDATE CASCADE ON DELETE CASCADE,
    CONSTRAINT AK_IBAN UNIQUE(IBAN)
);

CREATE TABLE Movimenti(
    idMovimenti NUMERIC(12) NOT NULL,
    IBANCommittente varchar(20) NOT NULL,
    tipo VARCHAR(255) NOT NULL,
    importo DECIMAL(12,2) NOT NULL,
    IBANBeneficiario varchar(20),
    dataOra DATETIME NOT NULL,
    CONSTRAINT PK_IdMovimenti PRIMARY KEY (IdMovimenti),
    CONSTRAINT FK_Committente FOREIGN KEY (IBANCommittente) REFERENCES ContoCorrente(IBAN)  ON UPDATE CASCADE,
    --CONSTRAINT FK_Beneficiario FOREIGN KEY (IBANBeneficiario) REFERENCES ContoCorrente(IBAN)  ON UPDATE CASCADE,
);
