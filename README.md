# AI Usage Bar — support and security reports

This is the maintainer-operated support repository for **AI Usage Bar**. It provides a public place for ordinary bug reports and a separate private channel for suspected security vulnerabilities.

This repository provides support documents, Windows candidate releases, and manual downloads of the Python companion for macOS. Active development continues in the private `noah-traderlab/ai-project` repository under `ai-usage-bar/`. For the Python companion's source, download the named Python source distribution from its release page. GitHub's automatically generated source archives contain this support repository's documents and verification kits.

## Windows 0.1.1-rc.1 candidate / Windows候補版

[Download Windows 0.1.1-rc.1 ZIP](https://github.com/noah-traderlab/ai-usage-bar-support/releases/download/windows-v0.1.1-rc.1/AI-Usage-Bar-Windows-0.1.1-rc.1.zip) · [Release notes and checksums](https://github.com/noah-traderlab/ai-usage-bar-support/releases/tag/windows-v0.1.1-rc.1) · [2026-11-17 verification addendum / 検証追補](WINDOWS-0.1.1-rc.1-VERIFICATION.md)

[使用感を送る（匿名・ログイン不要・任意）](https://docs.google.com/forms/d/e/1FAIpQLScucMa18bwOd0HByQKaym4i5IFk9-P_EEkQg2sbT11G-9GJpg/viewform)。短い感想・困った点・不明点・未使用の意見も歓迎します。改善目的で回答を非公開保存します。回答はダウンロード・利用の条件ではありません。匿名性の限界・訂正削除方法は受付をご覧ください。

This free, unsigned **pre-release** requires explicit administrator approval of team devices and clears stale usage displays on reload, after revocation, and when loading fails. Old automatic approvals and incomplete approval records require manual reapproval; existing settings and data need not be deleted. The exact ZIP passed automated installation, running 0.1.0 update, synthetic settings preservation, startup/graceful exit, and removal on Windows Server 2025 with administrator role. Windows 10/11, non-administrator execution, SmartScreen, manual installer/settings UI, full asynchronous UI behavior, AI login, signed team reports, and Mac interoperability remain unverified. See the addendum before installation. The ZIP's older "unreleased/not installed" descriptions record its creation stage; the dated addendum records the subsequent verification and candidate publication.

The distribution includes the MIT license and third-party notices. Existing Windows/AI/API/storage contracts and costs are separate. No change is made to the historical Windows 0.1.0 or Mac/Python releases.

## Reproducible defensive maintenance / 公開検証

[Windows refresh-gate research kit](research/windows-refresh-2026-11-17/README.md) · [研究事例（日本語）](research/windows-refresh-2026-11-17/CASE-STUDY.md)

This free, source-available kit checks five synthetic callback-ordering scenarios directly against the published 0.1.1-rc.1 binary. It runs locally without installing the app. The code, expected/observed results, pinned hashes, MIT terms and limits are available together; it does not test the complete UI, authorization or signed reports.

[Optional feedback — no account or real name required](https://docs.google.com/forms/d/e/1FAIpQLScucMa18bwOd0HByQKaym4i5IFk9-P_EEkQg2sbT11G-9GJpg/viewform). Short comments and unused-product feedback are welcome. Responses are kept non-public for product improvement.

## Choose the appropriate channel

- **Ordinary bugs, installation questions, or feature requests:** [open a public issue](https://github.com/noah-traderlab/ai-usage-bar-support/issues/new/choose). Issues and their attachments are visible to everyone. Include only the platform, version/file name if known, expected behavior, and a description using synthetic examples.
- **Suspected security vulnerabilities:** use [Report a vulnerability](https://github.com/noah-traderlab/ai-usage-bar-support/security/advisories/new). This uses GitHub private vulnerability reporting; a GitHub account is required. Read [the security policy](SECURITY.md) first. Do not post vulnerability details in a public issue or Gist comment.

Do not send API keys, passwords, login links, raw AI prompts, private logs, personal information, or files from a shared team folder. Do not reinstall or download a package just to identify your version. If a detail is unknown, say so.

## Optional usage feedback / 任意の使用感

[Share feedback — optional, no sign-in or name required](https://docs.google.com/forms/d/e/1FAIpQLScucMa18bwOd0HByQKaym4i5IFk9-P_EEkQg2sbT11G-9GJpg/viewform). Short comments, difficulties, and questions are welcome, including from people who have not used the product. Responses are kept non-public for product improvement.

No name or email address is required. Do not include secrets, raw logs, or personal information. Complete anonymity, including records kept by Google, is not guaranteed. Sending feedback does not grant permission to quote or republish it for other purposes. The form explains how to request a correction or deletion and the limits of those requests.

[使用感を送る（匿名・ログイン不要・任意）](https://docs.google.com/forms/d/e/1FAIpQLScucMa18bwOd0HByQKaym4i5IFk9-P_EEkQg2sbT11G-9GJpg/viewform)。短い感想・困った点・不明点を歓迎します。未使用でも構いません。回答は一般公開せず、製品改善のため保管します。

氏名・メールアドレスを求めません。秘密情報・生ログ・個人情報は書かないでください。Google側の通信記録等を含む完全匿名性は保証しません。送信を別用途への引用・転載の許可とは扱いません。訂正・削除依頼の方法と対応の限界はフォームに記載しています。

If you prefer a public conversation, [GitHub usage feedback](https://github.com/noah-traderlab/ai-usage-bar-support/issues/new?template=usage_feedback.yml) remains available; a GitHub account is required and your issue/account are public. Neither route is a condition of downloading or using AI Usage Bar. Send suspected vulnerabilities only through the private reporting channel above.

## Python companion 0.1.1a8 for macOS — manual download

[Download the 0.1.1a8 wheel](https://github.com/noah-traderlab/ai-usage-bar-support/releases/download/python-v0.1.1a8/ai_usage_bar_mac-0.1.1a8-py3-none-any.whl) · [Python source, SHA-256 and installation instructions](https://github.com/noah-traderlab/ai-usage-bar-support/releases/tag/python-v0.1.1a8) · [使用感を送る（ログイン不要・任意）](https://docs.google.com/forms/d/e/1FAIpQLScucMa18bwOd0HByQKaym4i5IFk9-P_EEkQg2sbT11G-9GJpg/viewform)

The 0.1.1a8 alpha requires Python 3.11 or later and Tk for the optional GUI. When you leave a team, the GUI shows whether it removed your shared report, found it already absent, or could not confirm removal. If the shared folder is disconnected, the report may remain. Follow the manual installation instructions on the release page. The GUI has not been tested on a Mac for this release. Feedback is optional and kept private for product improvement. The [previous 0.1.1a7 manual release](https://github.com/noah-traderlab/ai-usage-bar-support/releases/tag/python-v0.1.1a7) is still available.

## Python companion alpha 0.1.1a6

The [0.1.1a6 release](https://pypi.org/project/ai-usage-bar-mac/0.1.1a6/) was published on 2026-11-17 at 18:51 JST. It repairs two input-processing problems: malformed Claude JSONL records could prevent later valid records in the same file from being counted, and an oversized finite integer in a friend percentage could interrupt list processing. The [maintenance case (Japanese)](research/python-local-inputs-2026-11-17/README.md) records the public a5 reproduction, fixes, 19 added regressions, exact artifact hashes, verification and limits.

This is an early-stage Python companion for macOS, requiring Python 3.11 or newer. The optional GUI requires Tk and remains unverified on end-user Macs. This does not update the separate Windows application or provide a signed/notarized macOS app.

**Leaving a team in 0.1.1a6.** If the shared folder is disconnected when you run `aiusagebar leave`, you leave the team on this device. The command can say it deleted your report even though a previously sent report is still in the shared folder. To confirm removal, ask the person who manages the shared folder to check.

**Publishing origin:** Publication moved from `noah-traderlab/ai-usage-bar` (`publish-pypi.yml`) to `noah-traderlab/ai-project` (`ai-usage-bar-publish.yml`) with 0.1.1a1. PyPI flagged that version as **Change of provenance**. Before installing, inspect both files in the [0.1.1a6 Security tab](https://pypi.org/project/ai-usage-bar-mac/0.1.1a6/#security) and compare their publisher, workflow, and digests. Attestations identify the publishing origin; they do not guarantee code safety.

[Share feedback — optional, no sign-in or name required](https://docs.google.com/forms/d/e/1FAIpQLScucMa18bwOd0HByQKaym4i5IFk9-P_EEkQg2sbT11G-9GJpg/viewform). Short comments, difficulties, and questions are welcome, including from people who have not used the product. Responses are kept non-public for product improvement.

Download the exact wheel from the [release files](https://pypi.org/project/ai-usage-bar-mac/0.1.1a6/#files), verify its source and digest using the maintenance case, then install the local file in a new virtual environment:

~~~sh
python3 -m venv .venv
.venv/bin/python -m pip install --no-index --no-deps path/to/ai_usage_bar_mac-0.1.1a6-py3-none-any.whl
.venv/bin/aiusagebar --help
.venv/bin/aiusagebar status --sharing
~~~

This local-file installation writes the virtual environment without contacting a package index; it does not join a team or start the GUI. `--help` only displays command help. `status --sharing` reads saved local participation settings, which can contain a private key, but does not use or display that key or other personal values. It does not read raw logs, authentication data, or the shared folder, send reports, or save settings. It shows the consent ceiling, not a verified current effective sharing scope. Friends settings are separate. This check does not stop an already running joined GUI from reporting. Ordinary `status` can display personal values.

The matching [source distribution in the release files](https://pypi.org/project/ai-usage-bar-mac/0.1.1a6/#files), `ai_usage_bar_mac-0.1.1a6.tar.gz`, includes the companion source, nine synthetic regression test files, the existing selftest, packaging verifier, licenses, and `docs/python-package-release.md` verification instructions. The wheel does not include all these checks. Synthetic checks do not establish macOS GUI behavior or a security audit.

The a6 repair skips malformed Claude record shapes without losing later normal records or records in other files. Unconvertible friend percentages remain unknown while other quota windows and files stay available; normal range handling and existing time, ID, size and window limits are preserved. Twelve Claude shape methods and seven friend-value methods use finite synthetic inputs; the temporary friend JSON files remain unchanged. This does not establish affected user counts, real-user damage or macOS GUI behavior.

The exact published artifacts passed **163 regression methods with zero failures/errors/skips, plus all 36 existing selftest checks** on Linux publishing run `44059250444`. A separate local WSL check also passed, with its own artifact hashes. Public files fetched without authentication matched registry sizes/digests, CI digests and the reviewed source; publisher/commit/run fields were compared, without cryptographic verification of the full attestation chain. A logged-out browser check confirmed the a6 page and the feedback link near the install instructions. The case records these distinct checks and the preserved failed automatic HTML check.

This is personal, AI-assisted maintenance of the maintainer's product using finite synthetic fixtures. Independent AI review is not a human external audit. Test counts and publication do not establish real-user adoption, company qualifications, a CVE or formal access approval.

The historical Python 0.1.0 package does not contain `status --sharing`; that feature exists in the later versions documented here. For ordinary bugs or installation questions, use the public issue channel above. Send suspected vulnerabilities only through the private reporting channel; do not post private logs, keys, or vulnerability details publicly.

## Source and release history

The [original public repository](https://github.com/noah-traderlab/ai-usage-bar) remains a read-only source and release archive. Its historical source, tags, and releases are not replaced by this support repository. It cannot accept new reports while archived.

- [Windows 0.1.0 release](https://github.com/noah-traderlab/ai-usage-bar/releases/tag/windows-v0.1.0)
- [Python/macOS companion 0.1.0 on PyPI](https://pypi.org/project/ai-usage-bar-mac/0.1.0/)
- [Previous Python companion alpha 0.1.1a1](https://pypi.org/project/ai-usage-bar-mac/0.1.1a1/)
- [Previous Python companion alpha 0.1.1a2](https://pypi.org/project/ai-usage-bar-mac/0.1.1a2/) fixed Codex record object shapes that could interrupt later usage parsing. That release's final Linux package verification passed 49 regression methods without failures, errors, or skips, plus 36 selftest checks.
- [Previous Python companion alpha 0.1.1a3](https://pypi.org/project/ai-usage-bar-mac/0.1.1a3/) was published on 2026-11-10. It keeps daily totals available when a Claude or Codex model field has an invalid type, classifying affected usage under an unknown model instead of the previous model while preserving normal model changes and duplicate handling. Claude remaining-quota parsing keeps nonnumeric or non-finite utilization unknown instead of displaying 100% remaining or raising a conversion error; a valid value for the other quota window remains available. That release's final Linux package verification passed 122 regression methods without failures, errors, or skips, plus 36 selftest checks. These are historical a3 results, not a substitute for checking a4.
- [Previous Python companion alpha 0.1.1a4](https://pypi.org/project/ai-usage-bar-mac/0.1.1a4/) was published on 2026-11-11 UTC (2026-11-12 JST). It prevents a positive non-finite token count, such as the JSON numeric value `1e309`, from stopping later valid Claude/Codex records. Non-finite floats are treated as zero while normal integer/fractional handling, negative values, booleans, and deduplication remain unchanged. Its 10 added token-value tests used synthetic data; affected user counts remain unknown. That release's final Linux package verification passed 132 regressions without failures/errors/skips and 36 selftest checks. These are a4 results, not a5 verification. The [matching a4 source distribution](https://pypi.org/project/ai-usage-bar-mac/0.1.1a4/#files) remains available; previous artifacts are not replaced. The a4 repair did not change timestamps, sharing/reporting behavior, or notification delivery.
- [Previous Python companion alpha 0.1.1a5](https://pypi.org/project/ai-usage-bar-mac/0.1.1a5/) was published on 2026-11-17 at 17:58 JST. It validates local participation settings and resets partially loaded state on read or validation failure without overwriting the original file. Its 12 added regression methods preserve normal saved-state behavior. That release's Linux publishing check passed 144 regressions without failures/errors/skips and 36 selftest checks. Its [maintenance case](research/python-local-settings-2026-11-17/README.md) preserves the public a4 reproduction, exact a5 artifact hashes, the limited Windows result (25 passed, one skipped), Linux checks and limits. These are a5 results; existing artifacts remain unchanged.
- [Published maintenance documents, fixed historical revision](https://gist.github.com/noah-traderlab/04dd9db11afbf5ba97c3f4c18fc688d5/e7224dc4b45babc065c3b4fa98bbe65c9a345a99)

[使用感を送る（匿名・ログイン不要・任意）](https://docs.google.com/forms/d/e/1FAIpQLScucMa18bwOd0HByQKaym4i5IFk9-P_EEkQg2sbT11G-9GJpg/viewform)。短い感想・困った点・不明点を歓迎します。未使用でも構いません。回答は一般公開せず、製品改善のため保管します。

The same version number alone does not prove two files are identical. Standalone macOS application builds remain development previews, not a public signed/notarized release. Private development changes are not automatically included in previously published packages.

## Maintenance

Reports are handled by [@noah-traderlab](https://github.com/noah-traderlab). Ordinary issues and private reports are reviewed through GitHub. Receipt, reproduction, impact assessment, a fix, and a published release are separate stages; none is promised by opening a report. There is no paid bounty or guaranteed response deadline. No report is required to use the application.

AI assistance used for investigation, writing, or fixes should be disclosed. Reported impact must remain distinct from untested assumptions.

## 日本語での受付案内

通常の不具合・導入の質問は公開Issue、セキュリティ上の問題は上記の非公開「Report a vulnerability」を使ってください。非公開報告にはGitHubへのログインが必要です。OS、分かる範囲の版・ファイル名、期待する動作と実際の症状、架空データでの再現手順を記載してください。秘密情報、生ログ、入力本文、第三者の個人情報は不要です。不明な版は不明のままで構いません。

旧リポジトリは閲覧用アーカイブのまま、開発正本はprivateの `ai-project/ai-usage-bar/` です。実際の配布版と検証範囲は上記のRelease・PyPI案内を参照してください。受付の設置やダウンロード数を、確認済み利用者数・第三者採用には換算しません。
