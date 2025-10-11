# LemmaPart

Related: `<form type="lemma">`, `lemon:LexicalEntry`, `lemon:canonicalForm`.

- `lid`\*: the calculated lexicographic ID for this form. The lexicographic ID can be defined in different ways according to the project. In the case of Minerva it could just be the word form with only lowercase letters and no diacritics, followed by the homograph number if greater than 0.
- `homograph` (short): the homograph number. This is usually 0. Set to 1 or more to define the order of homograph forms. Related: `<hom>`, `lemon:sense` (for disambiguation).
- `forms`\* ([WordForm[]](word-forms-part.md))
