-- 选择 OptimalControl 数据库
Use OptimalControl
Go

-- 判断是否存在 表（Log），如果存在，则删除表 Log
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'Log'))
	Drop Table Log
Go 

-- 创建 表（Log）
Create Table Log
(
	-- 主键列，自动增长 标识种子为 1 
	[Id] int identity(1,1) Constraint [PK_LogId] Primary Key,

	-- 设备名
	[LogTime] datetime Not Null,
	
	-- 设备状态
	[Type] int Not Null,
	
	-- 设备状态
	[Content] Nvarchar(500) Not Null,
	
	-- 设备IP地址
	[State] bit Not Null,
)
Go