# Security policy for AI Usage Bar

## Private reporting

For suspected vulnerabilities in AI Usage Bar, use [GitHub private vulnerability reporting](https://github.com/noah-traderlab/ai-usage-bar-support/security/advisories/new) in this support repository. A GitHub account is required. Reports are shared privately with repository administrators and authorized security collaborators, rather than posted as public issues. Do not add collaborators or publish a report without considering the information they would receive.

If the private reporting form is unavailable, do not send details through public issues or comments. You may open an ordinary issue saying only that the private form is unavailable, with no vulnerability details or sensitive attachments.

Include:

1. The affected component, OS, and version, file name, or source revision if known.
2. Expected behavior, actual behavior, and the security boundary you believe is affected.
3. Minimal reproducible steps using synthetic data only.
4. Observed impact, with hypotheses and untested cases clearly marked.
5. Whether AI assisted the investigation or report.

Do not include credentials, API keys, access tokens, login links, private AI prompts, shared-team files, personal data, or raw production logs. Do not test systems or accounts you are not authorized to test.

## Assessment and disclosure

The maintainer is [@noah-traderlab](https://github.com/noah-traderlab). The maintainer reviews reports for reproducibility, impact, affected versions, and available fixes. A submitted report is not automatically accepted as a confirmed vulnerability. Private reports are not deliberately published as part of routine intake; public disclosure, researcher credit, and any advisory or CVE request require separate assessment and coordination. There is no paid bounty or guaranteed response deadline.

This repository is an intake channel for the existing AI Usage Bar project.

## Maintenance scope

Reports concerning the [historical public source](https://github.com/noah-traderlab/ai-usage-bar), Windows 0.1.0, and the PyPI macOS companion 0.1.0 can be submitted here. Please identify the actual file or version used; unknown versions remain unknown until checked. Reports on standalone macOS development previews should say so explicitly. Receiving a report is not a guarantee that a particular old release will receive a backport.

Active development is in the private `noah-traderlab/ai-project/ai-usage-bar/` directory. This does not grant reporters access to that repository. The original public source archive remains archived and is not itself a reporting channel. A private source change is not a distributed fix until a corresponding release has actually been published and verified.
