# Windows PowerShell 5.1 / .NET Framework only. No installer or application entry point is invoked.
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)][ValidateNotNullOrEmpty()][string]$ZipPath,
    [string]$ResultPath
)
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest
$record = [ordered]@{
    schemaVersion = 1
    kitVersion = '1.0.0'
    targetVersion = '0.1.1-rc.1'
    scope = 'Single-threaded, synthetic callback ordering in the published TeamRefreshGate type only.'
    status = 'failed'
    stage = 'environment'
    runtime = @{ powerShell = $PSVersionTable.PSVersion.ToString(); clr = [Environment]::Version.ToString() }
    input = [ordered]@{
        zipExpectedSha256 = 'de18af23a57f4d3b2b29ee68b46eb477abe7edd307d7bc09cb7eef3b18cfbd16'
        setupExpectedSha256 = 'bd14eaa2313a1267f1d2a6ed59e586c7e411fb62b9f4ddabb6222e8c483466cd'
        appExpectedSha256 = 'd93b0bf33397c01c40c0c4a6fb5979463b8c7c2243c3b8d876c3790da5c62c60'
    }
    setupReflectionOnlyLoaded = $false
    appLoaded = $false
    probeInvoked = $false
    cases = @()
    limitations = @(
        'Not a UI, authorization, report validation, signature, network or installation test.',
        'No Program.Main, setup Main, Form, TeamService, key generation, shared folder or user data access.',
        'Callbacks run synchronously on one owner thread; this does not test concurrent access.',
        'Pinned hashes identify these exact bytes; they are not a publisher signature or proof of all program behavior.'
    )
}
$exitCode = 1
$canWriteResult = $false
function HashBytes([byte[]]$Bytes) {
    $sha = [Security.Cryptography.SHA256]::Create()
    try { return ([BitConverter]::ToString($sha.ComputeHash($Bytes))).Replace('-', '').ToLowerInvariant() }
    finally { $sha.Dispose() }
}
function ReadStream([IO.Stream]$Stream) {
    $memory = New-Object IO.MemoryStream
    try { $Stream.CopyTo($memory); return ,$memory.ToArray() }
    finally { $memory.Dispose() }
}
try {
    if ($PSVersionTable.PSVersion.Major -ne 5 -or $PSVersionTable.PSVersion.Minor -ne 1 -or
        $PSVersionTable.PSEdition -ne 'Desktop' -or [Environment]::OSVersion.Platform -ne 'Win32NT') {
        throw 'unsupported_runtime'
    }
    if (-not [string]::IsNullOrWhiteSpace($ResultPath)) {
        $record.stage = 'result_destination'
        $ResultPath = [IO.Path]::GetFullPath($ResultPath)
        if ([IO.File]::Exists($ResultPath) -or [IO.Directory]::Exists($ResultPath) -or
            -not [IO.Directory]::Exists([IO.Path]::GetDirectoryName($ResultPath))) { throw 'result_must_be_a_new_file_in_an_existing_directory' }
        $canWriteResult = $true
    }
    $record.stage = 'zip_sha256'
    $zipBytes = [IO.File]::ReadAllBytes([IO.Path]::GetFullPath($ZipPath))
    $record.input.zipActualSha256 = HashBytes $zipBytes
    if ($record.input.zipActualSha256 -ne $record.input.zipExpectedSha256) { throw 'zip_sha256_mismatch' }

    # Only framework ZIP readers run before setup resource inspection. Nothing is extracted to disk.
    $record.stage = 'setup_resource'
    Add-Type -AssemblyName System.IO.Compression
    $zipMemory = [IO.MemoryStream]::new($zipBytes, $false)
    $archive = [IO.Compression.ZipArchive]::new($zipMemory, [IO.Compression.ZipArchiveMode]::Read)
    try {
        $entry = $archive.GetEntry('AI-Usage-Bar-Windows-0.1.1-rc.1/AI-Usage-Bar-Setup-0.1.1-rc.1.exe')
        if ($null -eq $entry) { throw 'setup_entry_missing' }
        $stream = $entry.Open()
        try { $setupBytes = ReadStream $stream } finally { $stream.Dispose() }
    } finally { $archive.Dispose(); $zipMemory.Dispose() }
    $record.input.setupActualSha256 = HashBytes $setupBytes
    if ($record.input.setupActualSha256 -ne $record.input.setupExpectedSha256) { throw 'setup_sha256_mismatch' }
    $setup = [Reflection.Assembly]::ReflectionOnlyLoad($setupBytes)
    $record.setupReflectionOnlyLoaded = $true
    $stream = $setup.GetManifestResourceStream('AIUsageBar.exe')
    if ($null -eq $stream) { throw 'app_resource_missing' }
    try { $appBytes = ReadStream $stream } finally { $stream.Dispose() }
    $record.stage = 'app_sha256'
    $record.input.appActualSha256 = HashBytes $appBytes
    if ($record.input.appActualSha256 -ne $record.input.appExpectedSha256) { throw 'app_sha256_mismatch' }

    # The small host creates only TeamRefreshGate and calls only its Begin/Apply/Close methods.
    $record.stage = 'compile_probe'
    Add-Type -Path (Join-Path $PSScriptRoot 'GateProbe.cs')
    $record.stage = 'gate_callbacks'
    $record.probeInvoked = $true
    $probe = [AiubPublicGateKit.Probe]::Run($appBytes)
    $record.appLoaded = $probe.AppLoaded
    $record.cases = @($probe.Cases)
    $record.passedCases = @($probe.Cases | Where-Object { $_.Passed }).Count
    $record.failedCases = @($probe.Cases | Where-Object { -not $_.Passed }).Count
    if (-not $probe.Passed) { throw 'gate_contract_failed' }
    $record.stage = 'complete'
    $record.status = 'passed'
    $exitCode = 0
} catch {
    # Do not copy exception messages, input paths, user names or machine names to portable results.
    $record.failureStage = $record.stage
    $record.errorCode = 'operation_failed'
    $known = @('unsupported_runtime','result_must_be_a_new_file_in_an_existing_directory','zip_sha256_mismatch','setup_entry_missing','setup_sha256_mismatch','app_resource_missing','app_sha256_mismatch','gate_contract_failed')
    if ($known -contains $_.Exception.Message) { $record.errorCode = $_.Exception.Message }
} finally {
    $json = $record | ConvertTo-Json -Depth 10
    if ($canWriteResult) {
        try {
            $file = [IO.File]::Open($ResultPath, [IO.FileMode]::CreateNew, [IO.FileAccess]::Write, [IO.FileShare]::None)
            try {
                $bytes = [Text.UTF8Encoding]::new($false).GetBytes($json + [Environment]::NewLine)
                $file.Write($bytes, 0, $bytes.Length)
            } finally { $file.Dispose() }
        } catch {
            $record.status = 'failed'; $record.errorCode = 'result_write_failed'; $exitCode = 1
            $json = $record | ConvertTo-Json -Depth 10
        }
    }
    Write-Output $json
}
exit $exitCode
