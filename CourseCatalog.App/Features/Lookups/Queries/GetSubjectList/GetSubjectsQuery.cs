using System.Collections.Generic;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetSubjectList
{
    public class GetSubjectListQuery : IRequest<List<Subject>>
    {
    }
}