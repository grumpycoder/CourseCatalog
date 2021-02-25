using CourseCatalog.Domain.Entities;

namespace CourseCatalog.App.Features.Programs.Queries.GetProgramList
{
    public class ProgramListDto
    {
        public int ProgramId { get; set; }
        public string ProgramCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? BeginYear { get; set; }
        public int? EndYear { get; set; }

        public ProgramType ProgramType { get; set; }

    }



}