using Bogus;
using Cadmus.Core;
using Cadmus.Lexicography.Parts;
using Fusi.Tools.Configuration;
using System;

namespace Cadmus.Seed.Lexicography.Parts;

/// <summary>
/// Seeder for <see cref="LemmaPart"/>.
/// Tag: <c>seed.it.vedph.lexicography.lemma</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.lexicography.lemma")]
public sealed class LemmaPartSeeder : PartSeederBase
{
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

        string lemma = new Faker().Lorem.Word();

        LemmaPart part = new Faker<LemmaPart>()
           .RuleFor(p => p.Lid, f => lemma.ToLowerInvariant())
           .RuleFor(p => p.Forms, f => WordFormsPartSeeder.GetWordForms(
                f.Random.Number(1, 3), f, lemma))
           .Generate();

        SetPartMetadata(part, roleId, item);

        return part;
    }
}
