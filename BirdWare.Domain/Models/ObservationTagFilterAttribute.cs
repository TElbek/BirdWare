﻿namespace BirdWare.Domain.Models
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ObservationTagFilterAttribute : Attribute
    {
        public TagTypes TagType { get; set; }
        public TagTypes[] TagTypeList { get; set; } = [];
    }
}
