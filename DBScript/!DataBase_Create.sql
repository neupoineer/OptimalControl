-- ѡ�� Master ���ݿ�
Use [Master]
Go

-- У�� OptimalControl ���ݿ��Ƿ���ڣ����Ѵ�����ɾ��
If Exists(Select * From SysDataBases Where Name = 'OptimalControl')
	Drop DataBase OptimalControl
Go

-- ���� OptimalControl ���ݿ�
Create DataBase OptimalControl
On
(
	Name = 'OptimalControl',
	FileName = 'E:\Library\Database\MSSQLSERVER\OptimalControl.mdf',
	Size = 5mb,
	FileGrowth = 1mb
)
Log On
(
	Name = 'OptimalControl_log',
	FileName = 'E:\Library\Database\MSSQLSERVER\OptimalControl.ldf',
	Size = 1mb,
	FileGrowth = 1mb
)
Go