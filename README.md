# Cadmus Lexicography

Essential components for simple lexicographic data.

## Parts

Lexicographic parts:

- [LemmaPart](docs/lemma-part.md): lemma
- [WordFormsPart](docs/word-forms-part.md): word forms
- [WordSensesPart](docs/word-senses-part.md): word senses
- [WordCollocationsPart](docs/word-collocations-part.md): word collocations

## Item

For instance, a lexical entry item might be defined by the following parts (referred to a simple thematic lexicon about juridical terms):

- *identity*:
  - [metadata](https://github.com/vedph/cadmus-general/blob/master/docs/metadata.md)\*
  - [LemmaPart](docs/lemma-part.md) (LEX)\*
  - [links](https://vedph.github.io/cadmus-doc/models/(https://github.com/vedph/cadmus-general/blob/master/docs/fr.pin-links.md).md):`entry` related entries. Related: `lemon:relatedEntry`, `lemon:relation`.
  - [decorated counts](https://github.com/vedph/cadmus-general/blob/master/docs/decorated-counts.md): frequencies.
- *morphology*:
  - [WordFormsPart](docs/word-forms-part.md):`infl` (LEX): inflection.
- *sense*:
  - [WordSensesPart](docs/word-senses-part.md) (LEX): senses.
  - [comment](https://github.com/vedph/cadmus-general/blob/master/docs/comment.md): discussion about the lemma.
  - [collocations](docs/word-collocations-part.md) (LEX): collocations.
  - [links](https://github.com/vedph/cadmus-general/blob/master/docs/fr.pin-links.md):`law` related law articles. One could link article numbers to online references like [Brocardi.it](https://www.brocardi.it) e.g. <https://www.brocardi.it/codice-di-procedura-civile/libro-secondo/titolo-iii/capo-iii/sezione-i/art368.html> for `Art. 368 c.p.c`.
- *editorial*:
  - [note](https://github.com/vedph/cadmus-general/blob/master/docs/note.md)

## TEI Example

This is a generic TEI example for a lexical entry representing the English verb "run" with two senses.

```xml
<entry xml:id="lex001" xmlns="http://www.tei-c.org/ns/1.0">
  <form type="lemma">
    <orth>run</orth>
    <gramGrp>
      <pos>verb</pos>
      <subc>intransitive</subc>
    </gramGrp>
  </form>

  <sense xml:id="sense1">
    <def>To move swiftly on foot so that both feet leave the ground during each stride.</def>
    <usg type="domain">physical activity</usg>
    <usg type="register">neutral</usg>
    <cit type="example">
      <quote>She runs every morning before breakfast.</quote>
    </cit>
  </sense>

  <sense xml:id="sense2">
    <def>To operate or function, especially of machines or systems.</def>
    <usg type="domain">technology</usg>
    <usg type="register">neutral</usg>
    <cit type="example">
      <quote>The engine runs smoothly even in cold weather.</quote>
    </cit>
  </sense>

  <etym>
    <orig>Old English <mentioned>rinnan</mentioned>, later influenced by Old Norse <mentioned>renna</mentioned>.</orig>
  </etym>
</entry>
```
