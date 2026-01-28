# WordCollocationsPart

- 🚩 `it.vedph.lexicography.word-collocations`

- `collocations`\* (`WordCollocation[]`):
  - `tag` (`string`) 📚 `lex-word-collocation-tags`: generic tag. When collocations change according to sense, this tag can be used to specify the sense they refer to.
  - `rank` (`short`)
  - `tokens`\* (`WordCollocationToken[]`): the pattern tokens, 1 per POS tag building the pattern, in pattern order:
    - `pos`\* (`string` 📚 `lex-word-ctoken-pos-tags`, hierarchical): POS tag.
    - `features` (`string[]` 📚 `lex-word-ctoken-features`, hierarchical): features (e.g. number, gender...).
    - `isHead` (`bool`): true if this refers to the head of the pattern.
    - `isOptional` (`bool`): true if optional in pattern. Of course, this is incompatible with `isHead`.
    - `note` (`string`): an optional free text note about token.
  - `examples` (`WordCollocationExampleToken[]`): example tokens for the pattern, each made of one or more literals:
      - `tokenNr` (`short`): the ordinal token number referred to the pattern (1-N), or 0 if it's just connective text.
      - `literals`\* (`WordCollocationLiteral[]`): one or more alternative texts:
        - `value` (`string`): the literal text value.
        - `tag` (`string` 📚 `lex-word-cliteral-tags`, hierarchical): a generic tag.
        - `note` (`string`): an optional free text note.
  - `note` (`string`): a note about pattern.

This is a word collocation pattern. In a traditional dictionary, this is expressed by a sample phrase showing how a word is used in combination with other words, often with some indication of the grammatical structure. For instance, we might have a text like this under a lexical entry for `prefettura` (a NOUN):

```txt
- con i verbi:
  - la Prefettura applica, decide, emette (provvedimenti)
```

This means that:

- the collocation pattern is NOUN + VERB. This is because NOUN is implied by the lexical entry we are defining the collocation for, and the other token is a VERB. The order of these tokens in the pattern is defined by the order of their occurrence in the example text.
- the head token of the pattern is token 1 = NOUN, because it is the lexical entry we are defining the collocation for.
- as examples of token 2 we have 3 alternative literals: `applica`, `decide`, and `emette`; the last one has a note `(provvedimenti)` which clarifies its meaning.

In the above model, this example would be encoded as follows:

- `rank`: 0 (=unspecified)
- 2 pattern tokens:
  - 1 (=`Prefettura`, the head token):
    - `pos`=`NOUN`
    - `isHead`=true
  - 2:
    - `pos`=`VERB`
- 3 example tokens:
  - 1:
    - `literals`:
      - 1:
        - `value`="la" (this is just connective text, so `tokenNr`=0)
  - 2:
    - `tokenNr`=1 (the NOUN)
    - `literals`:
      - 1:
        - `value`="Prefettura"
  - 3:
    - `tokenNr`=2 (the VERB)
    - `literals`: 3 alternative values:
      - 1:
        - `value`="applica"
      - 2:
        - `value`="decide"
      - 3:
        - `value`="emette"
        - `note`="(provvedimenti)"

>Note that there is no 1:1 relationship between pattern tokens and example tokens. Example tokens are portions of the example text in their order. The example text is split into tokens so that each token is either just connective text (not linked to any pattern token) or is linked to a specific pattern token. Example tokens are just portions of text resulting after splitting the example text so that each portion corresponding to a pattern token is isolated. All the other tokens are connective text and do not refer to any pattern token.

In the UI we can provide a quick way to enter such data via a simple DSL for the essential properties, e.g.:

```txt
(1) <NOUN>+VERB: la <Prefettura> [applica, decide, emette (provvedimenti)] -- la <Prefettura> [?ha emesso] il [decreto, provvedimento]
```

This will be automatically parsed into the above structure. Then, you will also be able to visually edit the pattern and add more details, even if this will be rarely required.

This DSL works as follows:

1. **rank** (optional), at the beginning of the line, enclosed in `()`. Default is 0 (unspecified).

2. **tokens** (numbered in the order they are specified):

- tokens are separated by `+` and end with `:`.
- the head token is enclosed in `<>`.
- notes about tokens are enclosed in `()` (e.g. `<NOUN (note)>` or `VERB (note)`).

3. **example tokens** with literals:

- example tokens follow `:`.
- each example token consists of literals, defined as follows:
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
