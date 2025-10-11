# WordCollocationsPart

- 🚩 `it.vedph.lexicography.word-collocations`

- `collocations`\* (`WordCollocation[]`):
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

- rank: 0 (unspecified)
- tokens:
  - 1 (=Prefettura, the head):
    - pos=noun
    - isHead=true
  - 2:
    - pos=verb
- examples:
  - 1:
    - literals:
      - 1:
        - value="la"
  - 2:
    - tokenNr=1
    - literals:
      - 1:
        - value="Prefettura"
  - 3:
    - tokenNr=2
    - literals:
      - 1:
        - value="applica"
      - 2:
        - value="decide"
      - 3:
        - value="emette"
        - note="(provvedimenti)"

In the UI we can provide a quick way to enter such data via a simple DSL for the essential properties, e.g.:

```txt
NOUN+VERB: la [[Prefettura]] [applica, decide, emette (provvedimenti)]
```

This will be automatically parsed into the above structure. Then, you will also be able to visually edit the pattern and add more details, even if this will be rarely required.
