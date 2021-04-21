using AutoMapper;
using CourseCatalog.Application.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CourseCatalog.App.Features.Courses.Queries.GetCoursesByCertholder
{
    public class GetCoursesByCertholderQueryHandler : IRequestHandler<GetCoursesByCertholderQuery, List<CertholderCourseListDto>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public GetCoursesByCertholderQueryHandler(IMapper mapper, ICourseRepository courseRepository)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        public async Task<List<CertholderCourseListDto>> Handle(GetCoursesByCertholderQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var courses = await _courseRepository.GetCoursesByCertholderId(request.CertholderId);

                var dto = _mapper.Map<List<CertholderCourseListDto>>(courses);

                return dto;

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

    }
}
