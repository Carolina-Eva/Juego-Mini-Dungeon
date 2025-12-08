USE [Sokoban]
GO
/****** Object:  Table [dbo].[Puntaje]    Script Date: 8/12/2025 19:11:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Puntaje](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[Puntaje] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 8/12/2025 19:11:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[PasswordHash] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Puntaje] ON 
GO
INSERT [dbo].[Puntaje] ([Id], [UsuarioId], [Puntaje], [Fecha]) VALUES (1, 1, 1, CAST(N'2025-12-08T18:17:26.217' AS DateTime))
GO
INSERT [dbo].[Puntaje] ([Id], [UsuarioId], [Puntaje], [Fecha]) VALUES (2, 1, 1, CAST(N'2025-12-08T18:22:29.420' AS DateTime))
GO
INSERT [dbo].[Puntaje] ([Id], [UsuarioId], [Puntaje], [Fecha]) VALUES (3, 1, 1, CAST(N'2025-12-08T18:25:58.913' AS DateTime))
GO
INSERT [dbo].[Puntaje] ([Id], [UsuarioId], [Puntaje], [Fecha]) VALUES (4, 1, 1, CAST(N'2025-12-08T19:08:10.330' AS DateTime))
GO
INSERT [dbo].[Puntaje] ([Id], [UsuarioId], [Puntaje], [Fecha]) VALUES (5, 1, 1, CAST(N'2025-12-08T19:10:35.583' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Puntaje] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 
GO
INSERT [dbo].[Usuario] ([Id], [Username], [PasswordHash]) VALUES (1, N'prueba', N'A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ=')
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Usuario__536C85E4FD4743CC]    Script Date: 8/12/2025 19:11:17 ******/
ALTER TABLE [dbo].[Usuario] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Puntaje] ADD  DEFAULT (getdate()) FOR [Fecha]
GO
ALTER TABLE [dbo].[Puntaje]  WITH CHECK ADD FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuario] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[GUARDAR_PUNTAJE]    Script Date: 8/12/2025 19:11:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[GUARDAR_PUNTAJE]
    @UsuarioId INT,
    @Puntaje INT,
    @Fecha DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Puntaje (UsuarioId, Puntaje, Fecha)
    VALUES (@UsuarioId, @Puntaje, @Fecha);

    SELECT TOP 1 *
    FROM Puntaje
    WHERE UsuarioId = @UsuarioId
    ORDER BY Id DESC;
END
GO
/****** Object:  StoredProcedure [dbo].[OBTENER_PUNTAJE]    Script Date: 8/12/2025 19:11:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[OBTENER_PUNTAJE]
    @UsuarioId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ISNULL(SUM(Puntaje), 0) AS PuntajeTotal
    FROM Puntaje
    WHERE UsuarioId = @UsuarioId;
END
GO
/****** Object:  StoredProcedure [dbo].[OBTENER_USUARIO_ID]    Script Date: 8/12/2025 19:11:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[OBTENER_USUARIO_ID]
    @Nombre NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id 
    FROM Usuario
    WHERE Username = @Nombre;
END
GO
/****** Object:  StoredProcedure [dbo].[VALIDAR_USUARIO]    Script Date: 8/12/2025 19:11:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[VALIDAR_USUARIO]
    @Username NVARCHAR(50),
    @PasswordHash NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) 
    FROM Usuario 
    WHERE Username = @Username 
      AND PasswordHash = @PasswordHash;
END
GO
USE [master]
GO
ALTER DATABASE [Sokoban] SET  READ_WRITE 
GO
