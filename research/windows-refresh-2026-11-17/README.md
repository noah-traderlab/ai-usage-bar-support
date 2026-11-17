# AI Usage Bar 0.1.1-rc.1: public binary refresh-gate check

This small verification kit checks the **actual `AIUsageBar.TeamRefreshGate` type inside the published Windows ZIP**. It uses synthetic state and callbacks on one thread. It does not copy or reimplement the gate algorithm.

The refresh gate prevents an older asynchronous result from replacing the state of a newer refresh. Its owner-thread contract matters: `Begin`, `Apply`, `Close` and the callbacks must be called on the same UI thread. This kit simulates their arrival order on one thread; it does not create a UI or run concurrent calls.

[研究事例（日本語）](CASE-STUDY.md) · [Recorded results and kit checksums](VERIFICATION.md)

## Requirements and run

- Windows PowerShell **5.1**, with the bundled .NET Framework and C# compiler. PowerShell 7 is intentionally rejected because the setup resource reader uses .NET Framework `ReflectionOnlyLoad`.
- No administrator privileges, installation, account, network connection or added dependency is required. The wrapper reads the ZIP and this kit, compiles the small C# host using the framework, and optionally writes one result JSON file. PowerShell's compiler may use its ordinary temporary files. It does not extract an executable to disk.
- Obtain the existing [Windows 0.1.1-rc.1 pre-release ZIP](https://github.com/noah-traderlab/ai-usage-bar-support/releases/tag/windows-v0.1.1-rc.1) separately. The kit never downloads anything. The installer remains unexecuted.

[Send optional feedback (no login or real name required)](https://docs.google.com/forms/d/e/1FAIpQLScucMa18bwOd0HByQKaym4i5IFk9-P_EEkQg2sbT11G-9GJpg/viewform). Short comments, difficulties, unknowns and unused-product feedback are welcome. Responses are stored privately for product improvement; the form explains anonymity limits and correction/deletion handling. Do not include secrets, raw personal logs or third-party personal information. Feedback is not a condition of use.

For a suspected vulnerability, use [private vulnerability reporting](https://github.com/noah-traderlab/ai-usage-bar-support/security/advisories/new) instead (GitHub login required), following the [security policy](https://github.com/noah-traderlab/ai-usage-bar-support/blob/main/SECURITY.md). If that private form is unavailable, do not put vulnerability details in a public issue or feedback response.

Download [the PowerShell wrapper](Invoke-TeamRefreshGateCheck.ps1) and [the C# host](GateProbe.cs) into the same folder (use GitHub’s raw-file download). Compare them with [the checksums](SHA256SUMS.txt). Keep the [license](LICENSE) with any copies you redistribute.

Open Windows PowerShell and run from the folder containing this kit. Use a new result filename; existing files are never overwritten. Replace the example input path with the ZIP you downloaded:

```powershell
powershell.exe -NoLogo -NoProfile -NonInteractive -File .\Invoke-TeamRefreshGateCheck.ps1 -ZipPath C:\Downloads\AI-Usage-Bar-Windows-0.1.1-rc.1.zip -ResultPath .\gate-result.json
$LASTEXITCODE
```

Do not change an organizational execution policy to run the kit. If local policy disallows scripts or compilation, leave that protection in place and treat the check as unexecuted. Start a fresh PowerShell process per run, as in the command above. `-ResultPath` is optional; JSON is always printed to standard output. Its fields omit the input/result paths, user name and machine name.

## Pinned input

| Bytes | SHA-256 |
| --- | --- |
| Published ZIP | `de18af23a57f4d3b2b29ee68b46eb477abe7edd307d7bc09cb7eef3b18cfbd16` |
| Setup entry | `bd14eaa2313a1267f1d2a6ed59e586c7e411fb62b9f4ddabb6222e8c483466cd` |
| Embedded application | `d93b0bf33397c01c40c0c4a6fb5979463b8c7c2243c3b8d876c3790da5c62c60` |

The entire ZIP hash is checked before ZIP parsing or product assembly loading. The setup bytes are checked before reflection-only loading; the application bytes are checked before executable assembly loading. A mismatch exits with code `1`. The setup is loaded for manifest-resource reads only; neither entry point runs. The C# host instantiates the gate and calls only its `Begin`, `Apply` and `Close` methods. SHA-256 equality identifies exact bytes and does not authenticate a publisher or establish every behavior of an assembly.

## Expected result

For these exact published bytes, expect exit code `0`, `status: "passed"`, `passedCases: 5`, `failedCases: 0`, and these five scenario results:

1. `begin-clears-snapshot`: `Begin` immediately runs the clear callback.
2. `current-success-applies`: the current callback applies its synthetic state.
3. `superseded-callbacks-rejected`: old success/error and zero request callbacks cannot restore a snapshot or alter newer busy/error state.
4. `current-failure-and-recovery`: the current failure leaves an empty snapshot, and a later recovery cannot be overwritten by that older failure.
5. `closed-gate-rejects-work`: `Close` prevents queued callbacks and subsequent `Begin` work.

The hash checks are input checks, not extra functional test cases. This is not a claim that a request can only ever be applied once: the gate checks generation/currentness; the test calls each accepted callback once.

Each case separates its fixed `Expected` description from `Observed` callback return values, synthetic snapshot/busy/error fields and callback counts. A false result therefore retains the actual state instead of describing an expected outcome as observed.

For a different or modified ZIP, expect exit code `1`, `failureStage: "zip_sha256"`, `errorCode: "zip_sha256_mismatch"`, and all three load/invoke flags false, with an empty `cases` array. An unreadable file instead produces an operation failure at the same stage. This difference is intentional.

## Limits and license

This checks a narrow production ordering helper. It **does not verify** form rendering/disposal wiring, aggregate calculations, device authorization, report parsing, signatures, encryption, installation/update/uninstall, URLs, real accounts, network behavior or application startup. It invokes no `Program.Main`, setup `Main`, `Form`, `TeamService`, key generation, shared folder or user-data path. It is not evidence of adoption, a security certification, or access-program eligibility.

Only the newly authored kit files are offered under the included MIT license. The AI Usage Bar ZIP and its application/source are separate works with their own included terms; this kit grants no new rights to them and does not include those binaries. The published application's source repository is private. The check can be reproduced with the public binary alone.
