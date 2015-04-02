-- 选择 Master 数据库
Use [Master]
Go

-- 校验 OptimalControl 数据库是否存在，若已存在则删除
If Exists(Select * From SysDataBases Where Name = 'OptimalControl')
	Drop DataBase OptimalControl
Go

-- 创建 OptimalControl 数据库
Create DataBase OptimalControl
On
(
	Name = 'OptimalControl',
	FileName = 'E:\Library\Database\MSSQLSERVER\OptimalControl.mdf',
	Size = 5mb,
	FileGrowth = 1mb
)
Log On
(
	Name = 'OptimalControl_log',
	FileName = 'E:\Library\Database\MSSQLSERVER\OptimalControl.ldf',
	Size = 1mb,
	FileGrowth = 1mb
)
Go