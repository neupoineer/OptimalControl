-- 选择 OptimalControl 数据库
Use [OptimalControl]
Go

INSERT INTO [Device] ([Name],[State],[SyncState],[IP],[Port],[UnitID])
     VALUES
           ('管理端','true','true','127.0.0.1','501','1'),
           ('负荷监测设备','true','true','127.0.0.1','502','2')
GO