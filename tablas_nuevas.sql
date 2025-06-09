CREATE TABLE Centrocosto(
	idCentrocosto int IDENTITY(1,1) NOT NULL,
	descripcion varchar(50) NULL,
	esActivo bit NULL,
	fechaRegistro [datetime] NULL)
	go

	CREATE TABLE Area(
	idArea int IDENTITY(1,1) NOT NULL,
	descripcion varchar(50) NULL,
	esActivo bit NULL,
	fechaRegistro [datetime] NULL)
	go

		create TABLE TipoCambio(
	idArea int IDENTITY(1,1) NOT NULL,
	FecTC varchar(10) NOT NULL,
	Cd_Mda varchar(2) NULL,
	TCCom numeric (13,3),
	TCVta numeric (13,3),
	TCPro numeric (13,3),
	idUsuario int references Usuario(idUsuario)
	)
	go



