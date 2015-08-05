-- 选择 OptimalControl 数据库
Use OptimalControl
Go

-- 判断是否存在 表（Device），如果存在，则删除表 Device
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'Device'))
	Drop Table [Device]
Go 

-- 创建 表（Device）
Create Table [Device]
(
	-- 主键列，自动增长 标识种子为 1 
	[Id] int identity(1,1) Constraint [PK_DeviceId] Primary Key,

	-- 设备名
	[Name] Nvarchar(50) Not Null,
	
	-- 设备状态
	[State] bit Not Null,
	
	-- 设备状态
	[SyncState] bit Not Null,
	
	-- 设备IP地址
	[IP] Nvarchar(15) Not Null,
	
	-- 设备端口号
	[Port] int Not Null,
	
	-- 设备从站地址号
	[UnitID] tinyint Not Null
)
Go
