-- ѡ�� OptimalControl ���ݿ�
Use OptimalControl
Go

-- �ж��Ƿ���� ������Ϣ��Rules����������ڣ���ɾ���� Rules
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'Rules'))
	Drop Table Rules
Go 

-- ���� ������Ϣ��Rules��
Create Table Rules
(
	-- �����У��Զ����� ��ʶ����Ϊ 1 
	[Id] int identity(1,1) Constraint [PK_RuleId] Primary Key,

	-- ������
	[Name] Nvarchar(50) Not Null,

	-- ���ʽ
	[Expression] Nvarchar(1000) Not Null,

	-- ����
	[Operation] Nvarchar(1000) Not Null,

	-- ��������
	[Period] int,
	
	-- ʹ��
	[State] bit Not Null,
)
Go