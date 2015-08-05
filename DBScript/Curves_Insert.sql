-- 选择 OptimalControl 数据库
Use [OptimalControl]
Go

INSERT INTO [Curve]([VariableCode],[Name],[DeviceID],[Address],[LineColor],[LineType],[LineWidth],[SymbolType],[SymbolSize],[XTitle],[YTitle],[YMax],[YMin])
     VALUES
			('CS010100020201','半自磨给矿量设定值','0','101',null,'true',null,null,null,'时间/(秒)','半自磨给矿量设定值','0','0'),
			('CS010100020101','半自磨原矿量','0','103',null,'true',null,null,null,'时间/(秒)','半自磨原矿量','0','0'),
			('CS010100020102','半自磨总矿量','0','105',null,'true',null,null,null,'时间/(秒)','半自磨总矿量','0','0'),
			('CS040100030171','7A#皮带电流','0','109',null,'true',null,null,null,'时间/(秒)','7A#皮带电流','0','0'),
			('CS040100030101','半自磨功率(左)','0','117',null,'true',null,null,null,'时间/(秒)','半自磨功率(左)','0','0'),
			('CS060100030102','半自磨出料端压力','0','125',null,'true',null,null,null,'时间/(秒)','半自磨出料端压力','0','0'),
			('CS060100030104','半自磨负荷','2','1',null,'true',null,null,null,'时间/(秒)','半自磨负荷','0','0'),
			('CS010100060101','半自磨顽石产生量','0','107',null,'true',null,null,null,'时间/(秒)','半自磨顽石产生量','0','0'),
			('CS040100020201','半自磨给水量设定值','0','113',null,'true',null,null,null,'时间/(秒)','半自磨给水量设定值','0','0'),
			('CS040100020101','半自磨给水量','0','115',null,'true',null,null,null,'时间/(秒)','半自磨给水量','0','0'),
			('CS040200020201','磨矿泵池补加水设定值','0','127',null,'true',null,null,null,'时间/(秒)','磨矿泵池补加水设定值','0','0'),
			('CS040200020101','磨矿泵池补加水量','0','129',null,'true',null,null,null,'时间/(秒)','磨矿泵池补加水量','0','0')
GO
