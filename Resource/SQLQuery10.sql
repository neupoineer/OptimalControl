use ControlPlatform
declare @sql varchar(8000)
select @sql = isnull(@sql + '],[' , '') + Name from Curves group by Name
set @sql = '[' + @sql + ']'
declare @sql1 varchar(8000)
select @sql1 = isnull(@sql1 + ']),max([' , '') + Name from Curves group by Name + isnull(@sql + '],[' , '') + Name from Curves group by Name
set @sql1 = 'max([' + @sql1 + '])'
exec ('select [time],' + @sql1 + ' from (select * from data where Time > ''2014-08-15'' and Time <''2014-08-16'') as a pivot (max(Value) for ParameterName in (' + @sql + ')) b group by time order by time')
