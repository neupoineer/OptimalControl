-- 选择 OptimalControl 数据库
Use OptimalControl
Go

-- 判断是否存在 规则信息表（Rules），如果存在，则删除表 Rules
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'Rules'))
	Drop Table Rules
Go 

-- 创建 规则信息表（Rules）
Create Table Rules
(
	-- 主键列，自动增长 标识种子为 1 
	[Id] int identity(1,1) Constraint [PK_RuleId] Primary Key,

	-- 规则名
	[Name] Nvarchar(50) Not Null,

	-- 表达式
	[Expression] Nvarchar(1000) Not Null,

	-- 动作
	[Operation] Nvarchar(1000) Not Null,

	-- 控制周期
	[Period] int,
	
	-- 使能
	[State] bit Not Null,
)
Go