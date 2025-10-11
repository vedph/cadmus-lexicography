using Bogus;
using Cadmus.Core;
using Cadmus.Lexicography.Parts;
using Cadmus.Refs.Bricks;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Lexicography.Parts;

/// <summary>
/// Seeder for <see cref="WordSensesPart"/>.
/// Tag: <c>seed.it.vedph.lexicography.word-senses</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.lexicography.word-senses")]
public sealed class WordSensesPartSeeder : PartSeederBase
{
    private static List<WordSense> GetWordSenses(int count, Faker f)
    {
        List<WordSense> senses = [];
        string[] tagPool = ["archaic", "informal", "regional", "technical", "literary"];

        for (int i = 0; i < count; i++)
        {
            WordSense sense = new()
            {
                Eid = f.Random.Bool(0.35f) ? Guid.NewGuid().ToString() : null,
                Tags = [],
                Definition = f.Lorem.Sentences(f.Random.Int(1, 3)),
                Note = f.Random.Bool(0.25f) ? f.Lorem.Sentence() : null
            };

            // add 0..2 tags
            if (f.Random.Bool(0.6f))
            {
                int tcount = f.Random.Int(1, 2);
                for (int t = 0; t < tcount; t++)
                {
                    string tag = f.PickRandom(tagPool);
                    if (!sense.Tags.Contains(tag)) sense.Tags.Add(tag);
                }
            }

            // optionally related ids (one or two)
            if (f.Random.Bool(0.2f))
            {
                sense.RelatedIds = [];
                int rcount = f.Random.Int(1, 2);
                for (int r = 0; r < rcount; r++)
                {
                    sense.RelatedIds.Add(new AssertedCompositeId
                    {
                        Target = new PinTarget
                        {
                            Gid = f.Internet.Url()
                        }
                    });
                }
            }

            // optionally examples
            if (f.Random.Bool(0.6f))
            {
                sense.Examples = [];
                int exCount = f.Random.Int(1, 2);
                for (int e = 0; e < exCount; e++)
                {
                    WordSenseExample ex = new()
                    {
                        Tags = f.Random.Bool(0.25f)
                            ? [f.PickRandom(tagPool)]
                            : null,
                        Text = f.Lorem.Sentence(),
                        Explanation = f.Random.Bool(0.25f)
                            ? f.Lorem.Sentence(2)
                            : null,
                        Citation = f.Random.Bool(0.25f) ?
                            $"{f.Person.LastName} " +
                            $"{f.Random.Number(1, 10)},{f.Random.Number(1, 100)}"
                            : null,
                        Note = f.Random.Bool(0.15f) ? f.Lorem.Sentence(2) : null
                    };
                    sense.Examples.Add(ex);
                }
            }

            senses.Add(sense);
        }

        return senses;
    }

    /// <summary>
    /// Creates and seeds a new part.
    /// </summary>
    /// <param name="item">The item this part should belong to.</param>
    /// <param name="roleId">The optional part role ID.</param>
    /// <param name="factory">The part seeder factory. This is used
    /// for layer parts, which need to seed a set of fragments.</param>
    /// <returns>A new part or null.</returns>
    /// <exception cref="ArgumentNullException">item or factory</exception>
    public override IPart? GetPart(IItem item, string? roleId,
        PartSeederFactory? factory)
    {
        ArgumentNullException.ThrowIfNull(item);

        WordSensesPart part = new Faker<WordSensesPart>()
           .RuleFor(p => p.Senses, f => GetWordSenses(f.Random.Number(1, 3), f))
           .Generate();

        SetPartMetadata(part, roleId, item);

        return part;
    }
}
