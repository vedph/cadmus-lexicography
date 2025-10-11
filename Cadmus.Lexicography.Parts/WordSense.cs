using Cadmus.Refs.Bricks;
using System.Collections.Generic;

namespace Cadmus.Lexicography.Parts;

/// <summary>
/// A sense in a word's set of senses.
/// </summary>
public class WordSense
{
    /// <summary>
    /// Optional entity ID to identify this sense. This can be used
    /// for cross-references, semantic projection, etc.
    /// </summary>
    public string? Eid { get; set; }

    /// <summary>
    /// The tags, e.g. <c>archaic</c>, <c>informal</c>, etc. Usually from
    /// hierarchical thesaurus <c>lex-word-sense-tags</c>. Related: TEI
    /// <c>usg</c>, Lemon <c>usage</c>, <c>context</c>.
    /// </summary>
    public List<string> Tags { get; set; } = [];

    /// <summary>
    /// The literal definition of the sense. Usually a Markdown text.
    /// </summary>
    public string Definition { get; set; } = "";

    /// <summary>
    /// A general note about this sense. Related to TEI <c>note</c>.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Related senses, usually expressed via semantic identifiers from some
    /// ontologies (e.g. SKOS, LexInfo); using a structured ID allows to
    /// also use internal links. Related: TEI <c>relation</c>, <c>ref</c>,
    /// <c>lemon:relatedSense</c>, <c>lemon:relation</c>.
    /// </summary>
    public List<AssertedCompositeId>? RelatedIds { get; set; }

    /// <summary>
    /// Examples for this sense.
    /// </summary>
    public List<WordSenseExample>? Examples { get; set; }
}
