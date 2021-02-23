using CourseCatalog.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CourseCatalog.App.Features.Lookups.Queries.GetEndorsementList
{
    public class GetEndorsementListQuery : IRequest<List<Endorsement>>
    {

    }
}
