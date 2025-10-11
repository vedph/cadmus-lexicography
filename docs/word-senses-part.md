# WordSensesPart

Related: `<sense>/<def>`, `lemon:sense`, `lemon:definition`.

- `senses`\* (`WordSense[]`):
  - `eid` (`string`)
  - `tags` (`string[]` 📚 `lex-word-sense-tags`)
  - `literal` (`string` MD): literal definition.
  - `relations` (`string[]`): semantic relations expressed via identifiers using some ontology (e.g. SKOS, LexInfo) identifiers. Related: `<relation>`, `<ref>`, `lemon:relatedSense`, `lemon:relation`.
  - `examples` (`WordSenseExample[]`). Related: `<cit>`, `<quote>`, `lemon:example`.
    - `tags` (`string[]` 📚 `lex-word-sense-tags`)
    - `text`\* (`string`, MD): the example's text.
    - `explanation` (`string`, MD): the example's explanation, translation, etc.
    - `citation` (`string`): some conventionally structured citation form for the text (e.g. "Aen. 1,23").
    - `note` (`string`)
