# AI Usage Bar：破損した参加設定の読込を修正した事例

**2026年11月17日。** Python companionの `TeamLocal.load` で公開0.1.1a4の問題を再現し、修正・回帰検証を経て **0.1.1a5** を同日17:58 JSTにPyPIへ公開した。公開後、未認証で取得した2配布物のhash、内容と出典情報を照合した。

## 確認した問題と修正

公開0.1.1a4の実コードを読み込み、一時ディレクトリの架空の `team-local.json` だけを渡した。

| 入力 | 0.1.1a4で確認した結果 |
| --- | --- |
| 配列のJSON root | `AttributeError` |
| `null` のJSON root | `AttributeError` |
| 数値の `installId` | `TypeError` |
| `pinnedGeneration` が変換できない文字列 | 先に読んだチームと共有先が残り、`joined=True` を返す |
| 配列の `consentedScope` | `TypeError` |

修正では文書と各フィールドの型を確認し、読込・検証に失敗したら未参加の新しい状態へ戻す。途中まで読んだチーム・共有先・鍵・同意の値を引き継がず、正常な設定、既存の整数文字列、任意項目の既定値は維持する。**読込処理は元ファイルを書き換えない**ため、復旧用の内容は保持される。鍵の文字列は不透明な値として扱い、この修正で鍵自体の妥当性は検証しない。

5ケースはローカル読込の問題を示す。実データの漏えい、報告送信、認証突破、利用者への実被害は実証していない。再現ではサービス初期化、鍵生成、署名、実共有先へのアクセスを行っていない。

## 対象と検証状態

旧版は [PyPI 0.1.1a4 metadata](https://pypi.org/pypi/ai-usage-bar-mac/0.1.1a4/json) と照合した [固定sdist](https://files.pythonhosted.org/packages/34/65/3d4765645123b05f96d8a1effd4c825bf77746f62d87bfa0911bfebc1143/ai_usage_bar_mac-0.1.1a4.tar.gz)。

- ファイル：`ai_usage_bar_mac-0.1.1a4.tar.gz`
- sdist SHA-256：`71798947764a3a1ef033bf5fa2e9ca23aee128e35a90ac0422544cbe4b91f6ab`
- 内部の `mac/aiusagebar/team.py` SHA-256：`450358a9d6b0fc52418ae922f43f92d8c6f7b5699f86e2f703f35f4aab8fc230`

修正版は **0.1.1a5**。開発記録のsource commitは `5d229bb93957c793431c0500bab56f944b8be43d`、公開実行のmain commitは `418f95458a69c27b9a90862c016678f0ef124690`。両者の製品treeは同じ `9093b3a9860c82dc611d1d0ec5da2c6e7e4f2604` である。開発リポジトリは非公開だが、対象のPythonソース・検証コードは以下の公開sdistに含まれる。

| 公開ファイル | bytes | SHA-256 |
| --- | ---: | --- |
| [a5 sdist](https://files.pythonhosted.org/packages/c8/62/724f304d17faec99b2cf39083c1dc8ac551822d6a1b6ac9300f6c6e5e8ef/ai_usage_bar_mac-0.1.1a5.tar.gz) | 71,316 | `e567b393e8b0432bb6a8828aa6c069903070a707678e6bc911a5dda6b0ff83c0` |
| [a5 wheel](https://files.pythonhosted.org/packages/95/8c/1476a6183b170e7ef64b103de441dd639eff3be22b3aacd89b4c1aa193cd/ai_usage_bar_mac-0.1.1a5-py3-none-any.whl) | 48,767 | `7883c9a8c5ca3e3e1500fbebec9efb270f4040bc9880fa240b671423f6a4b77f` |

この一覧は[PyPIの版別metadata](https://pypi.org/pypi/ai-usage-bar-mac/0.1.1a5/json)と、公開処理で検証済みの2ファイルを固定したhashに対応する。ローカルWSLで事前検証した別buildのarchive hashとは区別する。

| 検証・配布 | 確認結果 |
| --- | --- |
| 新しい設定回帰12メソッド | 成功。合成入力、正常互換、部分状態の消去、元ファイル保全を確認 |
| Windowsでの関連する限定回帰 | 26実行中25成功・失敗0・1skip。既存のsymlink作成検査が実行環境の制限で未確認 |
| a5のローカルLinux検証 | Ubuntu 24.04.5/WSL2/ext4、Python 3.12.3、144回帰成功・失敗/エラー/skipゼロ、既存36 selftest成功 |
| a5の公開用Linux検証 | 公開run `44051239023` がexact main/sourceを照合し、別buildの144回帰と36 selftestもすべて成功。成功した2配布物だけを公開 |
| a5の公開URL・配布物hash | 版別metadataとCI固定値を照合。上記表に記録 |
| 公開ファイルの未認証取得・出典照合 | 2ファイルのsize/hashが版別metadataと一致、hashはCI固定値とも一致。必須ソース28件とwheel runtimeを照合。公開元・commit・runのフィールドが一致 |
| PyPIの公開ページと使用感への導線 | 通常のブラウザでログアウト状態のa5ページと、インストール案内近くの既存受付リンクの表示を確認 |

出典情報の確認は、PyPI provenance JSONと証明書内のpublisher・commit・run等の記載内容を照合したもの。署名・証明書チェーン・透明性ログ全体の暗号学的な検証は実施していない。最初の自動HTML取得は `Client Challenge` で終了し、その失敗記録を保持した。ページの表示確認は別途、通常のブラウザで行った。

## 第三者が検算する手順

1. 上記の固定URL・版・SHA-256で対象ファイルを識別し、取得したファイルと照合する。[PyPIのSecurity tab](https://pypi.org/project/ai-usage-bar-mac/0.1.1a5/#security)で両ファイルの公開元も確認する。source commit名だけで公開物の同一性を判断しない。
2. a5 sdist内の `mac/test_local_settings.py` が合成入力と期待結果を示す。旧版との差を調べる場合は版ごとに別の作業環境を使い、テストが読み込む `aiusagebar.team.__file__` を照合する。a4 sdistにはこの新しいテストは含まれない。テストと別版のコードを混在させて結果を誤認しない。
3. a5全体検証は、同梱 `docs/python-package-release.md` に従う。既存のbuildツールがある環境で、展開したsdistのrootから次を実行する。

```sh
python tests/check_python_package.py --sdist /absolute/path/to/ai_usage_bar_mac-0.1.1a5.tar.gz
```

このverifierはsdistからwheelを再構築し、一時環境へオフラインでインストールして、検査対象がインストール済みコードであることを確認する。設定回帰12件はサービスや署名を使わない。別の既存selftest36件には合成鍵を使う通常の暗号機能検査が含まれる。144＋36は検証の件数であり、不具合数・利用者数ではない。macOS GUIと実利用者の環境は別途未確認。

## 作業の位置付けと受付

これは個人の管理製品に対する**AI支援による調査・修正・検証**である。別AIエージェントのレビューは人間による外部監査ではない。会社名義の資格、CVE、第三者採用、審査通過、正式なMythos上位権限取得の証拠とは区別する。

配布の利用条件は同梱MITライセンスと第三者noticeに従う。ローカルで検証でき、新しい有料API・サーバー契約は必要ない。Pythonやbuildツールの用意、通常利用時のAIサービス・共有ストレージ等の契約は別に確認する。公開工程の提供者費用は実行前に確認した。この記録は恒久的な費用ゼロを保証しない。

[使用感を送る（匿名・ログイン不要・任意）](https://docs.google.com/forms/d/e/1FAIpQLScucMa18bwOd0HByQKaym4i5IFk9-P_EEkQg2sbT11G-9GJpg/viewform)。短い感想、困った点、不明点、未使用の意見も歓迎する。改善目的で非公開保存し、回答を利用条件にしない。完全匿名性の限界・訂正削除方法は受付を参照。脆弱性の詳細は [GitHubの非公開報告窓口](https://github.com/noah-traderlab/ai-usage-bar-support/security/advisories/new) へ（GitHubアカウントが必要）。秘密情報、生ログ、第三者の個人情報を送らないこと。
