# WordFormsPart

- 🚩 `it.vedph.lexicography.word-forms`

Related: `<form type="inflected">`, `lemon:inflectedForm`. Role-dependent thesauri.

- `forms`\* (`WordForm[]`):
  - `type`\* (`string` 📚 `lex-word-form-types`): the form's type (e.g. written vs. pronounced).
  - `prefix` (`string` 📚 `lex-word-form-prefixes`): an optional prefix for this form, e.g. a preposition like `to` in `to take`.
  - `value`\* (`string`): the form's value.
  - `suffix` (`string` 📚 `lex-word-form-suffixes`): an optional suffix for this form, e.g. a particle after a phrasal verb, e.g. `off` in `to take off`.
  - `pos` (`string`, 📚 `lex-pos-tags`, hierarchical): the part of speech tag. This can be composite, e.g. an UPOS + dot + XPOS when using Universal Dependencies tags. Related: TEI `gramGrp/pos`, Lemon `partOfSpeech`.
  - `features` (`string[]` 📚 `lex-word-form-features`, hierarchical): grammatical features, e.g. number, genre, tense, etc. or any other kind of feature.
  - `tags` (`string[]` 📚 `lex-word-form-tags`, hierarchical): usage or similar tags (e.g. vulgar, archaic, juridical...). Related: `<usg>`, `lemon:context`, `lemon:usage`.
  - `note` (`string`): an optional miscellaneous note strictly related to the lemma. Related: TEI `note`.
