CREATE DATABASE IF NOT EXISTS BTBDatabase;
USE BTBDatabase;

-- =====================
-- TIER
-- =====================
CREATE TABLE Tier (
    IdTier INT AUTO_INCREMENT PRIMARY KEY,
    Titulo VARCHAR(128) NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Visible BOOLEAN NOT NULL DEFAULT TRUE
) ENGINE=InnoDB;

-- =====================
-- USUARIO
-- =====================
CREATE TABLE Usuario (
    IdUsuario INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(128) NOT NULL,
    Correo VARCHAR(255) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    Visible BOOLEAN NOT NULL DEFAULT TRUE,
    FechaCreacion DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    IdTier INT,
    CONSTRAINT FK_Usuario_Tier
        FOREIGN KEY (IdTier) REFERENCES Tier(IdTier)
) ENGINE=InnoDB;

-- =====================
-- PARTIDA
-- =====================
CREATE TABLE Partida (
    IdPartida INT AUTO_INCREMENT PRIMARY KEY,
    Ganador INT,
    CONSTRAINT FK_Partida_Ganador
        FOREIGN KEY (Ganador) REFERENCES Usuario(IdUsuario)
) ENGINE=InnoDB;

-- =====================
-- PARTIDA_USUARIO
-- =====================
CREATE TABLE PartidaUsuario (
    IdPartida INT PRIMARY KEY,
    IdUsuario1 INT NOT NULL,
    IdUsuario2 INT NOT NULL,
    CONSTRAINT FK_PU_Partida
        FOREIGN KEY (IdPartida) REFERENCES Partida(IdPartida),
    CONSTRAINT FK_PU_Usuario1
        FOREIGN KEY (IdUsuario1) REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_PU_Usuario2
        FOREIGN KEY (IdUsuario2) REFERENCES Usuario(IdUsuario),
    CONSTRAINT CK_Usuarios_Distintos
        CHECK (IdUsuario1 <> IdUsuario2)
) ENGINE=InnoDB;

-- =====================
-- TROPA
-- =====================
CREATE TABLE Tropa (
    IdTropa INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(128) NOT NULL,
    Vida INT NOT NULL,
    Dano INT NOT NULL
) ENGINE=InnoDB;

-- =====================
-- NODO
-- =====================
CREATE TABLE Nodo (
    IdNodo INT AUTO_INCREMENT PRIMARY KEY,
    DuenoNodo INT,
    CONSTRAINT FK_Nodo_Usuario
        FOREIGN KEY (DuenoNodo) REFERENCES Usuario(IdUsuario)
) ENGINE=InnoDB;

-- =====================
-- MOVIMIENTOS
-- =====================
CREATE TABLE Movimientos (
    IdMovimiento INT AUTO_INCREMENT PRIMARY KEY,
    IdTropa INT NOT NULL,
    NodoOrigen INT NOT NULL,
    NodoDestino INT NOT NULL,
    CONSTRAINT FK_Mov_Tropa
        FOREIGN KEY (IdTropa) REFERENCES Tropa(IdTropa),
    CONSTRAINT FK_Mov_Origen
        FOREIGN KEY (NodoOrigen) REFERENCES Nodo(IdNodo),
    CONSTRAINT FK_Mov_Destino
        FOREIGN KEY (NodoDestino) REFERENCES Nodo(IdNodo)
) ENGINE=InnoDB;

-- =====================
-- PARTIDA_MOVIMIENTO
-- =====================
CREATE TABLE PartidaMovimiento (
    IdPartida INT NOT NULL,
    IdMovimiento INT NOT NULL,
    PRIMARY KEY (IdPartida, IdMovimiento),
    CONSTRAINT FK_PM_Partida
        FOREIGN KEY (IdPartida) REFERENCES Partida(IdPartida),
    CONSTRAINT FK_PM_Movimiento
        FOREIGN KEY (IdMovimiento) REFERENCES Movimientos(IdMovimiento)
) ENGINE=InnoDB;


-- =====================
-- RELLENO DE DATOS
-- =====================
INSERT INTO Tier (Titulo) VALUES
('Bronce'),
('Plata'),
('Oro');

INSERT INTO Usuario (Nombre, Correo, Password, IdTier) VALUES
('Alice', 'alice@mail.com', 'aaa', 1),
('Bob', 'bob@mail.com', 'bbb', 2),
('Charlie', 'charlie@mail.com', 'ccc', 3);

INSERT INTO Tropa (Nombre, Vida, Dano) VALUES
('Cuadrado', 100, 50),
('Triangulo', 100, 50),
('Circulo', 100, 50);

INSERT INTO Nodo (DuenoNodo) VALUES
(1), -- Alice
(2), -- Bob
(NULL); -- Nodo neutral

INSERT INTO Partida (Ganador) VALUES
(1); -- Alice gana

INSERT INTO PartidaUsuario (IdPartida, IdUsuario1, IdUsuario2) VALUES
(1, 1, 2);


INSERT INTO Movimientos (IdTropa, NodoOrigen, NodoDestino) VALUES
(1, 1, 2),
(2, 2, 3);

INSERT INTO PartidaMovimiento (IdPartida, IdMovimiento) VALUES
(1, 1),
(1, 2);
