INSERT [dbo].[Factory] ([Name], [Description]) VALUES (N'����', N'���������� ��������������������� �����')
INSERT [dbo].[Factory] ([Name], [Description]) VALUES (N'����', N'������ ��������������������� �����')

INSERT [dbo].[Unit] ([Name], [FactoryId]) VALUES (N'���-1', 1)
INSERT [dbo].[Unit] ([Name], [FactoryId]) VALUES (N'���-2', 1)
INSERT [dbo].[Unit] ([Name], [FactoryId]) VALUES (N'��������� ���-6', 2)

INSERT [dbo].[Tank] ([Name], [Volume], [MaxVolume], [UnitId]) VALUES (N'��������� 1', 1500, 2000, 1)
INSERT [dbo].[Tank] ([Name], [Volume], [MaxVolume], [UnitId]) VALUES (N'��������� 2', 2500, 3000, 1)
INSERT [dbo].[Tank] ([Name], [Volume], [MaxVolume], [UnitId]) VALUES (N'���. ��������� 24', 3000, 3000, 2)
INSERT [dbo].[Tank] ([Name], [Volume], [MaxVolume], [UnitId]) VALUES (N'��������� 35', 3000, 3000, 2)
INSERT [dbo].[Tank] ([Name], [Volume], [MaxVolume], [UnitId]) VALUES (N'��������� 47', 4000, 5000, 2)
INSERT [dbo].[Tank] ([Name], [Volume], [MaxVolume], [UnitId]) VALUES (N'��������� 256', 500, 500, 3)
