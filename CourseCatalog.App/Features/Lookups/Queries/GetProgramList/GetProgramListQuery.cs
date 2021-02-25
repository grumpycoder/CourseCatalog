using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Lookups.Queries.GetProgramList
{
    public class GetProgramListQuery : IRequest<List<ProgramListDto>>
    {
        //TODO: Create ProgramDto
    }

    public class ProgramListDto
    {
        public int ProgramId { get; set; }
        public string ProgramCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string DisplayName => $"{Name} ({ProgramCode})"; 

        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }

        public bool? TraditionalForMales { get; set; }
        public bool? TraditionalForFemales { get; set; }

        public int? ProgramTypeId { get; set; }

        public string ProgramType { get; set; }
        public int ClusterId { get; set; }

        public string Cluster { get; set; }

    }
}
