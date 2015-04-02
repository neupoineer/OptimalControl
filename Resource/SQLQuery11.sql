use ControlPlatform

declare @sql1 varchar(8000)
select @sql1 = isnull(@sql1 + '],[' , '') + [Name] from [Curves] group by [Name]
set @sql1 = '[' + @sql1 + ']'
--print (@sql1)

declare @sql2 varchar(8000)
select @sql2 = isnull(@sql2 + ''',max([' , '') + [Name] +']) as ''' + [Name]  from [Curves] group by [Name]
set @sql2 = 'max([' + @sql2 + ''''
--print (@sql2)

declare @startTime varchar(50)
set @startTime = '''2014-08-15 00:00:00'''

declare @endTime varchar(50)
set @endTime = '''2014-08-15 01:00:00'''

exec 
('
	select [Time] as ''Ê±¼ä'',' + @sql2 + ' 
	from 
	(
		select * from [Data] where [Time] > ' + @startTime + ' and [Time] <' + @endTime + '
	) 
	as a pivot (max([Value]) for [ParameterName] in (' + @sql1 + ')) b
	group by [Time]
	order by [Time]
')
