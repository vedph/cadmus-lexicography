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
  - [asserted historical dates](https://github.com/vedph/cadmus-general/blob/master/docs/asserted-historical-dates.md): date of first attestation and other eventual dates.
- *morphology*:
  - [WordFormsPart](docs/word-forms-part.md) (LEX): inflection.
- *sense*:
  - [WordSensesPart](docs/word-senses-part.md) (LEX): senses.
  - [comment](https://github.com/vedph/cadmus-general/blob/master/docs/comment.md): discussion about the lemma.
  - [collocations](docs/word-collocations-part.md) (LEX): collocations.
  - [links](https://github.com/vedph/cadmus-general/blob/master/docs/fr.pin-links.md):`law` related law articles. One could link article numbers to online references like [Brocardi.it](https://www.brocardi.it) e.g. <https://www.brocardi.it/codice-di-procedura-civile/libro-secondo/titolo-iii/capo-iii/sezione-i/art368.html> for `Art. 368 c.p.c`.
- *editorial*:
  - [references](https://github.com/vedph/cadmus-general/blob/master/docs/doc-references.md): generic documental references for this entry.
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

## Lemon Example

This Turtle example represents the same lexical entry using the Lemon model, incorporating `LexInfo` for linguistic annotations.

```turtle
@prefix lemon: <http://www.lemon-model.net/lemon#> .
@prefix lexinfo: <http://www.lexinfo.net/ontology/2.0/lexinfo#> .
@prefix ontolex: <http://www.w3.org/ns/lemon/ontolex#> .
@prefix vartrans: <http://www.w3.org/ns/lemon/vartrans#> .
@prefix dct: <http://purl.org/dc/terms/> .
@prefix ex: <http://example.org/lexicon#> .

ex:lex001 a ontolex:LexicalEntry ;
    ontolex:canonicalForm [
        ontolex:writtenRep "run"@en ;
        lexinfo:partOfSpeech lexinfo:verb ;
        lexinfo:subcategorizationFrame lexinfo:IntransitiveFrame
    ] ;
    ontolex:sense ex:sense1, ex:sense2 ;
    dct:source "Old English 'rinnan', later influenced by Old Norse 'renna'" .

ex:sense1 a ontolex:LexicalSense ;
    ontolex:definition "To move swiftly on foot so that both feet leave the ground during each stride."@en ;
    lexinfo:domain lexinfo:PhysicalActivity ;
    lexinfo:register lexinfo:neutralRegister ;
    ontolex:example "She runs every morning before breakfast."@en .

ex:sense2 a ontolex:LexicalSense ;
    ontolex:definition "To operate or function, especially of machines or systems."@en ;
    lexinfo:domain lexinfo:Technology ;
    lexinfo:register lexinfo:neutralRegister ;
    ontolex:example "The engine runs smoothly even in cold weather."@en .
```

## History

### 1.0.0

- 2025-11-24: ⚠️ upgraded to NET 10.

### 0.0.1

- 2025-10-22: more thesauri.
