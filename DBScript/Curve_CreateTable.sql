-- ѡ�� OptimalControl ���ݿ�
Use OptimalControl
Go

-- �ж��Ƿ���� ������Ϣ����Curve����������ڣ���ɾ���� Curve
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'Curve'))
	Drop Table Curve
Go 

-- ���� ������Ϣ����Curve��
Create Table Curve
(
	-- �����У��Զ����� ��ʶ����Ϊ 1 
	[Id] int identity(1,1) Constraint [PK_CurveId] Primary Key,

	-- ��������
	[VariableCode] Nvarchar(16) Not Null,
	
	-- ������
	[Name] Nvarchar(50) Not Null,

	-- �豸ID
	[DeviceID] int Not Null,

	-- ������ַ
	[Address] int Not Null,

	-- ������ɫ
	[LineColor] Nvarchar(50),
	
	-- ��������
	[LineType] bit,
	
	-- ���߿���
	[LineWidth] real,
	
	-- ��������
	[SymbolType] Nvarchar(30),
	
	-- ���Ŵ�С
	[SymbolSize] real,
	
	-- X������
	[XTitle] Nvarchar(50) Not Null,
	
	-- Y������
	[YTitle] Nvarchar(50) Not Null,
	
	-- Y�����ֵ
	[YMax] real Not Null,
	
	-- Y����Сֵ
	[YMin] real Not Null
)
Go