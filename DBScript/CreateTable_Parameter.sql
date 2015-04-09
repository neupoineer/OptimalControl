-- ѡ�� OptimalControl ���ݿ�
Use OptimalControl
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

	-- �Ŵ����
	[Ratio] real Not Null,

	--������
	[UpperLimit] real,
	[LowerLimit] real,
	[UltimateUpperLimit] real,
	[UltimateLowerLimit] real,

	--��������
	[ControlPeriod] int,

	--������ʱ
	[OperateDelay] int,

	-- �豸ID
	[DeviceID] int Not Null
)
Go
