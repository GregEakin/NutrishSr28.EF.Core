Get-ChildItem -File | ForEach-Object {
    $lineCount = (Get-Content $_.FullName).Count
    [PSCustomObject]@{
        FileName  = $_.Name
        LineCount = $lineCount
    }
} | Format-Table -AutoSize