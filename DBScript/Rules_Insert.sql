-- 选择 OptimalControl 数据库
Use [OptimalControl]
Go

INSERT INTO [Rules]([Name],[Expression],[Operation],[Period],[State],[Priority],[Type],[IsLogged])
     VALUES
('半自磨功率求和','1','[@半自磨功率]=[@半自磨1#电机功率]+[@半自磨2#电机功率]','0','true','0','true','false'),
('球磨机功率求和','1','[@球磨机功率]=[@球磨机1#电机功率]+[@球磨机2#电机功率]','0','true','0','true','false'),
('半自磨电量单耗','1','[@半自磨电量单耗]=([@半自磨1#电机功率]+[@半自磨2#电机功率])/[@半自磨原矿量]','0','true','0','true','false'),
('半自磨功率保护','([@半自磨1#电机功率]+[@半自磨2#电机功率])>11000','[@优化软件使能]=0','90','true','1','true','true'),
('半自磨轴压保护','([@半自磨进料端压力]>6)|([@半自磨出料端压力]>6)','[@优化软件使能]=0','90','true','1','true','true'),
('半自磨循环负荷保护','[@半自磨顽石产生量]>300','[@优化软件使能]=0','90','true','1','true','true'),
('顽石破碎机启动','([@7A#皮带电流]-[@7A#皮带电流.HistoryValue])>3','[@给矿量优化设定值]=[@半自磨给矿量设定值]-50','180','true','2','false','true'),
('顽石破碎机停止','([@7A#皮带电流.HistoryValue]-[@7A#皮带电流])>3','[@给矿量优化设定值]=[@半自磨给矿量设定值]+50','180','true','2','false','true'),
('稳定','([@半自磨出料端压力.Trend]=0)&([@半自磨功率.Trend]=0)&([@半自磨顽石产生量.Trend]=0)','[@半自磨工作状态]=0','5','true','3','false','false'),
('过载1','([@半自磨出料端压力.Trend]>0)&([@半自磨出料端压力.State]>0)&([@半自磨功率.Trend]<0)&([@半自磨功率.State]>(0-1))&([@半自磨顽石产生量.Trend]>0)','[@给矿量优化设定值]=[@半自磨给矿量设定值]-100;[@前水量优化设定值]=[@半自磨给水量设定值]+10;[@半自磨工作状态]=2','180','true','3','false','true'),
('过载2','([@半自磨出料端压力.Trend]>0)&([@半自磨出料端压力.State]>0)&([@半自磨功率.Trend]<0)&([@半自磨功率.State]>(0-1))&([@半自磨顽石产生量.Trend]=0)','[@给矿量优化设定值]=[@半自磨给矿量设定值]-100;[@前水量优化设定值]=[@半自磨给水量设定值]+20;[@半自磨工作状态]=2','180','true','3','false','true'),
('过载3','([@半自磨出料端压力.Trend]>0)&([@半自磨出料端压力.State]>0)&([@半自磨功率.Trend]<0)&([@半自磨功率.State]>(0-1))&([@半自磨顽石产生量.Trend]<0)&([@半自磨顽石产生量.State]>(0-1))','[@给矿量优化设定值]=[@半自磨给矿量设定值]-125;[@前水量优化设定值]=[@半自磨给水量设定值]+20;[@半自磨工作状态]=2','180','true','3','false','true'),
('过载4','([@半自磨出料端压力.Trend]>0)&([@半自磨出料端压力.State]>0)&([@半自磨功率.Trend]<0)&([@半自磨功率.State]>(0-1))&([@半自磨顽石产生量.Trend]<0)&([@半自磨顽石产生量.State]<0)','[@给矿量优化设定值]=[@半自磨给矿量设定值]-125;[@前水量优化设定值]=[@半自磨给水量设定值]+20;[@半自磨工作状态]=2','180','true','3','false','true'),
('过载趋势1','([@半自磨出料端压力.Trend]>0)&([@半自磨出料端压力.State]>(0-1))&([@半自磨功率.Trend]=0)&([@半自磨功率.State]>0)&([@半自磨顽石产生量.Trend]>0)&([@半自磨顽石产生量.State]>(0-1))','[@给矿量优化设定值]=[@半自磨给矿量设定值]-50;[@前水量优化设定值]=[@半自磨给水量设定值]+10;[@半自磨工作状态]=1','180','true','3','false','true'),
('过载趋势2','([@半自磨出料端压力.Trend]>0)&([@半自磨出料端压力.State]>(0-1))&([@半自磨功率.Trend]=0)&([@半自磨功率.State]>0)&([@半自磨顽石产生量.Trend]=0)&([@半自磨顽石产生量.State]>(0-1))','[@给矿量优化设定值]=[@半自磨给矿量设定值]-50;[@前水量优化设定值]=[@半自磨给水量设定值]+20;[@半自磨工作状态]=1','180','true','3','false','true'),
('过载趋势3','([@半自磨出料端压力.Trend]>0)&([@半自磨出料端压力.State]>(0-1))&([@半自磨功率.Trend]=0)&([@半自磨功率.State]>0)&([@半自磨顽石产生量.Trend]<0)&([@半自磨顽石产生量.State]>(0-1))','[@给矿量优化设定值]=[@半自磨给矿量设定值]-75;[@前水量优化设定值]=[@半自磨给水量设定值]+20;[@半自磨工作状态]=1','180','true','3','false','true'),
('过载趋势4','([@半自磨出料端压力.Trend]>0)&([@半自磨出料端压力.State]>(0-1))&([@半自磨功率.Trend]=0)&([@半自磨功率.State]>0)&([@半自磨顽石产生量.Trend]<0)&([@半自磨顽石产生量.State]<0)','[@给矿量优化设定值]=[@半自磨给矿量设定值]-75;[@前水量优化设定值]=[@半自磨给水量设定值]+30;[@半自磨工作状态]=1','180','true','3','false','true'),
('过载趋势5','([@半自磨出料端压力.Trend]>0)&([@半自磨出料端压力.State]>(0-1))&([@半自磨功率.Trend]>0)&([@半自磨功率.State]>(0-1))&([@半自磨顽石产生量.Trend]>0)&([@半自磨顽石产生量.State]>(0-1))','[@给矿量优化设定值]=[@半自磨给矿量设定值]-50;[@前水量优化设定值]=[@半自磨给水量设定值]+10;[@半自磨工作状态]=1','180','true','3','false','true'),
('过载趋势6','([@半自磨出料端压力.Trend]>0)&([@半自磨出料端压力.State]>(0-1))&([@半自磨功率.Trend]>0)&([@半自磨功率.State]>(0-1))&([@半自磨顽石产生量.Trend]=0)&([@半自磨顽石产生量.State]>(0-1))','[@前水量优化设定值]=[@半自磨给水量设定值]+20;[@半自磨工作状态]=1','180','true','3','false','true'),
('过载趋势7','([@半自磨出料端压力.Trend]>0)&([@半自磨出料端压力.State]>(0-1))&([@半自磨功率.Trend]>0)&([@半自磨功率.State]>(0-1))&([@半自磨顽石产生量.Trend]<0)&([@半自磨顽石产生量.State]>(0-1))','[@前水量优化设定值]=[@前水量优化设定值]+30;[@半自磨工作状态]=1','180','true','3','false','true'),
('过载趋势8','([@半自磨出料端压力.Trend]>0)&([@半自磨出料端压力.State]>(0-1))&([@半自磨功率.Trend]>0)&([@半自磨功率.State]>(0-1))&([@半自磨顽石产生量.Trend]<0)&([@半自磨顽石产生量.State]<0)','[@给矿量优化设定值]=[@半自磨给矿量设定值]-25;[@前水量优化设定值]=[@半自磨给水量设定值]+30;[@半自磨工作状态]=1','180','true','3','false','true'),
('欠载趋势1','([@半自磨出料端压力.Trend]<0)&([@半自磨功率.Trend]<0)&([@半自磨顽石产生量.Trend]>0)&([@半自磨顽石产生量.State]>(0-1))','[@给矿量优化设定值]=[@半自磨给矿量设定值]+20;[@前水量优化设定值]=[@半自磨给水量设定值]-10;[@半自磨工作状态]=(0-1)','210','true','3','false','true'),
('欠载趋势2','([@半自磨出料端压力.Trend]<0)&([@半自磨功率.Trend]<0)&([@半自磨顽石产生量.Trend]=0)','[@给矿量优化设定值]=[@半自磨给矿量设定值]+20;[@半自磨工作状态]=(0-1)','210','true','3','false','true'),
('欠载趋势3','([@半自磨出料端压力.Trend]=0&([@半自磨功率.Trend]<0)&([@半自磨顽石产生量.Trend]>0)','[@半自磨工作状态]=(0-1)','210','true','3','false','true'),
('欠载趋势4','([@半自磨出料端压力.Trend]=0&([@半自磨功率.Trend]<0)&([@半自磨顽石产生量.Trend]=0)','[@半自磨工作状态]=(0-1)','210','true','3','false','true'),
('欠载趋势5','([@半自磨出料端压力.Trend]=0)&([@半自磨功率.Trend]<0)&([@半自磨顽石产生量.Trend]<0)&([@半自磨顽石产生量.State]>(0-1))','[@半自磨工作状态]=(0-1)','210','true','3','false','true'),
('欠载趋势6','([@半自磨出料端压力.Trend]=0)&([@半自磨功率.Trend]<0)&([@半自磨顽石产生量.Trend]<0)&([@半自磨顽石产生量.State]<0)','[@前水量优化设定值]=[@半自磨给水量设定值]-20;[@半自磨工作状态]=(0-1)','210','true','3','false','true'),
('欠载1','([@半自磨出料端压力.Trend]=0)&([@半自磨出料端压力.State]<1)&([@半自磨功率.Trend]>0)&([@半自磨功率.State]<0)&([@半自磨顽石产生量.Trend]>0)&([@半自磨顽石产生量.State]>0)','[@前水量优化设定值]=[@半自磨给水量设定值]-20;[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载2','([@半自磨出料端压力.Trend]=0)&([@半自磨出料端压力.State]<1)&([@半自磨功率.Trend]>0)&([@半自磨功率.State]<0)&([@半自磨顽石产生量.Trend]>0)&([@半自磨顽石产生量.State]<1)','[@前水量优化设定值]=[@半自磨给水量设定值]-10;[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载3','([@半自磨出料端压力.Trend]=0)&([@半自磨出料端压力.State]<1)&([@半自磨功率.Trend]>0)&([@半自磨功率.State]<0)&([@半自磨顽石产生量.Trend]=0)&([@半自磨顽石产生量.State]>0)','[@前水量优化设定值]=[@半自磨给水量设定值]-10;[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载4','([@半自磨出料端压力.Trend]=0)&([@半自磨出料端压力.State]<1)&([@半自磨功率.Trend]>0)&([@半自磨功率.State]<0)&([@半自磨顽石产生量.Trend]=0)&([@半自磨顽石产生量.State]<1)','[@给矿量优化设定值]=[@半自磨给矿量设定值]+20;[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载5','([@半自磨出料端压力.Trend]=0)&([@半自磨出料端压力.State]=0)&([@半自磨功率.Trend]>0)&([@半自磨功率.State]<0)&([@半自磨顽石产生量.Trend]<0)&([@半自磨顽石产生量.State]<1)','[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载6','([@半自磨出料端压力.Trend]=0)&([@半自磨出料端压力.State]<0)&([@半自磨功率.Trend]>0)&([@半自磨功率.State]<0)&([@半自磨顽石产生量.Trend]<0)&([@半自磨顽石产生量.State]<1)','[@给矿量优化设定值]=[@半自磨给矿量设定值]+20;[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载7','([@半自磨出料端压力.Trend]=0)&([@半自磨出料端压力.State]<1)&([@半自磨功率.Trend]=0)&([@半自磨功率.State]<0)&([@半自磨顽石产生量.Trend]>0)&([@半自磨顽石产生量.State]>0)','[@前水量优化设定值]=[@半自磨给水量设定值]-20;[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载8','([@半自磨出料端压力.Trend]=0)&([@半自磨出料端压力.State]<1)&([@半自磨功率.Trend]=0)&([@半自磨功率.State]<0)&([@半自磨顽石产生量.Trend]>0)&([@半自磨顽石产生量.State]<1)','[@前水量优化设定值]=[@半自磨给水量设定值]-10;[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载9','([@半自磨出料端压力.Trend]=0)&([@半自磨出料端压力.State]=0)&([@半自磨功率.Trend]=0)&([@半自磨功率.State]<0)&([@半自磨顽石产生量.Trend]<0)','[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载10','([@半自磨出料端压力.Trend]=0)&([@半自磨出料端压力.State]<0)&([@半自磨功率.Trend]=0)&([@半自磨功率.State]<0)&([@半自磨顽石产生量.Trend]<0)','[@给矿量优化设定值]=[@半自磨给矿量设定值]+30;[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载11','([@半自磨出料端压力.Trend]<0)&([@半自磨出料端压力.State]<1)&([@半自磨功率.Trend]>0)&([@半自磨功率.State]<1)&([@半自磨顽石产生量.Trend]>0)&([@半自磨顽石产生量.State]>0)','[@前水量优化设定值]=[@半自磨给水量设定值]-20;[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载12','([@半自磨出料端压力.Trend]<0)&([@半自磨出料端压力.State]<1)&([@半自磨功率.Trend]>0)&([@半自磨功率.State]<1)&([@半自磨顽石产生量.Trend]>0)&([@半自磨顽石产生量.State]=0)','[@前水量优化设定值]=[@半自磨给水量设定值]-10;[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载13','([@半自磨出料端压力.Trend]<0)&([@半自磨出料端压力.State]<1)&([@半自磨功率.Trend]>0)&([@半自磨功率.State]<1)&([@半自磨顽石产生量.Trend]=0)&([@半自磨顽石产生量.State]>0)','[@前水量优化设定值]=[@半自磨给水量设定值]-10;[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载14','([@半自磨出料端压力.Trend]<0)&([@半自磨出料端压力.State]<1)&([@半自磨功率.Trend]>0)&([@半自磨功率.State]<1)&([@半自磨顽石产生量.Trend]=0)&([@半自磨顽石产生量.State]=0)','[@给矿量优化设定值]=[@半自磨给矿量设定值]+30;[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载15','([@半自磨出料端压力.Trend]<0)&([@半自磨出料端压力.State]<1)&([@半自磨功率.Trend]>0)&([@半自磨功率.State]<1)&([@半自磨顽石产生量.Trend]<0)&([@半自磨顽石产生量.State]<1)','[@给矿量优化设定值]=[@半自磨给矿量设定值]+30;[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载16','([@半自磨出料端压力.Trend]<0)&([@半自磨出料端压力.State]<1)&([@半自磨功率.Trend]=0)&([@半自磨功率.State]<0)&([@半自磨顽石产生量.Trend]>0)&([@半自磨顽石产生量.State]>0)','[@前水量优化设定值]=[@半自磨给水量设定值]-20;[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载17','([@半自磨出料端压力.Trend]<0)&([@半自磨出料端压力.State]<1)&([@半自磨功率.Trend]=0)&([@半自磨功率.State]<0)&([@半自磨顽石产生量.Trend]>0)&([@半自磨顽石产生量.State]=0)','[@前水量优化设定值]=[@半自磨给水量设定值]-10;[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载18','([@半自磨出料端压力.Trend]<0)&([@半自磨出料端压力.State]<1)&([@半自磨功率.Trend]=0)&([@半自磨功率.State]<0)&([@半自磨顽石产生量.Trend]=0)&([@半自磨顽石产生量.State]>0)','[@前水量优化设定值]=[@半自磨给水量设定值]-10;[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载19','([@半自磨出料端压力.Trend]<0)&([@半自磨出料端压力.State]<1)&([@半自磨功率.Trend]=0)&([@半自磨功率.State]<0)&([@半自磨顽石产生量.Trend]=0)&([@半自磨顽石产生量.State]=0)','[@给矿量优化设定值]=[@半自磨给矿量设定值]+30;[@半自磨工作状态]=(0-2)','240','true','3','false','true'),
('欠载20','([@半自磨出料端压力.Trend]<0)&([@半自磨出料端压力.State]<1)&([@半自磨功率.Trend]=0)&([@半自磨功率.State]<0)&([@半自磨顽石产生量.Trend]<0)&([@半自磨顽石产生量.State]>0)','[@半自磨工作状态]=(0-2)','240','true','3','false','true')
GO
