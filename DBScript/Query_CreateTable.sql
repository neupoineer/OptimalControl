-- 选择 OptimalControl 数据库
Use OptimalControl
Go

-- 判断是否存在 曲线信息表（Query），如果存在，则删除表 Query
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'Query'))
	Drop Table Query
Go 

-- 创建 曲线信息表（Query）
Create Table Query
(
	-- 主键列，自动增长 标识种子为 1 
	[Id] int identity(1,1) Constraint [PK_QueryId] Primary Key,

	-- 变量编码
	[VariableCode] Nvarchar(16) Not Null,
	
	-- 名称
	[VariableName] Nvarchar(50) Not Null,
)
Go