using System.Collections.Generic;
using CourseCatalog.Domain.Entities;
using MediatR;

namespace CourseCatalog.App.Features.Lookups.Queries.GetEndorsementList
{
    public class GetEndorsementListQuery : IRequest<List<Endorsement>>
    {
    }
}