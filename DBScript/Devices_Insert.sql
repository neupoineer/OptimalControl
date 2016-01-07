-- 选择 OptimalControl 数据库
Use [OptimalControl]
Go

INSERT INTO [Device] ([Name],[State],[SyncState],[IP],[Port],[UnitID])
     VALUES
           ('管理端','true','true','127.0.0.1','501','1'),
           ('半自磨负荷监测','true','true','192.168.40.40','502','2'),
		   ('球磨机负荷监测','false','false','192.168.40.41','502','3'),
		   ('仿真模型','true','true','127.0.0.1','503','4')
GO