# Verification record — 2026-11-17 JST

Kit version: 1.0.0. Maintainer-operated, AI-assisted checks; not an independent human audit.

The exact published 0.1.1-rc.1 ZIP passed all three pinned artifact hashes. Windows PowerShell 5.1.26100.9818 / CLR 4.0.30319.42000 ran the gate-only probe: **5 scenarios passed, 0 failed, process exit 0**. See [the recorded output](verified-result.json), including actual callback return values and synthetic state. No application/installer entry point, UI, shared folder, key generation or signed report was invoked.

A separate copy of the ZIP with one byte changed was rejected with process exit 1, `zip_sha256_mismatch`, all product-load/probe flags false and no scenarios executed. See [the rejection output](wrong-hash-result.json). This is an input-integrity check, not another product defect or a functional scenario.

The first kit draft could not find the gate's implicit constructor because it searched only non-public constructors. The kit was corrected to inspect public and non-public constructors and rerun successfully. That was a verification-host defect, not a newly discovered application bug. These JSON files record the corrected kit.

[SHA256SUMS.txt](SHA256SUMS.txt) identifies the reviewed kit, documentation and saved observations. Hash equality is only a check of byte identity. Readers can run their own fresh result using the [documented command](README.md); do not substitute these maintainer-produced results for your own result, or treat multiple test executions as additional users.

No remote CI, paid API, server, new service contract or application installation was used for this kit. It is published as source in the existing support repository. Normal local Windows/PowerShell availability remains the reader's responsibility. [The case study](CASE-STUDY.md) distinguishes this narrow check from historical development tests and the prior isolated installation test.
