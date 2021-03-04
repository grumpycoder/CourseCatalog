using System;
using AutoMapper;
using CourseCatalog.Application.Contracts;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Courses.Queries.GetCoursesByEndorsement
{
    public class GetCoursesByEndorsementQueryHandler : IRequestHandler<GetCoursesByEndorsementQuery, List<EndorsementCourseListDto>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public GetCoursesByEndorsementQueryHandler(IMapper mapper, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        public async Task<List<EndorsementCourseListDto>> Handle(GetCoursesByEndorsementQuery request, CancellationToken cancellationToken)
        {
            try
            {
            var courses = await _courseRepository.GetCoursesByEndorseId(request.EndorseId);

            var dto = _mapper.Map<List<EndorsementCourseListDto>>(courses);

            return dto;

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw ;
            }
        }

    }
}
