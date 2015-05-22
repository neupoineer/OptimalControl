
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe OptimalControlService.exe
Net Start OptimalControlService
sc config OptimalControlService start= auto