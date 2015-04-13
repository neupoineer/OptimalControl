-- ѡ�� OptimalControl ���ݿ�
Use OptimalControl
Go

-- �ж��Ƿ���� ��Device����������ڣ���ɾ���� Device
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'Device'))
	Drop Table [Device]
Go 

-- ���� ��Device��
Create Table [Device]
(
	-- �����У��Զ����� ��ʶ����Ϊ 1 
	[Id] int identity(1,1) Constraint [PK_DeviceId] Primary Key,

	-- �豸��
	[Name] Nvarchar(50) Not Null,
	
	-- �豸״̬
	[State] bit Not Null,
	
	-- �豸״̬
	[SyncState] bit Not Null,
	
	-- �豸IP��ַ
	[IP] Nvarchar(15) Not Null,
	
	-- �豸�˿ں�
	[Port] int Not Null,
	
	-- �豸��վ��ַ��
	[UnitID] tinyint Not Null
)
Go
