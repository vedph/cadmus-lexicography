using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Configuration;

namespace Cadmus.Lexicography.Parts;

/// <summary>
/// Words collocations part.
/// <para>Tag: <c>it.vedph.lexicography.word-collocations</c>.</para>
/// </summary>
[Tag("it.vedph.lexicography.word-collocations")]
public sealed class WordCollocationsPart : PartBase
{
    /// <summary>
    /// Gets or sets the collocations.
    /// </summary>
    public List<WordCollocation> Collocations { get; set; } = [];

    /// <summary>
    /// Get all the key=value pairs (pins) exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>The pins: <c>tot-count</c> and a collection of pins.</returns>
    public override IEnumerable<DataPin> GetDataPins(IItem? item = null)
    {
        DataPinBuilder builder = new();

        builder.Set("tot", Collocations?.Count ?? 0, false);

        if (Collocations?.Count > 0)
        {
            HashSet<string> pos = [];
            foreach (WordCollocation collocation in Collocations)
            {
                if (collocation.Tokens.Count > 0)
                {
                    pos.Add(string.Join("+",
                        collocation.Tokens.Select(t => t.Pos)));
                }
            }
            builder.AddValues("pos", pos);
        }

        return builder.Build(this);
    }

    /// <summary>
    /// Gets the definitions of data pins used by the implementor.
    /// </summary>
    /// <returns>Data pins definitions.</returns>
    public override IList<DataPinDefinition> GetDataPinDefinitions()
    {
        return new List<DataPinDefinition>(
        [
            new DataPinDefinition(DataPinValueType.Integer,
               "tot-count",
               "The total count of collocations."),
            new DataPinDefinition(DataPinValueType.String,
               "pos",
               "The part-of-speech tag sequence of a collocation, " +
               "i.e. the concatenation with '+' of the POS tags of its tokens.",
               "M"),
        ]);
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        StringBuilder sb = new();

        sb.Append("[WordCollocations]");

        if (Collocations?.Count > 0)
        {
            sb.Append(' ');
            int n = 0;
            foreach (var entry in Collocations)
            {
                if (++n > 3) break;
                if (n > 1) sb.Append("; ");
                sb.Append(entry);
            }
            if (Collocations.Count > 3)
                sb.Append("...(").Append(Collocations.Count).Append(')');
        }

        return sb.ToString();
    }
}
