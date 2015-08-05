-- ѡ�� OptimalControl ���ݿ�
Use OptimalControl
Go

-- �ж��Ƿ���� ��Variable����������ڣ���ɾ���� Variable
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'Variable'))
	Drop Table Variable
Go 

-- ���� ��Variable��
Create Table [Variable]
(
	-- �����У��Զ����� ��ʶ����Ϊ 1 
	[Id] int identity(1,1) Constraint [PK_VariableId] Primary Key,
	
	-- ��������
	[Code] Nvarchar(16) Not Null,

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
	[DeviceID] int Not Null,

	-- �Ƿ񱣴����
	[IsEnabled] bit Not Null,

	-- �Ƿ��˲�
	[IsOutput] bit Not Null,

	-- �Ƿ���ʾ����
	[IsDisplayed] bit Not Null,
	
	-- �Ƿ񱣴����
	[IsSaved] bit Not Null,

	-- �Ƿ��˲�
	[IsFiltered] bit Not Null,

	-- ��ʷ���ݳ���
	[HistoryListLength] int Not Null,

	-- ���Ƽ������
	[TrendLength] int Not Null,
	
	-- ���Ƽ�����
	[TrendInterval] int Not Null,
	
	-- �����ж�����
	[TrendHigherLimit] real Not Null,
	
	-- �����ж�����
	[TrendLowerLimit] real Not Null,

	-- �����жϵ���
	[TrendListLength] int Not Null,
)
Go
