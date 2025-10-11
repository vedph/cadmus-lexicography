﻿using System;
using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Cadmus.Refs.Bricks;
using Fusi.Tools.Configuration;

namespace Cadmus.Lexicography.Parts;

/// <summary>
/// Word's senses.
/// <para>Tag: <c>it.vedph.lexicography.word-senses</c>.</para>
/// </summary>
[Tag("it.vedph.lexicography.word-senses")]
public sealed class WordSensesPart : PartBase
{
    /// <summary>
    /// Gets or sets the senses.
    /// </summary>
    public List<WordSense> Senses { get; set; } = [];

    /// <summary>
    /// Get all the key=value pairs (pins) exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>The pins: <c>tot-count</c> and a collection of pins</returns>
    public override IEnumerable<DataPin> GetDataPins(IItem? item = null)
    {
        DataPinBuilder builder = new();

        builder.Set("tot", Senses?.Count ?? 0, false);

        if (Senses?.Count > 0)
        {
            foreach (WordSense sense in Senses)
            {
                if (!string.IsNullOrEmpty(sense.Eid))
                    builder.AddValue("sense-id", sense.Eid);

                if (sense.RelatedIds?.Count > 0)
                {
                    foreach (AssertedCompositeId id in sense.RelatedIds)
                    {
                        if (!string.IsNullOrEmpty(id.Target?.Gid))
                            builder.AddValue("related-id", id.Target.Gid);
                    }
                }
            }
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
               "The total count of senses."),
            new DataPinDefinition(DataPinValueType.String,
                "sense-id",
                "The IDs of the senses."),
            new DataPinDefinition(DataPinValueType.String,
                "related-id",
                "The IDs of the related senses.")
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

        sb.Append("[WordSenses]");

        if (Senses?.Count > 0)
        {
            sb.Append(' ');
            int n = 0;
            foreach (WordSense entry in Senses)
            {
                if (++n > 3) break;
                if (n > 1) sb.Append("; ");
                sb.Append(entry);
            }
            if (Senses.Count > 3)
                sb.Append("...(").Append(Senses.Count).Append(')');
        }

        return sb.ToString();
    }
}
