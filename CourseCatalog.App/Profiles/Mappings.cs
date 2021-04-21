using AutoMapper;
using CourseCatalog.App.Features.Clusters.Commands.CreateCluster;
using CourseCatalog.App.Features.Clusters.Commands.UpdateCluster;
using CourseCatalog.App.Features.Clusters.Queries.GetClusterDetail;
using CourseCatalog.App.Features.Clusters.Queries.GetClusterList;
using CourseCatalog.App.Features.Courses.Queries.GetCourseDetail;
using CourseCatalog.App.Features.Courses.Queries.GetCoursesByEndorsement;
using CourseCatalog.App.Features.Credentials.Commands.CreateCredential;
using CourseCatalog.App.Features.Credentials.Commands.CreateCredentialProgram;
using CourseCatalog.App.Features.Credentials.Commands.UpdateCredential;
using CourseCatalog.App.Features.Credentials.Queries.GetCredentialDetail;
using CourseCatalog.App.Features.Credentials.Queries.GetCredentialList;
using CourseCatalog.App.Features.Drafts.Commands.CreateDraft;
using CourseCatalog.App.Features.Drafts.Commands.CreateDraftProgram;
using CourseCatalog.App.Features.Drafts.Commands.CreateDraftRequirement;
using CourseCatalog.App.Features.Drafts.Commands.UpdateDraft;
using CourseCatalog.App.Features.Drafts.Queries.GetDraftDetail;
using CourseCatalog.App.Features.Groups.Commands.CreateGroupUser;
using CourseCatalog.App.Features.Groups.Queries.GetGroupList;
using CourseCatalog.App.Features.Lookups.Queries.GetProgramList;
using CourseCatalog.App.Features.Programs.Commands.CreateProgram;
using CourseCatalog.App.Features.Programs.Commands.CreateProgramCredential;
using CourseCatalog.App.Features.Programs.Commands.UpdateProgram;
using CourseCatalog.App.Features.Programs.Queries.GetProgramDetail;
using CourseCatalog.App.Features.Refs.ClusterTypes.Commands.CreateClusterType;
using CourseCatalog.App.Features.Refs.ClusterTypes.Commands.UpdateClusterType;
using CourseCatalog.App.Features.Refs.CourseLevels.Commands.CreateCourseLevel;
using CourseCatalog.App.Features.Refs.CourseLevels.Commands.UpdateCourseLevel;
using CourseCatalog.App.Features.Refs.CredentialTypes.Commands.CreateCredentialType;
using CourseCatalog.App.Features.Refs.CredentialTypes.Commands.UpdateCredentialType;
using CourseCatalog.App.Features.Refs.CreditTypes.Commands.CreateCreditType;
using CourseCatalog.App.Features.Refs.CreditTypes.Commands.UpdateCreditType;
using CourseCatalog.App.Features.Refs.DeliveryTypes.Commands.CreateDeliveryType;
using CourseCatalog.App.Features.Refs.DeliveryTypes.Commands.UpdateDeliveryType;
using CourseCatalog.App.Features.Refs.ProgramTypes.Commands.CreateProgramType;
using CourseCatalog.App.Features.Refs.ProgramTypes.Commands.UpdateProgramType;
using CourseCatalog.App.Features.Refs.Subjects.Commands;
using CourseCatalog.App.Features.Users.Queries.GetUser;
using CourseCatalog.App.Features.Users.Queries.GetUserGroupList;
using CourseCatalog.App.Services;
using CourseCatalog.Domain.Entities;
using System.Linq;
using CourseCatalog.App.Features.Courses.Queries.GetCoursesByCertholder;

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

            CreateMap<Course, EndorsementCourseListDto>()
                .ForMember(d => d.GradeScale,
                    o => o.MapFrom(d => d.GradeScale.Configuration))
                .ForMember(d => d.Subject,
                    o => o.MapFrom(d => d.Subject.Name))
                .ForMember(d => d.LowGrade,
                    o => o.MapFrom(d => d.LowGrade.Name))
                .ForMember(d => d.HighGrade,
                    o => o.MapFrom(d => d.HighGrade.Name))
                .ForMember(d => d.ScedCategory,
                    o => o.MapFrom(d => d.ScedCategory.Name))
                .ForMember(d => d.CourseLevel,
                    o => o.MapFrom(d => d.CourseLevel.Name))
                .ReverseMap();

            CreateMap<Course, CertholderCourseListDto>(); 

            CreateMap<Course, UDefCourses>()
                .ForMember(d => d.CourseCode, o =>
                    o.MapFrom(s => s.ArchiveCourseCode))
                .ForMember(d => d.CourseName, o =>
                    o.MapFrom(s => s.Name))
                .ForMember(d => d.CipCode, o =>
                    o.MapFrom(s => s.CipCode ?? string.Empty))
                .ForMember(d => d.LowGrade, o =>
                    o.MapFrom(s => s.LowGrade.Name))
                .ForMember(d => d.HighGrade, o =>
                    o.MapFrom(s => s.HighGrade.Name))
                .ForMember(d => d.IsSpecialEd, o =>
                    o.MapFrom(s => s.IsSpecialEducation))
                .ForMember(d => d.CollegeCourseCode, o =>
                    o.MapFrom(s => s.CollegeCourseId ?? string.Empty))
                .ForMember(d => d.EndYear, o =>
                    o.MapFrom(s => s.EndYear.ToString()))
                .ForMember(d => d.BeginYear, o =>
                    o.MapFrom(s => s.BeginYear.ToString() ?? string.Empty))
                .ForMember(d => d.LocallyEditable, o =>
                    o.MapFrom(s => s.IsLocallyEditable))
                .ForMember(d => d.Subject, o =>
                    o.MapFrom(s => s.Subject.Name ?? string.Empty))
                .ForMember(d => d.CreditType, o =>
                    o.MapFrom(s => string.Join(",", s.CreditTypes)))
                .ForMember(d => d.Endorsements, o =>
                    o.MapFrom(s => string.Join(",", s.Endorsements.Select(x => x.Endorsement.EndorseCode))))
                ;


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

            CreateMap<DraftEndorsement, CourseEndorsement>().ReverseMap();
            CreateMap<ProgramDraft, ProgramCourse>().ReverseMap();
            CreateMap<DraftDeliveryType, CourseDeliveryType>().ReverseMap();

            //Clusters Mappings
            CreateMap<Cluster, ClusterListDto>()
                .ForMember(d => d.ClusterTypeName, o => o.MapFrom(d => d.ClusterType.Name))
                .ReverseMap();

            CreateMap<ClusterDetailDto, Cluster>().ReverseMap();

            CreateMap<ClusterType, ClusterTypeDto>().ReverseMap();
            CreateMap<Program, ClusterProgramListDto>().ReverseMap();
            CreateMap<Cluster, UpdateClusterCommand>().ReverseMap();
            CreateMap<Cluster, CreateClusterCommand>().ReverseMap();

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
            CreateMap<Program, CreateProgramCommand>().ReverseMap();

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

            CreateMap<Credential, UpdateCredentialCommand>().ReverseMap();
            CreateMap<Credential, CreateCredentialCommand>().ReverseMap();

            //UserGroup Mappings
            CreateMap<Group, GroupListDto>()
                .ForMember(d => d.GroupId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.GroupName, o => o.MapFrom(s => s.Name))
                .ReverseMap();

            CreateMap<UserGroup, UserDto>()
                .ForMember(d => d.IdentityGuid, o => o.MapFrom(s => s.User.IdentityGuid))
                //.ForMember(d => d.UserId, o => o.MapFrom(s => s.User.UserId))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.User.Username))
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.User.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.User.LastName))
                .ForMember(d => d.FullName, o => o.MapFrom(s => s.User.FullName))
                .ReverseMap();


            CreateMap<User, UserDetailDto>()
                //.ForMember(d => d.IdentityGuid, o => o.MapFrom(s => s.User.IdentityGuid))
                //.ForMember(d => d.UserId, o => o.MapFrom(s => s.User.UserId))
                .ReverseMap();

            CreateMap<UserGroup, UserGroupDto>()
                .ForMember(d => d.GroupUserId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.GroupId, o => o.MapFrom(s => s.Group.Id))
                .ForMember(d => d.GroupName, o => o.MapFrom(s => s.Group.Name))
                .ReverseMap();

            CreateMap<UserGroup, UserGroupListDto>().ReverseMap();

            CreateMap<UserGroup, CreateGroupUserCommandDto>()
                .ForMember(d => d.FirstName, o => o.MapFrom(d => d.User.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(d => d.User.LastName))
                .ForMember(d => d.FullName, o => o.MapFrom(d => d.User.FullName))
                .ForMember(d => d.IdentityGuid, o => o.MapFrom(d => d.User.IdentityGuid))
                .ForMember(d => d.Username, o => o.MapFrom(d => d.User.Username))
                .ForMember(d => d.UserId, o => o.MapFrom(d => d.User.Id))
                .ReverseMap();

            //Subject Mappings 
            CreateMap<Subject, UpdateSubjectCommand>().ReverseMap();
            CreateMap<Subject, CreateSubjectCommand>().ReverseMap();

            //CourseLevel Mappings 
            CreateMap<CourseLevel, UpdateCourseLevelCommand>().ReverseMap();
            CreateMap<CourseLevel, CreateCourseLevelCommand>().ReverseMap();

            //DeliveryType Mappings 
            CreateMap<DeliveryType, UpdateDeliveryTypeCommand>().ReverseMap();
            CreateMap<DeliveryType, CreateDeliveryTypeCommand>().ReverseMap();

            //CreditType Mappings 
            CreateMap<Tag, UpdateCreditTypeCommand>().ReverseMap();
            CreateMap<Tag, CreateCreditTypeCommand>().ReverseMap();

            //ClusterType Mappings 
            CreateMap<ClusterType, UpdateClusterTypeCommand>().ReverseMap();
            CreateMap<ClusterType, CreateClusterTypeCommand>().ReverseMap();

            //ProgramType Mappings 
            CreateMap<ProgramType, UpdateProgramTypeCommand>().ReverseMap();
            CreateMap<ProgramType, CreateProgramTypeCommand>().ReverseMap();

            //CredentialType Mappings 
            CreateMap<CredentialType, UpdateCredentialTypeCommand>().ReverseMap();
            CreateMap<CredentialType, CreateCredentialTypeCommand>().ReverseMap();

        }
    }
}

