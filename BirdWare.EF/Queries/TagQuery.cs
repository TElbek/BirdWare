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
            return GetTagList(true);
        }

        public List<Tag> GetTagListFugletur()
        {
            return GetTagList();
        }

        private List<Tag> GetTagList(bool includeObservationTags = false)
        {
            var danishCulture = CultureInfo.GetCultureInfo("da-DK");
            var tagList = new List<Tag>();

            var textInfo = new CultureInfo("da-DK", false).TextInfo;

            GetAarstal()
                .ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Aarstal, Id = t, Name = t.ToString() }));

            GetMaaneder()
                .ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Maaned, Id = t, Name = textInfo.ToTitleCase(danishCulture.DateTimeFormat.GetMonthName(t)) }));

            GetLokaliteter()
                .ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Lokalitet, Id = t.Id, ParentId = t.RegionId, Name = t.Navn ?? string.Empty}));

            GetRegioner()
                .ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Region, Id = t.Id, Name = t.Navn ?? string.Empty }));


            if (includeObservationTags)
            {
                GetFamilier()
                    .ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Familie, Id = t.Id, Name = t.Navn ?? string.Empty }));

                GetArt_Grupper()
                    .ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Gruppe, Id = t.Id, ParentId = t.FamilieId, Name = t.Navn ?? string.Empty }));

                GetArter()
                    .ForEach(t => tagList.Add(new Tag { TagType = TagTypes.Art, Id = t.Id, ParentId = (int)t.GruppeId, Name = t.Navn ?? string.Empty }));
            }

            tagList.Add(new Tag {Id = 1, ParentId = 0, Name = "Forår", TagType = TagTypes.SaesonForaar});
            tagList.Add(new Tag {Id = 2, ParentId = 0, Name = "Sommer", TagType = TagTypes.SaesonSommer});
            tagList.Add(new Tag {Id = 3, ParentId = 0, Name = "Efterår", TagType = TagTypes.SaesonEfteraar});
            tagList.Add(new Tag {Id = 4, ParentId = 0, Name = "Vinter", TagType = TagTypes.SaesonVinter});

            tagList.Add(new Tag { Id = 1, ParentId = 0, Name = "Danmark", TagType = TagTypes.Land });

            return [.. tagList.OrderBy(r => r.Name)];
        }

        public Tag GetArtTagById(short Id)
        {
            var art = birdWareContext.Art.FirstOrDefault(r => r.Id == Id);
            return art != null ? new Tag { Id = art.Id, Name = art.Navn ?? string.Empty, TagType = TagTypes.Art } : new Tag();
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