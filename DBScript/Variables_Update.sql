USE [OptimalControl]
GO

--�����Ż��趨
UPDATE [dbo].[Variable]
   SET 
       --[UltimateUpperLimit] = 1300,
       --[UltimateLowerLimit] = 0,
       [ControlPeriod] = 120
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
       [ControlPeriod] = 3
 WHERE [Code] = 'CS040200020301'
GO

--ĥ��Ũ��
UPDATE [dbo].[Variable]
   SET 
     --[UltimateUpperLimit] = 1,
     --[UltimateLowerLimit] = 0.5,
       [UpperLimit] = 0.83
     --[LowerLimit] = 0.75
 WHERE [Code] = 'CS020600081001'
GO

--��ʯ������
UPDATE [dbo].[Variable]
   SET 
       [UltimateUpperLimit] = 350,
     --[UltimateLowerLimit] = 50,
       [UpperLimit] = 200,
       [LowerLimit] = 125
 WHERE [Code] = 'CS010100060101'
GO

--����ĥĥ��
UPDATE [dbo].[Variable]
   SET 
     --[UltimateLowerLimit] = 0.8,
       [UpperLimit] = 1,
       [LowerLimit] = 0.9
 WHERE [Code] = 'CS060100030103'
GO

--���϶���ѹ
UPDATE [dbo].[Variable]
   SET 
       [UltimateUpperLimit] = 6.2,
     --[UltimateLowerLimit] = 4.3,
	   [UpperLimit] = 5.4,
       [LowerLimit] = 4.8
 WHERE [Code] = 'CS060100030101'
GO

--���϶���ѹ
UPDATE [dbo].[Variable]
   SET 
       [UltimateUpperLimit] = 6,
     --[UltimateLowerLimit] = 4.3,
       [UpperLimit] = 5.3,
       [LowerLimit] = 4.7
 WHERE [Code] = 'CS060100030102'
GO

--1#����������Ũ��
UPDATE [dbo].[Variable]
   SET 
     --[UltimateUpperLimit] = 0.45,
     --[UltimateLowerLimit] = 0.2,
       [UpperLimit] = 0.39
     --[LowerLimit] = 0.3
 WHERE [Code] = 'CS020200070111'
GO

--2#����������Ũ��
UPDATE [dbo].[Variable]
   SET 
     --[UltimateUpperLimit] = 0.45,
     --[UltimateLowerLimit] = 0.2,
       [UpperLimit] = 0.39
     --[LowerLimit] = 0.3
 WHERE [Code] = 'CS020200070121'
GO

--����ĥ����
UPDATE [dbo].[Variable]
   SET 
       [UltimateUpperLimit] = 10000,
     --[UltimateLowerLimit] = 6000,
       [UpperLimit] = 9000,
       [LowerLimit] = 7500
 WHERE [Code] = 'CS040100030103'
GO

--����ĥ����
UPDATE [dbo].[Variable]
   SET 
     --[UltimateUpperLimit] = 1200,
     --[UltimateLowerLimit] = 100,
       [UpperLimit] = 200,
       [LowerLimit] = 120
 WHERE [Code] = 'CS060100030104'
GO