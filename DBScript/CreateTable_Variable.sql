-- 选择 OptimalControl 数据库
Use OptimalControl
Go

-- 判断是否存在 表（Variable），如果存在，则删除表 Variable
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'Variable'))
	Drop Table Variable
Go 

-- 创建 表（Variable）
Create Table [Variable]
(
	-- 主键列，自动增长 标识种子为 1 
	[Id] int identity(1,1) Constraint [PK_VariableId] Primary Key,
	
	-- 参数编码
	[Code] Nvarchar(16) Not Null,

	-- 参数名
	[Name] Nvarchar(50) Not Null,

	-- 参数地址
	[Address] int Not Null,

	-- 放大比例
	[Ratio] real Not Null,

	--控制限
	[UpperLimit] real,
	[LowerLimit] real,
	[UltimateUpperLimit] real,
	[UltimateLowerLimit] real,

	--控制周期
	[ControlPeriod] int,

	--动作延时
	[OperateDelay] int,

	-- 设备ID
	[DeviceID] int Not Null,

	-- 显示变量
	[IsDisplayed] bit Not Null,
)
Go
