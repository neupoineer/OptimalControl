-- ѡ�� OptimalControl ���ݿ�
Use OptimalControl
Go

-- �ж��Ƿ���� ��Devices����������ڣ���ɾ���� Devices
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'Devices'))
	Drop Table [Devices]
Go 

-- ���� ��Devices��
Create Table [Devices]
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

-- �ж��Ƿ���� ��Parameters����������ڣ���ɾ���� Parameters
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'Parameters'))
	Drop Table [Parameters]
Go 

-- ���� ��Parameters��
Create Table [Parameters]
(
	-- �����У��Զ����� ��ʶ����Ϊ 1 
	[Id] int identity(1,1) Constraint [PK_ParameterId] Primary Key,

	-- ������
	[Name] Nvarchar(50) Not Null,

	-- ������ַ
	[Address] int Not Null,

	-- ��������
	[Ratio] real Not Null,

	-- �豸ID
	[DeviceID] int Not Null
)
Go
