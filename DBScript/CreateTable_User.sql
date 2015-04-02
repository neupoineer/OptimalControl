-- ѡ�� OptimalControl ���ݿ�
Use OptimalControl
Go

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RightsGroupId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RightsRelation]'))
ALTER TABLE [dbo].[RightsRelation] DROP CONSTRAINT [FK_RightsGroupId]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OperatorId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RightsRelation]'))
ALTER TABLE [dbo].[RightsRelation] DROP CONSTRAINT [FK_OperatorId]

-- �ж��Ƿ���� ����Ա��Ϣ��Operator����������ڣ���ɾ���� Operator
IF EXISTS(SELECT * FROM SysObjects WHERE Id = OBJECT_ID(N'Operator'))
	Drop Table [Operator]
Go 

-- ���� ����Ա��Ϣ��Operator��
Create Table [Operator]
(
	-- �����У��Զ����� ��ʶ����Ϊ 1 
	[Id] int identity(1,1) Constraint [PK_OperatorId] Primary Key,

	-- ����Ա����
	[OperatorName] nVarChar(50) Constraint [UQ_OperatorName] Unique(OperatorName) Not Null,

	-- ����
	[Password] nVarChar(50) Constraint [CK_Password] Check(len([Password])>=6) Not Null,

	-- ����ԱȨ���б�
	[RightsList] varBinary(max) Null,

	-- �û���ǰ״̬
	[State] bit Constraint [DF_State] Default('true') Constraint [CK_State] Check([State] in ('true','false')) Not Null
)
Go

-- �ж��Ƿ���� Ȩ������Ϣ��RightsGroup����������ڣ���ɾ���� RightsGroup
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'RightsGroup'))
	Drop Table [RightsGroup]
Go 

-- ���� Ȩ������Ϣ��RightsGroup��
Create Table [RightsGroup]
(
	-- �����У��Զ����� ��ʶ����Ϊ 1 
	[Id] int Identity(1,1) Constraint [PK_RightsGroupId] Primary Key,
	
	-- Ȩ��������
	[GroupName] nVarChar(50) Constraint[UQ_GroupName] Unique (GroupName) Not Null,

	-- ��Ȩ���б�
	[GroupRightsList] varBinary(max) Null
)
Go

-- �ж��Ƿ����Ȩ�޹�ϵ��RightsRelation����������ڣ���ɾ���� RightsRelation
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'RightsRelation'))
	drop table [RightsRelation]
Go

-- ���� Ȩ�޹�ϵ��RightsRelation��
Create Table [RightsRelation]
(
	-- �����У��Զ����� ��ʶ����Ϊ 1 
	[Id] int Identity(1, 1) Constraint [PK_RightsRelationId] Primary Key,

	-- ����Ա Id
	[OperatorId] int Constraint [FK_OperatorId]	Foreign Key References Operator([Id]) Not Null,
	
	-- Ȩ���� Id
	[RightsGroupId] int Constraint [FK_RightsGroupId] Foreign Key References RightsGroup([Id]) Not Null
)
Go