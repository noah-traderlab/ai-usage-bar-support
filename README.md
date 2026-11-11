# AI Usage Bar — support and security reports

This is the maintainer-operated support repository for **AI Usage Bar**. It provides a public place for ordinary bug reports and a separate private channel for suspected security vulnerabilities.

This repository contains support documents, not executable releases. Active development remains in the private `noah-traderlab/ai-project` repository, under `ai-usage-bar/`.

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

## Python companion alpha 0.1.1a4

The [0.1.1a4 release](https://pypi.org/project/ai-usage-bar-mac/0.1.1a4/) was published on 2026-11-11 UTC (2026-11-12 JST). This is an early-stage Python companion for macOS, requiring Python 3.11 or newer. The optional GUI requires Tk and remains unverified on end-user Macs. This does not update the separate Windows application or provide a signed/notarized macOS app.

**Publishing origin:** Publication moved from `noah-traderlab/ai-usage-bar` (`publish-pypi.yml`) to `noah-traderlab/ai-project` (`ai-usage-bar-publish.yml`) with 0.1.1a1. PyPI flagged that version as **Change of provenance**. Before installing, inspect both files in the [PyPI Security tab](https://pypi.org/project/ai-usage-bar-mac/0.1.1a4/#security) and compare their publisher, workflow, and digests. Attestations identify the publishing origin; they do not guarantee code safety.

[Share feedback — optional, no sign-in or name required](https://docs.google.com/forms/d/e/1FAIpQLScucMa18bwOd0HByQKaym4i5IFk9-P_EEkQg2sbT11G-9GJpg/viewform). Short comments, difficulties, and questions are welcome, including from people who have not used the product. Responses are kept non-public for product improvement.

In a new working directory, install this exact alpha into a virtual environment:

~~~sh
python3 -m venv .venv
.venv/bin/python -m pip install "ai-usage-bar-mac==0.1.1a4"
.venv/bin/aiusagebar --help
.venv/bin/aiusagebar status --sharing
~~~

Installation contacts the package index and writes the virtual environment; it does not join a team or start the GUI. `--help` only displays command help. `status --sharing` reads saved local participation settings, which can contain a private key, but does not use or display that key or other personal values. It does not read raw logs, authentication data, or the shared folder, send reports, or save settings. It shows the consent ceiling, not a verified current effective sharing scope. Friends settings are separate. This check does not stop an already running joined GUI from reporting. Ordinary `status` can display personal values.

The matching [source distribution in the release files](https://pypi.org/project/ai-usage-bar-mac/0.1.1a4/#files), `ai_usage_bar_mac-0.1.1a4.tar.gz`, includes the companion source, synthetic tests, packaging verifier, licenses, and `docs/python-package-release.md` verification instructions. The wheel does not include all these checks. Synthetic checks do not establish macOS GUI behavior or a security audit.

Version 0.1.1a4 prevents a positive non-finite token count, such as the JSON numeric value `1e309`, from stopping later valid records in a Claude or Codex log file. Non-finite float counts are treated as zero while other valid values and subsequent records remain usable. Existing handling of integers, finite positive floats, truncation, negative values, booleans, and duplicate records is preserved. The 10 added token-value regression methods use synthetic data; real-world occurrence and affected user counts remain unknown.

The final Linux package verification passed all 132 synthetic regression methods with no failures, errors, or skips, and all 36 selftest checks. These checks use synthetic data and do not establish real-user adoption, macOS GUI behavior, or a security audit. The repair does not change timestamps, sharing/reporting behavior, or notification delivery.

The historical Python 0.1.0 package does not contain `status --sharing`; use the exact version above if you want this feature. For ordinary bugs or installation questions, use the public issue channel above. Send suspected vulnerabilities only through the private reporting channel; do not post private logs, keys, or vulnerability details publicly.

## Source and release history

The [original public repository](https://github.com/noah-traderlab/ai-usage-bar) remains a read-only source and release archive. Its historical source, tags, and releases are not replaced by this support repository. It cannot accept new reports while archived.

- [Windows 0.1.0 release](https://github.com/noah-traderlab/ai-usage-bar/releases/tag/windows-v0.1.0)
- [Python/macOS companion 0.1.0 on PyPI](https://pypi.org/project/ai-usage-bar-mac/0.1.0/)
- [Previous Python companion alpha 0.1.1a1](https://pypi.org/project/ai-usage-bar-mac/0.1.1a1/)
- [Previous Python companion alpha 0.1.1a2](https://pypi.org/project/ai-usage-bar-mac/0.1.1a2/) fixed Codex record object shapes that could interrupt later usage parsing. That release's final Linux package verification passed 49 regression methods without failures, errors, or skips, plus 36 selftest checks.
- [Previous Python companion alpha 0.1.1a3](https://pypi.org/project/ai-usage-bar-mac/0.1.1a3/) was published on 2026-11-10. It keeps daily totals available when a Claude or Codex model field has an invalid type, classifying affected usage under an unknown model instead of the previous model while preserving normal model changes and duplicate handling. Claude remaining-quota parsing keeps nonnumeric or non-finite utilization unknown instead of displaying 100% remaining or raising a conversion error; a valid value for the other quota window remains available. That release's final Linux package verification passed 122 regression methods without failures, errors, or skips, plus 36 selftest checks. These are historical a3 results, not a substitute for checking a4.
- [Published maintenance documents, fixed historical revision](https://gist.github.com/noah-traderlab/04dd9db11afbf5ba97c3f4c18fc688d5/e7224dc4b45babc065c3b4fa98bbe65c9a345a99)

[使用感を送る（匿名・ログイン不要・任意）](https://docs.google.com/forms/d/e/1FAIpQLScucMa18bwOd0HByQKaym4i5IFk9-P_EEkQg2sbT11G-9GJpg/viewform)。短い感想・困った点・不明点を歓迎します。未使用でも構いません。回答は一般公開せず、製品改善のため保管します。

The same version number alone does not prove two files are identical. Standalone macOS application builds remain development previews, not a public signed/notarized release. Private development changes are not automatically included in previously published packages.

## Maintenance

Reports are handled by [@noah-traderlab](https://github.com/noah-traderlab). Ordinary issues and private reports are reviewed through GitHub. Receipt, reproduction, impact assessment, a fix, and a published release are separate stages; none is promised by opening a report. There is no paid bounty or guaranteed response deadline. No report is required to use the application.

AI assistance used for investigation, writing, or fixes should be disclosed. Reported impact must remain distinct from untested assumptions.

## 日本語での受付案内

通常の不具合・導入の質問は公開Issue、セキュリティ上の問題は上記の非公開「Report a vulnerability」を使ってください。非公開報告にはGitHubへのログインが必要です。OS、分かる範囲の版・ファイル名、期待する動作と実際の症状、架空データでの再現手順を記載してください。秘密情報、生ログ、入力本文、第三者の個人情報は不要です。不明な版は不明のままで構いません。

旧リポジトリは閲覧用アーカイブのまま、開発正本はprivateの `ai-project/ai-usage-bar/` です。この受付の設置は、新しい修正版の配布や利用者数の増加を意味しません。
