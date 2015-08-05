-- ѡ�� OptimalControl ���ݿ�
Use OptimalControl
Go

-- �ж��Ƿ���� ��Log����������ڣ���ɾ���� Log
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'Log'))
	Drop Table Log
Go 

-- ���� ��Log��
Create Table Log
(
	-- �����У��Զ����� ��ʶ����Ϊ 1 
	[Id] int identity(1,1) Constraint [PK_LogId] Primary Key,

	-- �豸��
	[LogTime] datetime Not Null,
	
	-- �豸״̬
	[Type] int Not Null,
	
	-- �豸״̬
	[Content] Nvarchar(500) Not Null,
	
	-- �豸IP��ַ
	[State] bit Not Null,
)
Go