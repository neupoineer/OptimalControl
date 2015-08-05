USE [OptimalControl]
GO

--给矿优化设定
UPDATE [dbo].[Variable]
   SET [UltimateUpperLimit] = 1300
      ,[UltimateLowerLimit] = 0
      ,[ControlPeriod] = 300
 WHERE [Code] = 'CS010100020301'
GO

--给水优化设定
UPDATE [dbo].[Variable]
   SET [UltimateUpperLimit] = 200
      ,[UltimateLowerLimit] = 300
      ,[ControlPeriod] = 120
 WHERE [Code] = 'CS040100020301'
GO

--补加水优化设定
UPDATE [dbo].[Variable]
   SET [UltimateUpperLimit] = 1500
      ,[UltimateLowerLimit] = 1000
      ,[ControlPeriod] = 120
 WHERE [Code] = 'CS040200020301'
GO

--顽石产生量
UPDATE [dbo].[Variable]
   SET [UltimateUpperLimit] = 300
      ,[UpperLimit] = 200
      ,[LowerLimit] = 100
      ,[UltimateLowerLimit] = 50
 WHERE [Code] = 'CS010100060101'
GO

--进料端轴压
UPDATE [dbo].[Variable]
   SET [UltimateUpperLimit] = 5.8
      ,[UpperLimit] = 5.3
      ,[LowerLimit] = 4.8
      ,[UltimateLowerLimit] = 4.3
 WHERE [Code] = 'CS060100030101'
GO

--出料端轴压
UPDATE [dbo].[Variable]
   SET [UltimateUpperLimit] = 5.8
      ,[UpperLimit] = 5.3
      ,[LowerLimit] = 4.8
      ,[UltimateLowerLimit] = 4.3
 WHERE [Code] = 'CS060100030102'
GO

--半自磨功率
UPDATE [dbo].[Variable]
   SET [UltimateUpperLimit] = 10000
      ,[UpperLimit] = 8500
      ,[LowerLimit] = 7500
      ,[UltimateLowerLimit] = 6000
 WHERE [Code] = 'CS040100030103'
GO

--半自磨负荷
UPDATE [dbo].[Variable]
   SET [UltimateUpperLimit] = 1200
      ,[UpperLimit] = 400
      ,[LowerLimit] = 180
      ,[UltimateLowerLimit] = 120
 WHERE [Code] = 'CS060100030104'
GO

--半自磨磨音
UPDATE [dbo].[Variable]
   SET [UpperLimit] = 1
      ,[LowerLimit] = 0.9
      ,[UltimateLowerLimit] = 0.8
 WHERE [Code] = 'CS060100030103'
GO