# WordSensesPart

Related: `<sense>/<def>`, `lemon:sense`, `lemon:definition`.

- `senses`\* (`WordSense[]`):
  - `eid` (`string`): optional entity ID to identify this sense. This can be used for cross-references, semantic projection, etc.
  - `tags` (`string[]` 📚 `lex-word-sense-tags`, hierarchical)
  - `definition` (`string` MD): literal definition.
  - `note` (`string`)
  - `relatedIds` (`AssertedCompositeId[]`): related senses, usually expressed via semantic identifiers from some ontologies (e.g. SKOS, LexInfo); using a structured ID allows to also use internal links. Related: `<relation>`, `<ref>`, `lemon:relatedSense`, `lemon:relation`.
  - `examples` (`WordSenseExample[]`). Related: `<cit>`, `<quote>`, `lemon:example`.
    - `tags` (`string[]` 📚 `lex-word-sense-tags`, hierarchical)
    - `text`\* (`string`, MD): the example's text.
    - `explanation` (`string`, MD): the example's explanation, translation, etc.
    - `citation` (`string`): some conventionally structured citation form for the text (e.g. "Aen. 1,23").
    - `note` (`string`)
