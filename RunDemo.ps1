Start-Process -FilePath 'dotnet' -WorkingDirectory './AlgoraCodingTaskWebSocket' -ArgumentList 'run'
Test-NetConnection -ComputerName localhost -Port 5000
Start-Process -FilePath 'dotnet' -WorkingDirectory './AlgoraCodingTaskClient' -ArgumentList 'run'