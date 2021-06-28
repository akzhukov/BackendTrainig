INSERT [dbo].[Factory] ([Name], [Description]) VALUES (N'МНПЗ', N'Московский нефтеперерабатывающий завод')
INSERT [dbo].[Factory] ([Name], [Description]) VALUES (N'ОНПЗ', N'Омский нефтеперерабатывающий завод')

INSERT [dbo].[Unit] ([Name], [FactoryId]) VALUES (N'ГФУ-1', 1)
INSERT [dbo].[Unit] ([Name], [FactoryId]) VALUES (N'ГФУ-2', 1)
INSERT [dbo].[Unit] ([Name], [FactoryId]) VALUES (N'Установка АВТ-6', 2)

INSERT [dbo].[Tank] ([Name], [Volume], [MaxVolume], [UnitId]) VALUES (N'Резервуар 1', 1500, 2000, 1)
INSERT [dbo].[Tank] ([Name], [Volume], [MaxVolume], [UnitId]) VALUES (N'Резервуар 2', 2500, 3000, 1)
INSERT [dbo].[Tank] ([Name], [Volume], [MaxVolume], [UnitId]) VALUES (N'Доп. резервуар 24', 3000, 3000, 2)
INSERT [dbo].[Tank] ([Name], [Volume], [MaxVolume], [UnitId]) VALUES (N'Резервуар 35', 3000, 3000, 2)
INSERT [dbo].[Tank] ([Name], [Volume], [MaxVolume], [UnitId]) VALUES (N'Резервуар 47', 4000, 5000, 2)
INSERT [dbo].[Tank] ([Name], [Volume], [MaxVolume], [UnitId]) VALUES (N'Резервуар 256', 500, 500, 3)
