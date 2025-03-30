using BirdWare.Domain.Entities;
using BirdWare.Domain.Models;
using BirdWare.EF.Interfaces;
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
            PopulateRegioner(tagList);
            PopulateFamilier(tagList);
            PopulateGrupper(tagList);
            PopulateArter(tagList);
            PopulateSaeson(tagList);
            PopulateLand(tagList);
            return SortByName(tagList);
        }

        public List<Tag> GetTagListFugletur()
        {
            var tagList = TagListFactory();
            PopulateAarstal(tagList);
            PopulateMaaneder(tagList);
            PopulateLokaliteter(tagList);
            PopulateRegioner(tagList);
            PopulateSaeson(tagList);
            PopulateLand(tagList);
            return SortByName(tagList);
        }

        private static List<Tag> TagListFactory()
        {
            return [];
        }

        private static List<Tag> SortByName(List<Tag> tagList)
        {
            return [.. tagList.OrderBy(r => r.Name)];
        }

        private static void PopulateLand(List<Tag> tagList)
        {
            tagList.Add(new Tag { Id = 1, ParentId = 0, Name = "Danmark", TagType = TagTypes.Land });
        }

        private static void PopulateSaeson(List<Tag> tagList)
        {
            tagList.Add(new Tag { Id = 1, ParentId = 0, Name = "Forår", TagType = TagTypes.SaesonForaar });
            tagList.Add(new Tag { Id = 2, ParentId = 0, Name = "Sommer", TagType = TagTypes.SaesonSommer });
            tagList.Add(new Tag { Id = 3, ParentId = 0, Name = "Efterår", TagType = TagTypes.SaesonEfteraar });
            tagList.Add(new Tag { Id = 4, ParentId = 0, Name = "Vinter", TagType = TagTypes.SaesonVinter });
        }

        private void PopulateArter(List<Tag> tagList)
        {
            GetArter().ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Art, Id = t.Id, ParentId = (int)t.GruppeId, Name = t.Navn ?? string.Empty }));
        }

        private void PopulateGrupper(List<Tag> tagList)
        {
            GetArt_Grupper().ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Gruppe, Id = t.Id, ParentId = t.FamilieId, Name = t.Navn ?? string.Empty }));
        }

        private void PopulateFamilier(List<Tag> tagList)
        {
            GetFamilier().ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Familie, Id = t.Id, Name = t.Navn ?? string.Empty }));
        }

        private void PopulateRegioner(List<Tag> tagList)
        {
            GetRegioner().ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Region, Id = t.Id, Name = t.Navn ?? string.Empty }));
        }

        private void PopulateLokaliteter(List<Tag> tagList)
        {
            GetLokaliteter().ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Lokalitet, Id = t.Id, ParentId = t.RegionId, Name = t.Navn ?? string.Empty }));
        }

        private void PopulateMaaneder(List<Tag> tagList)
        {
            var danishCulture = CultureInfo.GetCultureInfo("da-DK");
            var textInfo = new CultureInfo("da-DK", false).TextInfo;

            GetMaaneder().ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Maaned, Id = t, Name = textInfo.ToTitleCase(danishCulture.DateTimeFormat.GetMonthName(t)) }));
        }

        private void PopulateAarstal(List<Tag> tagList)
        {
            GetAarstal().ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Aarstal, Id = t, Name = t.ToString() }));
        }

        private List<int> GetAarstal()
        {
            return [.. birdWareContext.Fugletur
                .Select(r => r.Dato.HasValue ? r.Dato.Value.Year : 0).Distinct()];
        }

        private List<int> GetMaaneder()
        {
            return [.. birdWareContext.Fugletur
                .Select(r => r.Dato.HasValue ? r.Dato.Value.Month : 0).Distinct()];
        }

        private List<Lokalitet> GetLokaliteter()
        {
            return [.. birdWareContext.Lokalitet];
        }

        private List<Region> GetRegioner()
        {
            return [.. birdWareContext.Region];
        }

        private List<Familie> GetFamilier()
        {
            return [.. birdWareContext.Familie];
        }

        private List<Gruppe> GetArt_Grupper()
        {
            return [.. birdWareContext.Gruppe];
        }

        private List<Art> GetArter()
        {
            return [.. birdWareContext.Art];
        }
    }
}