USE [OptimalControl]
GO

--�����Ż��趨
UPDATE [dbo].[Variable]
   SET 
       --[UltimateUpperLimit] = 1300,
       --[UltimateLowerLimit] = 0,
       [ControlPeriod] = 300
 WHERE [Code] = 'CS010100020301'
GO

--��ˮ�Ż��趨
UPDATE [dbo].[Variable]
   SET 
     --[UltimateUpperLimit] = 200,
     --[UltimateLowerLimit] = 300,
       [ControlPeriod] = 120
 WHERE [Code] = 'CS040100020301'
GO

--����ˮ�Ż��趨
UPDATE [dbo].[Variable]
   SET 
     --[UltimateUpperLimit] = 1500,
     --[UltimateLowerLimit] = 1000,
       [ControlPeriod] = 120
 WHERE [Code] = 'CS040200020301'
GO

--��ʯ������
UPDATE [dbo].[Variable]
   SET 
     --[UltimateUpperLimit] = 300,
     --[UltimateLowerLimit] = 50,
       [UpperLimit] = 200,
       [LowerLimit] = 100
 WHERE [Code] = 'CS010100060101'
GO

--���϶���ѹ
UPDATE [dbo].[Variable]
   SET 
     --[UltimateUpperLimit] = 5.8,
     --[UltimateLowerLimit] = 4.3,
	   [UpperLimit] = 5.4,
       [LowerLimit] = 4.8
 WHERE [Code] = 'CS060100030101'
GO

--���϶���ѹ
UPDATE [dbo].[Variable]
   SET 
     --[UltimateUpperLimit] = 5.8,
     --[UltimateLowerLimit] = 4.3,
       [UpperLimit] = 5.3,
       [LowerLimit] = 4.8
 WHERE [Code] = 'CS060100030102'
GO

--����ĥ����
UPDATE [dbo].[Variable]
   SET 
     --[UltimateUpperLimit] = 10000,
     --[UltimateLowerLimit] = 6000,
       [UpperLimit] = 8500,
       [LowerLimit] = 7500
 WHERE [Code] = 'CS040100030103'
GO

--����ĥ����
UPDATE [dbo].[Variable]
   SET 
     --[UltimateUpperLimit] = 1200,
     --[UltimateLowerLimit] = 120,
       [UpperLimit] = 350,
       [LowerLimit] = 180
 WHERE [Code] = 'CS060100030104'
GO

--����ĥĥ��
UPDATE [dbo].[Variable]
   SET 
     --[UltimateLowerLimit] = 0.8,
       [UpperLimit] = 1,
       [LowerLimit] = 0.9
 WHERE [Code] = 'CS060100030103'
GO