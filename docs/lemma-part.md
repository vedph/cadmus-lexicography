# LemmaPart

Related: `<form type="lemma">`, `lemon:LexicalEntry`, `lemon:canonicalForm`.

- `lid`\*: the calculated lexicographic ID for this form. The lexicographic ID can be defined in different ways according to the project. In the case of Minerva it could just be the word form with only lowercase letters and no diacritics, followed by the homograph number if greater than 0.
- `homograph` (short): the homograph number. This is usually 0. Set to 1 or more to define the order of homograph forms. Related: `<hom>`, `lemon:sense` (for disambiguation).
- `forms`\* (`WordForm[]`):
  - `type`\* (`string` 📚 `lex-word-form-types`): the form's type (e.g. written vs. pronounced).
  - `prefix`: any text prefixed to the lemma (e.g. `to` before an English verb).
  - `value`\*: the lemma's value.
  - `suffix`: any text suffixed to the lemma (e.g. e.g. a particle after a phrasal verb, like `off` in `log off`; or the article representing the genre in a Greek).
  - `pos` (`string`, 📚 `lex-pos-tags`, hierarchical): the part of speech. This can also be composite, like e.g. an UPOS + an XPOS in Universal Dependencies. Related `<gramGrp>/<pos>`, `lemon:partOfSpeech`.
  - `features` (`string[]` 📚 `lex-word-form-features`, hierarchical): grammatical features.
  - `tags` (`string[]` 📚 `lex-word-form-tags`): usage or similar tags (e.g. vulgar, archaic, juridical...). Related: `<usg>`, `lemon:context`, `lemon:usage`.
  - `note` (`string`): an optional miscellaneous note strictly related to the lemma. Related: `<note>`.
