using AutoMapper;
using CourseCatalog.App.Features.Clusters.Commands.UpdateCluster;
using CourseCatalog.App.Features.Clusters.Queries.GetClusterDetail;
using CourseCatalog.App.Features.Clusters.Queries.GetClusterList;
using CourseCatalog.App.Features.Courses.Queries.GetCourseDetail;
using CourseCatalog.App.Features.Credentials.Commands.CreateCredentialProgram;
using CourseCatalog.App.Features.Credentials.Queries.GetCredentialDetail;
using CourseCatalog.App.Features.Credentials.Queries.GetCredentialList;
using CourseCatalog.App.Features.Drafts.Commands.Create;
using CourseCatalog.App.Features.Drafts.Commands.CreateDraftProgram;
using CourseCatalog.App.Features.Drafts.Commands.CreateDraftRequirement;
using CourseCatalog.App.Features.Drafts.Commands.UpdateDraft;
using CourseCatalog.App.Features.Drafts.Queries.GetDraftDetail;
using CourseCatalog.App.Features.Lookups.Queries.GetProgramList;
using CourseCatalog.App.Features.Programs.Commands.CreateProgramCredential;
using CourseCatalog.App.Features.Programs.Commands.UpdateProgram;
using CourseCatalog.App.Features.Programs.Queries.GetProgramDetail;
using CourseCatalog.App.Features.Users.Queries.GetUserGroupList;
using CourseCatalog.Domain.Entities;

namespace CourseCatalog.App.Profiles
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            //Course Mappings
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

            //Draft Mappings
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

            CreateMap<DraftDetailDto, Draft>().ReverseMap();
            CreateMap<Draft, UpdateDraftCommand>().ReverseMap();
            CreateMap<DraftDeliveryType, UpdateDraftDeliveryTypeDto>().ReverseMap();
            CreateMap<DraftDeliveryType, CreateDraftDeliveryTypeDto>().ReverseMap();
            CreateMap<Draft, CreateDraftCommand>().ReverseMap();

            //Course/Draft Mapping
            CreateMap<Draft, Course>()
                .ForMember(d => d.Endorsements, o => o.Ignore())
                .ForMember(d => d.DeliveryTypes, o => o.Ignore())
                .ForMember(d => d.Programs, o => o.Ignore())
                .ReverseMap();

            CreateMap<Course, Draft>()
                .ForMember(d => d.Endorsements, o => o.Ignore())
                .ForMember(d => d.DeliveryTypes, o => o.Ignore())
                .ForMember(d => d.Programs, o => o.Ignore())
                .ReverseMap();


            //Clusters Mappings
            CreateMap<Cluster, ClusterListDto>()
                .ForMember(d => d.ClusterTypeName, o => o.MapFrom(d => d.ClusterType.Name))
                .ReverseMap();

            CreateMap<ClusterDetailDto, Cluster>().ReverseMap();

            CreateMap<ClusterType, ClusterTypeDto>().ReverseMap();
            CreateMap<Program, ClusterProgramListDto>().ReverseMap();
            CreateMap<Cluster, UpdateClusterCommand>().ReverseMap();

            //Programs Mappings
            CreateMap<Program, ProgramListDto>()
                .ForMember(d => d.Cluster, o => o.MapFrom(s => s.Cluster.Name))
                .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.ProgramType, o => o.MapFrom(s => s.ProgramType.Name))
                .ReverseMap();

            CreateMap<Program, ProgramDetailDto>()
                .ForMember(d => d.Cluster, o => o.MapFrom(s => s.Cluster.Name))
                .ForMember(d => d.ProgramType, o => o.MapFrom(s => s.ProgramType.Name))
                .ReverseMap();

            CreateMap<ProgramCredential, ProgramCredentialListDto>()
                .ForMember(d => d.CredentialCode, o => o.MapFrom(s => s.Credential.CredentialCode))
                .ForMember(d => d.CredentialName, o => o.MapFrom(s => s.Credential.Name))
                .ReverseMap();

            CreateMap<Program, UpdateProgramCommand>().ReverseMap();

            CreateMap<ProgramCredential, CreateProgramCredentialDto>()
                .ForMember(d => d.CredentialCode, o => o.MapFrom(s => s.Credential.CredentialCode))
                .ForMember(d => d.CredentialName, o => o.MapFrom(s => s.Credential.Name))
                .ReverseMap();

            //Credentials Mappings 
            CreateMap<Credential, CredentialListDto>()
                .ForMember(d => d.CredentialType, o => o.MapFrom(s => s.CredentialType.Name))
                .ForMember(d => d.CredentialTypeCode, o => o.MapFrom(s => s.CredentialType.CredentialTypeCode))
                .ReverseMap();

            CreateMap<Credential, CredentialDetailDto>()
                .ForMember(d => d.CredentialType, o => o.MapFrom(s => s.CredentialType.Name))
                .ReverseMap();

            CreateMap<ProgramCredential, CredentialProgramListDto>()
                .ForMember(d => d.ProgramCode, o => o.MapFrom(s => s.Program.ProgramCode))
                .ForMember(d => d.ProgramName, o => o.MapFrom(s => s.Program.Name))
                .ReverseMap();

            CreateMap<ProgramCredential, CreateCredentialProgramDto>()
                .ForMember(d => d.ProgramCode, o => o.MapFrom(s => s.Program.ProgramCode))
                .ForMember(d => d.ProgramName, o => o.MapFrom(s => s.Program.Name))
                .ReverseMap();

            //User/Group Mappings
            CreateMap<UserGroup, UserGroupListDto>().ReverseMap();

            //CreateMap<DraftListVm, Draft>().ReverseMap();
            //CreateMap<Draft, CreateDraftCommand>().ReverseMap();

            //CreateMap<ClusterDetailVm, Cluster>().ReverseMap();
            //CreateMap<ClusterListVm, Cluster>().ReverseMap();
            //CreateMap<ClusterTypeDto, ClusterType>().ReverseMap();
            //CreateMap<Cluster, CreateClusterCommand>().ReverseMap();

            //CreateMap<ProgramDetailVm, Program>().ReverseMap();
            //CreateMap<ProgramListVm, Program>().ReverseMap();
            //CreateMap<Program, CreateProgramCommand>().ReverseMap();
            //CreateMap<Program, UpdateProgramCommand>().ReverseMap();

            //CreateMap<CredentialListVm, Credential>().ReverseMap();
            //CreateMap<Credential, CreateCredentialCommand>().ReverseMap();
            //CreateMap<Credential, UpdateCredentialCommand>().ReverseMap();


        }
    }
}
