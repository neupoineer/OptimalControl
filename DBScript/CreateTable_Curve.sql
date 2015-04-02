-- 选择 OptimalControl 数据库
Use OptimalControl
Go

-- 判断是否存在 曲线信息表（Curves），如果存在，则删除表 Curves
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'Curves'))
	Drop Table Curves
Go 

-- 创建 曲线信息表（Curves）
Create Table Curves
(
	-- 主键列，自动增长 标识种子为 1 
	[Id] int identity(1,1) Constraint [PK_CurveId] Primary Key,

	-- 曲线名
	[Name] Nvarchar(50) Not Null,

	-- 设备ID
	[DeviceID] int Not Null,

	-- 参数地址
	[Address] int Not Null,

	-- 曲线颜色
	[LineColor] Nvarchar(50),
	
	-- 曲线类型
	[LineType] bit,
	
	-- 曲线宽度
	[LineWidth] real,
	
	-- 符号类型
	[SymbolType] Nvarchar(30),
	
	-- 符号大小
	[SymbolSize] real,
	
	-- X轴名称
	[XTitle] Nvarchar(50) Not Null,
	
	-- Y轴名称
	[YTitle] Nvarchar(50) Not Null,
	
	-- Y轴最大值
	[YMax] real Not Null,
	
	-- Y轴最小值
	[YMin] real Not Null
)
Go