using AutoMapper;
using CourseCatalog.App.Features.Courses.Queries.GetCourseDetail;
using CourseCatalog.App.Features.Drafts.Commands.Create;
using CourseCatalog.App.Features.Drafts.Commands.CreateDraftProgram;
using CourseCatalog.App.Features.Drafts.Commands.CreateDraftRequirement;
using CourseCatalog.App.Features.Drafts.Commands.UpdateDraft;
using CourseCatalog.App.Features.Drafts.Queries.GetDraftDetail;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.App.Profiles
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<CourseDetailDto, Course>().ReverseMap();
            CreateMap<CourseDeliveryType, CourseDeliveryTypeDto>()
                .ForMember(d => d.DeliveryTypeName,
                    o => o.MapFrom(d => d.DeliveryType.Name))
                .ReverseMap();
            CreateMap<CourseEndorsement, CourseEndorsementDto>()
                .ForMember(d => d.EndorseCode,
                    o => o.MapFrom(d => d.Endorsement.EndorseCode))
                .ForMember(d => d.Description,
                    o => o.MapFrom(d => d.Endorsement.Description))
                .ReverseMap();
            CreateMap<ProgramCourse, ProgramCourseDto>()
                .ForMember(d => d.Name,
                    o => o.MapFrom(d => d.Program.Name))
                .ForMember(d => d.ProgramCode,
                    o => o.MapFrom(d => d.Program.ProgramCode))
                .ReverseMap();

            CreateMap<DraftDetailDto, Draft>().ReverseMap();
            CreateMap<DraftDeliveryType, DraftDeliveryTypeDto>()
                .ForMember(d => d.DeliveryTypeName,
                    o => o.MapFrom(d => d.DeliveryType.Name))
                .ReverseMap();
            CreateMap<DraftEndorsement, DraftEndorsementDto>()
                .ForMember(d => d.EndorseCode,
                    o => o.MapFrom(d => d.Endorsement.EndorseCode))
                .ForMember(d => d.Description,
                    o => o.MapFrom(d => d.Endorsement.Description))
                .ReverseMap();

            CreateMap<DraftEndorsement, CreatedDraftEndorsementDto>()
                .ForMember(d => d.EndorseCode,
                    o => o.MapFrom(d => d.Endorsement.EndorseCode))
                .ForMember(d => d.Description,
                    o => o.MapFrom(d => d.Endorsement.Description))
                .ReverseMap();

            CreateMap<ProgramDraft, CreatedDraftProgramDto>()
                .ForMember(d => d.Name,
                    o => o.MapFrom(d => d.Program.Name))
                .ForMember(d => d.ProgramCode,
                    o => o.MapFrom(d => d.Program.ProgramCode))
                .ReverseMap();

            CreateMap<ProgramDraft, ProgramDraftDto>()
                .ForMember(d => d.Name,
                    o => o.MapFrom(d => d.Program.Name))
                .ForMember(d => d.ProgramCode,
                    o => o.MapFrom(d => d.Program.ProgramCode))
                .ReverseMap();

            //CreateMap<CourseListVm, Course>().ReverseMap();
            //CreateMap<Course, UpdateCourseCommand>().ReverseMap();
            //CreateMap<Course, CreateCourseCommand>().ReverseMap();

            //CreateMap<Draft, CreateCourseDraftDetailVm>().ReverseMap();
            //CreateMap<Course, CreateCourseDraftDetailVm>().ReverseMap();

            CreateMap<DraftDetailDto, Draft>().ReverseMap();
            CreateMap<Draft, UpdateDraftCommand>().ReverseMap();
            CreateMap<DraftDeliveryType, UpdateDraftDeliveryTypeDto>().ReverseMap();
            CreateMap<DraftDeliveryType, CreateDraftDeliveryTypeDto>().ReverseMap();

            CreateMap<Draft, CreateDraftCommand>().ReverseMap();



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
