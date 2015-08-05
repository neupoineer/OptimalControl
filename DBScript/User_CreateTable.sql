-- 选择 OptimalControl 数据库
Use OptimalControl
Go

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RightsGroupId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RightsRelation]'))
ALTER TABLE [dbo].[RightsRelation] DROP CONSTRAINT [FK_RightsGroupId]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OperatorId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RightsRelation]'))
ALTER TABLE [dbo].[RightsRelation] DROP CONSTRAINT [FK_OperatorId]

-- 判断是否存在 操作员信息表（Operator），如果存在，则删除表 Operator
IF EXISTS(SELECT * FROM SysObjects WHERE Id = OBJECT_ID(N'Operator'))
	Drop Table [Operator]
Go 

-- 创建 操作员信息表（Operator）
Create Table [Operator]
(
	-- 主键列，自动增长 标识种子为 1 
	[Id] int identity(1,1) Constraint [PK_OperatorId] Primary Key,

	-- 操作员姓名
	[OperatorName] nVarChar(50) Constraint [UQ_OperatorName] Unique(OperatorName) Not Null,

	-- 密码
	[Password] nVarChar(50) Constraint [CK_Password] Check(len([Password])>=6) Not Null,

	-- 操作员权限列表
	[RightsList] varBinary(max) Null,

	-- 用户当前状态
	[State] bit Constraint [DF_State] Default('true') Constraint [CK_State] Check([State] in ('true','false')) Not Null
)
Go

-- 判断是否存在 权限组信息表（RightsGroup），如果存在，则删除表 RightsGroup
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'RightsGroup'))
	Drop Table [RightsGroup]
Go 

-- 创建 权限组信息表（RightsGroup）
Create Table [RightsGroup]
(
	-- 主键列，自动增长 标识种子为 1 
	[Id] int Identity(1,1) Constraint [PK_RightsGroupId] Primary Key,
	
	-- 权限组名称
	[GroupName] nVarChar(50) Constraint[UQ_GroupName] Unique (GroupName) Not Null,

	-- 组权限列表
	[GroupRightsList] varBinary(max) Null
)
Go

-- 判断是否存在权限关系表（RightsRelation），如果存在，则删除表 RightsRelation
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'RightsRelation'))
	drop table [RightsRelation]
Go

-- 创建 权限关系表（RightsRelation）
Create Table [RightsRelation]
(
	-- 主键列，自动增长 标识种子为 1 
	[Id] int Identity(1, 1) Constraint [PK_RightsRelationId] Primary Key,

	-- 操作员 Id
	[OperatorId] int Constraint [FK_OperatorId]	Foreign Key References Operator([Id]) Not Null,
	
	-- 权限组 Id
	[RightsGroupId] int Constraint [FK_RightsGroupId] Foreign Key References RightsGroup([Id]) Not Null
)
Go