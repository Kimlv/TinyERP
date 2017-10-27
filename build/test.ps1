function Test-Output {
    Write-Output "Hello World"
}

function Test-Output2 {
    Write-Host "Hello World"
}

function Receive-Output {
    process { Write-Host $_ -foreground Yellow }
}

#Output piped to another function, not displayed in first.
Test-Output | Receive-Output

#Output not piped to 2nd function, only displayed in first.
Test-Output2 | Receive-Output 

#Pipeline sends to Out-Default at the end.
Test-Output
