IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblArtista]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[tblArtista](
		IdArtista BIGINT IDENTITY(1,1) NOT NULL,
		Nombre VARCHAR(150) NOT NULL,
		Pais VARCHAR(150) NOT NULL,
		CasaDisquera VARCHAR(150) NOT NULL
	 CONSTRAINT [PK_tblArtista] PRIMARY KEY CLUSTERED 
	(
		[IdArtista] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 95, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END