

if (Test-Path "TestResults") {
    Remove-Item "TestResults" -Force -Recurse
}

dotnet test --collect "Xplat Code Coverage"

$recentCoverageFile = Get-ChildItem -File -Filter *.xml -Path ./TestResults -Name -Recurse | Select-Object -First 1;

$recentCoveragePath = Split-Path $recentCoverageFile

reportgenerator -reports:"TestResults/$recentCoverageFile" -targetdir:"TestResults/coveragereport"

Invoke-Expression "TestResults/coveragereport/index.html"
