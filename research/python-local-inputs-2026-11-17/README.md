# AI Usage Bar：不正な入力が後続の正常データを止める問題の修正

**2026年11月17日。** 2つの入力処理の問題を修正し、回帰検証を経て **0.1.1a6** を同日18:51 JSTにPyPIへ公開した。公開後、未認証で取得した2配布物のsize・hash・内容と出典情報を照合し、通常のログアウト状態のブラウザで公開ページと使用感への導線を確認した。

2026年11月17日。公開Python companion 0.1.1a5と同じコードに有限の架空データを渡し、2つの入力処理の問題を確認した。個人が管理する製品の、AI支援による保守調査である。実利用者の申告や実ログによる発見ではない。

| 問題 | 修正前の観測 | 修正 |
| --- | --- | --- |
| Claude JSONLの構造が不正な行 | `message` が文字列の123 bytesの行を、正常13 tokensと20 tokensの間に挿入すると、後続20 tokensが未集計になった。別ファイル7 tokensは残り、対照40 tokens/3件が20 tokens/2件となった。`scan()` は例外を呼出し側へ返さない | 不正な文書・message・識別子を行単位で除外し、後続と別ファイルを継続。プロンプトの不正なattachmentやtextも保守的に除外する |
| フレンド残り率の数値変換 | 16 KiB上限内のJSONでも、401桁の有限整数を残り率へ入れると `OverflowError` が発生。一時フォルダの一覧処理も中断した | その値の変換不能だけを不明として扱い、正常な他の窓・他のファイルを保持する |

修正後の限定回帰では、Claudeログ12メソッドとフレンド値7メソッドが成功した。元のClaude再現例は40 tokens/3件に戻った。修正前のfailure/error数にはsubTestが含まれ、バグ件数や利用者数ではない。

Claudeの通常のトークン・キャッシュ集計、重複排除、セッションとモデル、親子関係、人間のプロンプト選別とtool/automation/sidechain/subagentの除外を対照で確認した。不正な識別子は文字列化せず行を除外する。欠落・null・空文字列の識別子には既存の互換性を維持する。フレンドの正常値の範囲処理、時刻・ID・サイズ・窓数の制約は変更せず、読込時の元JSONのbytesも保持する。

## 対象と検算

修正前の固定sdist：`ai_usage_bar_mac-0.1.1a5.tar.gz`、SHA-256 `e567b393e8b0432bb6a8828aa6c069903070a707678e6bc911a5dda6b0ff83c0`。[a5の版別metadata](https://pypi.org/pypi/ai-usage-bar-mac/0.1.1a5/json)。対象の公開 `logs.py` と `friends.py` が修正前ソースと改行形式以外は一致することを確認した。

修正版source commit：`8f79b0c4928e94489a6c89c1fe9e6c97775a4d5d`、公開実行のmain commit：`ad0f66bd8bf5c54dd2f3a059719d154bae0b8e52`、製品tree：`71bea82a178a5d0dc8db10c0e2630865a3faaca6`。開発repositoryは非公開だが、対象のPythonソース・検証コードは以下の公開sdistに含まれる。

| 検証・配布 | 現在確認できた結果 |
| --- | --- |
| 新規19回帰メソッド | Claude構造12・フレンド値7、限定検証ですべて成功 |
| a6のローカルLinux全体検証 | Ubuntu 24.04.5/WSL2/ext4、Python 3.12.3。163回帰成功、失敗/エラー/skipゼロ、既存36 selftest成功 |
| a6の公開用CI | run `44059250444`、exact source/main照合後の別buildで163回帰成功、失敗/エラー/skipゼロ、既存36 selftest成功。installed wheelを対象に確認 |
| PyPIへのupload工程 | CIのpublish jobは2026-11-17 09:51:25 UTC（18:51:25 JST）にsuccessで終了 |
| 版別metadata・公開ファイルの未認証取得 | 2ファイルのsize/hashが版別metadataと一致し、hashはCI固定値とも一致。sdist内の必須ソース30件、wheel内のruntime12件とライセンスを照合。publisher・commit・runの出典フィールドが一致 |
| 公開ページと使用感への導線 | 通常のChromeでログアウト状態のa6ページ、版表示とインストール案内近くの既存受付リンクを確認 |

公開ファイルと[PyPIの版別metadata](https://pypi.org/pypi/ai-usage-bar-mac/0.1.1a6/json)を照合した固定URL・bytes・SHA-256は次のとおり。CIが検証後に固定した値とも一致する。ローカルWSLの事前buildとは別のarchive hashである。

| 公開ファイル | bytes | SHA-256 |
| --- | ---: | --- |
| [a6 sdist](https://files.pythonhosted.org/packages/c0/aa/91ad5dfff895198a724592d0409fb58bd9e6fbb4f2e9cc3738275f7f273a/ai_usage_bar_mac-0.1.1a6.tar.gz) | 76,661 | `f082db71d5db045eb43f4fa7c815c85b4cb6510341941b6fa26ae1bc22d3c538` |
| [a6 wheel](https://files.pythonhosted.org/packages/3f/b1/71674a6d6fa3ab7ec36e63c42489368e590e5c32885facfc430fafcac3bf/ai_usage_bar_mac-0.1.1a6-py3-none-any.whl) | 49,197 | `95af6e558f89f60043b14185a420e0de8eb10ff3adea67a3a7c36b08b9d3be27` |

版別metadataのupload日時はwheelが `2026-11-17T09:51:21.352799Z`、sdistが `2026-11-17T09:51:22.791046Z`。出典確認は公開provenance JSONと証明書内フィールドの比較であり、署名・証明書チェーン・透明性ログ全体の暗号学的検証は実施していない。

自動HTML取得は `Client Challenge` となり、ページ確認に失敗した記録を保持している。これは配布物・ソース・出典フィールドの成功結果と分けた。公開ページの表示は、通常のChromeで別途確認した。フォームの送信や設定変更は行っていない。

## 第三者が検算する手順

上記の固定URL・版・SHA-256で対象ファイルを識別し、取得物を照合する。[a6 Security tab](https://pypi.org/project/ai-usage-bar-mac/0.1.1a6/#security)で両ファイルの公開元も確認する。source commit名や版番号だけで公開物の同一性を判断しない。

a6 sdist内の `mac/test_claude_log_shapes.py` と `mac/test_friend_values.py` に有限の合成入力と期待結果が含まれる。a5 sdistにはこの新規19回帰は含まれない。旧版との差を調べる場合は別の作業環境を使い、読み込む実装の版とパスを確認する。

全体検証は同梱 `docs/python-package-release.md` に従う。必要な既存buildツールがある環境で、確認済みsdistを新しいディレクトリへ展開し、そのrootから実行する。

```sh
python tests/check_python_package.py --sdist /absolute/path/to/ai_usage_bar_mac-0.1.1a6.tar.gz
```

verifierはsdistからwheelを再構築して一時環境へオフラインでインストールし、検査対象がインストール済み実装であることを確認する。成功条件は既存144＋新規19＝163回帰で失敗・エラー・skipなし、および既存36 selftest成功。新規19回帰はサービス・鍵生成・署名を使用しない。別の既存36 selftestには合成鍵による通常の暗号機能検査が含まれる。件数はバグ数・利用者数ではなく、macOS GUIの検証も意味しない。

## 範囲と受付

2問題の再現と新規回帰は有限の架空JSONL・JSONと明示的な一時ローカルフォルダを使用した。実ログ、実フレンドのデータ、実共有フォルダ、サービスやGUI、同期・送信は使っていない。全体検証の既存selftestの範囲は上記と区別する。macOS GUI、実利用者の環境、発生頻度、実被害は未確認。全種類の入力・全製品経路の安全性を保証するものではない。

別AIエージェントのレビューは人間の外部監査ではない。個人・AI支援による保守実績として記録し、CVE、第三者採用、組織資格、CVP審査通過、正式なMythos上位権限の取得とは区別する。配布の利用条件は同梱MITと第三者noticeに従い、通常利用のAIサービス等の契約は別途必要になることがある。

[使用感を送る（匿名・ログイン不要・任意）](https://docs.google.com/forms/d/e/1FAIpQLScucMa18bwOd0HByQKaym4i5IFk9-P_EEkQg2sbT11G-9GJpg/viewform)。短い感想・困った点・不明点・未使用の意見も歓迎。改善目的で非公開保存し、回答を利用条件にしない。完全匿名性の限界と訂正削除は受付を参照。秘密情報や生ログは送らず、脆弱性は[非公開報告窓口](https://github.com/noah-traderlab/ai-usage-bar-support/security/advisories/new)へ（GitHubアカウントが必要）。
