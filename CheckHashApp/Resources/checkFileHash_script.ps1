param([Parameter()]
   [String]$filePath,
   [String]$importedHash,
   [String]$algorithm
)
$importedHash -eq (Get-FileHash $filePath -Algorithm $algorithm).Hash