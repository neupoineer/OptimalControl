-- 选择 OptimalControl 数据库
Use OptimalControl
Go

-- 判断是否存在 表（Data），如果存在，则删除表 Data
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'Data'))
	Drop Table [Data]
Go 

-- 创建 表（Data）
Create Table [Data]
(
	-- 主键列，自动增长 标识种子为 1 
	[Id] int identity(1,1) Constraint [PK_DataId] Primary Key,

	-- 参数编码
	[VariableCode] Nvarchar(16) Not Null,

	-- 时间
	[TimeValue] datetime Not Null,

	-- 参数值
	[Value] real Not Null,

	-- 设备ID
	[DeviceID] int Not Null
)
Go
