USE [inmobiliria]
GO

INSERT INTO [dbo].[tbl_tpinmu]
           (tpi_id,[tpi_nombre])
     VALUES
           (1,'Pisos nuevos'),
		   (2,'Casas'),
		   (3,'Locales'),
		   (4,'Apartamentos'),
		   (5,'Oficinas');
GO

INSERT INTO [dbo].[tbl_tptran]
           (tpt_id,[tpt_nombre])
     VALUES
           (1,'Venta'),
		   (2,'Alquiler');
GO

INSERT INTO [dbo].[tbl_estado]
           (est_id,[est_nombre])
     VALUES
           (1,'Activo'),
		   (2,'Eliminado');
GO
INSERT INTO [dbo].[tbl_sucurs]
           ([est_id]
           ,[suc_nombre]
           ,[suc_direcc]
           ,[suc_telefo])
     VALUES
           (1,'Manizales','calle 23 # 3-45','8873456'),
		   (1,'Pereira','Av 5 # 3 - 56','8965743'),
		   (1,'Armenia','Barrio el bosque','3112345456');
   
GO

