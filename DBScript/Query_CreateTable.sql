-- ѡ�� OptimalControl ���ݿ�
Use OptimalControl
Go

-- �ж��Ƿ���� ������Ϣ��Query����������ڣ���ɾ���� Query
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'Query'))
	Drop Table Query
Go 

-- ���� ������Ϣ��Query��
Create Table Query
(
	-- �����У��Զ����� ��ʶ����Ϊ 1 
	[Id] int identity(1,1) Constraint [PK_QueryId] Primary Key,

	-- ��������
	[VariableCode] Nvarchar(16) Not Null,
	
	-- ����
	[VariableName] Nvarchar(50) Not Null,
)
Go