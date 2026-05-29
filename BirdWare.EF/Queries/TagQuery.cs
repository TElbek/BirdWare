using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BirdWare.EF.Queries
{
    public class TagQuery(BirdWareContext birdWareContext) : ITagQuery
    {
        public List<Tag> GetTagList()
        {
            var tagList = TagListFactory();
            PopulateAarstal(tagList);
            PopulateMaaneder(tagList);
            PopulateLokaliteter(tagList);
            PopulateKommuner(tagList);
            PopulateDistance(tagList);
            PopulateRegioner(tagList);
            PopulateFamilier(tagList);
            PopulateGrupper(tagList);
            PopulateArter(tagList);
            PopulateSaeson(tagList);
            PopulateSenesteNAar(tagList);
            PopulateLand(tagList);

            return SortByName(tagList);
        }

        public List<Tag> GetTagListFugletur()
        {
            var tagList = TagListFactory();
            PopulateAarstal(tagList);
            PopulateMaaneder(tagList);
            PopulateLokaliteter(tagList);
            PopulateKommuner(tagList);
            PopulateRegioner(tagList);
            PopulateDistance(tagList);
            PopulateSaeson(tagList);
            PopulateSenesteNAar(tagList);
            PopulateLand(tagList);

            return SortByName(tagList);
        }

        private static List<Tag> TagListFactory() => [];

        private static List<Tag> SortByName(List<Tag> tagList) => [.. tagList.OrderBy(r => r.Name)];

        private static void PopulateLand(List<Tag> tagList) => tagList.Add(new Tag { Id = 1, Name = "Danmark", TagType = TagTypes.Land });

        private static void PopulateSenesteNAar(List<Tag> tagList)
        {
            tagList.Add(new Tag { Id = 1, Name = "Seneste år", TagType = TagTypes.SenesteNÅr, SomeValue = 1 });
            tagList.Add(new Tag { Id = 2, Name = "Seneste 2 år", TagType = TagTypes.SenesteNÅr, SomeValue = 2 });
            tagList.Add(new Tag { Id = 3, Name = "Seneste 5 år", TagType = TagTypes.SenesteNÅr, SomeValue = 5 });
            tagList.Add(new Tag { Id = 4, Name = "Seneste 10 år", TagType = TagTypes.SenesteNÅr, SomeValue = 10 });
        }

        private static void PopulateSaeson(List<Tag> tagList)
        {
            tagList.Add(new Tag { Id = 1, Name = "Forår", TagType = TagTypes.SaesonForaar });
            tagList.Add(new Tag { Id = 2, Name = "Sommer", TagType = TagTypes.SaesonSommer });
            tagList.Add(new Tag { Id = 3, Name = "Efterår", TagType = TagTypes.SaesonEfteraar });
            tagList.Add(new Tag { Id = 4, Name = "Vinter", TagType = TagTypes.SaesonVinter });
            tagList.Add(new Tag { Id = 5, Name = "Vinterhalvår", TagType = TagTypes.HalvårVinter });
            tagList.Add(new Tag { Id = 6, Name = "Sommerhalvår", TagType = TagTypes.HalvårSommer });
        }

        private void PopulateArter(List<Tag> tagList) => GetArter().ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Art, Id = t.Id, Name = t.Navn ?? string.Empty }));

        private void PopulateGrupper(List<Tag> tagList)
        {
            GetArt_Grupper().ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Gruppe, Id = t.Id, Name = t.Navn ?? string.Empty }));
        }

        private void PopulateFamilier(List<Tag> tagList) => GetFamilier().ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Familie, Id = t.Id, Name = t.Navn ?? string.Empty }));

        private void PopulateRegioner(List<Tag> tagList) => GetRegioner().ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Region, Id = t.Id, Name = t.Navn ?? string.Empty }));

        private void PopulateLokaliteter(List<Tag> tagList) => GetLokaliteter().ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Lokalitet, Id = t.Id, Name = t.Navn ?? string.Empty }));

        private void PopulateKommuner(List<Tag> tagList) => GetKommuner().ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Kommune, Id = t.Id, Name = $"{t.Navn} Kommune" ?? string.Empty }));

        private void PopulateDistance(List<Tag> tagList)
        {
            tagList.Add(new Tag { Id = 1, Name = "Indenfor 1 km", TagType = TagTypes.Distance, SomeValue = 1 });
            tagList.Add(new Tag { Id = 1, Name = "Indenfor 2 km", TagType = TagTypes.Distance, SomeValue = 2 });
            tagList.Add(new Tag { Id = 1, Name = "Indenfor 5 km", TagType = TagTypes.Distance, SomeValue = 5 });
            tagList.Add(new Tag { Id = 1, Name = "Indenfor 10 km", TagType = TagTypes.Distance, SomeValue = 10 });
            tagList.Add(new Tag { Id = 1, Name = "Indenfor 20 km", TagType = TagTypes.Distance, SomeValue = 20 });
            tagList.Add(new Tag { Id = 1, Name = "Indenfor 25 km", TagType = TagTypes.Distance, SomeValue = 25 });
            tagList.Add(new Tag { Id = 1, Name = "Indenfor 50 km", TagType = TagTypes.Distance, SomeValue = 50 });
        }

        private void PopulateMaaneder(List<Tag> tagList)
        {
            var danishCulture = CultureInfo.GetCultureInfo("da-DK");
            var textInfo = new CultureInfo("da-DK", false).TextInfo;

            GetMaaneder().ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Maaned, Id = t, Name = textInfo.ToTitleCase(danishCulture.DateTimeFormat.GetMonthName(t)) }));
        }

        private void PopulateAarstal(List<Tag> tagList) => GetAarstal().ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Aarstal, Id = t, Name = t.ToString() }));

        private List<int> GetAarstal() => [.. birdWareContext.Fugletur.AsNoTracking().Select(r => r.Dato.HasValue ? r.Dato.Value.Year : 0).Distinct()];

        private List<int> GetMaaneder() => [.. birdWareContext.Fugletur.AsNoTracking().Select(r => r.Dato.HasValue ? r.Dato.Value.Month : 0).Distinct()];

        private List<Lokalitet> GetLokaliteter() => [.. birdWareContext.Lokalitet.AsNoTracking()];

        private List<Region> GetRegioner() => [.. birdWareContext.Region.AsNoTracking()];

        private List<Kommune> GetKommuner() => [.. birdWareContext.Kommune.AsNoTracking()];

        private List<Familie> GetFamilier() => [.. birdWareContext.Familie.AsNoTracking()];

        private List<Gruppe> GetArt_Grupper() => [.. birdWareContext.Gruppe.AsNoTracking()];

        private List<Art> GetArter() => [.. birdWareContext.Art.AsNoTracking()];
    }
}