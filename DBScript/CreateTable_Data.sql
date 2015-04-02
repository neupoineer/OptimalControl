-- ѡ�� OptimalControl ���ݿ�
Use OptimalControl
Go

-- �ж��Ƿ���� ����Data����������ڣ���ɾ���� Data
if exists(Select * From SysObjects Where Id = OBJECT_ID(N'Data'))
	Drop Table [Data]
Go 

-- ���� ����Data��
Create Table [Data]
(
	-- �����У��Զ����� ��ʶ����Ϊ 1 
	[Id] int identity(1,1) Constraint [PK_DataId] Primary Key,

	-- ʱ��
	[Time] datetime Not Null,

	-- ������
	[ParameterName] Nvarchar(50) Not Null,

	-- ����ֵ
	[Value] real Not Null,

	-- �豸ID
	[DeviceID] int Not Null
)
Go