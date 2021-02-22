using AutoMapper;
using CourseCatalog.App.Features.Courses.Queries.GetCourseDetail;
using CourseCatalog.App.Features.Drafts.Queries.GetDraftDetail;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.App.Profiles
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<CourseDetailDto, Course>().ReverseMap();
            //CreateMap<CourseListVm, Course>().ReverseMap();
            //CreateMap<Course, UpdateCourseCommand>().ReverseMap();
            //CreateMap<Course, CreateCourseCommand>().ReverseMap();

            //CreateMap<Draft, CreateCourseDraftDetailVm>().ReverseMap();
            //CreateMap<Course, CreateCourseDraftDetailVm>().ReverseMap();

            CreateMap<DraftDetailDto, Draft>().ReverseMap();
            //CreateMap<DraftListVm, Draft>().ReverseMap();
            //CreateMap<Draft, CreateDraftCommand>().ReverseMap();
            
            //CreateMap<ClusterDetailVm, Cluster>().ReverseMap();
            //CreateMap<ClusterListVm, Cluster>().ReverseMap();
            //CreateMap<ClusterTypeDto, ClusterType>().ReverseMap();
            //CreateMap<Cluster, CreateClusterCommand>().ReverseMap();
            //CreateMap<Cluster, UpdateClusterCommand>().ReverseMap();

            //CreateMap<ProgramDetailVm, Program>().ReverseMap();
            //CreateMap<ProgramListVm, Program>().ReverseMap();
            //CreateMap<Program, CreateProgramCommand>().ReverseMap();
            //CreateMap<Program, UpdateProgramCommand>().ReverseMap();

            //CreateMap<CredentialDetailVm, Credential>().ReverseMap();
            //CreateMap<CredentialListVm, Credential>().ReverseMap();
            //CreateMap<Credential, CreateCredentialCommand>().ReverseMap();
            //CreateMap<Credential, UpdateCredentialCommand>().ReverseMap();
        }
    }
}
