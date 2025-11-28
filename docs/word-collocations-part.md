# WordCollocationsPart

- 🚩 `it.vedph.lexicography.word-collocations`

- `collocations`\* (`WordCollocation[]`):
  - `tag` (`string`) 📚 `lex-word-collocation-tags`: generic tag. When collocations change according to sense, this tag can be used to specify the sense they refer to.
  - `rank` (`short`)
  - `tokens`\* (`WordCollocationToken[]`):
    - `pos`\* (`string` 📚 `lex-word-ctoken-pos-tags`, hierarchical): POS tag.
    - `features` (`string[]` 📚 `lex-word-ctoken-features`, hierarchical): features.
    - `isHead` (`bool`): true if this refers to the head of the pattern.
    - `isOptional` (`bool`): true if optional in pattern.
    - `note` (`string`): note about token.
  - `examples` (`WordCollocationExample[]`): pattern examples:
    - `tokenNr` (`short`): the ordinal token number referred to the pattern, or 0 if it's just connective text.
    - `literals`\* (`WordCollocationLiteral[]`): one or more alternative texts:
      - `value` (`string`): the literal text value.
      - `tag` (`string` 📚 `lex-word-cliteral-tags`, hierarchical)
      - `note` (`string`)
  - `note` (`string`): note about pattern.

Example: say we want to represent the collocation of "Prefettura" represented by this text: "Con i verbi (2): la Prefettura applica, decide, emette (provvedimenti)". The pattern model would have:

- `rank`: 0 (=unspecified)
- 2 tokens:
  - 1 (=`Prefettura`, the head token):
    - `pos`=`NOUN`
    - `isHead`=true
  - 2:
    - `pos`=`VERB`
- 1 example:
  - 1:
    - `literals`:
      - 1:
        - `value`="la"
  - 2:
    - `tokenNr`=1
    - `literals`:
      - 1:
        - `value`="Prefettura"
  - 3:
    - `tokenNr`=2
    - `literals`:
      - 1:
        - `value`="applica"
      - 2:
        - `value`="decide"
      - 3:
        - `value`="emette"
        - `note`="(provvedimenti)"

In the UI we can provide a quick way to enter such data via a simple DSL for the essential properties, e.g.:

```txt
(1) <NOUN>+VERB: la <Prefettura> [applica, decide, emette (provvedimenti)]
```

This will be automatically parsed into the above structure. Then, you will also be able to visually edit the pattern and add more details, even if this will be rarely required.

This DSL works as follows:

1. **rank** (optional), at the beginning of the line, enclosed in `()`. Default is 0 (unspecified).

2. **tokens** (numbered in the order they are specified):

- tokens are separated by `+` and end with `:`.
- the head token is enclosed in `<>`.
- notes about tokens are enclosed in `()` (e.g. `<NOUN (note)>` or `VERB (note)`).

3. **examples** with literals:

- examples follow `:` and are separated by newlines.
- each example consists of literals, defined as follows:
  - the literal corresponding to the head token is enclosed in `<>`. It can have an optional note enclosed in `()` after itself (e.g. `<Prefettura (note)>`).
  - other literals corresponding to the other tokens are enclosed in `[]`; in it, alternative literals are separated by `,`. Each literal can have an optional note enclosed in `()` after itself. E.g. `[applica, decide, emette (provvedimenti)]` corresponds to 1 token with 3 alternative literals: `applica`, `decide`, and `emette` with note `provvedimenti`. If `[` is followed by `?`, this is an optional literal (e.g. `[?example]` or `[?example, alternative example]`).
  - all the other tokens separated by spaces are simple literals (e.g. `la`).

To keep it simple for end users, other data about the collocation are not specified by the DSL. You can add them later via the visual editor if needed.

So, from `(1) <NOUN>+VERB: la <Prefettura> [applica, decide, emette (provvedimenti)]` we would have:

```json
{
  "rank": 1,
  "tokens": [
    {
      "pos": "NOUN",
      "isHead": true
    },
    {
      "pos": "VERB"
    }
  ],
  "examples": [
    {
      "tokenNr": 0,
      "literals": [
        {
          "value": "la"
        }
      ]
    },
    {
      "tokenNr": 1,
      "literals": [
        {
          "value": "Prefettura"
        }
      ]
    },
    {
      "tokenNr": 2,
      "literals": [
        {
          "value": "applica"
        },
        {
          "value": "decide"
        },
        {
          "value": "emette",
          "note": "provvedimenti"
        }
      ]
    }
  ]
}
```
